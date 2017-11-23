using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour {

    public const int RAYCASTLENGTH = 20;
    public const float pushForce = 50.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hitInfo;
        Ray ray = GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
        bool rayCasted = Physics.Raycast(ray, out hitInfo, RAYCASTLENGTH);

        if (rayCasted)
        {
            rayCasted = hitInfo.transform.CompareTag("Pushable");
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (rayCasted)
            {
                Debug.Log("Object pushed");
                hitInfo.rigidbody.AddRelativeForce((hitInfo.rigidbody.position + ray.direction * RAYCASTLENGTH) * pushForce);
            }
        }
    }
}
