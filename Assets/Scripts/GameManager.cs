using System;
using System.Collections.Generic;
using UnityEngine;

public enum Color { Red, Yellow, Green, Blue }

public class GameManager : MonoBehaviour {
    public int MaxTime = 10;
    public float FPS = 30.0f;
    public Transform SpawnPoint;
    public GameObject Player;
    public GameObject GhostPrefab;

    private int CurrentFrame;
    private int NFrames;
    private Color CurrentCol = Color.Red;
    private bool Updating = true;
    private Dictionary <Color, Vector3[]> Positions;
    private Dictionary <Color, GameObject> Ghosts;
    private Dictionary <Color, Color32> Cols;

    public static GameManager Instance {get; private set;}

    void Start() {
        Positions = new Dictionary<Color, Vector3[]>();
        Ghosts = new Dictionary<Color, GameObject>();
        Cols = new Dictionary<Color, Color32>();
        Cols[Color.Red] = new Color32(230, 90, 90, 255);
        Cols[Color.Yellow] = new Color32(255, 220, 0, 255);
        Cols[Color.Green] = new Color32(40, 210, 40, 255);
        Cols[Color.Blue] = new Color32(70, 130, 255, 255);
        CurrentFrame = 0;

        Time.fixedDeltaTime = 1.0f / FPS;
        NFrames = (int) Math.Ceiling(MaxTime * FPS);
        Positions[CurrentCol] = new Vector3[NFrames];
        Player.transform.position = SpawnPoint.position;
        Player.GetComponent<SpriteRenderer>().color = Cols[CurrentCol];

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
            Ghosts[pair.Key].transform.position = pair.Value[CurrentFrame];
        }

        ++CurrentFrame;
        if (CurrentFrame >= NFrames) CycleColor();
    }

    void CycleColor() {
        Updating = true;
        CurrentFrame = 0;

        // Spawn ghost
        if (!Ghosts.ContainsKey(CurrentCol)) {
            GameObject ghost = Instantiate(GhostPrefab, gameObject.transform);
            Color32 TransCol = Cols[CurrentCol];
            TransCol.a = 100;
            ghost.GetComponent<SpriteRenderer>().color = TransCol;
            Ghosts[CurrentCol] = ghost;
        }

        int ColIndex = (int) CurrentCol;
        ColIndex += 1;
        ColIndex %= 4;
        CurrentCol = (Color) ColIndex;
        Positions[CurrentCol] = new Vector3[NFrames];

        // Reset ghosts
        foreach (var pair in Ghosts) {
            if (pair.Key == CurrentCol) {
                pair.Value.SetActive(false);
                continue;
            }
            pair.Value.SetActive(true);
            pair.Value.transform.position = SpawnPoint.position;
        }

        Player.transform.position = SpawnPoint.position;
        Player.GetComponent<SpriteRenderer>().color = Cols[CurrentCol];
        Updating = false;
    }
}
