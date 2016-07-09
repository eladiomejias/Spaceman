using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	/* Definiendo la clase Player Estadisticas */
	[System.Serializable]
	public class EnemyStats{
		/* Constructor */

		public int maximaVida = 100;
		private int _vidaActual;

		public int vidaActual{



			get{ return _vidaActual; }
			set{ _vidaActual = Mathf.Clamp (value, 0, maximaVida);}


		}

		public int damage = 40;
	

		public void Init(){

			vidaActual = maximaVida;

		}


	}




	/* Creando instancia de la clase player */
	public EnemyStats myenemy = new EnemyStats();
	public Transform deathParticles;
	public float shakeAmount = 0.1f;
	public float shakeLength = 0.1f;



	[Header ("Optional: ")]
	[SerializeField]
	private StatusIndicator statusIndicator;
	void Start(){

		myenemy.Init ();

		if(statusIndicator != null){

			statusIndicator.SetHealth (myenemy.vidaActual, myenemy.maximaVida);

		}

		if(deathParticles == null){
			Debug.LogError ("No death particles, have been found");
		}

	}



	/* Metodo del daño */
	public void DamageEnemy(int damage){
		myenemy.vidaActual -= damage;

		if(myenemy.vidaActual <= 0){
			//Debug.LogError ("Myplayer die");
			GameMaster.KillEnemy(this);
		}

		if(statusIndicator != null){

			statusIndicator.SetHealth (myenemy.vidaActual, myenemy.maximaVida);

		}
	}

	void OnCollisionEnter2D(Collision2D _colInfo){

		Player _player = _colInfo.collider.GetComponent<Player>();
		if(_player != null){ _player.DamagePlayer (myenemy.damage); DamageEnemy (999999);}

	}


}
