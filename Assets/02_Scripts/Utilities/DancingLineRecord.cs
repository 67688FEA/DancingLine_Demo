using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Utilities;
using System.IO;

public class DancingLineRecord : MonoBehaviour
{

    public bool start = false;
    public static bool turn = false;
    public GameObject head;
    public GameObject prefab;
    public AudioSource audioSource;
    public float speed;
    public string RecordFileName;
    public enum OperationMode { space, mouse };
    public OperationMode operationMode = OperationMode.space;
    private float distFromGround = 0.3f;
    private Vector3 pos;
    private int loopCount = 1;

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

        if (start)
        {
            pos = head.transform.position;

            DancingLineTransform dancingLineTransform = new DancingLineTransform();
            dancingLineTransform.SetPosition(pos.x, pos.y, pos.z);
            dancingLineTransformList.Add(dancingLineTransform);

            head.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (IsGrounded())
            {
                Instantiate(prefab).transform.position = pos;
                if (operationMode == OperationMode.space)
                {
                    if (Input.GetKeyDown("space"))
                    {
                        Move();
                    }
                }
                else if (operationMode == OperationMode.mouse)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        Move();
                    }
                }
            }
        }
        else
        {
            if (operationMode == OperationMode.space)
            {
                if (Input.GetKeyDown("space"))
                {
                    PlayMusic();
                }
            }
            else if (operationMode == OperationMode.mouse)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    PlayMusic();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Save();
            start = false;
            audioSource.Stop();
        }
    }

    void Move()
    {
        if (turn == false)
        {
            if (loopCount % 2 == 0)
            {
                head.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else
            {
                head.transform.eulerAngles = new Vector3(0, 0, 0);
            }
            loopCount++;
        }
        else
        {
            if (loopCount % 2 == 0)
            {
                head.transform.eulerAngles = new Vector3(0, 90, 0);
            }
            else
            {
                head.transform.eulerAngles = new Vector3(0, 180, 0);
            }
            loopCount++;
        }
    }

    void PlayMusic()
    {
        start = true;
        audioSource.Play();
    }

    bool IsGrounded()
    {
        return Physics.Raycast(head.transform.position, Vector3.down, distFromGround);
    }

    void Save()
    {
        string path;
        string json;
        path = Application.dataPath + "/DancingLineRecord/" + RecordFileName + ".json";
        json = JsonMapper.ToJson(dancingLineTransformList);
        StreamWriter sw = new StreamWriter(path);
        sw.Write(json);
        sw.Close();
    }

}
