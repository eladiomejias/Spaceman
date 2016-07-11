using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;

public class inicio_sesion : MonoBehaviour
{

    public Button btn_jugar;
    public Text msj_inicio;
    public InputField userInp, passwInp;
    public static string userSaved;
    public db_spaceman bdd;
    public bool comprobar;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        btn_jugar = GetComponent<Button>();
        btn_jugar.onClick.AddListener(() => { cambiarEstado(); });
    }

    void cambiarEstado()
    {
        bdd = new db_spaceman();
        msj_inicio = GameObject.Find("msj_inicio").GetComponent<Text>();
        userInp = GameObject.Find("userInput").GetComponent<InputField>();
        passwInp = GameObject.Find("passwInput").GetComponent<InputField>();
        bdd.conectarMysql();
        userSaved = userInp.text;
        comprobar = bdd.comprobarUser(userSaved, passwInp.text);
        if (comprobar == true)
        {
            msj_inicio.text = "Usuario conectado!";
        }
        else
        {
            msj_inicio.text = "Usuario incorrecto...";
        }
        SceneManager.LoadScene("First");
    }

    public static string getUsername()
    {
        return userSaved;
    }

}
