using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TextFileDownloader : MonoBehaviour {

	public InputField ipf;

	public string url;
	// Use this for initialization
	void Start () {
		StartCoroutine(StartURLRead());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator StartURLRead() {
		WWW www = new WWW(url);
		yield return www;
		print(www.text);

		List<string> lines = new List<string>(www.text.Split(new string[] { "\r","\n" },System.StringSplitOptions.RemoveEmptyEntries) );
		foreach(string s in lines){
			print(s);
		}

		ipf.text = www.text;
		//ipf.text = "<i>hello</i> what is up?";

	}

	IEnumerator StartURLDL() {
		WWW w = WWW.LoadFromCacheOrDownload(url,0);
		yield return w;
		print(w.text);

	}


}


/*
 * 
 * For the current Unity version I can propose a simple workaround, 
 * a Paste UI button near the input field which will popup an html input field where user can paste the system clipboard and click OK 
 * (no need for overlays, just use the JavaScript prompt function for that); and a Copy UI button near the input field that will copy 
 * the field content into the system clipboard (using the code above) when user clicks the button (expect Safari). Also you might need 
 * an example of how to perform an action restricted by the browser security policy from a Unity UI button OnButtonPointerDown event 
*/
