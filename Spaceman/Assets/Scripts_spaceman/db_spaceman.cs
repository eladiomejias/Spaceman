using UnityEngine;
using UnityEngine.UI;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;


public class db_spaceman : MonoBehaviour
{
    /*Definicion de variables para la conexion de base de datos:
        -espcifConex: Contiene las especificaciones del sistema correspondiente que hace 
        la conexion a base de datos, en este caso ese sistema es WAMP.
        
        -instrMysql: Guarda la instruccion en codigo SQL que varia segun la funcion llamada.
        
        -user_encontrado: Guarda el usuario que fue encontrado en la tabla login_user en
        la lectura actual que se este realizando a la base de datos.
        
        -pass_encontrado: Guarda la clave secreta que fue encontrada en la tabla login_user en
        la lectura actual que se este realizando a la base de datos.
        
         -conexMysql: Variable que se encarga ya directamente de crear la conexion.
         
         -cmdMysql: Variable que recibe la instruccion y una conexion a base de datos
         para asi ejecutar luego esa misma instruccion.
         
         -rdrMysql: Guarda cada registro que encuentra en la base de datos, basandose en la
         instruccion dada.*/
    
    private static string especifConex, instrMysql, user_encontrado, pass_encontrado;
    private static MySqlConnection conexMysql = null;
    private static MySqlCommand cmdMysql = null;
    public static MySqlDataReader rdrMysql = null;

    //Procedimiento estatico para conectar a la base de datos especificada.
    public static void conectarMysql()
    {

        especifConex = "Server=127.0.0.1" + ";Database=spaceman" + ";User=root" + ";Password=root";

        try
        {
            conexMysql = new MySqlConnection(especifConex);
            conexMysql.Open();
        }
        catch (MySqlException err)
        {
            Debug.Log("Error encontrado:" + err.ToString());
        }
    }

    //Procedimiento para cerrar la base de datos que fue abierta con 'conectarMysql()'.
    public static void cerrarMysql()
    {
        if (conexMysql != null)
        {
            if (conexMysql.State.ToString() != "Closed")
            {
                conexMysql.Close();
                Debug.Log("Conexion a MySql cerrada");
            }
            conexMysql.Dispose();
        }
    }

    //Procedimiento para hacer una consulta simple SELECT a la base de datos.
    public static void seleccionar(string tabla, string[] atributos)
    {
        int i;

        instrMysql = "SELECT ";
        try
        {
            conectarMysql();
            for (i=0; i < atributos.Length; i++)
            {
                if (i == atributos.Length - 1) instrMysql += atributos[i];
                else instrMysql += atributos[i] + ",";
            }
            instrMysql += " FROM " + tabla;
            cmdMysql = new MySqlCommand(instrMysql, conexMysql);
            rdrMysql = cmdMysql.ExecuteReader();
        }
        catch (MySqlException err)
        {
            Debug.Log("Error encontrado:" + err.ToString());
        }
    }

    /*Procedimiento para insertar a la base de datos unos valores dependiendo de sus
    atributos.*/
    public static void insertar(string tabla, string[] atributos, string[] valores)
    {
        int i;
        conectarMysql();
        instrMysql = "";
        try
        {
            instrMysql = "INSERT INTO " + tabla + " (";
            for (i = 0; i < atributos.Length; i++)
            {
                if (i == atributos.Length - 1)
                {
                    instrMysql += atributos[i] + ")";
                }
                else
                {
                    instrMysql += atributos[i] + ",";
                }
            }
            instrMysql += " VALUES (";
            for (i = 0; i < valores.Length; i++)
            {
                if (i == valores.Length - 1)
                {
                    instrMysql += "'" + valores[i] + "');";
                }
                else
                {
                    instrMysql += "'" + valores[i] + "',";
                }
            }
            cmdMysql = new MySqlCommand(instrMysql, conexMysql);
            cmdMysql.ExecuteNonQuery();
        }
        catch (MySqlException err)
        {
            Debug.Log("Error encontrado:" + err.ToString());
        }
        finally
        {
            cerrarMysql();
        }
    }

    public static void llenarTablaPunt(int scoresID, ref Text[,] tablaP , int fila)
    {
        instrMysql = "SELECT login_user_username, score, perc_enemyKilled, punteria, rondas FROM scores WHERE id_scores = " + scoresID + ";";
        try
        {
            conectarMysql();
            cmdMysql = new MySqlCommand(instrMysql, conexMysql);
            rdrMysql = cmdMysql.ExecuteReader();
            while (rdrMysql.Read())
            {
                tablaP[fila, 0].text = rdrMysql.GetString(0);
                tablaP[fila, 1].text = rdrMysql.GetString(1);
                tablaP[fila, 2].text = rdrMysql.GetString(2);
                tablaP[fila, 3].text = rdrMysql.GetString(3);
                tablaP[fila, 4].text = rdrMysql.GetString(4);
            }
        }
        catch (MySqlException err)
        {
            Debug.Log("Error encontrado:" + err.ToString());
        }
        finally
        {
            if (rdrMysql != null)
            {
                rdrMysql.Close();
            }
            cerrarMysql();
        }
    }

    //FUNCIONES Y PROCEDIMIENTOS ADICIONALES USADOS EN CONJUNTO CON LAS CONSULTAS

    public static bool comprobarUser(string username, string password)
    {
        try
        {
            while (rdrMysql.Read())
            {
                user_encontrado = db_spaceman.rdrMysql.GetString(0);
                pass_encontrado = db_spaceman.rdrMysql.GetString(1);
                if (username.Equals(user_encontrado) && password.Equals(pass_encontrado))
                {
                    return true;
                }
            }
        }
        catch (MySqlException err)
        {
            Debug.Log("Error encontrado:" + err.ToString());
        }
        finally
        {
            if (rdrMysql != null)
            {
                rdrMysql.Close();
            }
            cerrarMysql();
        }
        return false;
    }

    public static void mostrarPuntuaciones(ref int[,] puntAltos)
    {
        /*Variables que guardan los valores anteriores del id del usuario y su puntaje para
         ser evaluados en el siguiente ciclo del for.*/
        int i, punt_aux, user_aux;
        
        /*Valores actuales con los que se trabaja para comparar en el arreglo de los TOP 5*/        
        int user, puntaje;

        for (i = 0; i < 5; i++) puntAltos[i, 1] = 0;

        try
        {
            while (rdrMysql.Read())
            {
                user = rdrMysql.GetInt32(0);
                puntaje = rdrMysql.GetInt32(1);
                for (i = 0; i < 5; i++)
                {
                    if (puntAltos[i, 1] < puntaje && puntAltos[i, 1] != 0)
                    {
                        user_aux = puntAltos[i, 0];
                        punt_aux = puntAltos[i, 1];
                        puntAltos[i, 0] = user;
                        puntAltos[i, 1] = puntaje;
                        user = user_aux;
                        puntaje = punt_aux;
                    }
                    else if (puntAltos[i, 1] < puntaje && puntAltos[i, 1] == 0)
                    {
                        puntAltos[i, 0] = user;
                        puntAltos[i, 1] = puntaje;
                        break;
                    }
                }
            }
        }
        catch (MySqlException err)
        {
            Debug.Log("Error encontrado:" + err.ToString());
        }
        finally
        {
            if (rdrMysql != null)
            {
                rdrMysql.Close();
            }
            cerrarMysql();
        }
    }
}

