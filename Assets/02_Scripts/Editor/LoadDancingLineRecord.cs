using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using Utilities;

public class LoadDancingLineRecord : EditorWindow
{
    public int index;
    public string[] options;
    public GameObject prefab;

    [MenuItem("DanceLine Tools/Load A Dance Route..", false, 1)]
    static void Init()
    {
        LoadDancingLineRecord window = GetWindowWithRect<LoadDancingLineRecord>(new Rect(100, 100, 320, 110));
        window.Show();
    }

    private void OnGUI()
    {
        string path = Application.dataPath + "/DancingLineRecord";
        if (Directory.Exists(path))
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] files = directoryInfo.GetFiles("*.json", SearchOption.AllDirectories);
            options = new string[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                options[i] = files[i].Name;
            }
        }
        GUILayout.BeginArea(new Rect(10, 10, 300, 60));
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        GUILayout.Label("Head Prefab");
        GUILayout.Label("Dance Route");
        GUILayout.EndVertical();

        GUILayout.Space(20);

        GUILayout.BeginVertical();
        prefab = (GameObject)EditorGUILayout.ObjectField(prefab, typeof(GameObject), true);
        index = EditorGUILayout.Popup(index, options);

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();


        GUILayout.BeginArea(new Rect(0, 60, 300, 60));
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        GUILayout.Label("");
        GUILayout.EndVertical();

        GUILayout.Space(230);

        GUILayout.BeginVertical();

        if (GUILayout.Button("Create", GUILayout.Width(60), GUILayout.Height(20)))
        {
            CreateDancingLine();
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }

    protected void CreateDancingLine()
    {
        if (EditorApplication.isPlaying)
        {
            Debug.LogWarning("Cannot load the dance route record while in play mode.  Exit play mode first.");
            return;
        }

        GameObject instance = null;
        List<DancingLineTransform> dancingLineTransformList = new List<DancingLineTransform>();
        string path;
        string json = "";
        path = Application.dataPath + "/DancingLineRecord/" + options[index];
        StreamReader sr = new StreamReader(path);
        json = sr.ReadToEnd();
        sr.Close();
        JsonData jsonData = JsonMapper.ToObject(json);
        for (int i = 0; i < jsonData.Count; i++)
        {
            instance = Instantiate(prefab);
            instance.transform.position = new Vector3(float.Parse(jsonData[i]["x"].ToString()), float.Parse(jsonData[i]["y"].ToString()), float.Parse(jsonData[i]["z"].ToString()));
            instance.transform.parent = FindObjectOfType<DancingLineRecord>().gameObject.transform;
        }
    }

}
