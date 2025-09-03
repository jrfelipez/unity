// Lee un archivo .obj
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LeeArchivo : MonoBehaviour
{
    void Start()
    {
        string nomArchivo = "Bed1";
        string ruta = "Assets/Modelos3d/" + nomArchivo + ".obj";

        StreamReader reader = new StreamReader(ruta);
        string datos = (reader.ReadToEnd());

        MuestraLasLineas(datos);

        reader.Close();

        Debug.Log(datos);
    }

    void MuestraLasLineas(string datos)
    {
        string[] lineas = datos.Split('\n');
        for (int i = 0; i < lineas.Length; i++)
        {
            Debug.Log(lineas[i]);
        }
    }
    void ProcesaLasLineas(string datos)
    {
        string[] lineas = datos.Split('\n');
        for (int i = 0; i < lineas.Length; i++)
        {
            if (lineas[i].StartsWith("v "))
            {
                // es un vertice, que hacemos? 
            } else if (lineas[i].StartsWith("f ")) {
                // es un cara, que hacemos? 
            }
        }
    }
}