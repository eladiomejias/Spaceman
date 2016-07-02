using UnityEngine;
using System.Collections;

public class Parallaxing : MonoBehaviour {
	// Declarando array

	public Transform[] backgrounds;			 // Lista de todos los fondos para actuar en el parallax.
	private float[] parallaxScales;			// La proporcion del movimiento de la camara respecto al bg.
	public float smoothing = 1f;			// Lo suave del parallax, de su movimiento.

	private Transform cam;					// Referencia de la camara principal;
	private Vector3 previousCamPos;			// Guardara la posicion de la camara anterior

	// Es llamada después de Start() ' Se usa para referencias.
	void Awake () {
		// Referenciando la camara.
		cam = Camera.main.transform;
	}

	// Use this for initialization
	void Start () {
		// Tomando el valor de la camara al iniciar.
		previousCamPos = cam.position;

		// Asignando las correspondientes escalas del parallax.
		parallaxScales = new float[backgrounds.Length];
		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales[i] = backgrounds[i].position.z*-1;
		}
	}

	// Update is called once per frame
	void Update () {

		// for each background
		for (int i = 0; i < backgrounds.Length; i++) {
			// Colocando un target x de la posicion el cual se referencia la posicion
			// actual dependiendo del parallax.
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales[i];


			// Creando una posicion del objeto x el cual es la posicion del fondo 
			// respecto al parallax del objeto.

			float backgroundTargetPosX = backgrounds[i].position.x + parallax;

			// create a target position which is the background's current position with it's target x position
			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);

			// fade between current position and the target position using lerp
			backgrounds[i].position = Vector3.Lerp (backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		// Asignando la camara anterior al final del frame.
		previousCamPos = cam.position;
	}
}
