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
			if(authorMan.actionObjectReady == authorMan.objectBeingDragged.gameObject){
				authorMan.SpawnNewObjectAction();
			}
			if(authorMan.textObjectReady == authorMan.objectBeingDragged.gameObject){
				authorMan.SpawnNewObjectText();
			}

			Destroy (authorMan.objectBeingDragged.gameObject);
			authorMan.objectBeingDragged = null;
			authorMan.slotBeingDraggedFrom = null;


			authorMan.RefreshObjectList ();
		}
		if(authorMan.shadowObjectBeingDragged){
			authorMan.shadowObjects.Remove(authorMan.shadowObjectBeingDragged);
			authorMan.SpawnNewShadowObject(authorMan.shadowObjectBeingDragged.objectlink);
			Destroy (authorMan.shadowObjectBeingDragged.gameObject);
			authorMan.shadowObjectBeingDragged = null;
		}
	}

	#endregion
}
