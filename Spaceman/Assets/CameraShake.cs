using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public Camera mainCam;
	float shakeAmount = 0;

	// Use this for initialization
	void Awake () {

		if(mainCam == null){
			mainCam = Camera.main;
		}
	
	}



	// Metodo principal donde definiremos los pasos del shake camera.
	public void Shake(float amt, float length){
		shakeAmount = amt;
		InvokeRepeating ("DoShake", 0, 0.01f);
		Invoke ("StopShake", length);

	}
	
	void DoShake () {
		if(shakeAmount > 0){
			Vector3 cameraPos = mainCam.transform.position;

			// formula pre def
			float shakeAmtX = Random.value * shakeAmount * 2 - shakeAmount;
			float shakeAmtY = Random.value * shakeAmount * 2 - shakeAmount;

			// definiendo los valores pre def
			cameraPos.x += shakeAmtX;
			cameraPos.y += shakeAmtY;

			// la camara tendra esta nueva posicion
			mainCam.transform.position = cameraPos;

		}
	}


	void StopShake () {
		CancelInvoke ("DoShake");
		mainCam.transform.localPosition = Vector3.zero;

	}
}
