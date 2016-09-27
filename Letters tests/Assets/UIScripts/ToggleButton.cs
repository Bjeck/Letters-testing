using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleButton : Toggle {

	InspectionPanel inspP;

	// Use this for initialization
	void Start () {
		inspP = GameObject.Find("InspectionPanel").GetComponent<InspectionPanel>();
	}

	public override void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) {
		if(isOn){ //I Only want this because I have two buttons, and don't need people to press the same button more than once.
			return;
		}
		base.OnPointerClick(eventData);

		// override the color such that the toggle state of the button is obvious
		// by its color. 
		if (isOn) {
			image.color = this.colors.pressedColor;            
		} else {
			image.color = this.colors.normalColor;           
		}

		inspP.ToggleAB(this);
	}

	public void CheckColor(){
		if (isOn) {
			image.color = this.colors.pressedColor;            
		} else {
			image.color = this.colors.normalColor;           
		}
	}

}
