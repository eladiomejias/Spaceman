using UnityEngine;
using System.Collections;
using System.Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

//Clase que maneja la escena de puntuaciones TOP 5 del juego
public class puntuaciones : MonoBehaviour
{

    /*btn_regresar: Boton que permite al usuario regresar al Menu Principal del juego.
      
      puntAltos: Matriz que guarda los id de los puntajes de los usuarios y sus valores
                 correspondientes.
                 
      tabla: Tabla de etiquetas de textos que van a representar la tabla de las puntuaciones
             TOP 5 con sus caracteristicas correspondientes.*/
    private Button btn_regresar;
    public int[,] puntAltos = new int[5, 2];
    public Text[,] tabla = new Text[5, 5];

    /*Procedimiento que refencia a los componentes de la GUI como el boton y la tabla
     para luego mostrar las puntaciones.*/
    void Awake()
    {
        int i, j;
        btn_regresar = GameObject.Find("btn_regresar").GetComponent<Button>();
        btn_regresar.onClick.AddListener(() => { regresarMenu(); });
        for (i = 0; i < 5; i++)
        {
            for (j = 0; j < 5; j++)
            {
                tabla[i, j] = GameObject.Find("TablaP[" + i + "," + j + "]").GetComponent<Text>();
            }
        }
        mostrarPuntuaciones();
    }

    //Procedimiento para regresar al menu principal del juego.
    void regresarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    //Procedimiento para mostrar las puntuaciones en la tabla de etiquetas de texto.
    void mostrarPuntuaciones()
    {
        /*Declaracion de variables para los ciclos y del arreglo que guarda los atributos
         que va a usar la instruccion SQL.*/
        int i, j;
        string[] atributo = { "id_scores", "score" };

        /*Llamada en orden a los siguientes metodos: Primero llamada al metodo de consulta de la
         base de datos segun la instruccion SQL establecida en el metodo, luego otro procedimiento
         que llena la matriz de id de puntajes de usuario y los valores de esos puntajes para
         buscar y guardar alli los top 5 puntajes del total. Al final ordena de mayor a menor por
         el algoritmo de ordenamiento de quicksort.*/
        db_spaceman.seleccionar("scores", atributo);
        db_spaceman.mayoresPuntuaciones(ref puntAltos);
        quicksort.ordenarQuick(ref puntAltos, 0, 4);

        //Llenado de la tabla de puntuaciones  por el metodo llenarTablaPunt
        j = 4;
        for (i = 0; i < 5; i++)
        {
            db_spaceman.llenarTablaPunt(puntAltos[j, 0], ref tabla, i);
            --j;
        }
    }
}