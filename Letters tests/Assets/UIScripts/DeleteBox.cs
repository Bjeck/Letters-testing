using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DeleteBox : MonoBehaviour, IDropHandler {

	[SerializeField] AuthorUIManager authorMan;

	// Use this for initialization
	void Start () {
	
	}

	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		if (authorMan.objectBeingDragged) {
			Destroy (authorMan.objectBeingDragged.gameObject);
			authorMan.objectBeingDragged = null;
			authorMan.slotBeingDraggedFrom = null;
			authorMan.RefreshObjectList ();
		}
	}

	#endregion
}
