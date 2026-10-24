
using UnityEngine;
using UnityEditor;

public class S13_SaveMesh
{
    [MenuItem("Tools/S13/Save Selected Mesh")]
    static void SaveSelectedMesh()
    {
        UnityEngine.GameObject obj = UnityEditor.Selection.activeGameObject;

        if (obj == null)
        {
            Debug.LogWarning("오브젝트를 먼저 선택하세요.");
            return;
        }

        MeshFilter filter = obj.GetComponent<MeshFilter>();

        if (filter == null || filter.sharedMesh == null)
        {
            Debug.LogWarning("저장할 Mesh가 없습니다.");
            return;
        }

        string path = EditorUtility.SaveFilePanelInProject(
            "Save Mesh",
            obj.name + "_Mesh",
            "asset",
            "메시 저장 위치를 선택하세요."
        );

        if (string.IsNullOrEmpty(path))
            return;

        Mesh meshCopy = Object.Instantiate(filter.sharedMesh);
        meshCopy.name = obj.name + "_Mesh";

        AssetDatabase.CreateAsset(meshCopy, path);
        AssetDatabase.SaveAssets();

        Debug.Log("메시 저장 완료: " + path);
    }
}
