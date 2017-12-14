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

			// get eulerAngles and adjust to positive z axis (+180°)
			// adjust angle to have right and left at the right place
			// float theta = (90.0f - ((transform.rotation.eulerAngles.y + 180.0f) - 90.0f));
			float theta = transform.rotation.eulerAngles.y;
			theta = theta * Mathf.Deg2Rad;

			float x = hookChain.transform.position.y - transform.position.y;
			float y = hookChain.transform.position.z - transform.position.z;
			float rotX = scaleDistance(Mathf.Cos (theta) * x - Mathf.Sin (theta) * y);
			float rotY = scaleDistance(Mathf.Sin (theta) * x + Mathf.Cos (theta) * y);


			t.localPosition = new Vector3 (
				t.localPosition.x,
				rotX,
				rotY
			);
		}
	}

	private float scaleDistance(float distance) {
		return clamp(distance / 500.0f);
	}

	private float clamp(float f) {
		return clamp(f, 0.1f);
	}

	private float clamp(float f, float limit) {
		return Mathf.Min (Mathf.Max (f, -limit), limit);
	}
}
