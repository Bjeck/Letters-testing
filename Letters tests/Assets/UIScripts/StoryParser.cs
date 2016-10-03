using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class StoryParser : MonoBehaviour {

	string fileName = "MyFile.txt";

	// Use this for initialization
	void Start () {

	}





	// Update is called once per frame
	void Update () {
	
	}


	public void SaveStoryToFile(string name, string storyString){
		fileName = "Assets/" + name + "_" + DateTime.Now.ToString("yy-MM-dd_hh-mm_ss") + ".txt";

		if (File.Exists(fileName))
		{
			Debug.Log(fileName+" already exists. Wait a second before saving again?");
			return;
		}
		var sr = File.CreateText(fileName);
		sr.WriteLine (name + " --------------- ");
		string[] split = storyString.Split (new string[] { "\r\n", "\n" },StringSplitOptions.None);

		foreach (string s in split) {
			sr.WriteLine (s);
		}


		sr.Close();
		print ("created");
	}


}
