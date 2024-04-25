using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class RainCloud : MonoBehaviour
{
    public LightningBoltScript lightning;
    public ParticleSystem rain;
    public string targetTag;
    public float time = 0.3f;
    public float maxSpeed = 30.0f;

    private List<Vector3> targetPositions;
    private int targetIdx = 0;
    private Vector3 velocity = Vector3.zero;
    private bool move = false;
    // Start is called before the first frame update
    void Start()
    {
        targetPositions = new List<Vector3>();
        foreach (GameObject target in GameObject.FindGameObjectsWithTag(targetTag))
        {
            targetPositions.Add(new Vector3(
                target.transform.position[0],
                transform.position[1],
                target.transform.position[2]
            ));
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeTarget();
        Move();
    }

    void ChangeTarget()
    {
        if (!move && Input.GetKeyDown(KeyCode.Z))
        {
            lightning.Trigger();
            lightning.GetComponent<AudioSource>().Play();
            rain.GetComponent<AudioSource>().Stop();
            rain.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            move = true;
        }
    }

    void Move()
    {
        if (move) {
            transform.position = Vector3.SmoothDamp(
                transform.position, targetPositions[targetIdx],
                ref velocity, time, maxSpeed
            );

            if (Vector3.Distance(transform.position, targetPositions[targetIdx]) < 0.001f)
            {
                move = false;
                rain.GetComponent<AudioSource>().Play();
                rain.Play();
                targetIdx = (targetIdx + 1) % targetPositions.Count;
            }
        }
    }
}