using System;
using System.Collections.Generic;
using UnityEngine;

public enum Color { Red, Yellow, Green, Blue }

public class GameManager : MonoBehaviour {
    [field: SerializeField] public int MaxTime {get; private set;} = 30;
    [field: SerializeField] public float FPS {get; private set;} = 30.0f;
    [field: SerializeField] public Vector3 SpawnPoint {get; private set;}
    public GameObject Player;

    public int CurrentFrame {get; private set;}
    private int NFrames;
    private Dictionary <Color, Vector3?[]> Positions;
    private Color CurrentCol = Color.Blue;
    private bool Updating = true;

    public static GameManager Instance {get; private set;}

    void Start() {
        Positions = new Dictionary<Color, Vector3?[]>();
        CurrentFrame = 0;
        Time.fixedDeltaTime = 1.0f / FPS;
        NFrames = (int) Math.Ceiling(MaxTime * FPS);
        CycleColor();
        Updating = false;
    }

    private void Awake() {
        Instance = this;
    }

    void FixedUpdate() {
        if (Updating) return;

        // Store position
        Positions[CurrentCol][CurrentFrame] = Player.transform.position;

        // Update ghosts
        foreach (var pair in Positions) {
            if (pair.Key == CurrentCol) continue;

            // Move ghost
        }

        ++CurrentFrame;
        if (CurrentFrame >= NFrames) CycleColor();
    }

    void CycleColor() {
        Updating = true;
        CurrentFrame = 0;

        int ColIndex = (int) CurrentCol;
        ColIndex += 1;
        ColIndex %= 4;
        CurrentCol = (Color) ColIndex;

        Positions[CurrentCol] = new Vector3?[NFrames];

        Player.transform.position = SpawnPoint;
        Updating = false;
    }
}
