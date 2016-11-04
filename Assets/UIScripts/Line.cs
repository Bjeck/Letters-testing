using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {

	public RectTransform fopp;
	public RectTransform fop;
	RectTransform thingA;
	RectTransform thingB;
	Vector3 pointA;
	Vector3 pointB;
	RectTransform imageRectTransform;
	public float lineWidth = 3;

	Vector3 differenceVector;

	void Awake()
	{
		imageRectTransform = GetComponent<RectTransform>();
	}

	void Start(){
	//	StartCoroutine(DrawLine());
	}

	public void DrawLine(UIOBject a, UIOBject b){
		thingA = a.GetComponent<RectTransform>();
		thingB = b.GetComponent<RectTransform>();
		StartCoroutine(DrawLine());
	}

	IEnumerator DrawLine()
	{
		while(true){
			pointA = thingA.anchoredPosition3D + new Vector3 (fop.rect.width/2,fop.rect.width/2,0); //USE free order coordinate????? 
			pointB = thingB.anchoredPosition3D + new Vector3 (fop.rect.width/2,fop.rect.width/2,0);

			differenceVector = pointB - pointA;
			imageRectTransform.sizeDelta = new Vector2( differenceVector.magnitude, lineWidth);
			imageRectTransform.pivot = new Vector2(0, 0.5f);
			imageRectTransform.position = pointA;
			float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
			imageRectTransform.rotation = Quaternion.Euler(0,0, angle);

			yield return new WaitForEndOfFrame();
		}
	}


}
