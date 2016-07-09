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
	public CameraShake cameraShake;

	public int spawnDelay = 1;

	void Start(){
		if(cameraShake == null){Debug.LogError ("No camera shake have referenced");}
	}

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
		gm._KillEnemy(enemy);

	}

	// local no static.
	public void _KillEnemy(Enemy _enemy){
		
		GameObject _clone = Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
		Destroy (_clone, 5f);
		cameraShake.Shake(_enemy.shakeAmount, _enemy.shakeLength);
		Destroy (_enemy.gameObject);

	}


}