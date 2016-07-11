using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
	
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
