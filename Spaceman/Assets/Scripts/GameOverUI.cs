using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {

	public void Start(){
		float porc =  (Weapons.disparosAcertados * 100 / Weapons.disparos);

		Weapons.disparosAcertados = 0;
		Weapons.disparos = 0;
		WaveSpawner.rondas = 0;

		Debug.Log ("El porcentaje es: "+porc+" %");
	}

	// Manejando la vista de GameOver
	public void Quit () {
		
		Debug.Log ("App quit");
		//Application.Quit();
		SceneManager.LoadScene("MenuPrincipal");
	}


	public void Retry () {
		Time.timeScale = 1;
		Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
