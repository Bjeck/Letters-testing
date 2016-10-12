using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ActionType{Phonecall,WebPageError,WordSubstitution,Questionnaire};


public class ActionObject : UIOBject {
	public ActionContent a, b;

	public List<List<UIOBject>> links = new List<List<UIOBject>>();
	string[] requirements = new string[2];

}
