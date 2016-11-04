using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ActionType{Phonecall,WebPageError,WordSubstitution,Questionnaire};


public class ActionObject : UIOBject {
	public ActionContent a, b;

	//public List<List<UIOBject>> links = new List<List<UIOBject>>();
	public UIOBject[,] links = new UIOBject[2,2];
	string[] requirements = new string[2];



	public void SetRequirement(string req, int nr){
		requirements [nr] = req;
	}

	public void SetLink(UIOBject obj, int x, int y){
		links [x,y] = obj;
		authorMan.orderPanel.DrawLine(this,obj);
		print("SET LINK");
	}

	public void SetLinks(UIOBject[,] l){
		links = l;
	}


}
