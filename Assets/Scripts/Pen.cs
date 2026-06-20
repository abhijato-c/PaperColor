using System;
using System.Collections.Generic;
using UnityEngine;

public class Pen : MonoBehaviour
{
    private Vector3?[] positions;
    public bool alive;
    public Color color;

    void Start() {
        positions = new Vector3?[(int)Math.Ceiling(GameManager.Instance.MaxTime * GameManager.Instance.FPS)];
        alive = true;
    }

    void FixedUpdate() {
        if (alive)
            // TODO: copy later if changing positions array
            print(GameManager.Instance.CurrentRoundIt);
            positions[GameManager.Instance.CurrentRoundIt] = gameObject.transform.position;
        

    }

    void Reset() {
        alive = false;
        GameManager.Instance.ResetColor(color, positions);
        CycleColor(ref color);
        gameObject.transform.position = GameManager.Instance.RespawnPoint;

    }

    private void CycleColor(ref Color color) {
        int e = (int)color++;
        e %= 4;
        color = (Color)e;
    }
}
