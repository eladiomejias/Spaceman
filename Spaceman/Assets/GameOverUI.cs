using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour {
	
	// Manejando la vista de GameOver
	public void Quit () {
		Debug.Log ("App quit");
		Application.Quit();
	}


	public void Retry () {
		Debug.Log ("App quit");
		Application.LoadLevel(Application.loadedLevel);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
