using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3?[] positions;
    private bool alive;

    void Start() {
        positions = new Vector3?[GameManager.Instance.maxTime];
        alive = true;
    }

    void FixedUpdate() {
        if (alive)
            // TODO: copy later if changing positions array
            positions[GameManager.Instance.CurrentRoundIt] = gameObject.transform.position;
        

    }
}
