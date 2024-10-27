using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FreeCamera freeCamera;
    public GameObject ui;
    public ObjectSpawner stoneSpawner;
    public CloudController cloudController;

    private void Update()
    {
        if (ui.activeSelf) return;

        freeCamera?.Move();
        if (Input.GetKeyDown(KeyCode.X))
        {
            stoneSpawner?.Spawn();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            cloudController?.ChangeTarget();
        }
    }
}
