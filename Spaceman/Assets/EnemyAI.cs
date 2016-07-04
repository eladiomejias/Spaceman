using UnityEngine;
using System.Collections;
using Pathfinding;

/* Requerimientos para rigidbody y Seeker */
[RequireComponent (typeof (Seeker))]
[RequireComponent (typeof (Rigidbody2D))]
public class EnemyAI : MonoBehaviour {

	public Transform target;

	// Las veces que act el path cada seg
	public float updateRate = 2f;

	// Elementos del UI 
	private Seeker seeker;
	private Rigidbody2D rb;

	// La ruta calculada
	public Path path;

	// AI Speed - La velocidad del AI por segundo.
	public float speed = 300f;
	public ForceMode2D forceMode;

	[HideInInspector]
	public bool rutaTerminada = false;

	// La distancia desde el AI hasta la coordenada para continuar a otra coordenada.
	public float proxRutaCoord = 3;

	// Actual coordenada al punto a la que nos moveremos
	private int actualCoord = 0;


	void Start(){

		seeker = GetComponent<Seeker> ();
		rb = GetComponent<Rigidbody2D> ();

		if(target == null){

			Debug.LogError ("Target not found");
			return;
		}

		// Start a new path to the target position, return the result to the OnPathComplete method
		seeker.StartPath (transform.position, target.position, OnPathComplete);
		StartCoroutine (UpdatePath());
	}



	IEnumerator UpdatePath(){

		if(target == null){
			//Todo: Insert a player search here.
			return false;
		}

		// Crea una nueva path desde la posicion del objetivo, y retorna una path
		// completa.
		seeker.StartPath (transform.position, target.position, OnPathComplete);

		yield return new WaitForSeconds (1f/updateRate);
		StartCoroutine (UpdatePath());

	}




	void OnPathComplete(Path p){

		//Debug.LogError ("Error? "+ p.error);

		if(!p.error){
			path = p;
			actualCoord = 0;
		}

	}

	void FixedUpdate(){

		if(target == null){
			//Todo: Insert a player search here.
			return;
		}

		//TODO: Siempre mirar al jugador
		if (path == null)
			return;

		if (actualCoord >= path.vectorPath.Count) {
			if (rutaTerminada)
				return;

			Debug.Log ("Final de la ruta encontrada");

			rutaTerminada = true;
			return;
		}

		rutaTerminada = false;

		// Direccion de la ruta.
		Vector3 dir = (path.vectorPath[actualCoord] - transform.position).normalized;
		dir *= speed * Time.fixedDeltaTime;

		// Movimiento del AI
		rb.AddForce (dir,forceMode);

		float distancia = Vector3.Distance (transform.position, path.vectorPath [actualCoord]);
		if(distancia < proxRutaCoord){

			actualCoord++;
			return;


		}
		
	}

	
}
