using UnityEngine;

// 과제: 다이아몬드에 x축 방향 shear 변환 적용
// 변환식: (x, y, z) -> (x + k*y, y, z)
// k = (학번 끝자리 + 1) / 5
[ExecuteAlways]
[RequireComponent(typeof(DiamondMesh))]
public class S09_Shear : MonoBehaviour
{
    [SerializeField] float k = 0.6f; // 학번 끝자리 2 , (2+1)/5 = 0.6

    DiamondMesh diamondMesh;

    void OnEnable()
    {
        diamondMesh = GetComponent<DiamondMesh>();
    }

    void Update()
    {
        if (diamondMesh == null || diamondMesh.BaseVertices == null) return;

        float[,] S = ShearMatrixRaw(k);
        Vector3[] baseVertices = diamondMesh.BaseVertices;
        Vector3[] verts = new Vector3[baseVertices.Length];
        for (int i = 0; i < baseVertices.Length; i++)
            verts[i] = FromHomogeneous(MultiplyMatrixVectorRaw(S, ToHomogeneous(baseVertices[i])));
        diamondMesh.SetVertices(verts);
    }

    [ContextMenu("Log Top Vertex")]
    void LogTopVertex()
    {
        float[,] S = ShearMatrixRaw(k);

        Vector3 top = new Vector3(0.5f, 1f, 0.5f);

        Vector3 transformedTop = FromHomogeneous(
            MultiplyMatrixVectorRaw(S, ToHomogeneous(top))
        );

        Debug.Log("k = " + k + ", Top Vertex: " + transformedTop);
    }

    // 참고: z축 회전 행렬 (e₃는 그대로, e₁과 e₂가 돎)
    float[,] RotationZMatrixRaw(float angleDegrees)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        float c = Mathf.Cos(rad);
        float s = Mathf.Sin(rad);
        return new float[,] {
            { c,  -s,  0f, 0f },
            { s,   c,  0f, 0f },
            { 0f,  0f, 1f, 0f },
            { 0f,  0f, 0f, 1f }
        };
    }

    // x축 회전 행렬 (e₁은 그대로, e₂와 e₃가 돎)
    float[,] RotationXMatrixRaw(float angleDegrees)
    {
        float rad = angleDegrees * Mathf.Deg2Rad;
        float c = Mathf.Cos(rad);
        float s = Mathf.Sin(rad);
        // TODO: x축 회전 행렬을 float[4,4]로 반환
        //       (4열과 4행은 z축 회전과 같음)

        return new float[,] {
            { 1f, 0f, 0f, 0f },
            { 0f, c, -s, 0f },
            { 0f, s, c, 0f },
            { 0f, 0f, 0f, 1f }
        };
    }

    // x축 방향 shear 행렬
    float[,] ShearMatrixRaw(float k)
    {
        return new float[,] {
            { 1f,  k, 0f, 0f },
            { 0f, 1f, 0f, 0f },
            { 0f, 0f, 1f, 0f },
            { 0f, 0f, 0f, 1f }
        };
    }

    Vector4 ToHomogeneous(Vector3 v)
    {
        return new Vector4(v.x, v.y, v.z, 1f);
    }

    Vector3 FromHomogeneous(Vector4 h)
    {
        return new Vector3(h.x, h.y, h.z);
    }

    Vector4 MultiplyMatrixVectorRaw(float[,] M, Vector4 v)
    {
        float[] input = { v.x, v.y, v.z, v.w };
        float[] result = new float[4];
        for (int row = 0; row < 4; row++)
            for (int col = 0; col < 4; col++)
                result[row] += M[row, col] * input[col];
        return new Vector4(result[0], result[1], result[2], result[3]);
    }
}