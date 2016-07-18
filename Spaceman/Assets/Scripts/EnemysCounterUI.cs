using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class EnemysCounterUI : MonoBehaviour {

	private Text enemysCounter;

	void Awake () {
		enemysCounter = GetComponent<Text> ();

	}

	// Update is called once per frame
	void Update () {
		enemysCounter.text = "ENEMIGOS ELIMINADOS: " + GameMaster.EnemysKilled.ToString ();

	}

}
