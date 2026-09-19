using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class S04_Diamond_Script : MonoBehaviour
{
    void Start()
    {
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0f, 0f, 0f), // 0 (정육면체 0번)
            new Vector3(1f, 0f, 0f), // 1 (정육면체 1번)
            new Vector3(1f, 0f, 1f), // 2 (정육면체 5번)
            new Vector3(0f, 0f, 1f), // 3 (정육면체 4번)
            new Vector3(0.5f, 1f, 0.5f), //4
            new Vector3(0.5f, -1f, 0.5f), //5
         
        };

        int[] triangles = new int[]
        {
            0, 1, 4,
            1, 2, 4, 
            2, 3, 4,
            0, 3, 4,

            0, 1, 5,
            1, 2, 5, 
            2, 3, 5, 
            3, 0, 5,

        };

        // 잘못된 예시 — 모든 삼각형의 정점 순서가 반대로 뒤집혀 있음
        //int[] triangles = new int[]
        //{
        //    1, 2, 3,  // 밑면 (원래: 1, 3, 2)
        //    0, 2, 1,  // 옆면 A (원래: 0, 1, 2)
        //    0, 3, 2,  // 옆면 B (원래: 0, 2, 3)
        //    0, 1, 3,  // 옆면 C (원래: 0, 3, 1)
        //};

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();

        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));
    }
}
