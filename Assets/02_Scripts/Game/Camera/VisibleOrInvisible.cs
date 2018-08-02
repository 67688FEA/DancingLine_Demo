using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisibleOrInvisible : MonoBehaviour {

    public GameObject head;
    public ThirdPersonCamera mainCamera;
    private Vector3 offset;
    private Vector3 targetPosition;
    public float smooth = 5f;
    void Start()
    {
        offset = transform.position - head.transform.position;
    }

    void FixedUpdate()
    {
        if (!IsVisibleOn3dCamera(head, gameObject.GetComponent<Camera>()))
        {
            //transform.position = head.transform.position - offset;
            targetPosition = head.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, smooth * Time.deltaTime);
            mainCamera.Move();
        }
    }

    bool IsVisibleOn3dCamera(GameObject obj, Camera camera)
    {
        Vector3 pos = camera.WorldToViewportPoint(obj.transform.position);
        bool isVisible = (camera.orthographic || pos.z > 0f) && (pos.x > 0f && pos.x < 1f && pos.y > 0f && pos.y < 1f);
        print(isVisible);
        return isVisible;
    }

}
