using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Vector3> positions;
    private bool recordPositions;

    void Start() {
        positions = new List<Vector3>(1000); // TODO: reserve space to improve insertion performance
        recordPositions = false;
    }

    void Update() {
        positions.Add(gameObject.transform.position);
    }
}
