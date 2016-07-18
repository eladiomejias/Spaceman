using UnityEngine;
using System.Collections;
using System;

public class quicksort : MonoBehaviour {

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
