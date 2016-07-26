using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;
using MySql.Data.MySqlClient;

public class inicio_sesion : MonoBehaviour
{
    /*btn_jugar: Boton que permite iniciar sesion con el usuario introducido para 
     *           empezar a jugar.

      btn_punt: Boton que permite ir a la escena donde se guardan las puntuaciones
                top 5 del juego.
                
      btn_registro: Boton que permite registrar a un nuevo usuario para poder iniciar
                    sesion en el juego y asi diferenciarse de los demas usuarios y sus
                    puntuaciones.
                    
      msj_inicio: Una simple etiqueta de texto que muestra el status del inicio de sesion
                  de un usuario.
                  
      userInp y passwInp: Estos son los campos de textos tipo input que manejan el nombre
                          de usuario y clave secreta de los usuarios, respectivamente.
                          
      userSaved: Usuario actual que va a manejar el juego para guardar su informacion en
                 la base de datos.
                 
      comprobar: Variable booleana que se encarga de comprobar si el usuario existe o no
                 en la base de datos.*/
    private Button btn_jugar, btn_punt, btn_registro;
    private Text msj_inicio;
    private InputField userInp, passwInp;
    private static string userSaved;
    private bool comprobar;

    //Procedimiento inicial de referencia de los elementos UI de la escena MenuPrincipal
    void Awake()
    {
        /*Al inicio del programa, aun asi no estando habilitadas los componentes de la UI,
         se hace la referencia a ellos y se les adjunta un evento 'onClick' correspondiente
         a cada uno de ellos. Al resto no se les adjunta ningun evento.*/
        btn_jugar = GameObject.Find("btn_jugar").GetComponent<Button>();
        btn_punt = GameObject.Find("btn_punt").GetComponent<Button>();
        btn_registro = GameObject.Find("btn_registro").GetComponent<Button>();
        msj_inicio = GameObject.Find("msj_inicio").GetComponent<Text>();
        userInp = GameObject.Find("userInput").GetComponent<InputField>();
        passwInp = GameObject.Find("passwInput").GetComponent<InputField>();

        //Asignacion de eventos especiales a los botones correspondientes
        btn_jugar.onClick.AddListener(() => { verificarUser(); });
        btn_punt.onClick.AddListener(() => { cambiarAPuntuaciones(); });
        btn_registro.onClick.AddListener(() => { irAlRegistro(); });
    }

    //Procedimiento que se encarga de verificar si el usuario existe o no en la base de datos
    void verificarUser()
    {
        /*Inicializacion de una variable atributos que corresponde a las columnas de la tabla
         que maneja la informacion de nombre de usuario y clave secreta en la base de datos.
         Luego se guarda el usuario registrado, ya siendo este vacio o no, se hace la consulta
         en la base de datos y se comprueba si esta registrado o no alli. Dependiendo del resul-
         tado, este procedimiento imprimira un mensaje diferente.*/
        string[] atributos = { "username", "password" };
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

    //Procedimiento que llama a la escena de Puntuaciones para ser vista por el usuario
    void cambiarAPuntuaciones()
    {
        SceneManager.LoadScene("Puntuaciones");
    }

    //Procedimiento que llama a la escena RegistroUsuario para ser vista por el usuario
    void irAlRegistro()
    {
        SceneManager.LoadScene("RegistroUsuario");
    }

    //Metodo que retorna el nombre de usuario para ser manejado externamente
    public static string getUsername()
    {
        return userSaved;
    }
}
