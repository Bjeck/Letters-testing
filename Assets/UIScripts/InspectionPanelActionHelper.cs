using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class InspectionPanelActionHelper : MonoBehaviour {


	[SerializeField] InspectionPanel ip;


	public void LinkFillIn(ActionObject obj){
		ShadowSlot[] sllss = ip.linkSetupAction.GetComponentsInChildren<ShadowSlot> ();
		print("adding links");
		int k = 0;
		for (int i = 0; i < 2; i++) {
			for (int j = 0; j < 2; j++) {
				if(obj.links[i,j] != null){
					ip.authorMan.SpawnShadowObjectOnSlot(sllss[k],obj.links[i,j]);
				}
				k++;
			}
		}
	}

	public void LinkFillIn(TextObject obj){
		ShadowSlot shslot = ip.linkSetupText.GetComponentInChildren<ShadowSlot> ();
		print("adding links");
		int k = 0;
		if(obj.link != null){
			ip.authorMan.SpawnShadowObjectOnSlot(shslot,obj.link);
		}
	}

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
			if (!string.IsNullOrEmpty (s)) {
				string[] ss = s.Split (new string[] { "¤" }, System.StringSplitOptions.None);
				ip.QuestionniareQIPF.text = ss [0];
				ip.QuestionniareA1IPF.text = ss [1];
				ip.QuestionniareA2IPF.text = ss [2];
			} else {
				ip.QuestionniareQIPF.text = "";
				ip.QuestionniareA1IPF.text = "";
				ip.QuestionniareA2IPF.text = "";
			}
			break;

		}
	}


	public void SaveAction(){
		ActionObject ao = ip.authorMan.objectBeingInspected as ActionObject;
		if (ip.IsInspectingLinks ()) 
		{
			ShadowSlot[] sllss = ip.linkSetupAction.GetComponentsInChildren<ShadowSlot> ();

			int k = 0;
			for (int i = 0; i < 2; i++) {
				for (int j = 0; j < 2; j++) {
					if (sllss [k].objOnMe != null) {
						print("DO SET LINK");
						ao.SetLink(sllss[k].objOnMe.objectlink,i,j);
						print (i + " " + j + " now has " + sllss [k].objOnMe.text.text + " in "+k);
					}
					k++;
				}
			}
		}
		else 
		{
			
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


	public void SaveText(){
		TextObject to = ip.authorMan.objectBeingInspected as TextObject;

		if (ip.IsInspectingLinks ()) 
		{
			ShadowSlot shslot = ip.linkSetupText.GetComponentInChildren<ShadowSlot> ();
			if(shslot.objOnMe != null){
				to.SetLink(shslot.objOnMe.objectlink);
			}
		}
		else {
			//Non link stuff (TEXT!)
		}


	}




}
