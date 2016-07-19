using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour {
	
	/* Definiendo la clase Player Estadisticas */
	[System.Serializable]
	public class PlayerStats{
		/* Constructor */
		public int maxVida = 100;

		private int _vidaActual;

		/* metodo para getters and setter */
		public int vidaActual{
			/* get and set */
			get{ return _vidaActual; }
			set{ _vidaActual = Mathf.Clamp (value, 0, maxVida); }

		}

			
		public void Init(){
			vidaActual = maxVida;

		}

	}
		

	/* Creando instancia de la clase player */
	public PlayerStats stats = new PlayerStats();

	public int caida = -20;

	// definiendo el status indicator
	[SerializeField]
	private StatusIndicator statusIndicator;

	void Start(){

		// init las stats.
		stats.Init ();

		if (statusIndicator == null) {
			Debug.LogError ("No existe un status indicator en el jugador");
		} else {

			// Llevando los datos de daño
			statusIndicator.SetHealth (stats.vidaActual, stats.maxVida);


		}

	}


	/* Update que act las frames*/
	void Update(){
		if(transform.position.y <= caida){
			DamagePlayer(99999);
		}

       // if (GameMaster.isEnabled == true) { Player.(); }
	}

	/* Metodo del daño */
	public void DamagePlayer(int damage){
		stats.vidaActual -= damage;

		if(stats.vidaActual <= 0){
			GameMaster.KillPlayer(this);
		}

		// Act cada statusIndicator
		statusIndicator.SetHealth (stats.vidaActual, stats.maxVida);

	}



}
