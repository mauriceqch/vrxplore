using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hook;

namespace Hook
{
    public class HookScript : MonoBehaviour {
        public GameObject chain;
        public GameObject dotPointer;
        public GameObject hookChainSpawnPoint;

        private GameObject currentDotPointer;
        private GameObject currentChain;

        // Use this for initialization
        void Start () {
            currentDotPointer = Instantiate(dotPointer);
        }
        
        // Update is called once per frame
        void Update () {
            Transform spawnPointTransform = hookChainSpawnPoint.transform;
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            Debug.DrawRay(spawnPointTransform.position, direction * 1000, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(spawnPointTransform.position, direction, out hit))
            {
                currentDotPointer.SetActive(true);
                currentDotPointer.transform.position = hit.point;

                if (Input.GetMouseButtonDown(0))
                {
                        print("Raycast hit, distance : " + hit.distance + " pos : " + hit.point);

                        if (currentChain != null)
                        {
                            Destroy(currentChain);
                        }

                        Debug.Log("Instantiating chain");

                        currentChain = Instantiate(chain, spawnPointTransform.position, spawnPointTransform.rotation);

                        currentChain.GetComponent<HookChainScript>().destination = hit.point;
                }
            } else
            {
                currentDotPointer.SetActive(false);
            }
        }
    }
}