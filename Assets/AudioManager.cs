using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour {
	public List<AudioSequencer> playlist = new List<AudioSequencer> ();

	void Start() {
		GetComponent<AudioSource>().clip = playlist [0].song.clip;
	}
}
