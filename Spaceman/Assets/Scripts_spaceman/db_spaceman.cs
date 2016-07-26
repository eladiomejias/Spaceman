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
        //Especificaciones para la conexion a base de datos
        especifConex = "Server=127.0.0.1" + ";Database=spaceman" + ";User=root" + ";Password=";

        /*Crea un nuevo objeto del tipo MysqlConnection con las especificaciones anteriores
         y abre la conexion. Cualquier error es manejado en el catch.*/
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

        /*Construye la instruccion SQL progresivamente a partir de los datos indicados
         mediante el nombre de la tabla y sus columnas o campos, para luego crear un
         nuevo objeto del tipo MySqlCommand que es el comando de consulta a ser
         ejecutado en la base de datos. Esta instruccion se encarga de seleccionar
         los elementos de la tabla especificada para luego trabajarlos en otro modulo. El
         cierre de la conexion a base de datos se hara posteriormente en los otros modulos
         donde se llame este procedimiento, despues de que haya cumplido su cometido.*/
        instrMysql = "SELECT ";
        try
        {
            conectarMysql();
            for (i = 0; i < atributos.Length; i++)
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
        /*Realiza la conexion a MySql y construye la instruccion SQL de tipo INSERT que inserta
         *los valores literales en los campos de la tabla indicada, con sus atributos o columnas
         indicados tambien.*/
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
            //Ejecuta la instruccion SQL ya habiendola construido
            cmdMysql = new MySqlCommand(instrMysql, conexMysql);
            cmdMysql.ExecuteNonQuery();
        }
        catch (MySqlException err)
        {
            Debug.Log("Error encontrado:" + err.ToString());
        }
        //Cierra la conexion a base de datos
        finally
        {
            cerrarMysql();
        }
    }

    /*Procedimiento que se encarga de llenar la tabla de puntuaciones en su escena correspondiente.
     Se pasa el id del puntaje guardado en la base de datos que lo diferencia de los demas puntajes,
     el objeto tablaP que es una matriz de componentes Text de la interface de la escena Puntuaciones
     para ser llenada progresivamente con los datos que haya encontrado en la base de datos, segun la
     instruccion SQL establecida.*/
    public static void llenarTablaPunt(int scoresID, ref Text[,] tablaP, int fila)
    {
        instrMysql = "SELECT login_user_username, score, perc_enemyKilled, punteria, rondas FROM scores WHERE id_scores = " + scoresID + ";";
        try
        {
            /*Conexion a la base de datos, creacion del comando segun la instruccion y
             tipo de conexion y, ejecucion de ese comando haciendo la lectura por cada
             registro indicado.*/
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
        /*Cierre de la lectura en base de datos, asi como la conexion a ella al final
         del procedimiento.*/
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

    /*Metodo que devuelve una variable booleana para establecer si el usuario y contraseña
     corresponde o no a alguno de los registros que se encuentra en la base de datos segun
     la tabla indicada.*/
    public static bool comprobarUser(string username, string password)
    {
        /*Procede a realizar la consulta en la base de datos y si encuentra algun usuario 
         valido retorna true, de lo contrario sino encontro ningun usuario entonces retor-
         nara falso.*/
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
        /*Cierre de la lectura en la base de datos asi como la conexion a la misma al final
         del procedimiento.*/
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

    /*Procedimiento que busca las 5 puntuaciones mas altas a ser guardadas para
     luego manejarlas en el proceso quicksort y asi poder ordenarlas de mayor a menor.
     Tiene como parametro una matriz de enteros que guarda estas puntuaciones con los
     id's de los puntajes correspondientes a esos usuarios.*/
    public static void mayoresPuntuaciones(ref int[,] puntAltos)
    {
        /*Variables que guardan los valores anteriores del id del puntaje del usuario
         * y su puntaje para ser evaluados en el siguiente ciclo del for.*/
        int i, punt_aux, idpunt_aux;

        /*Valores actuales con los que se trabaja para comparar en la matriz de los TOP 5*/
        int idpunt, puntaje;

        /*Inicializacion de la matriz en las puntuaciones para poder comparar los puntajes 
         almacenados en la base de datos.*/
        for (i = 0; i < 5; i++) puntAltos[i, 1] = 0;

        /*Ejecuta el lector de la instruccion de la base de datos que anteriormente fue
         ejecutada por el comando respectivo. Lee cada id del puntaje del usuario correspon-
         diente y su puntaje. Esto finaliza hasta que no encuentra mas registros en la base
         de datos.*/
        try
        {
            while (rdrMysql.Read())
            {
                idpunt = rdrMysql.GetInt32(0);
                puntaje = rdrMysql.GetInt32(1);

                /*Aqui recorre toda la matriz en la posicion del puntaje, verificando esto mismo
                 del usuario correspondiente. Si el puntaje que encontro en el registro es mas alto
                 que el puntaje actual que habia en la matriz y es distinto de 0, entonces procede
                 a guardar el valor anterior de la puntuacion de ese usuario en la posicion correspondiente
                 de la matriz al puntaje en una variable auxiliar para luego guardarla como el puntaje con el que se
                 va a comparar en el resto del recorrido de la matriz. Esto hace lo mismo pero con el 
                 id del puntaje del usuario.
                 
                 Luego guarda en el id del puntaje de usuario y el puntaje las variables encon-
                 tradas en el registro y procede a continuar el ciclo. Si esta primera condicion
                 no se cumple entonces pasa por la segunda condicion en donde verifica lo mismo
                 que en la primera pero revisa si el puntaje actual en la matriz es igual a 0.
                 De ser asi, guarda el registro encontrado en matriz con su id de puntaje y
                 procede a salirse del ciclo para continuar con la evaluacion del siguiente
                 registro de la base de datos.
                 
                 Esto ultimo se debe a que si no encontro ningun puntaje actual en el arreglo,
                 no necesita comparar ese mismo puntaje con las siguientes posisicones del
                 arreglo por si acaso es mayor ese puntaje a las otras siguientes de la matriz, y
                 que asi no se pierda la misma en este proceso.*/
                for (i = 0; i < 5; i++)
                {
                    if (puntAltos[i, 1] < puntaje && puntAltos[i, 1] != 0)
                    {
                        idpunt_aux = puntAltos[i, 0];
                        punt_aux = puntAltos[i, 1];
                        puntAltos[i, 0] = idpunt;
                        puntAltos[i, 1] = puntaje;
                        idpunt = idpunt_aux;
                        puntaje = punt_aux;
                    }
                    else if (puntAltos[i, 1] < puntaje && puntAltos[i, 1] == 0)
                    {
                        puntAltos[i, 0] = idpunt;
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
        /*Cierre de la lectura en la base de datos asi como la conexion a la misma al final
         del procedimiento.*/
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