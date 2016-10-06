using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum ObjectType{Letter, Action}

public class AuthorUIManager : MonoBehaviour {

	[SerializeField] GameObject slotPrefab;
	[SerializeField] GameObject actionObjectPrefab;
	[SerializeField] GameObject textObjectPrefab;

	[SerializeField] GameObject objectPanel;
	[SerializeField] GameObject actionObjectPoint;
	[SerializeField] GameObject textObjectPoint;

	public InspectionPanel inspP;
	[SerializeField] StoryManager storyMan;
	public GameObject slots;
	public List<Slot> allSlots = new List<Slot>();

	public GameObject actionObjectReady;
	public GameObject textObjectReady;

	public Slot slotBeingDraggedFrom;
	public UIOBject objectBeingDragged;
	public UIOBject objectBeingInspected;


	public string currentStory = "temp";
	public List<UIOBject> objectOrder = new List<UIOBject>();


	List<string> randomActionNames = new List<string>() {"Bomb","Fun","Cause","Sauce","Flirt","Tremor","Dance","Error","Spectacle"};
	List<string> randomLetterNames = new List<string>() {"Story","Help","Ghost","Family","Unintentional","Flight","Fall","Child"};


	public void InspectObject(UIOBject obj)
	{
		if (objectBeingInspected) { //if previous object
			objectBeingInspected.UnHighlight();
		}
		objectBeingInspected = obj;

		objectBeingInspected.Highlight ();
		inspP.tglA.isOn = true; inspP.tglA.CheckColor(); inspP.ToggleAB(inspP.tglA); //sort of ugly, but it works to set the Toggle correctly upon first click.
//		inspP.InspectObject(obj);
//		inspP.tglA.isOn = true; inspP.tglA.CheckColor();
	}


	/// <summary>
	/// Changes the ActionType of the currently Inspected object.
	/// </summary>
	/// <param name="at">ActionType you want it to change to.</param>
	public void ChangeActionObjectType(ActionType at){
		print("A? "+inspP.UIOBjectSide());
		if (inspP.UIOBjectSide ()) {
			print("changing side A "+at);
			(objectBeingInspected as ActionObject).a.ChangeType (at);
		} else {
			print("changing side B "+at);
			(objectBeingInspected as ActionObject).b.ChangeType (at);
		}
	}




	public void RefreshObjectList(){
		objectOrder.Clear();
		foreach(Slot s in slots.GetComponentsInChildren<Slot>()){
			if(s.objOnMe){
				objectOrder.Add(s.objOnMe);
			}
			if (!allSlots.Contains (s)) {
				allSlots.Add (s);
			}
		}
	}

	public void AddSlotToList(){
		print ("Add slot");
		GameObject newSlot = (GameObject)Instantiate(slotPrefab,slots.transform);
		newSlot.GetComponent<RectTransform>().localScale = Vector3.one;
		allSlots.Add (newSlot.GetComponent<Slot> ());
	}

	public void RemoveSlotFromList(){

		Slot toRemove = allSlots [allSlots.Count - 1];
		allSlots.Remove (toRemove);
		if (toRemove.objOnMe != null) {
			Destroy (toRemove.objOnMe.gameObject);
		}
		Destroy (toRemove.gameObject);
	}

	public void SpawnNewObjectAction(){
		GameObject newObject = (GameObject)Instantiate(actionObjectPrefab);
		newObject.transform.SetParent(objectPanel.transform,false);
		newObject.transform.position = actionObjectPoint.transform.position;
		newObject.GetComponent<RectTransform>().localScale = Vector3.one;
		actionObjectReady = newObject;

		newObject.GetComponent<ActionObject> ().text.text = "Action "+randomActionNames [Random.Range (0, randomActionNames.Count)];
		newObject.GetComponent<ActionObject> ().a.ChangeType(ActionType.Phonecall);
		newObject.GetComponent<ActionObject> ().b.ChangeType(ActionType.Phonecall);

	}

	public void SpawnNewObjectText(){
		GameObject newObject = (GameObject)Instantiate(textObjectPrefab);
		newObject.transform.SetParent(objectPanel.transform,false);
		newObject.transform.position = textObjectPoint.transform.position;
		newObject.GetComponent<RectTransform>().localScale = Vector3.one;
		textObjectReady = newObject;

		newObject.GetComponent<TextObject> ().text.text = randomLetterNames [Random.Range (0, randomLetterNames.Count)] + " Letter";
	}

	public void LoadTextObject(string[] sides){//(string a, string b, string nam){
		AddSlotToList ();
		string a = sides[0]; string b = sides[1];

		TextObject t = textObjectReady.GetComponent<TextObject> ();
		t.a.textString = a;
		t.b.textString = b;

		foreach(Slot s in slots.GetComponentsInChildren<Slot>()){
			if (s.objOnMe == null) {
				s.DropObjectOnMe (t);
				break;
			}
		}
		t.text.text = sides[2];

		SpawnNewObjectText ();

	}

	public void LoadActionObject(string[] sides){//(string a, string b, string nam){
		AddSlotToList ();
		string a = sides[0]; string b = sides[1];

		ActionObject t = actionObjectReady.GetComponent<ActionObject> ();
		print("LOADING ACTION "+a+" "+b);

		t.a.actionText.text = a;
		t.a.ChangeType ((ActionType)int.Parse (a));
		t.b.actionText.text = b;
		t.b.ChangeType ((ActionType)int.Parse (b));
		t.a.actionString = sides[2];
		t.b.actionString = sides[3];


		foreach(Slot s in slots.GetComponentsInChildren<Slot>()){
			if (s.objOnMe == null) {
				s.DropObjectOnMe (t);
				break;
			}
		}
		t.text.text = sides[4];

		SpawnNewObjectAction ();
	}



	public void ClearStoryFromUI(){
		//Clear whatever is present in the slots.
		List<UIOBject> sls = new List<UIOBject> ();
		foreach (Transform t in slots.transform) {		//THIS IS STUPID BUT MY BRAIN DOESN'T WANT TO FIGURE OUT THE RIGHT WAY.
			sls.Add (t.GetComponent<Slot>().objOnMe);
		}

		if (sls.Count > 0) {
			for (int i = 0; i < sls.Count; i++) {
				if (sls [i] != null) {
					print (i);
					sls [i].transform.SetParent (null);
					Destroy (sls [i]);
				}
			}
		}

		//clear all slots except one.
		for (int i = 0; i < slots.transform.childCount-1; i++) {
			RemoveSlotFromList ();
		}
	}

	/// <summary>
	/// Called to set up the final parts of the editor that needs to work.
	/// </summary>
	/// <param name="name">Name.</param>
	public void StartStoryEditing(string name){
		currentStory = name;
		RefreshObjectList ();
	}
		

	public void SaveStory(){
		storyMan.SaveStory (currentStory, objectOrder);
	}



}
