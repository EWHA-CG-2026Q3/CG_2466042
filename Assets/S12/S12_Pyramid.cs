using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S12_Pyramid : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices =
        {
            new Vector3(-0.5f, 0f, -0.5f), // 0
            new Vector3(0.5f, 0f, -0.5f),  // 1
            new Vector3(0f, 0f, 0.5f),    // 2
            new Vector3(0f, 1f, 0f)       // 3: 꼭대기
        };

        int[] triangles =
        {
            0, 2, 1, // 바닥
            0, 1, 3, // 앞면
            1, 2, 3, // 오른쪽 면
            2, 0, 3  // 왼쪽 면
        };

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
    }
}