using UnityEngine;
using System.Collections;
using System;

public class quicksort : MonoBehaviour
{

    /*Metodo que lleva a cabo el algoritmo de ordenamiento Quicksort que toma como
     parametros la matriz que contiene la informacion correspondiente a las mayores
     puntuaciones y, la posicion inicial y final del arreglo empezando desde 0.
     
     La unica modificacion a este algoritmo es: al intercambiar los valores de las
     posiciones en la matriz, tambien cambia los id del puntaje del usuario correspondiente
     para poder corresponder correctamente el id de la puntuacion del usuario a su valor
     correcto.*/
    public static void ordenarQuick(ref int[,] arreglo, int primero, int ultimo)
    {
        int i = primero, j = ultimo;
        int pivote = arreglo[(primero + ultimo) / 2, 1];
        int auxiliar_score, auxiliar_user;
        do
        {
            while (arreglo[i, 1] < pivote) i++;
            while (arreglo[j, 1] > pivote) j--;

            if (i <= j)
            {
                auxiliar_user = arreglo[j, 0];
                arreglo[j, 0] = arreglo[i, 0];
                arreglo[i, 0] = auxiliar_user;
                auxiliar_score = arreglo[j, 1];
                arreglo[j, 1] = arreglo[i, 1];
                arreglo[i, 1] = auxiliar_score;
                i++;
                j--;
            }
        } while (i <= j);

        if (primero < j) ordenarQuick(ref arreglo, primero, j);
        if (ultimo > i) ordenarQuick(ref arreglo, i, ultimo);
    }
}
