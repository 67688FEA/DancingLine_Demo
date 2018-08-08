using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour {

    public ParticleSystem particleSystem;
    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem instance = Instantiate(particleSystem);
        instance.transform.position = gameObject.transform.position;
        instance.Play();
        gameObject.SetActive(false);
    }

}
