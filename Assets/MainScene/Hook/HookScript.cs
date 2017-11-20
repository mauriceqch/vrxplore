using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using Hook;

namespace Hook
{
    public class HookScript : MonoBehaviour
    {
        public GameObject attractedObject;
        public GameObject chain;
        public GameObject dotPointer;
        public GameObject hookChainSpawnPoint;

        private GameObject currentDotPointer;
        private GameObject currentChain;

        // Use this for initialization
        void Start()
        {
            currentDotPointer = Instantiate(dotPointer);
        }

        // Update is called once per frame
        void Update()
        {
			if (Input.GetButton("FireRight"))
			{
				Debug.Log ("FireRight detected");
			}
            Transform spawnPointTransform = hookChainSpawnPoint.transform;
            Vector3 direction = transform.TransformDirection(Vector3.forward);

            RaycastHit hit;
            if (Physics.Raycast(spawnPointTransform.position, direction, out hit))
            {
                currentDotPointer.SetActive(true);
                currentDotPointer.transform.position = hit.point;
				if (Input.GetButtonDown("FireRight"))
                {
                    print("Raycast hit, distance : " + hit.distance + " pos : " + hit.point);

                    if (currentChain != null)
                    {
                        Destroy(currentChain);
                    }

                    createChain(hit);
                }
            }
            else
            {
                currentDotPointer.SetActive(false);
            }
        }

        void createChain(RaycastHit hit)
        {
            Debug.Log("Instantiating chain");
            Transform spawnPointTransform = hookChainSpawnPoint.transform;
            currentChain = Instantiate(chain, spawnPointTransform.position, spawnPointTransform.rotation);
            HookChainScript hookChainScript = currentChain.GetComponent<HookChainScript>();
            hookChainScript.destination = hit.point;
            hookChainScript.attractedObject = attractedObject;
        }
    }
}