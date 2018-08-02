using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    public bool start = false;
    public GameObject head;
    public GameObject prefab;
    public AudioSource audioSource;
    public float speed;
    private float distFromGround = 0.3f;
    private Vector3 pos;
    private int loopCount = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (start)
        {
            pos = head.transform.position;
            head.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (IsGrounded())
            {
                Instantiate(prefab).transform.position = pos;
                if (Input.GetMouseButtonDown(0))
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
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                start = true;
                audioSource.Play();
            }
        }

    }

    bool IsGrounded()
    {
        return Physics.Raycast(head.transform.position, Vector3.down, distFromGround);
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

}
