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

    void OnCollisionEnter(Collision col)
    {
        GetComponent<AudioSource>().PlayOneShot(collisionClip);
    }
}
