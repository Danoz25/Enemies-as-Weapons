using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
        transform.SetParent(null);
        Destroy(gameObject, timer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
