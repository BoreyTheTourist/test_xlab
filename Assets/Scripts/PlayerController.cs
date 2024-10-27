using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FreeCamera freeCamera;
    public GameObject ui;

    private void Update()
    {
        if (!ui.activeSelf) {
            freeCamera?.Move();
        }
    }
}
