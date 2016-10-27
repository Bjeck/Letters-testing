using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum ObjectType{Letter, Action}

public class AuthorUIManager : MonoBehaviour {

	[SerializeField] bool startWithDebug = false;

	[SerializeField] GameObject slotPrefab;
	[SerializeField] GameObject actionObjectPrefab;
	[SerializeField] GameObject textObjectPrefab;
	[SerializeField] GameObject objectShadowPrefab;

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
	public ShadowObject shadowObjectBeingDragged;

	public string currentStory = "temp";
	public List<UIOBject> objectOrder = new List<UIOBject>();
	public List<ShadowObject> shadowObjects = new List<ShadowObject>();

	public int maxID = 0;

	List<string> randomActionNames = new List<string>() {"Bomb","Fun","Cause","Sauce","Flirt","Tremor","Dance","Error","Spectacle"};
	List<string> randomLetterNames = new List<string>() {"Story","Help","Ghost","Family","Unintentional","Flight","Fall","Child"};

	void Start(){
		if(startWithDebug){
			StartStoryEditing ("DEBUG");
			FinalStartStoryEditing ();
		}
	}


	public void InspectObject(UIOBject obj)
	{
		if (objectBeingInspected) { //if previous object
			objectBeingInspected.UnHighlight();
		}

		//SAVE
		inspP.SaveObjectInfo();

		objectBeingInspected = obj;
		if(obj.GetType().Name == "TextObject"){
			inspP.inspectingAction = false;
		}
		else if(obj.GetType().Name == "ActionObject"){
			inspP.inspectingAction = true;
		}

		objectBeingInspected.Highlight (); 
		//inspP.tglA.isOn = true; inspP.tglA.CheckColor(); 
		//inspP.InspectObject(obj); 
		inspP.ToggleAB(inspP.tglA); //sort of ugly, but it works to set the Toggle correctly upon first click.
	}


	/// <summary>
	/// Changes the ActionType of the currently Inspected object.
	/// </summary>
	/// <param name="at">ActionType you want it to change to.</param>
	public void ChangeActionObjectType(ActionType at){
		print("A? "+inspP.a);
		if (inspP.a) {
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

		ActionObject aobj = newObject.GetComponent<ActionObject> ();

		aobj.text.text = "Action "+randomActionNames [Random.Range (0, randomActionNames.Count)];
		aobj.a.ChangeType(ActionType.Phonecall);
		aobj.b.ChangeType(ActionType.Phonecall);
		aobj.id = maxID;
		print("spawning action "+maxID);
		IterateMaxID ();
	}

	public void SpawnNewObjectText(){

		GameObject newObject = (GameObject)Instantiate(textObjectPrefab);
		newObject.transform.SetParent(objectPanel.transform,false);
		newObject.transform.position = textObjectPoint.transform.position;
		newObject.GetComponent<RectTransform>().localScale = Vector3.one;
		textObjectReady = newObject;

		newObject.GetComponent<TextObject> ().text.text = randomLetterNames [Random.Range (0, randomLetterNames.Count)] + " Letter";
		newObject.GetComponent<TextObject> ().id = maxID;
		print("spawning text "+maxID);
		IterateMaxID ();

	}

	public void LoadTextObject(string[] sides){//(string a, string b, string nam){
		print("loading text");
		AddSlotToList ();

		if (textObjectReady == null) {
			SpawnNewObjectText ();
		}

		TextObject t = textObjectReady.GetComponent<TextObject> ();
		t.text.text = sides[0];
		t.id = int.Parse(sides [1]);
		string a = sides[2]; string b = sides[3];

		a = a.Replace("§",System.Environment.NewLine);
		b = b.Replace("§",System.Environment.NewLine);

		t.a.letterString = a;
		t.b.letterString = b;

		foreach(Slot s in slots.GetComponentsInChildren<Slot>()){
			if (s.objOnMe == null) {
				s.DropObjectOnMe (t);
				break;
			}
		}

		SpawnNewObjectText ();

	}

	public void LoadActionObject(string[] sides){//(string a, string b, string nam){
		print("loading action");

		AddSlotToList ();

		if (actionObjectReady == null) {
			SpawnNewObjectAction ();
		}
			
		ActionObject t = actionObjectReady.GetComponent<ActionObject> ();
		t.text.text = sides[0];
		t.id = int.Parse(sides [1]);
		string a = sides[2]; string b = sides[3];

		print("LOADING ACTION "+a+" "+b);

		t.a.actionTypeText.text = a;
		t.a.ChangeType ((ActionType)int.Parse (a));
		t.b.actionTypeText.text = b;
		t.b.ChangeType ((ActionType)int.Parse (b));
		t.a.actionString = sides[4];
		t.b.actionString = sides[5];

		foreach(Slot s in slots.GetComponentsInChildren<Slot>()){
			if (s.objOnMe == null) {
				s.DropObjectOnMe (t);
				break;
			}
		}

		SpawnNewObjectAction ();
	}

	/// <summary>
	/// Loads links for all objects. Has to do this after all objects exist.
	/// </summary>
	public void LoadLinks(string l){
		string[] linkPerObj = l.Split ('\n');	//splitting apart each object

		int i = 0;
		foreach(UIOBject obj in objectOrder){

			string[] links = linkPerObj[i].Split('#');	//splitting apart each link.

			if(links[1] == obj.id.ToString()){
				if(links[0] == "A"){
					ActionObject t = (ActionObject)obj;
					UIOBject[,] tempLinks = new UIOBject[2,2];

					for (int j = 2; j < 6; j++) {
						foreach(UIOBject otherObj in objectOrder){
							if(otherObj == obj){
								continue;
							}
							if(links[j] == otherObj.id.ToString()){
								if(j-2 == 0){
									tempLinks[0,0] = otherObj;
								}
								else if(j-2 == 1){
									tempLinks[0,1] = otherObj;
								}
								else if(j-2 == 2){
									tempLinks[1,0] = otherObj;
								}
								else if(j-2 == 3){
									tempLinks[1,1] = otherObj;
								}
							}
						}
					}
					t.links = tempLinks;
				}
				else if(links[0] == "T"){
					
					TextObject t = (TextObject)obj;
					UIOBject templink = null;
					foreach(UIOBject otherObj in objectOrder){
						if(otherObj == obj){
							continue;
						}
						if(links[2] == otherObj.id.ToString()){
							templink = otherObj;
						}
					}
					print("TEMP LINK "+templink);
					t.link = templink;
				}
			}
			i++;
		}

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

		Destroy (textObjectReady);
		Destroy (actionObjectReady);

	}

	/// <summary>
	/// Called to set up the final parts of the editor that needs to work.
	/// </summary>
	/// <param name="name">Name.</param>
	public void StartStoryEditing(string name){
		currentStory = name;
		maxID = 0;

		if (actionObjectReady == null) {
			SpawnNewObjectAction ();
		}
		if (textObjectReady == null) {
			SpawnNewObjectText ();
		}
	}

	public void FinalStartStoryEditing(){ //IT SKIPS 4??????
		
		RefreshObjectList ();
	//	FindMaxID ();
	 //DON'T NEED THIS NOW
	}


	void IterateMaxID(){
		maxID += 1;
	}

/*	public void FindMaxID(){
		print ("BEF " + maxID);
		foreach (UIOBject o in objectOrder) {
			if (o.id > maxID) {
				maxID = o.id;
				print ("FOUND " + maxID);
			}

		}
		print ("IS " + maxID);
		if (textObjectReady.GetComponent<UIOBject> ().id > maxID) {
			maxID = textObjectReady.GetComponent<UIOBject> ().id;
		}
		if (actionObjectReady.GetComponent<UIOBject> ().id > maxID) {
			maxID = actionObjectReady.GetComponent<UIOBject> ().id;
		}
		print ("CHANGED TO " + maxID);
		maxID = maxID + 1; //setting it to be one higher, since we iterate AFTER setting it, when spawning
	}
*/


		

	public void SaveStory(){
		storyMan.SaveStory (currentStory, objectOrder);
	}



	public void ShadowMode (){
		print ("SHADOW MODE");
		foreach (UIOBject obj in objectOrder) {
			if (obj == objectBeingInspected) {
				continue;
			}
			GameObject shadow = (GameObject)Instantiate (objectShadowPrefab);
			shadow.transform.SetParent (obj.transform.parent.parent.parent.parent.parent,false);
			shadow.transform.position = obj.transform.position;
			ShadowObject sh = shadow.GetComponent<ShadowObject> ();
			sh.SetupLink (obj);
			shadowObjects.Add (sh);
			//shadow.transform.localScale = Vector3.one;
		}
	}

	public void SpawnNewShadowObject(UIOBject obj){
		GameObject shadow = (GameObject)Instantiate (objectShadowPrefab);
		shadow.transform.SetParent (obj.transform.parent.parent.parent.parent.parent,false);
		shadow.transform.position = obj.transform.position;
		ShadowObject sh = shadow.GetComponent<ShadowObject> ();
		sh.SetupLink (obj);
		shadowObjects.Add (sh);
	}

	public void SpawnShadowObjectOnSlot(ShadowSlot slot, UIOBject link){
		GameObject shadow = (GameObject)Instantiate (objectShadowPrefab);
		shadow.transform.SetParent (slot.transform,false);
		shadow.transform.position = slot.transform.position;
		ShadowObject sh = shadow.GetComponent<ShadowObject> ();
		sh.SetupLink (link);
		shadowObjects.Add (sh);
	}

	public void ExitShadowMode(){
		print("Destroying all shadows "+shadowObjects.Count);
		for (int i = 0; i < shadowObjects.Count; i++) {
			print(i);
			Destroy (shadowObjects [i].gameObject);
		}
		shadowObjects.Clear();
		print(shadowObjects.Count);

	}



}
