using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Utilities;
using System.IO;

public class DancingLineRecord : MonoBehaviour
{

    public bool start = false;
    public bool forward = false;
    public string dancingLineRecord;
    public AudioSource audioSource;
    public GameObject head;
    public float speed = 0.1f;
    private GameObject body;

    private List<DancingLineTransform> dancingLineTransformList;
    //private DancingLineTransform dancingLineTransform;
    
    // Use this for initialization
    void Start()
    {
        //dancingLineTransform = new DancingLineTransform();
        dancingLineTransformList = new List<DancingLineTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (start)
            {
                if (forward)
                {
                    forward = false;
                }
                else
                {
                    forward = true;
                }
            }
            else
            {
                start = true;
                audioSource.Play();
            }
        }
        Dance();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Save();
            start = false;
            audioSource.Stop();
        }
    }

    void Dance()
    {
        if (start)
        {
            DancingLineTransform dancingLineTransform = new DancingLineTransform();
            dancingLineTransform.SetPosition(head.transform.position.x, head.transform.position.y, head.transform.position.z);
            dancingLineTransformList.Add(dancingLineTransform);
            body = (GameObject)Resources.Load("Prefabs/Head");
            body = Instantiate(body, head.transform.position, head.transform.rotation);
            if (forward)
            {
                head.transform.position += new Vector3(0, 0, speed);
            }
            else
            {
                head.transform.position += new Vector3(speed, 0, 0);
            }
        }
    }

    void Save()
    {
        string path;
        string json;
        path = Application.dataPath + "/DancingLineRecord/" + dancingLineRecord + ".json";
        json = JsonMapper.ToJson(dancingLineTransformList);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(json);
        sw.Close();
    }

}
