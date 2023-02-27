using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reshape : MonoBehaviour
{
    void Start()
    {
        List<Vector3> l = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        MeshFilter m = GetComponent<MeshFilter>();
        List<Vector3>[] duplicates = new List<Vector3>[88];

        for (int i = 0; i < 88; i++){
            duplicates[i] = new List<Vector3>();
        }

        List<int>[] a = new List<int>[100];

        Vector3 index;
        Vector3 changedNormal = new Vector3(0,0,1);

        for (int i = 0; i < m.mesh.vertices.Length; i++){
            if(i == 40 || i == 41) index = m.mesh.vertices[i];
            if(!(i < 20 || (i >= 48 && i<= 67) || i == 47 || i == 44 || i == 42)){
                index = new Vector3(m.mesh.vertices[i].x * 0.707f, m.mesh.vertices[i].y , m.mesh.vertices[i].z * 0.707f);
            }else{
                index = m.mesh.vertices[i];
            }
            l.Add(index);
        }

        m.mesh.SetVertices(l);
    
        // set normals
        for (int i = 0; i < 40; i++){
            Vector3 p1 = m.mesh.vertices[m.mesh.triangles[3 * i]];
            Vector3 p2 = m.mesh.vertices[m.mesh.triangles[3 * i + 1]];
            Vector3 p3 = m.mesh.vertices[m.mesh.triangles[3 * i + 2]];
            changedNormal =  Vector3.Cross(p2 - p1, p3 - p1).normalized;
            duplicates[m.mesh.triangles[3 * i]].Add(changedNormal);
            duplicates[m.mesh.triangles[3 * i + 1]].Add(changedNormal);
            duplicates[m.mesh.triangles[3 * i + 2]].Add(changedNormal);
        }
        duplicates[40].Add(m.mesh.normals[40]);
        duplicates[41].Add(m.mesh.normals[41]);

        for (int i = 47; i < 88; i++){
            duplicates[i].Add(m.mesh.normals[i]);
        }

        foreach (List<Vector3> list in duplicates)
        {
            Vector3 finalVector = list[0];

            foreach (Vector3 vec in list)
            {
                finalVector = (finalVector + vec).normalized;
            }
            normals.Add(finalVector);
        }

        m.mesh.SetNormals(normals);

        GetComponent<MeshFilter>().mesh = m.mesh;
    }

    void Update()
    {
        DrawNormals();
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
