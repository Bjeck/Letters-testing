﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI.Extensions;

public class LineTest : MonoBehaviour {

	LineRenderer lr;

	public RectTransform rec;
	public RectTransform fopp;
//	public RectTransform fop;

	public RectTransform thingA;
	public RectTransform thingB;
	public Vector3 pointA;
	public Vector3 pointB;
	public RectTransform imageRectTransform;
	public float speed;
	public float lineWidth = 1;

	Vector3 differenceVector;

	public UILineRenderer uiline;



	void Awake()
	{
		imageRectTransform = GetComponent<RectTransform>();
	//	lr = GetComponent<LineRenderer>();
	}
	void Start(){
		//StartCoroutine(DrawLine());
	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine(DrawLine());
		}
	}
		
	IEnumerator DrawLine()
	{
		while(true){
		//	pointA = thingA.anchoredPosition3D + new Vector3 (rec.rect.width/2,rec.rect.height/2,0);
		//	pointB = thingB.anchoredPosition3D + new Vector3 (rec.rect.width/2,rec.rect.height/2,0);
		//	/
			uiline.Points [0] = thingA.anchoredPosition + new Vector2 (rec.rect.width/2,rec.rect.height/2);
			uiline.Points [1] = thingB.anchoredPosition + new Vector2 (rec.rect.width/2,rec.rect.height/2);
			uiline.SetVerticesDirty ();

		//	differenceVector = pointB - pointA;
		//	imageRectTransform.sizeDelta = new Vector2( differenceVector.magnitude, lineWidth);
		//	imageRectTransform.pivot = new Vector2(0, 0.5f);
		//	imageRectTransform.position = pointA;
		//	float angle = Mathf.Atan2(differenceVector.y, differenceVector.x) * Mathf.Rad2Deg;
		//	imageRectTransform.rotation = Quaternion.Euler(0,0, angle);

			yield return new WaitForEndOfFrame();
		}
	}


}
