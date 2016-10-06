using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

public class StoryParser : MonoBehaviour {

	public StoryManager storyMan;
	string fileName = "MyFile.txt";

	// Use this for initialization
	void Start () {

	}

	public string SaveStoryToFile(string nam, string storyString){

		fileName = "Assets/Stories/" + nam + "_" + DateTime.Now.ToString("yy-MM-dd_hh-mm-ss") + ".txt";

		if (File.Exists(fileName))
		{
			Debug.Log(fileName+" already exists. Wait a second before saving again?");
			return fileName;
		}
		var sr = File.CreateText(fileName);
		sr.WriteLine ("LETTERS STORY --------------- ");
		sr.WriteLine (nam + " --------------- ");
		string[] split = storyString.Split (new string[] { "\r\n", "\n" },StringSplitOptions.None);

		foreach (string s in split) {
			sr.WriteLine (s);
		}


		sr.Close();
		print ("created");
		return fileName;
	}



	public void LoadStoryFullString(string nam, string fullString){

		List<string> lines = new List<string>(fullString.Split(new string[] { "\r","\n" },System.StringSplitOptions.RemoveEmptyEntries) );

		try{
			foreach(string line in lines)
			{
				if (!String.IsNullOrEmpty (line)) {
					if (line.Contains (" --------------- ")) {
						continue;
					}

					print (line);

					string l = line; //can't edit foreach variables.

					if (l.Substring (0, 5) == "TEXT.") {
						//TEXT
						l = l.Remove (0, 5);
						print (l);
						string[] sides = l.Split ('|');

						storyMan.authorMan.LoadTextObject (sides);//(sides [0], sides [1], sides [2]);
					} else if (l.Substring (0, 7) == "ACTION.") {
						//ACTION
						l = l.Remove (0, 7);
						print (l);
						string[] sides = l.Split ('|');
						storyMan.authorMan.LoadActionObject (sides);//(sides [0], sides [1], sides [4]);
					}
				}
			}
		}
		catch(Exception e){
			print ("LOAD STORY FAILED " + e.Message);
		}
	}




	public void LoadStory(string nam){

		StreamReader sr = new StreamReader (Application.dataPath + "/Stories/" + nam); 
		string line;

		string full = sr.ReadToEnd ();
		print (full);

		print ("Load Beginning");
		sr.Close();

		LoadStoryFullString (nam, full);

/*		try{
			using(sr){
				do {
					line = sr.ReadLine();

					if(!String.IsNullOrEmpty(line)){
						if(line.Contains(" --------------- ")){
							continue;
						}

						print (line);

						if(line.Substring(0,5) == "TEXT."){
							//TEXT
							line = line.Remove(0,5);
							print (line);
							string[] sides = line.Split('|');

							storyMan.authorMan.LoadTextObject(sides[0],sides[1],sides[2]);
						}
						else if(line.Substring(0,7) == "ACTION."){
							//ACTION
							line = line.Remove(0,7);
							print (line);
							string[] sides = line.Split('|');
							storyMan.authorMan.LoadActionObject(sides[0],sides[1],sides[4]);
						}
					}
				} 
				while(line != null);


			}
		}
		catch(Exception e){
			print ("LOAD STORY FAILED " + e.Message);
		}*/

	}



}
