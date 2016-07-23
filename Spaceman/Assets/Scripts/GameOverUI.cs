using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {


	public void Start(){
		Weapons.disparosAcertados = 0;
		Weapons.disparos = 0;
		WaveSpawner.rondas = 0;
	}

	// Manejando la vista de GameOver
	public void Quit () {
		/* Act var */
		Weapons.disparosAcertados = 0;
		Weapons.disparos = 0;
		WaveSpawner.rondas = 0;
		// Esto se act
		GameMaster._remainingLives = 3;
		GameMaster._enemyCounters = 0;


        GameMaster.isEnabled = false;
		Debug.Log ("App quit");
		//Application.Quit();
		SceneManager.LoadScene("MenuPrincipal");
	}


	public void Retry () {
        GameMaster.isEnabled = false;
        //Time.timeScale = 1;
        Weapons.disparosAcertados = 0;
		Weapons.disparos = 0;
		WaveSpawner.rondas = 0;
		// Esto se act
		GameMaster._remainingLives = 3;
		GameMaster._enemyCounters = 0;

		Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
