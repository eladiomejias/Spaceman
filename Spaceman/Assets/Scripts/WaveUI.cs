using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour {

	[SerializeField]
	WaveSpawner spawner;

	[SerializeField]
	Animator WaveAnimator;

	[SerializeField]
	Text waveCountdownText;

	[SerializeField]
	Text waveCountText;

	private WaveSpawner.SpawnState previousState;

	// Use this for initialization
	void Start () {

		if(spawner == null){
			Debug.LogError ("No spawner in reference");
			this.enabled = false;
		}

		if(WaveAnimator == null){
			Debug.LogError ("No WaveAnimator in reference");
			this.enabled = false;
		}

		if(waveCountdownText == null){
			Debug.LogError ("No waveCountdownText in reference");
			this.enabled = false;
		}

		if(waveCountText == null){
			Debug.LogError ("No waveCountText in reference");
			this.enabled = false;
		}

	}

	// Update is called once per frame
	void Update () {
		switch (spawner.State) 
		{
		case WaveSpawner.SpawnState.COUNTING:
			UpdateCountingUI ();
			break;
		
		case WaveSpawner.SpawnState.SPAWNING: 
			UpdateSpawningUI ();
			break;
		
		}

		previousState = spawner.State;

	}

	void UpdateCountingUI () {
		if(previousState != WaveSpawner.SpawnState.COUNTING){
			
			WaveAnimator.SetBool ("WaveIncoming", false);
			WaveAnimator.SetBool ("WaveCountdown", true);

			//Debug.Log ("Counting");

		}

		waveCountdownText.text = ((int)spawner.WaveCountdown).ToString();

	}

	void UpdateSpawningUI () {
		if(previousState != WaveSpawner.SpawnState.SPAWNING){
			
			WaveAnimator.SetBool ("WaveCountdown", false);
			WaveAnimator.SetBool ("WaveIncoming", true);

			waveCountText.text = spawner.NextWave.ToString();
			//Debug.Log ("Spawning");

		}
	}
}
