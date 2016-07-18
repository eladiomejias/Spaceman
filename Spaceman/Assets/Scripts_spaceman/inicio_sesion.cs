using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;
using MySql.Data.MySqlClient;

public class inicio_sesion : MonoBehaviour
{

    private Button btn_jugar, btn_punt, btn_registro;
    private Text msj_inicio;
    private InputField userInp, passwInp;
    private static string userSaved;
    private bool comprobar;
    //Tabla para mostrar las puntuaciones mediante UI Text

    void Awake()
    {
        btn_jugar = GameObject.Find("btn_jugar").GetComponent<Button>();
        btn_punt = GameObject.Find("btn_punt").GetComponent<Button>();
        btn_registro = GameObject.Find("btn_registro").GetComponent<Button>();
        btn_jugar.onClick.AddListener(() => { verificarUser(); });
        btn_punt.onClick.AddListener(() => { cambiarAPuntuaciones(); });
        btn_registro.onClick.AddListener(() => { irAlRegistro(); });
    }

    void verificarUser()
    {
        string[] atributos = { "username", "password" };

        msj_inicio = GameObject.Find("msj_inicio").GetComponent<Text>();
        userInp = GameObject.Find("userInput").GetComponent<InputField>();
        passwInp = GameObject.Find("passwInput").GetComponent<InputField>();
        userSaved = userInp.text;
        db_spaceman.seleccionar("login_user", atributos);
        comprobar = db_spaceman.comprobarUser(userSaved, passwInp.text);
        if (comprobar == true)
        {
            msj_inicio.text = "Usuario conectado!";
            SceneManager.LoadScene("First");
        }
        else
        {
            msj_inicio.text = "Usuario incorrecto...";
        }
    }

    void cambiarAPuntuaciones()
    {
        SceneManager.LoadScene("Puntuaciones");
    }

    void irAlRegistro()
    {
        SceneManager.LoadScene("RegistroUsuario");
    }

    public static string getUsername()
    {
        return userSaved;
    }
}
