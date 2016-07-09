using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour {
	/* Iniciar todas las acciones del juego*/
	public static GameMaster gm;

	void Awake () {
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
		Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation);
		//Destroy (clonadoSpanPart, 1f);
	}

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		gm.StartCoroutine (gm.RespawnPlayer());
	}

	public static void KillEnemy (Enemy enemy) {
		Destroy (enemy.gameObject);
	//	gm.StartCoroutine (gm.RespawnPlayer());
	}

}