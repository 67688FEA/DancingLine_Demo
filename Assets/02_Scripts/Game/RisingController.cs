using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RisingController : MonoBehaviour {

    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            animator.SetTrigger("Rise");
        }
    }

}
