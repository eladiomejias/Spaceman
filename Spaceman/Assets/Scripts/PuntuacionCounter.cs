using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PuntuacionCounter : MonoBehaviour {

	private Text livesText;

	void Awake () {
		livesText = GetComponent<Text> ();

	}
	// Update is called once per frame
	void Update () {
		livesText.text = "PUNTUACION: " + (GameMaster.EnemysKilled * 10).ToString ();
	}
}
