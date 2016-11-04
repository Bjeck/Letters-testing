using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOBject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {

	public Text text;

	public bool isDragging = false;
	public Vector3 startPos;
	public Transform startParent;

	public Slot slotImOn;
	[SerializeField] Image highlightIMG;
	public AuthorUIManager authorMan;

	public int id;

	float lerpSpeed = 0.7f;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		authorMan = GameObject.Find("StoryCanvas").GetComponent<AuthorUIManager>();
	}


	public void ObjClick(){
		authorMan.InspectObject(this);
	}

	public void Highlight(){
		highlightIMG.gameObject.SetActive (true);
	}

	public void UnHighlight(){
		highlightIMG.gameObject.SetActive (false);
	}



	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		if (authorMan.inspP.IsInspectingLinks ()) {
			return;
		}
		isDragging = true;
		startPos = transform.position;
		startParent = transform.parent;
		authorMan.objectBeingDragged = this;
		GetComponent<CanvasGroup>().blocksRaycasts = false;

		if(slotImOn){
			authorMan.slotBeingDraggedFrom = slotImOn;
			slotImOn.objOnMe = null;
			slotImOn = null;
		}

	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if (authorMan.inspP.IsInspectingLinks ()) {
			return;
		}
//		transform.position = Input.mousePosition; //Could do a Lerp here!

		transform.position = Vector3.Lerp(transform.position,Input.mousePosition,lerpSpeed);
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		print("end drag");
		if (authorMan.inspP.IsInspectingLinks ()) {
			return;
		}
		isDragging = false;

		if(authorMan.actionObjectReady == this.gameObject){
			if(transform.parent == startParent){	//failsafe. if parent hasn't changed (edited by OnDrop in Slot) move the object back to original position.
				transform.position = startPos;
			}
			else{
				authorMan.SpawnNewObjectAction();
			}
		}
		else if(authorMan.textObjectReady == this.gameObject){
			if(transform.parent == startParent){	//failsafe. if parent hasn't changed (edited by OnDrop in Slot) move the object back to original position.
				transform.position = startPos;
			}
			else{
				if(authorMan.textObjectReady == this.gameObject){
					authorMan.SpawnNewObjectText();
				}
			}
		}

		authorMan.objectBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		authorMan.slotBeingDraggedFrom = null;
	}

	#endregion



	#region IDropHandler implementation
	/// <summary>
	/// For dropping a UIObject on another, for swapping.
	/// </summary>
	/// <param name="eventData">Event data.</param>
	public void OnDrop (PointerEventData eventData)
	{
		if(authorMan.objectBeingDragged){
			if(authorMan.objectBeingDragged.transform.parent != transform.parent && authorMan.slotBeingDraggedFrom){
				print(authorMan.objectBeingDragged);

				UIOBject tempObj = authorMan.objectBeingDragged;
				Transform temp = authorMan.objectBeingDragged.transform.parent;

				authorMan.objectBeingDragged.transform.SetParent(transform.parent);
				authorMan.objectBeingDragged.slotImOn = slotImOn;
				authorMan.objectBeingDragged.slotImOn.objOnMe = tempObj;

				transform.SetParent(temp);
				slotImOn = authorMan.slotBeingDraggedFrom;
				slotImOn.objOnMe = this;

				print("swapped "+authorMan.objectBeingDragged);

			}
		}
		authorMan.RefreshObjectList();
	}

	#endregion
}
