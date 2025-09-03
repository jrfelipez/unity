// Crea Material
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadrado : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] indices;
    private GameObject objetoCuadrado;
    private GameObject miCamara;

    void Start()
    {
        objetoCuadrado = new GameObject();
        objetoCuadrado.AddComponent<MeshFilter>();
        objetoCuadrado.GetComponent<MeshFilter>().mesh = new Mesh();
        objetoCuadrado.AddComponent<MeshRenderer>();
        CreaModelo();
        UpdateMalla();
        CreaMaterial();
        CreaCamara();
    }

    private void CreaMaterial()
    {
        Material nuevoMaterial = new Material(Shader.Find("Standard"));
        objetoCuadrado.GetComponent<MeshRenderer>().material = nuevoMaterial;
    }

    private void CreaCamara()
    {
        miCamara = new GameObject();
        miCamara.AddComponent<Camera>();
        miCamara.transform.position = new Vector3(0, 2, 0);
        miCamara.transform.rotation = Quaternion.Euler(90, 0, 0);
        miCamara.GetComponent<Camera>().clearFlags = CameraClearFlags.SolidColor;
        miCamara.GetComponent<Camera>().backgroundColor = Color.black;
    }

    private void CreaModelo()
    {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0), // 0
            new Vector3(1,0,0), // 1
            new Vector3(1,0,1), // 2
            new Vector3(0,0,1) // 3
        };

        indices = new int[]
        {
            0,2,1, // T0
            0,3,2  // T1
        };
    }

    private void UpdateMalla()
    {
        objetoCuadrado.GetComponent<MeshFilter>().mesh.vertices = vertices;
        objetoCuadrado.GetComponent<MeshFilter>().mesh.triangles = indices;
    }

    void Update()
    {
        
    }
}