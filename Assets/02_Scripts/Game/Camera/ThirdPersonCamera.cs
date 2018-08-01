using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform head;
    public float smooth = 5f;
    private Vector3 offset;
    private Vector3 targetPosition;
    public static bool visible = false;
    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;

    // Use this for initialization
    void Start () {
        offset = transform.position - head.position;
        // Keep a note of the time the movement started.
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(transform.position, head.position);
    }

    // Update is called once per frame
    void Update()
    {
        //if (!visible)
        //{
        //    //transform.position = head.position - offset;
        //    targetPosition = head.position + offset;
        //    transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
        //    visible = true;
        //}

        //float distCovered = (Time.time - startTime) * smooth;
        //targetPosition = head.position + offset;
        //float fracJourney = distCovered / journeyLength;
        //transform.position = Vector3.Lerp(head.position, targetPosition, fracJourney);

        transform.position = head.position + offset;
    }

    public void Move()
    {
        float distCovered = (Time.time - startTime) * smooth;
        targetPosition = head.position + offset;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(head.position, targetPosition, fracJourney);
        //targetPosition = head.position + offset;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
    }

}
