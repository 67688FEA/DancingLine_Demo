using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingController : MonoBehaviour {

    public Animator destination;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            destination.SetTrigger("Rise");
        }
    }

}
