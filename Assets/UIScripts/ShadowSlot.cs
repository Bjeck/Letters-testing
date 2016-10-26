using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ShadowSlot : MonoBehaviour, IDropHandler {

	[SerializeField] AuthorUIManager authorMan;
	public ShadowObject objOnMe;

	public void Awake(){
		authorMan = GameObject.Find("StoryCanvas").GetComponent<AuthorUIManager>();
	}


	#region IDropHandler implementation

	public void OnDrop (PointerEventData eventData)
	{
		DropObjectOnMe (authorMan.shadowObjectBeingDragged);
	}

	#endregion

	public void DropObjectOnMe(ShadowObject obj){
		print("beginning drop");
		if (obj != null) {
			obj.transform.SetParent(transform);
			objOnMe = obj;
			obj.slotImOn = this;
			print(obj.name + " DROPPED ON "+gameObject.name);
		}
	}
}
