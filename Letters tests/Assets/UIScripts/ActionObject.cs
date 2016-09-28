using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public enum ActionType{Phonecall,WebPageError,WordSubstitution};


public class ActionObject : UIOBject {
	public ActionContent a, b;
	public string[] names = {"Phonecall", "Webpage Error", "Word Substitution"};
}
