using UnityEngine;
using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;


public class db_spaceman : MonoBehaviour {

    
    public bool pooling = true;
    private string cadenaConeccion, comando, user, pass;
    private MySqlConnection conexMysql = null;
    private MySqlCommand cmdMysql = null;
    private MySqlDataReader rdrMysql = null;

    public void conectar() {

        cadenaConeccion = "Server=127.0.0.1" + ";Database=spaceman" + ";User=root" + ";Password=";
        /*+ ";Pooling=";
        if (pooling) {
            cadenaConeccion += "true;";
        }
        else {
            cadenaConeccion += "false;";
        }*/

        try {
            conexMysql = new MySqlConnection(cadenaConeccion);
            conexMysql.Open();
            Debug.Log("Estado de MySql: " + conexMysql.State);
        } catch (Exception e) {
            Debug.Log(e);
        }
    }

    void OnApplicationQuit() {
        if (conexMysql != null) {
            if (conexMysql.State.ToString() != "Closed") {
                conexMysql.Close();
                Debug.Log("Conexion a MySql cerrada");
            }
            conexMysql.Dispose();
        }
    }

    public string GetEstadoConexion() {
        return this.conexMysql.State.ToString();
    }

    public bool consultar(string username, string password)
    {
        try
        {
            comando = "SELECT * FROM login_user";
            MySqlCommand cmdMysql = new MySqlCommand(comando, conexMysql);
            rdrMysql = cmdMysql.ExecuteReader();
            while (rdrMysql.Read())
            {
                user = rdrMysql.GetString(0);
                pass = rdrMysql.GetString(1);
            }
        }
        catch (MySqlException err)
        {
            Console.WriteLine("Error: " + err.ToString());
        }

        if (username.Equals(user) && password.Equals(pass))
        {
            return true;
        } else
        {
            return false;
        }
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
