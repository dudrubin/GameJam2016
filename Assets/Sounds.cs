using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour {

	private static Dictionary<string,AudioSource> sources = new Dictionary<string, AudioSource>();

	// Use this for initialization
	void Awake () {
		foreach (AudioSource source in GetComponents<AudioSource>()) {
			Debug.Log("========="+source.clip.name);
			sources.Add(source.clip.name,source);
		}
	}

	public static void Play(string name) {
		if (sources.ContainsKey(name)){
			sources[name].Play();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
