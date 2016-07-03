using UnityEngine;
using System.Collections;

public class MovBala : MonoBehaviour {


	public float velocidadMov = 230;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		/* Velocidad de movimiento */

		/* Direccion */

		/* Tiempo de ejecucion*/

		/*Velocidad de translate*/

		transform.Translate(Vector3.right * Time.deltaTime * velocidadMov);
		Destroy (this.gameObject, 1);
	
	}
}
