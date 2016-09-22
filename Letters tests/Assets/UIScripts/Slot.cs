using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

	public AuthorUIManager authorMan;
	public UIOBject objOnMe;

	public void Start(){
		authorMan = GameObject.Find("Canvas").GetComponent<AuthorUIManager>();
	}



	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		authorMan.objectBeingDragged.transform.SetParent(transform);
		objOnMe = authorMan.objectBeingDragged;
		authorMan.objectBeingDragged.slotImOn = this;
		authorMan.RefreshObjectList();
		print("DROPPED ON "+gameObject.name);
	}

	#endregion
}
