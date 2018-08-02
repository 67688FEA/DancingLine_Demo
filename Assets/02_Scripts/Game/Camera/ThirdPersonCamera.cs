using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {

    public Transform head;
    public float smooth = 10f;
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
        if (!IsVisibleOn3dCamera(head.gameObject, gameObject.GetComponent<Camera>()))
        {
            float distCovered = (Time.time - startTime) * smooth;
            targetPosition = head.position + offset;
            float fracJourney = distCovered / journeyLength;
            transform.position = Vector3.Lerp(transform.position, targetPosition, fracJourney);
        }

        //transform.position = head.position + offset;
    }

    bool IsVisibleOn3dCamera(GameObject obj, Camera camera)
    {
        Vector3 pos = camera.WorldToViewportPoint(obj.transform.position);
        bool isVisible = (pos.y > 0.4f && pos.y < 0.6f);
        print(isVisible);
        return isVisible;
    }

    public void Move()
    {
        float distCovered = (Time.time - startTime) * smooth;
        targetPosition = head.position + offset;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, targetPosition, fracJourney);
        //targetPosition = head.position + offset;
        //transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
    }

}
