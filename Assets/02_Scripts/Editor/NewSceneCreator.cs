using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewSceneCreator : EditorWindow
{

    public GameObject head;
    public AudioClip music;
    public string newSceneName;
    private bool error = false;

    [MenuItem("DanceLine Tools/Create New Scene..", false, 0)]
    static void init()
    {
        NewSceneCreator window = GetWindowWithRect<NewSceneCreator>(new Rect(200, 100, 300, 110));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 280, 60));
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        GUILayout.Label("SceneName");
        GUILayout.Label("Head");
        GUILayout.Label("Music");
        GUILayout.EndVertical();

        GUILayout.Space(20);

        GUILayout.BeginVertical();
        newSceneName = EditorGUILayout.TextField(newSceneName);
        head = (GameObject)EditorGUILayout.ObjectField(head, typeof(GameObject), true);
        music = (AudioClip)EditorGUILayout.ObjectField(music, typeof(AudioClip), true);

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        GUILayout.BeginArea(new Rect(0, 70, 300, 60));
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();
        //GUILayout.Label("Head");
        //GUILayout.Label("Music");
        GUILayout.Label("");
        GUILayout.EndVertical();

        GUILayout.Space(200);

        GUILayout.BeginVertical();
        //head = (GameObject)EditorGUILayout.ObjectField(head, typeof(GameObject), true);
        //music = (AudioSource)EditorGUILayout.ObjectField(music, typeof(AudioSource), true);

        if (GUILayout.Button("Create", GUILayout.Width(60), GUILayout.Height(20)))
        {
            CheckAndCreateScene();
            Close();
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    protected void CheckAndCreateScene()
    {
        if (EditorApplication.isPlaying)
        {
            Debug.LogError("Cannot create scenes while in play mode.  Exit play mode first.");
            error = true;
            return;
        }

        if (newSceneName == "")
        {
            Debug.LogError("The SceneName is null.  Please enter a name of the scene.");
            error = true;
            return;
        }

        if (!head)
        {
            Debug.LogError("The Head is null.  Please choose a head prefab.");
            error = true;
            return;
        }

        if (!music)
        {
            Debug.LogError("The Music is null.  Please enter a audio clip.");
            error = true;
            return;
        }

        Scene currentScene = SceneManager.GetActiveScene();

        if (currentScene.isDirty)
        {
            string title = currentScene.name + "Has Been Modified";
            string message = "Do you want to sace the changes you made to" + currentScene.path + "?\nChanges will be lost if you don't save them.";
            int option = EditorUtility.DisplayDialogComplex(title, message, "Save", "Don't Save", "Cancel");

            if (option == 0)
            {
                EditorSceneManager.SaveScene(currentScene);
            }
            else if (option == 2)
            {
                return;
            }
        }
        CreateScene();
    }

    protected void CreateScene()
    {
        string[] templateScene = AssetDatabase.FindAssets("_TemplateScene");
        if (templateScene.Length > 0)
        {
            string newScenePath = "Assets/01_Scenes/Game Levels/" + newSceneName + ".unity";
            AssetDatabase.CopyAsset(AssetDatabase.GUIDToAssetPath(templateScene[0]), newScenePath);
            AssetDatabase.Refresh();
            Scene newScene = EditorSceneManager.OpenScene(newScenePath, OpenSceneMode.Single);
            InstantiatePrefab();
            AddSceneToBuildingSetting(newScene);
            Close();
        }
        else
        {
            EditorUtility.DisplayDialog("Error",
                "The scene _TemplateScene was not found in Assets/02.Scenes folder. This scene is required by the New Scene Creator.",
                "OK");
        }
    }

    protected void InstantiatePrefab()
    {
        GameObject instance = null;
        instance = Instantiate(head);
        FindObjectOfType<AudioSource>().clip = music;
        FindObjectOfType<ThirdPersonCamera>().head = instance.transform;
        FindObjectOfType<DancingLineController>().head = instance;
        FindObjectOfType<DancingLineRecord>().head = instance;
        FindObjectOfType<DancingLineRecord>().dancingLineRecord = newSceneName+"_Route";
    }

    protected void AddSceneToBuildingSetting(Scene scene)
    {
        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes;
        EditorBuildSettingsScene[] newBuildScenes = new EditorBuildSettingsScene[buildScenes.Length + 1];
        for (int i = 0; i < buildScenes.Length; i++)
        {
            newBuildScenes[i] = buildScenes[i];
        }
        newBuildScenes[buildScenes.Length] = new EditorBuildSettingsScene(scene.path, true);
        EditorBuildSettings.scenes = newBuildScenes;
    }

}
