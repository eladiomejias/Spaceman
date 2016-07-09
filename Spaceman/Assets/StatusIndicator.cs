using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StatusIndicator : MonoBehaviour {

	[SerializeField]
	private RectTransform statusVidaRect;
	[SerializeField]
	private Text statusVidaText;

	void Start(){
		// Verificacion si existen elementos llamados en la scene.
		if(statusVidaRect == null){
			Debug.LogError ("STATUS: No se encontro ningun elemento de status vida.");
		}

		if(statusVidaText == null){
			Debug.LogError ("STATUS: No se encontro ningun elemento de status vida texto.");
		}

	}

	// Accesible solo en el metodo.
	public void SetHealth(int _cur, int _max){

		float _value = (float)_cur / _max;

		statusVidaRect.localScale = new Vector3 (_value, statusVidaRect.localScale.y, statusVidaRect.localScale.z);
		statusVidaText.text = _cur + "/" + _max + "HP";
	}
}
