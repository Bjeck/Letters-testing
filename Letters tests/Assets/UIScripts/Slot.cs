using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IDropHandler {

	public AuthorUIManager authorMan;
	public UIOBject objOnMe;

	public void Awake(){
		authorMan = GameObject.Find("StoryCanvas").GetComponent<AuthorUIManager>();
	}



	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		DropObjectOnMe (authorMan.objectBeingDragged);
	}

	#endregion

	public void DropObjectOnMe(UIOBject obj){
		if (obj != null) {
			obj.transform.SetParent(transform);
			objOnMe = obj;
			obj.slotImOn = this;
			authorMan.RefreshObjectList();
			print(obj.name + " DROPPED ON "+gameObject.name);
		}
	}


}
