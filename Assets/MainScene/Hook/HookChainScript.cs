using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook
{
    public class HookChainScript : MonoBehaviour {
        public float chainSpeed = 1000;
        public float attractionForce = 500;
        public float hookPadding = 1;

        public Vector3 destination;
        public GameObject attractedObject;

        // Use this for initialization
        void Start () {
            
        }
        
        // Update is called once per frame
        void Update () {
            float deltaTime = Time.smoothDeltaTime;

            transform.position = Vector3.MoveTowards(transform.position, destination, chainSpeed * deltaTime);
        }

        void FixedUpdate()
        {
            Transform playerTransform = attractedObject.transform;
            float distance = Vector3.Distance(playerTransform.position, destination);
            if (distance > hookPadding)
            {
				Rigidbody rb = attractedObject.GetComponent<Rigidbody> ();

				float velocity = rb.velocity.magnitude;

				Vector3 attraction = (destination - playerTransform.position).normalized * ((float) (1 - Math.Exp(-Math.Sqrt(distance)))) * attractionForce;

				Vector3 massAttenuationForce = Vector3.up * 10 * ((float) (1 - Math.Exp(-distance)));

				Vector3 stabilizationDirection = (destination - (playerTransform.position + rb.velocity)).normalized;
				// stabilizationDirection.Scale(stabilizationDirection);
				stabilizationDirection.Scale(new Vector3 (1, 1, 0.5f));
				Vector3 stabilization = ((float)(Math.Exp(-Math.Sqrt(distance / 3f)))) * stabilizationDirection * attractionForce * velocity * ((float)  Math.Sqrt(velocity)) / 5.0f;
				rb.AddForce(attraction + stabilization + massAttenuationForce, ForceMode.Force);

            }
        }
    }
}