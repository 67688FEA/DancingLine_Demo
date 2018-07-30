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

    [MenuItem("DanceLine Tools/Load A Dance Route..")]
    static void Init()
    {
        LoadDancingLineRecord window = GetWindowWithRect<LoadDancingLineRecord>(new Rect(100, 100, 300, 120));
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
        GUILayout.Space(10);
        GUILayout.Label("Select a Dance Route", EditorStyles.boldLabel);
        index = EditorGUILayout.Popup(index, options);

        GUILayout.BeginArea(new Rect(0, 60, 300, 60));
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();//1.将11111和22222的盒子纵向做出 并结束
        GUILayout.Label("");
        GUILayout.EndVertical();

        GUILayout.Space(200); //2.偏移20像素 为后面的 33333和44444盒子做纵向布局做准备

        GUILayout.BeginVertical();//3.将11111和22222的盒子纵向做出 并结束      
        
        if (GUILayout.Button("Create",GUILayout.Width(60), GUILayout.Height(20)))
        {
            CreateDancingLine();
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

    }

    protected void CreateDancingLine()
    {
        GameObject instance = null;
        List<DancingLineTransform> dancingLineTransformList = new List<DancingLineTransform>();
        string path;
        string json="";
        path = Application.dataPath + "/DancingLineRecord/" + options[index];
        StreamReader sr = new StreamReader(path);
        json = sr.ReadToEnd();
        sr.Close();
        JsonData jsonData = JsonMapper.ToObject(json);
        for(int i = 0; i < jsonData.Count; i++)
        {
            instance = Instantiate(prefab);
            instance.transform.position = new Vector3(float.Parse(jsonData[i]["x"].ToString()), float.Parse(jsonData[i]["y"].ToString()), float.Parse(jsonData[i]["z"].ToString()));
            instance.transform.parent = FindObjectOfType<DancingLineRecord>().gameObject.transform;
        }
    }

}
