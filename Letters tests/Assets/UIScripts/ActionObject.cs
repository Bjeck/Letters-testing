using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ActionType{webPageError,Phonecall,wordSubstitution}

public class ActionObject : UIOBject {
	public ActionType acType;
	public Text actionText;



	public void ChangeType(ActionType at){
		acType = at;
		actionText.text = at.ToString();
		print("changed type of "+name+" to "+acType);
	}

}
