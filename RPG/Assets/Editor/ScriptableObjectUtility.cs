using UnityEngine;
using UnityEditor;
using System.IO;

public static class ScriptableObjectUtility
{
	/// <summary>
	//	This makes it easy to create, name and place unique new ScriptableObject asset files.
	/// </summary>
	public static void CreateBaseObject<LootableObject>() where LootableObject : ScriptableObject
	{
		LootableObject asset = ScriptableObject.CreateInstance<LootableObject>();

		string path = "Assets/ScriptableObjects";
		string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(LootableObject).ToString() + ".asset");

		AssetDatabase.CreateAsset(asset, assetPathAndName);

		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = asset;
	}
}

public class YourClassAsset
{
	[MenuItem("Assets/Create Scriptable/LootableObject")]
	public static void CreateAsset()
	{
		ScriptableObjectUtility.CreateBaseObject<LootableObject>();
	}
}