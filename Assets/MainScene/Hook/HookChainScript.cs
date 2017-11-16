using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hook
{
    public class HookChainScript : MonoBehaviour {
        public float chainSpeed;
        public Vector3 destination;

        // Use this for initialization
        void Start () {
            
        }
        
        // Update is called once per frame
        void Update () {
            float step = chainSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, destination, step);
        }
    }
}