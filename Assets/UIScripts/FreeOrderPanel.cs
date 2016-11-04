using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using System.Linq;

public class FreeOrderPanel : MonoBehaviour, IDropHandler {

	[SerializeField] AuthorUIManager authorMan;
	[SerializeField] GameObject linePrefab;
	public List<UIOBject> objects = new List<UIOBject>();


	// Use this for initialization
	void Start () {
	
	}

	public void DrawLine(UIOBject oa, UIOBject ob){
		print("DRAW");
		if(objects.Contains(oa) && objects.Contains(ob)){
			GameObject g = (GameObject)Instantiate(linePrefab);
			g.transform.SetParent(transform,false);
			Line l = g.GetComponent<Line>();
			l.fop = GetComponent<RectTransform>();
			l.DrawLine(oa,ob);
		}
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
			authorMan.RefreshObjectList();
			print(obj.name + " DROPPED ON "+gameObject.name);
			objects.Add(obj); 	//HOW DO I DELETE THIS?? I KNOW HOW: IN DELETE BOX: IF IN OBJECTS LIST DELETE. CAPS: GOOD.
		}
	}


}
