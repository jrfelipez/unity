// Cuadrado con interpolacion de colores
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuadrado : MonoBehaviour
{
    private Vector3[] vertices;
    private int[] indices;
    private Color[] colores;
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
        Material nuevoMaterial = new Material(Shader.Find("ShaderBasico"));
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
        
        colores = new Color[]
        {
            new Color(1,0,0), // 0
            new Color(1,0,0), // 1
            new Color(1,0,0), // 2
            new Color(1,0,0)  // 3
        };
    }

    private void UpdateMalla()
    {
        objetoCuadrado.GetComponent<MeshFilter>().mesh.vertices = vertices;
        objetoCuadrado.GetComponent<MeshFilter>().mesh.triangles = indices;
        objetoCuadrado.GetComponent<MeshFilter>().mesh.colors = colores;
    }

    private Matrix4x4 CreaModeloMatriz(Vector3 T, Vector3 R, Vector3 S)
    {
        Matrix4x4 MatrizTraslacion = new Matrix4x4 (
            new Vector4(1f, 0f, 0f, T.x),
            new Vector4(0f, 1f, 0f, T.y),
            new Vector4(0f, 0f, 1f, T.z),
            new Vector4(0f, 0f, 0f, 1f)
        );
        Debug.Log(MatrizTraslacion[0]);
        MatrizTraslacion = MatrizTraslacion.transpose;

        Matrix4x4 MatrizRotacionX = new Matrix4x4 (
            new Vector4(1f, 0f, 0f, 0f),
            new Vector4(0f, Mathf.Cos(R.x), -Mathf.Sin(R.x), 0f),
            new Vector4(0f, Mathf.Sin(R.x), Mathf.Cos(R.x), 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );
        MatrizRotacionX = MatrizRotacionX.transpose;

        Matrix4x4 MatrizRotacionY = new Matrix4x4 (
            new Vector4(Mathf.Cos(R.y), 0f, Mathf.Sin(R.y), 0f),
            new Vector4(0f, 1f, 0f, 0f),
            new Vector4(-Mathf.Sin(R.y), 0f, Mathf.Cos(R.y), 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );
        MatrizRotacionY = MatrizRotacionY.transpose;

        Matrix4x4 MatrizRotacionZ = new Matrix4x4 (
            new Vector4(Mathf.Cos(R.z), -Mathf.Sin(R.z), 0f, 0f),
            new Vector4(Mathf.Sin(R.z), Mathf.Cos(R.z), 0f, 0f),
            new Vector4(0f, 0f, 1f, 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );
        MatrizRotacionZ = MatrizRotacionZ.transpose;

        Matrix4x4 MatrizEscalacion = new Matrix4x4 (
            new Vector4(S.x, 0f, 0f, 0f),
            new Vector4(0f, S.y, 0f, 0f),
            new Vector4(0f, 0f, S.z, 0f),
            new Vector4(0f, 0f, 0f, 1f)
        );
        MatrizEscalacion = MatrizEscalacion.transpose;

        Matrix4x4 MatrizModelo = MatrizTraslacion;
        MatrizModelo = MatrizModelo * MatrizRotacionX;
        MatrizModelo = MatrizModelo * MatrizRotacionY;
        MatrizModelo = MatrizModelo * MatrizRotacionZ;
        MatrizModelo = MatrizModelo * MatrizEscalacion;
        return MatrizModelo;
    }

    void Update()
    {
        Vector3 nuevaPosicion = new Vector3(-0.5f,0,-0.5f);
        Vector3 nuevaRotacion = new Vector3(0, Mathf.Deg2Rad * 45, 0); 
        Vector3 nuevaEscalacion = new Vector3(0.5f, 0.5f, 0.5f); 
        Matrix4x4 MatrizModelo = CreaModeloMatriz(nuevaPosicion, nuevaRotacion, nuevaEscalacion);
        objetoCuadrado.GetComponent<Renderer>().material.SetMatrix("uMatrizModelo", MatrizModelo);
   }
}