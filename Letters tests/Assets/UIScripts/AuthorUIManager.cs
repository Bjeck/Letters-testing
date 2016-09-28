using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ObjectType{Letter, Action}

public class AuthorUIManager : MonoBehaviour {

	[SerializeField] GameObject slotPrefab;
	[SerializeField] GameObject actionObjectPrefab;
	[SerializeField] GameObject textObjectPrefab;

	[SerializeField] GameObject objectPanel;
	[SerializeField] GameObject actionObjectPoint;
	[SerializeField] GameObject textObjectPoint;

	public InspectionPanel inspP;
	public GameObject slots;

	public GameObject actionObjectReady;
	public GameObject textObjectReady;

	public Slot slotBeingDraggedFrom;
	public UIOBject objectBeingDragged;
	public UIOBject objectBeingInspected;


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
		}
	}

	public void AddSlotToList(){
		GameObject newSlot = (GameObject)Instantiate(slotPrefab,slots.transform);
		newSlot.GetComponent<RectTransform>().localScale = Vector3.one;
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
}
