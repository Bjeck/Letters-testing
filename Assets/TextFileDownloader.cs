﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.IO;

public class TextFileDownloader : MonoBehaviour {

	public InputField ipf;
	[SerializeField] StoryManager sm;
	[SerializeField] StoryParser sp;

	public string url;

	public void StartURLReadFromInput(){
		url = ipf.text;
		StartCoroutine(StartURLRead());
	}

	IEnumerator StartURLRead() {
		WWW www = new WWW(url);
		yield return www;
		print(www.text);


		//retrieving the name
		string[] oddlines = www.text.Split (new string[] {" --------------- ","\n"} ,StringSplitOptions.RemoveEmptyEntries);
		string n = oddlines[2]; //The name is always the third entry because of the story formatting.

		//loading the story
		sm.LoadStory (n,www.text);

		ipf.transform.parent.gameObject.SetActive (false);
	}

	//DELETE??
	IEnumerator StartURLDL() {
		WWW w = WWW.LoadFromCacheOrDownload(url,0);
		yield return w;
		print(w.text);

	}


}


/* COPY PASTE FIX?
 * 
 * For the current Unity version I can propose a simple workaround, 
 * a Paste UI button near the input field which will popup an html input field where user can paste the system clipboard and click OK 
 * (no need for overlays, just use the JavaScript prompt function for that); and a Copy UI button near the input field that will copy 
 * the field content into the system clipboard (using the code above) when user clicks the button (expect Safari). Also you might need 
 * an example of how to perform an action restricted by the browser security policy from a Unity UI button OnButtonPointerDown event 
*/
