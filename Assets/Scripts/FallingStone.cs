using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingStone : MonoBehaviour
{
    public AudioClip awakeClip;
    public AudioClip collisionClip;
    void Awake()
    {
        GetComponent<AudioSource>().PlayOneShot(awakeClip);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision col)
    {
        GetComponent<AudioSource>().PlayOneShot(collisionClip);
    }
}
