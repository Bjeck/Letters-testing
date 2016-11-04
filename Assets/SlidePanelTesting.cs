using UnityEngine;
using System.Collections;

public class SlidePanelTesting : MonoBehaviour {

	public Animator anim;
	RectTransform rect;
	public float lerpSpeed = 2f;

	public bool slideIn = true;

	// Use this for initialization
	void Start () {
		rect = GetComponent<RectTransform> ();
	}
	
	// Update is called once per frame
	void Update () {
	
		//print (Camera.main.WorldToScreenPoint (Input.mousePosition) +" "+Screen.width+" "+Screen.height);

		if (Input.GetKeyDown (KeyCode.Space)) {
			anim.SetTrigger ("slide");
			//Slide(slideIn);
			slideIn = !slideIn;
		}

	}


	void Slide(bool inwards){
		if (inwards) {
			//Vector2 pos = rect.anchoredPosition;
			//pos.x += rect.rect.width;
			//rect.anchoredPosition = pos;
			float endpoint = rect.anchoredPosition.x + rect.rect.width;
			endpoint = Mathf.Clamp (endpoint, endpoint - rect.rect.width, endpoint + rect.rect.width);


			StartCoroutine (SlideRoutine (endpoint, inwards));

		} else {
			//Vector2 pos = rect.anchoredPosition;
			//pos.x -= rect.rect.width;
			//rect.anchoredPosition = pos;
			float endpoint = rect.anchoredPosition.x - rect.rect.width;
			endpoint = Mathf.Clamp (endpoint, endpoint - rect.rect.width, endpoint + rect.rect.width);

			StartCoroutine (SlideRoutine (endpoint, inwards));
		}
	}

	IEnumerator SlideRoutine(float endPoint, bool inwards){
		Vector2 pos = rect.anchoredPosition;
		if (inwards) {
			while (rect.anchoredPosition.x < endPoint) {

				//lerp 
				pos.x = Mathf.Lerp(pos.x,endPoint,Time.deltaTime*lerpSpeed);
				rect.anchoredPosition = pos;

				if (Mathf.Abs (rect.anchoredPosition.x - endPoint) < 0.1) {
					pos.x = endPoint;
				}

				yield return new WaitForEndOfFrame();
			}
				
		} else {
			while (rect.anchoredPosition.x > endPoint) {
				pos.x = Mathf.Lerp(pos.x,endPoint,Time.deltaTime*lerpSpeed);
				rect.anchoredPosition = pos;

				if (Mathf.Abs (rect.anchoredPosition.x - endPoint) < 0.1) {
					pos.x = endPoint;
				}

				yield return new WaitForEndOfFrame();
			}
		}



	}




}
