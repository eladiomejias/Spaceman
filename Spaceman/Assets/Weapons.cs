using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

	public float valorDisparo = 0 ;
	public float damage = 0;
	public LayerMask debemosDisparar;


	float tiempoDeDisparo = 0;
	Transform firePoint;


	// Use this for initialization
	void Awake () {

		// Se busca el elemento del mismo nombre, en este caso firepoint que es
		// el punto en la pistola.
		firePoint = transform.FindChild ("FirePoint");

		if (firePoint == null) {
			Debug.LogError ("Sin firepoint");
		}

	}

	// Update is called once per frame
	void Update () {

		// Codigo para saber si esta disparando una sola vez o disparos consecutivos.


		// Si pulso el boton una sola vez
		if (valorDisparo == 0) {
			if (Input.GetButtonDown ("Fire1")) {
				//Se llama el metodo de disparo. 
				Disparar ();
			}

		} else {
			if (Input.GetButton ("Fire1") && Time.time > tiempoDeDisparo) {
				// Disparo consecutivo.
				// Aqui toma el valor del tiempo de ejecucion en el disparo consec
				tiempoDeDisparo = Time.time + 1 / tiempoDeDisparo;
				// Se llama al metodo de disparo.
				Disparar ();
			}

		}


	}

	// Metodo de disparo
	void Disparar(){

		Debug.Log ("pew");
		// Tomando el valor del mouse desde la camara principal.
		Vector2 mousePos = new Vector2 (Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);

		// Tomando el valor del sitio de disparo respecto a la pos del puntero.
		Vector2 firePointPos;
		firePointPos = new Vector2 (firePoint.position.x, firePoint.position.y);

		// Creando la animacion del raycast para el disparo.
		RaycastHit2D disparo = Physics2D.Raycast(firePointPos, mousePos-firePointPos, 100, debemosDisparar);
		Debug.DrawLine (firePointPos, (mousePos-firePointPos)*100,Color.white);

		// Cuando toque algo
		if (disparo.collider != null) {
			Debug.DrawLine (firePointPos, disparo.point, Color.red);
			Debug.Log ("Le disparamos a "+disparo.collider+" he hicimos: "+damage+" de daño");

		}

	}
}