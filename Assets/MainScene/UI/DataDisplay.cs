using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataDisplay : MonoBehaviour {
	public GameObject character;
	[Multiline]
	public string textTemplate;

	private Text textComponent;

	// Use this for initialization
	void Start () {
		textComponent = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (textComponent == null)
		{
			Debug.Log ("Erreur : texte non trouvé");
		}
		else
		{
			string text = textTemplate;

			Rigidbody rb = character.GetComponent<Rigidbody> ();

			Vector3 position = rb.position;
			text = text.Replace("{x}", ftos(position.x));
			text = text.Replace("{y}", ftos(position.y));
			text = text.Replace("{z}", ftos(position.z));

			// Speed m/s to km/h
			float velocity = rb.velocity.magnitude / 3.6f;
			text = text.Replace("{vitesse}", velocity.ToString("F2"));

			textComponent.text = text;
		}
	}

	private string ftos(float f) {
		return f.ToString ("F1");
	}
}
