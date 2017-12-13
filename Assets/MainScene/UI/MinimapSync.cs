using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hook;

public class MinimapSync : MonoBehaviour {
	public GameObject[] hookScriptsGameObjects;
	public GameObject[] mapPointers;
	public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		ProcessHook (0);
		ProcessHook (1);
	}

	private void ProcessHook(int idx) {
		HookScript script = hookScriptsGameObjects [idx].GetComponent<HookScript> ();
		GameObject hookChain = script.currentChain;
		GameObject mapPointer = mapPointers [idx];

		if (hookChain == null) {
			mapPointer.SetActive (false);
		} else {
			mapPointer.SetActive (true);
			Transform t = mapPointer.transform;
			float x = calculateScaledPosition (transform.position.y, hookChain.transform.position.y);
			float y = calculateScaledPosition (transform.position.z, hookChain.transform.position.z);
			float theta = camera.transform.rotation.y;

			t.localPosition = new Vector3 (
				t.localPosition.x,
				Mathf.Cos(theta) * x - Mathf.Sin(theta) * y,
				Mathf.Sin(theta) * x + Mathf.Cos(theta) * y
			);
		}
	}

	private float calculateScaledPosition(float origin, float position) {
		return clamp((position - origin) / 500.0f);
	}

	private float clamp(float f) {
		return clamp(f, 0.1f);
	}

	private float clamp(float f, float limit) {
		return Mathf.Min (Mathf.Max (f, -limit), limit);
	}
}
