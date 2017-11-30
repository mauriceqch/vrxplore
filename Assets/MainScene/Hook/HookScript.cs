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
			if (Input.GetButton("RightTrigger"))
			{
				Debug.Log ("FireRight detected");
			}
            Transform spawnPointTransform = hookChainSpawnPoint.transform;
            Vector3 direction = transform.TransformDirection(Vector3.forward);

            RaycastHit hit;
            if (Physics.Raycast(spawnPointTransform.position, direction, out hit))
            {
				print("Raycast hit, distance : " + hit.distance + " pos : " + hit.point);

                currentDotPointer.SetActive(true);
                currentDotPointer.transform.position = hit.point;

				if (Input.GetButtonDown("RightTrigger"))
                {
                    if (currentChain != null)
                    {
                        Destroy(currentChain);
                    }

                    createChain(hit);
                }

				if (Input.GetButtonDown("RightTrackpad"))
				{
					print ("Object push try");
					Rigidbody rb = hit.rigidbody;
					if (rb != null && hit.distance < 2) {
						print ("Object pushed");
						rb.AddForce (direction.normalized * 10, ForceMode.Impulse);
					} else {
						print ("Object push fail");
					}
					// attractedObject.GetComponent<Rigidbody> ().AddForce (-direction.normalized * 100, ForceMode.Impulse);
				}

				print(Input.GetAxisRaw ("RightGrip"));
				//	print ("right grip pressed");
				//}
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