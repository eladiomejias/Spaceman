using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RondasCounter : MonoBehaviour {


	private Text livesText;

	void Awake () {
		livesText = GetComponent<Text> ();

	}
	// Update is called once per frame
	void Update () {
		livesText.text = "RONDAS COMPLETAS: " + WaveSpawner.rondas.ToString ();
	}
}
