using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform head;
    private Vector3 offset;

	// Use this for initialization
	void Start () {
        offset = head.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = head.position - offset;
	}
}
