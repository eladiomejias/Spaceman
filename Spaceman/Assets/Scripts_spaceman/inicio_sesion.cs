using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class inicio_sesion : MonoBehaviour {

    public Button btn_jugar;
    public Text p_conexion, p_conexion2;
    public InputField inp1, inp2;
    public db_spaceman bdd;
    public bool comprobar;

    
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    void Awake()
    {
        btn_jugar = GetComponent<Button>();
        btn_jugar.onClick.AddListener(() => { cambiarTexto(); });
    }

    void cambiarTexto()
    {
        bdd = new db_spaceman();
        p_conexion = GameObject.Find("pruebaconex").GetComponent<Text>();
        p_conexion2 = GameObject.Find("pruebaconex2").GetComponent<Text>();
        inp1 = GameObject.Find("userInput").GetComponent<InputField>();
        inp2 = GameObject.Find("passwInput").GetComponent<InputField>();
        bdd.conectar();
        p_conexion.text = bdd.GetEstadoConexion();
        comprobar = bdd.consultar(inp1.text, inp2.text);
        if (comprobar == true)
        {
            p_conexion2.text = "Usuario conectado!";
        } else {
            p_conexion2.text = "Usuario incorrecto...";
        }
    }
    
}
