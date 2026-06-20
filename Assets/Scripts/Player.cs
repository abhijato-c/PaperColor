using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Vector3> positions;

    void Start() {
        positions = new List<Vector3>(); // todo: reserve space to improve insertion performance
    }

    void Update() {
        positions.Add(gameObject.transform.position);
    }
}
