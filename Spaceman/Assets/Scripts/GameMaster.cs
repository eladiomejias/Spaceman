using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameMaster : MonoBehaviour {
	/* Iniciar todas las acciones del juego*/
	public static GameMaster gm;

    public static bool isEnabled;


    // El contador de vidas con metodo get para usarlo en LiveCounterUI
    public static int _remainingLives = 3;
	public static int _enemyCounters = 0; // Era private
    public static float porc;

	public static int RemainingLives{
		get{ return _remainingLives; }
	}
	public static int EnemysKilled{
		get{ return _enemyCounters; }
	}


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

	[SerializeField]
	public GameObject GameOverUI;


	void Start(){
		if(cameraShake == null){Debug.LogError ("No camera shake have referenced");}
	}

	public void EndGame(){
        
        string[] valores = new string[5];
        string[] atributos = { "login_user_username", "score", "perc_enemyKilled", "punteria", "rondas" };

        if (Weapons.disparos == 0)
        {
            porc = 0;
        }
        else { porc = ((float)(Weapons.disparosAcertados) * 100 / Weapons.disparos); }
        valores[0] = inicio_sesion.getUsername();
        valores[1] = (EnemysKilled * 10).ToString();
        valores[2] = (EnemysKilled).ToString();
        valores[3] = porc.ToString();
        valores[4] = (WaveSpawner.rondas).ToString();
        db_spaceman.insertar("scores", atributos, valores);
        Debug.LogError("GAME OVER");

        Weapons.disparosAcertados = 0;
        Weapons.disparos = 0;
        WaveSpawner.rondas = 0;

        //Activa la ventana de GAMEOVER
        GameOverUI.SetActive(true);
        isEnabled = true;
        _remainingLives = 3;
		_enemyCounters = 0;
		// Test for invoke 
		//gm.Invoke("StopGame", 1);
	
	}

	public void StopGame(){
		Time.timeScale = 0;
	}

	public IEnumerator RespawnPlayer () {
		yield return new WaitForSeconds (spawnDelay);

		Instantiate (playerPrefab, spawnPoint.position, spawnPoint.rotation);
		Instantiate (spawnPrefab, spawnPoint.position, spawnPoint.rotation);
		//Destroy (clonadoSpanPart, 1f);
	}

	public static void KillPlayer (Player player) {
		Destroy (player.gameObject);
		// decrement the lives
		_remainingLives -= 1;
		if(_remainingLives <= 0){
            /// si se le acabo la vida.
            if (isEnabled == false) { gm.EndGame(); } 
		}else{
			
		}

		gm.StartCoroutine (gm.RespawnPlayer());
	}

	public static void KillEnemy (Enemy enemy) {
		gm._KillEnemy(enemy);
		_enemyCounters += 1;
	}

	// local no static.
	public void _KillEnemy(Enemy _enemy){
		
		GameObject _clone = Instantiate (_enemy.deathParticles, _enemy.transform.position, Quaternion.identity) as GameObject;
		Destroy (_clone, 5f);
		cameraShake.Shake(_enemy.shakeAmount, _enemy.shakeLength);
		Destroy (_enemy.gameObject);

	}


}