using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingLineController : MonoBehaviour {

    public bool start = false;
    public bool forward = false;
    public AudioSource audioSource;
    public GameObject head;
    public float speed;
    private float InstantiationTimer;
    private GameObject body;

    // Use this for initialization
    void Start () {
        InstantiationTimer = speed;
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
    }

    void Dance()
    {
        if (start)
        {
            InstantiationTimer -= Time.deltaTime;
            if (InstantiationTimer <= 0)
            {
                body = (GameObject)Resources.Load("Prefabs/Head");
                body = Instantiate(body, head.transform.position, head.transform.rotation);
                if (forward)
                {
                    head.transform.position += new Vector3(0, 0, 0.2f);
                }
                else
                {
                    head.transform.position += new Vector3(0.2f, 0, 0);
                }
                InstantiationTimer = speed;
            }
        }
    }

}
