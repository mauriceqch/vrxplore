using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WindSound : MonoBehaviour {

	public AudioSource sourceStrong;
	public AudioSource sourceSoft;
	private float velToVol = 40.0f;
	private float velocitySoundChoice = 0.5f;


	void Awake () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rigidbody rb = this.GetComponent<Rigidbody> ();
		// float volume = Math.Max(rb.velocity.magnitude / velToVol, 1.0f) / 10.0f;
		float volume = Math.Min(Math.Max(0f, rb.velocity.magnitude - 10.0f) / velToVol, 1.0f);
		volume = volume * volume;
		volume = volume / 2.0f;
		volume = Math.Max (0.05f, volume);
		print ("wind volume : " + volume);
		if (rb.velocity.magnitude < velocitySoundChoice) {
			sourceSoft.volume = volume;
			// sourceSoft.Play ();
		} else {
			sourceStrong.volume = volume;
			// sourceStrong.Play ();
		}
	}
}
