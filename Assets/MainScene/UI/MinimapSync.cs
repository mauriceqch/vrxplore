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

			float theta = camera.transform.rotation.eulerAngles.y;
			theta = theta * Mathf.Deg2Rad;
            // camera rotation cancel angle
            theta = -theta;

            // Minimap y : player camera direction (z), UI up direction
            // Minimap z : player lateral direction (x), positive = right, negative = left
			float zDiff = hookChain.transform.position.z - transform.position.z;
			float xDiff = hookChain.transform.position.x - transform.position.x;
            // Cancel camera rotation
            float y = scaleDistance(Mathf.Cos(theta) * zDiff - Mathf.Sin(theta) * xDiff);
            float z = scaleDistance(Mathf.Sin(theta) * zDiff + Mathf.Cos(theta) * xDiff);

            // Project on minimap
			t.localPosition = new Vector3 (
				t.localPosition.x,
				y,
				z
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
