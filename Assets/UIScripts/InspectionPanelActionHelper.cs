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
		case ActionType.Questionnaire:
			string s = ip.a ? obj.a.actionString : obj.b.actionString;
			string[] ss = s.Split (new string[] {"¤"}, System.StringSplitOptions.None);

			ip.QuestionniareQIPF.text = ss [0];
			ip.QuestionniareA1IPF.text = ss [1];
			ip.QuestionniareA2IPF.text = ss [2];

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
		case ActionType.Questionnaire:
			if (ip.a) {
				ao.a.actionString = ip.QuestionniareQIPF.text + "¤" + ip.QuestionniareA1IPF.text + "¤" + ip.QuestionniareA2IPF;
			} else {
				ao.b.actionString = ip.QuestionniareQIPF.text + "¤" + ip.QuestionniareA1IPF.text + "¤" + ip.QuestionniareA2IPF;
			}
			break;

		}

	}




}
