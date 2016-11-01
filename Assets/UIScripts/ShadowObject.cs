using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShadowObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

	public UIOBject objectlink;
	public Text text;

	bool isDragging = false;
	Vector3 startPos;
	Transform startParent;
	public ShadowSlot slotImOn;
	bool isOnObject = true;
	public float lerpSpeed = 2f;

	public void SetupLink(UIOBject o){
		objectlink = o;
		text.text = o.text.text;
	}







	#region IBeginDragHandler implementation
	public void OnBeginDrag (PointerEventData eventData)
	{
		if(objectlink == null){
			return;
		}

		isDragging = true;
		startPos = transform.position;
		startParent = transform.parent;
		GetComponent<CanvasGroup>().blocksRaycasts = false;
		objectlink.authorMan.shadowObjectBeingDragged = this;

/*		if(slotImOn){
			authorMan.slotBeingDraggedFrom = slotImOn;
			slotImOn.objOnMe = null;
			slotImOn = null;
		}*/
	}
	#endregion

	#region IDragHandler implementation

	public void OnDrag (PointerEventData eventData)
	{
		if(objectlink == null){
			return;
		}
	//	transform.position = Input.mousePosition; //Could do a Lerp here!

		transform.position = Vector3.Lerp(transform.position,Input.mousePosition,lerpSpeed);

	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if(objectlink == null){
			return;
		}
		isDragging = false;

		if(transform.parent == startParent){
			transform.position = startPos;
		}
		else{
			if(isOnObject){
				objectlink.authorMan.SpawnNewShadowObject(objectlink);
				isOnObject = false;
			}
		}

		GetComponent<CanvasGroup>().blocksRaycasts = true;
		objectlink.authorMan.shadowObjectBeingDragged = null;
	}

	#endregion



/*	#region IDropHandler implementation

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

	#endregion*/




}
