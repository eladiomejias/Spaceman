using UnityEngine;
using System.Collections;
using System.Data;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class puntuaciones : MonoBehaviour {

    private Button btn_regresar;
    public int[,] puntAltos = new int[5, 2];
    public Text[,] tabla = new Text[5, 5];

    void Awake () {
        btn_regresar = GameObject.Find("btn_regresar").GetComponent<Button>();
        btn_regresar.onClick.AddListener(() => { regresarMenu(); });
        mostrarPuntuaciones();
    }

    void regresarMenu()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    void mostrarPuntuaciones()
    {
        int i, j;
        string[] atributo = { "id_scores", "score" };

        for (i=0; i < 5; i++)
        {
            for (j = 0; j < 5; j++)
            {
                tabla[i, j] = GameObject.Find("TablaP[" + i + "," + j + "]").GetComponent<Text>();
            }
        }
        db_spaceman.seleccionar("scores", atributo);
        db_spaceman.mostrarPuntuaciones(ref puntAltos);
        quicksort.ordenarQuick(ref puntAltos, 0, 4);

        j = 4;
        for (i=0; i < 5; i++)
        {
            db_spaceman.llenarTablaPunt(puntAltos[j, 0], ref tabla, i);
            --j;
        }
    }
}
