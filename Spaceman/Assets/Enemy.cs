using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	/* Definiendo la clase Player Estadisticas */
	[System.Serializable]
	public class EnemyStats{
		/* Constructor */
		public float Vida = 100f;

	}

	/* Creand instancia de la clase player */
	public EnemyStats myenemy = new EnemyStats();


	/* Metodo del daño */
	public void DamageEnemy(int damage){
		myenemy.Vida -= damage;

		if(myenemy.Vida <= 0){
			//Debug.LogError ("Myplayer die");
			GameMaster.KillEnemy(this);
		}
	}


}
