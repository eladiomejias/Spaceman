using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class registroUser : MonoBehaviour
{

    /*btn_regresar2: Boton para regresar al menu principal del juego.
     
      btn_registrar: Boton para procesar el registro del usuario.
      
      userRegInp: Campo de texto que va a guardar el nombre de usuario a
                  registrar.
      
      passRegInp: Campo de texto que va a guardar la contraseña del usuario
                  a registrar.
                  
      status: Etiqueta de texto que va a mostrar el estado del registro de un usuario*/
    private Button btn_regresar2, btn_registrar;
    private InputField userRegInp, passRegInp;
    private Text status;
    private static int flag=0;

    //Referencia a los diferentes componentes de la GUI con sus eventos asociados respectivamente
    void Awake()
    {
        btn_regresar2 = GameObject.Find("btn_regresar2").GetComponent<Button>();
        btn_registrar = GameObject.Find("btn_registrar").GetComponent<Button>();
        userRegInp = GameObject.Find("reg_user").GetComponent<InputField>();
        status = GameObject.Find("status_reg").GetComponent<Text>();
        passRegInp = GameObject.Find("reg_pass").GetComponent<InputField>();
        btn_regresar2.onClick.AddListener(() => { regresarMenu(); });
        btn_registrar.onClick.AddListener(() => { introdRegistro(); });
    }

    //Metodo que carga la escena del Menu Principal del juego
    void regresarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    void OnGUI()
    {

        Event e = Event.current;
        if (e.keyCode == KeyCode.Return && userRegInp.isFocused)
        {
            passRegInp.ActivateInputField();
            passRegInp.Select();
        }
        else if (e.keyCode == KeyCode.Return && passRegInp.isFocused)
        {
            if (flag == 0) flag = 1;
            else {
                introdRegistro();
            }
        }
    }

    //Metodo que realiza el registro de un nuevo usuario en el juego
    void introdRegistro()
    {
        /*Variables usadas para la consulta SQL y la comparacion de lo que el usuario ha
         introducido en los campos de texto para el registor de un usuario.*/
        string[] atributos = { "username", "password" };
        string[] valores = new string[2];

        /*Guarda en el arreglo valores los valores literales de los campos de textos y procede
         a comparar si ambos o alguno de ellos esta vacio para validar que la consulta SQL sea
         la correcta. Si nada de lo anterior ocurre, procede a realizar la insercion en la base
         de datos, notifica al usuario que todo salio bien y limpia los campos por si se desea
         registrar otro usuario.*/
        valores[0] = userRegInp.text;
        valores[1] = passRegInp.text;
        if ((valores[0] == "" && valores[1] == "")) status.text = "Ingrese un nombre de usuario y clave secreta por favor";
        else if ((valores[0] == "")) status.text = "Ingrese un nombre de usuario por favor";
        else if ((valores[1] == "")) status.text = "Ingrese una clave secreta por favor";
        else
        {
            db_spaceman.insertar("login_user", atributos, valores);
            status.text = "Usuario ingresado exitosamente!!!";
            userRegInp.text = "";
            passRegInp.text = "";
        }
    }
}