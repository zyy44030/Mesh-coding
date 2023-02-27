using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulsate : MonoBehaviour
{
    private Vector3[] originVertices;
    void Start()
    {
        MeshFilter m = GetComponent<MeshFilter>();
        originVertices = m.mesh.vertices;
    }

    void Update()
    {
        DrawNormals();
        List<Vector3> l = new List<Vector3>();
        MeshFilter m = GetComponent<MeshFilter>();
        Vector3 index;
        for (int i = 0; i < m.mesh.vertices.Length; i++){
            index = new Vector3(originVertices[i].x * (0.707f + 0.518f*Mathf.Abs(Mathf.Sin(3*Time.time))), originVertices[i].y, originVertices[i].z * (0.707f + 0.518f*Mathf.Abs(Mathf.Sin(3*Time.time))));
            l.Add(index);
        }
        m.mesh.SetVertices(l);
    }

    void DrawNormals()
    {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] verts = mesh.vertices;
        Vector3[] norms = mesh.normals;

        for (int i = 0; i < verts.Length; ++i)
        {
            Vector3 start = transform.TransformPoint(verts[i]);
            Vector3 dir = transform.rotation * norms[i];

            Debug.DrawRay(start, dir, Color.red, 0, true);
        }
    }
}
