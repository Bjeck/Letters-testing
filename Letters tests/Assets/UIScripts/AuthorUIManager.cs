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
		inspP.InspectObject(obj);
	}


	public void ChangeActionObjectType(ActionType at){
		(objectBeingInspected as ActionObject).ChangeType(at);
	}




	public void RefreshObjectList(){
		objectOrder.Clear();
		foreach(Slot s in slots.GetComponentsInChildren<Slot>()){
			if(s.objOnMe){
				objectOrder.Add(s.objOnMe);
				print("SLOT "+s.objOnMe.name);
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
