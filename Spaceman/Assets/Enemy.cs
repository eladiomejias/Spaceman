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

		public void Init(){

			vidaActual = maximaVida;

		}


	}




	/* Creando instancia de la clase player */
	public EnemyStats myenemy = new EnemyStats();

	[Header ("Optional: ")]
	[SerializeField]
	private StatusIndicator statusIndicator;
	void Start(){

		myenemy.Init ();

		if(statusIndicator != null){

			statusIndicator.SetHealth (myenemy.vidaActual, myenemy.maximaVida);

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


}
