using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System.IO;
public static class SpattersGroupEditor
{
    [MenuItem("Cratolin/Create/Cratolin/Spatter Group", priority = 9)]
    [MenuItem("Assets/Create/Cratolin/Spatter Group", priority = 9)]
    static void CreateMaskGroupEditor()
    {
        var newSo = ScriptableObject.CreateInstance<SplattersGroup>();
        string path = GenerateUniqueAssetPath("Spatter Group");
        AssetDatabase.CreateAsset(newSo, path);
    }

    
public static string GenerateUniqueAssetPath(string name, string extension = "asset", string path = "")
{
    var uniquePath = "";
    var assetDirectory = "";

    if (!string.IsNullOrEmpty(path))
        assetDirectory = Path.GetDirectoryName(path);
    else if (Selection.activeObject == null)
        assetDirectory = "Assets";
    else if (Selection.activeObject is DefaultAsset)
        assetDirectory = AssetDatabase.GetAssetPath(Selection.activeObject);
    else
        assetDirectory = Path.GetDirectoryName(AssetDatabase.GetAssetPath(Selection.activeObject));

    uniquePath = AssetDatabase.GenerateUniqueAssetPath(assetDirectory + "/" + name + "." + extension);

    return uniquePath;
}
}
