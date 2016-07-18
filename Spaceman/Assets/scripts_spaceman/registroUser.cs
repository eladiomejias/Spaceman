using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class registroUser : MonoBehaviour {

    private Button btn_regresar2, btn_registrar;
    private InputField userRegInp, passRegInp;
    private Text status;

    void Awake()
    {
        btn_regresar2 = GameObject.Find("btn_regresar2").GetComponent<Button>();
        btn_registrar = GameObject.Find("btn_registrar").GetComponent<Button>();
        btn_regresar2.onClick.AddListener(() => { regresarMenu(); });        
        btn_registrar.onClick.AddListener(() => { introdRegistro(); });
    }

    void regresarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    void introdRegistro()
    {
        string[] atributos = { "username", "password" };
        string[] valores = new string[2];

        status = GameObject.Find("status_reg").GetComponent<Text>();
        userRegInp = GameObject.Find("reg_user").GetComponent<InputField>();
        passRegInp = GameObject.Find("reg_pass").GetComponent<InputField>();
        valores[0] = userRegInp.text;
        valores[1] = passRegInp.text;
        if ((valores[0] == "" && valores[1] == "")) status.text = "Ingrese un nombre de usuario y clave secreta por favor";
        else if ((valores[0] == "")) status.text = "Ingrese un nombre de usuario por favor";
        else if ((valores[1] == "")) status.text = "Ingrese una clave secreta por favor";
        else
        {
            db_spaceman.insertar("login_user", atributos, valores);
            status.text = "Usuario ingresado exitosamente!!!";
        }
    }
}
