using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	/* Iniciar todas las acciones del juego*/
	public static GameMaster gm;

	void Start () {
		if (gm == null) {
			gm = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster>();
		}
	}

	public Transform playerPrefab;
	public Transform spawnPoint;

	public Transform spawnPrefab;

	public int spawnDelay = 1;

	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		GameObject clonadoSpanPart = Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation) as GameObject;
		//Destroy (clonadoSpanPart, 1f);
	}

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		gm.StartCoroutine (gm.RespawnPlayer());
	}

}