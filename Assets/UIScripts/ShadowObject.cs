using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShadowObject : MonoBehaviour {

	public UIOBject link;
	public Text text;

	public void SetupLink(UIOBject o){
		print ("setting up links");
		link = o;
		text.text = o.text.text;
	}

}
