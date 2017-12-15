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
		public GameObject vibrationManager;
        public string side;

        private GameObject currentDotPointer;
        public GameObject currentChain;

        // Use this for initialization
        void Start()
        {
            currentDotPointer = Instantiate(dotPointer);
        }

        void Print(string s)
        {
            MonoBehaviour.print("HookScript " + side + " " + s);
        }

        // Update is called once per frame
        void Update()
        {
            debugInputs();
            Vector3 direction = transform.TransformDirection(Vector3.forward);
            Transform spawnPointTransform = hookChainSpawnPoint.transform;

            RaycastHit hit;
            if (Physics.Raycast(spawnPointTransform.position, direction, out hit))
            {
				Print("Raycast hit, distance : " + hit.distance + " pos : " + hit.point);

                currentDotPointer.SetActive(true);
                currentDotPointer.transform.position = hit.point;

				if (Input.GetButtonDown(side + "Trigger"))
                {
                    Print("Chain launched");
                    if (currentChain != null)
                    {
                        Destroy(currentChain);
                    }

                    createChain(hit);
                }

				if (Input.GetButtonDown(side + "Trackpad"))
				{
					Print ("Object push try");
					Rigidbody rb = hit.rigidbody;
					if (rb != null && hit.distance < 2) {
						Print ("Object pushed");
						rb.AddForce (direction.normalized * 10, ForceMode.Impulse);
					} else {
						Print ("Object push fail");
					}
					// attractedObject.GetComponent<Rigidbody> ().AddForce (-direction.normalized * 100, ForceMode.Impulse);
				}

                if (Input.GetAxis(side + "Grip") == 1.0f)
                {
                    Print("Grip detected");
					Rigidbody rb = hit.rigidbody;
					if (rb != null && hit.distance <= 1) {
						vibrationManager.GetComponent<OVRVibration> ().VibrateController (side == "Right" ? VRNode.RightHand : VRNode.LeftHand, 1, 200);
					}
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

        void debugInputs()
        {

            if (Input.GetButtonDown(side + "Trigger"))
            {
                Print("Trigger detected");
            }
            if (Input.GetButtonDown(side + "Trackpad"))
            {
                Print("Trackpad detected");
            }
            if (Input.GetAxis(side + "Grip") == 1.0f)
            {
                Print("Axis detected");

            }
        }
    }
}