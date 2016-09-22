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

	public List<UIOBject> objectOrder = new List<UIOBject>();

	public Slot slotBeingDraggedFrom;
	public UIOBject objectBeingDragged;
	public UIOBject objectBeingInspected;

	// Use this for initialization
	void Start () 
	{
	
	}

	public void InspectObject(UIOBject obj)
	{
		objectBeingInspected = obj;
		inspP.NewObject(obj);
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
		print("Spawning action object");
		GameObject newObject = (GameObject)Instantiate(actionObjectPrefab);
		newObject.transform.SetParent(objectPanel.transform,false);
		newObject.transform.position = actionObjectPoint.transform.position;
		newObject.GetComponent<RectTransform>().localScale = Vector3.one;
		actionObjectReady = newObject;
	}

	public void SpawnNewObjectText(){
		GameObject newObject = (GameObject)Instantiate(textObjectPrefab);
		newObject.transform.SetParent(objectPanel.transform,false);
		newObject.transform.position = textObjectPoint.transform.position;
		newObject.GetComponent<RectTransform>().localScale = Vector3.one;
		textObjectReady = newObject;
	}
}
