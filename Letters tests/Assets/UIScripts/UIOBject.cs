using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIOBject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler {

	public Text text;

	public bool isDragging = false;
	public Vector3 startPos;
	public Transform startParent;

	public Slot slotImOn;
	[SerializeField] Image highlightIMG;
	AuthorUIManager authorMan;

	// Use this for initialization
	void Start () {
		startPos = transform.position;
		authorMan = GameObject.Find("Canvas").GetComponent<AuthorUIManager>();
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
		transform.position = Input.mousePosition; //Could do a Lerp here!
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		isDragging = false;
		if(transform.parent == startParent){
			transform.position = startPos;
		}
		else{
			if(authorMan.actionObjectReady == this.gameObject){
				authorMan.SpawnNewObjectAction();
			}
			if(authorMan.textObjectReady == this.gameObject){
				authorMan.SpawnNewObjectText();
			}
		}

		authorMan.objectBeingDragged = null;
		GetComponent<CanvasGroup>().blocksRaycasts = true;
		authorMan.slotBeingDraggedFrom = null;
	}

	#endregion



	#region IDropHandler implementation

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
