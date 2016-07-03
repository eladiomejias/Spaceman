using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	
	/* Definiendo la clase Player Estadisticas */
	[System.Serializable]
	public class PlayerStats{
		/* Constructor */
		public float Vida = 100f;

	}

	/* Creand instancia de la clase player */
	public PlayerStats myplayer = new PlayerStats();

	public int caida = -20;

	/* Update que act las frames*/
	void Update(){
		if(transform.position.y <= caida){
			DamagePlayer(99999);
		}
	}

	/* Metodo del daño */
	public void DamagePlayer(int damage){
		myplayer.Vida -= damage;

		if(myplayer.Vida <= 0){
			//Debug.LogError ("Myplayer die");
			GameMaster.KillPlayer(this);
		}
	}


}
