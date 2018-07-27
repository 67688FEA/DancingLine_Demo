using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DancingLineController : MonoBehaviour {

    public bool start = false;
    public bool forward = false;
    public GameObject head;
    private GameObject body;

	// Use this for initialization
	void Start () {
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
            }
        }
        Dance();
    }

    void Dance()
    {
        if (start)
        {
            body = (GameObject)Resources.Load("Prefabs/Head");
            body = Instantiate(body, head.transform.position, head.transform.rotation);
            if (forward)
            {
                head.transform.position += new Vector3(0, 0, 0.1f);
            }
            else
            {
                head.transform.position += new Vector3(0.1f, 0, 0);
            }
        }
    }

}
