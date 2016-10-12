using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InspectionPanelActionHelper : MonoBehaviour {


	[SerializeField] InspectionPanel ip;


	public void ActionFillIn(ActionType act, ActionObject obj){

		switch(act){
		case ActionType.Phonecall:

			break;
		case ActionType.WebPageError:
			ip.webpageerrorIPF.text = ip.a ? obj.a.actionString : obj.b.actionString;
			break;
		case ActionType.WordSubstitution:

			break;

		}
	}


	public void SaveAction(){
		ActionObject ao = ip.authorMan.objectBeingInspected as ActionObject;
		ActionType acval = ip.a ? ao.a.acType : ao.b.acType;

		switch (acval) {
		case ActionType.Phonecall: //Phonecall

			break;
		case ActionType.WebPageError: //Webpage error
			if (ip.a) {
				ao.a.actionString = ip.webpageerrorIPF.text;
			} else {
				ao.b.actionString = ip.webpageerrorIPF.text;
			}

			break;
		case ActionType.WordSubstitution: //word substitution

			break;
		}

	}




}
