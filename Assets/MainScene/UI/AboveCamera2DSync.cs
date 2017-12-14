using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboveCamera2DSync : MonoBehaviour {
	public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraRotation = camera.transform.rotation.eulerAngles;
		Quaternion rotation = gameObject.transform.rotation;
		rotation.eulerAngles = new Vector3 (rotation.eulerAngles.x, cameraRotation.y - 90.0f, rotation.eulerAngles.z);
		gameObject.transform.rotation = rotation;
	}
}
