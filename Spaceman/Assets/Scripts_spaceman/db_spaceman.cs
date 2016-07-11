using UnityEngine;
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

    private string cadenaConex, comando, user_encontrado, pass_encontrado;
    private MySqlConnection conexMysql = null;
    private MySqlCommand cmdMysql = null;
    private MySqlDataReader rdrMysql = null;

    public void conectarMysql()
    {

        cadenaConex = "Server=127.0.0.1" + ";Database=spaceman" + ";User=root" + ";Password=";

        try
        {
            conexMysql = new MySqlConnection(cadenaConex);
            conexMysql.Open();
            Debug.Log("Estado de MySql: " + conexMysql.State);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    void OnApplicationQuit()
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

    public bool comprobarUser(string username, string password)
    {
        comando = "";
        try
        {
            comando = "SELECT * FROM login_user";
            cmdMysql = new MySqlCommand(comando, conexMysql);
            rdrMysql = cmdMysql.ExecuteReader();
            while (rdrMysql.Read())
            {
                user_encontrado = rdrMysql.GetString(0);
                pass_encontrado = rdrMysql.GetString(1);
                if (username.Equals(user_encontrado) && password.Equals(pass_encontrado))
                {
                    return true;
                }
            }
        }
        catch (MySqlException err)
        {
            Console.WriteLine("Error: " + err.ToString());
        }
        return false;
    }

    public void insertar(string tabla, string[] atributos, string[] valores)
    {
        int i;
        comando = "";
        try
        {
            comando = "INSERT INTO " + tabla + " (";
            for (i=0; i < atributos.Length; i++)
            {
                if (i == atributos.Length-1)
                {
                    comando += atributos[i] + ")";
                } else
                {
                    comando += atributos[i] + ",";
                }
            }
            comando += " VALUES (";
            for (i=0; i < valores.Length; i++)
            {
                if (i == valores.Length-1)
                {
                    comando += "'" + valores[i] + "');";
                }
                else
                {
                    comando += "'" + valores[i] + "',";
                }
            }
            cmdMysql = new MySqlCommand(comando, conexMysql);
            cmdMysql.ExecuteNonQuery();
        }
        catch (MySqlException err)
        {
            Console.WriteLine("Error: " + err.ToString());
        }
        
    }
}

