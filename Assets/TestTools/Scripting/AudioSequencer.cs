using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AudioSequencer : MonoBehaviour {
	[Serializable]
	public struct LoopSequence {
		public float start;
		public float end;
	}
	public List<LoopSequence> loopMarkers = new List<LoopSequence> ();

	public LoopSequence intro;
	public LoopSequence outro;

	public List<LoopSequence> sections = new List<LoopSequence> ();

	public float BPM;
	public float start;
	public int currentLoop;
	private bool BPM_Defined;
	//[HideInInspector]
	public AudioSource song;

	private bool rewind = false;
	private bool fastforward = false;

	public int currentSectionIndex = 0;
	private LoopSequence currentSection;

	void FindStamps(){
		Debug.Log(song.time);
	}

	void SongControls() {
		
		if (song.time <= 0.03f) song.time = start;
		if (Input.GetKeyDown (KeyCode.Comma)) rewind = true;
		if (Input.GetKeyDown (KeyCode.Period)) fastforward = true;
		if (rewind) {
			song.pitch = Mathf.Lerp(song.pitch, -3, Time.deltaTime * 2);
			if (song.pitch <= -2.9f) {
				if((currentSectionIndex > 0)) currentSectionIndex--;
				currentSection = sections [currentSectionIndex];
				song.time = currentSection.start;
				song.pitch = 1;
				rewind = false;
			}
		}
		if (fastforward) {
			song.pitch = Mathf.Lerp(song.pitch, 3, Time.deltaTime * 2);
			if (song.pitch >= 2.9f) {
				if((currentSectionIndex < sections.Count)) currentSectionIndex++;
				currentSection = sections [currentSectionIndex];
				song.time = currentSection.end;
				song.pitch = 1;
				fastforward = false;
			}
		}
	}
	 
	// Use this for initialization
	void Start () {
		song = GetComponent<AudioSource> ();
		outro.end = song.clip.length;
		sections.Add (intro);
		sections.AddRange (loopMarkers);
		sections.Add (outro);

		currentSection = sections[currentSectionIndex];
		start = intro.start;
		song.time = start;
	}
	
	// Update is called once per frame
	void Update () {	
		//FindStamps ();
		if (song.time >= (loopMarkers[currentLoop].end - 0.05f)) {
			currentLoop++;
			if (currentLoop >= loopMarkers.Count) currentLoop = 0;
			song.time = loopMarkers[currentLoop].start;
		}
		SongControls ();
	}
}
