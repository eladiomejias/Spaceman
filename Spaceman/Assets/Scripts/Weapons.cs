﻿using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {

	public float valorDisparo = 0 ;
	public int damage = 0;
	public LayerMask debemosDisparar;

	public static int disparosAcertados = 0;
	public static int disparos = 0;

	public Transform BulletTrailPrefad;
	public Transform hitPrefab;

	float tiempoDenuevoDisparo = 0;
	public float effectSpawnEffect = 10;

	float tiempoDeDisparo = 0;
	Transform firePoint;


	public Transform flash;

	// Manejando el shake de la camera
	public float camShakeAmount = 0.1f;
	public float camShakeLength = 0.2f;

	CameraShake camShake;

	void Start(){

		camShake = GameMaster.gm.GetComponent<CameraShake> ();

		if(camShake == null){
			Debug.LogError ("No camera shake, found");
		}
	}


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
				tiempoDeDisparo = Time.time + 1 / valorDisparo;
				// Se llama al metodo de disparo.
				Disparar ();
			}

		}


	}

	// Metodo de disparo
	void Disparar(){

		//Debug.Log ("pew");
		disparos = disparos + 1;
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
			//Debug.Log ("Le disparamos a "+disparo.collider+" he hicimos: "+damage+" de daño");
			Enemy enemy = disparo.collider.GetComponent<Enemy>();
			if (enemy != null) {
				disparosAcertados = disparosAcertados + 1;
				enemy.DamageEnemy (damage);
				Debug.Log ("Le disparamos a "+disparo.collider+" he hicimos: "+damage+" de daño");

			}
		}

		/* Llamada al efecto del disparo */
		if(Time.time >= tiempoDenuevoDisparo){
			Vector3 disparoPos;
			Vector3 disparoNormalAngle;

			if (disparo.collider == null) {
				disparoPos = (mousePos - firePointPos) * 30;
				disparoNormalAngle = new Vector3 (9999,9999,9999);
			} else {
				disparoPos = disparo.point;
				disparoNormalAngle = disparo.normal;
			}

			Effect (disparoPos, disparoNormalAngle); //here disparo.normal
			tiempoDenuevoDisparo = Time.time + 1 / effectSpawnEffect;
		}



	}


	/*Efecto de disparo del arma*/

	void Effect(Vector3 disparoPos, Vector3 disparoNormalAngle){
		/* Se instancia el objeto del juego, la posicion y la rotacion del angulo */
		Transform trail = Instantiate (BulletTrailPrefad, firePoint.position, firePoint.rotation) as Transform;
		LineRenderer lr = trail.GetComponent<LineRenderer> ();

		if(lr != null){
			// Posiciones de line renderer para trail. Donde hace hit.
			lr.SetPosition(0, firePoint.position);
			lr.SetPosition(1, disparoPos);

		}
		// Destruccion del efecto trail
		Destroy (trail.gameObject, 0.05f);

		// Efecto de choque con bala
		if(disparoNormalAngle != new Vector3(9999,9999,9999)){
			Transform hitParticle = Instantiate (hitPrefab, disparoPos, Quaternion.FromToRotation (Vector3.right, disparoNormalAngle)) as Transform; 
			Destroy (hitParticle.gameObject, 1f);

		}

		/* Segunda instancia para el flash*/
		Transform clonado = Instantiate (flash, firePoint.position, firePoint.rotation) as Transform;
		clonado.parent = firePoint;

		/* tamaño predeterminado del flash que sale del disparo */
		float tamaño = Random.Range (0.4f, 0.7f);
		/*se crea un puntero local */
		clonado.localScale = new Vector3 (tamaño, tamaño, tamaño);
		/* se destruye cada despues del disparo 0.05s*/
		Destroy (clonado.gameObject, 0.05f);

		// Shake the camera
		camShake.Shake(camShakeAmount, camShakeLength);
	}
}