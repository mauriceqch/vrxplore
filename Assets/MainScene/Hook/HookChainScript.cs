using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook
{
    public class HookChainScript : MonoBehaviour {
        public float chainSpeed = 1000;
        public float attractionForce = 1000;
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
            if (Vector3.Distance(playerTransform.position, transform.position) > hookPadding)
            {
                attractedObject.GetComponent<Rigidbody>().AddForce((destination - playerTransform.position).normalized * attractionForce);
            }
        }
    }
}