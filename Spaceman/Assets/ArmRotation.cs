using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour {
	// Codigo para la rotacion del brazo
	// Use this for initialization


	public int rotacionValor = 90;

	// Update is called once per frame
	void Update () {
		/* Se declara el valor del puntero en el juego referente a la camara. */
		Vector3 diferencia = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		// Normal - Nos da la direccion correcta del angulo - basandonos en una suma de vectores igual a uno siendo proporcional.
		diferencia.Normalize();

		// Declaramos una variable y buscamos el angulo principal en base al eje X y el vector que va hacia el
		// braso, lo convertimos despues a valores de angulos.

		float rotacion3 = Mathf.Atan2(diferencia.y, diferencia.x) * Mathf.Rad2Deg;
		// Aplicando la transformacion al pj
		transform.rotation = Quaternion.Euler(0f, 0f, rotacion3 + rotacionValor);
	}
}



