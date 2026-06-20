using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum Color
{
    Red,
    Yellow,
    Green,
    Blue
}

public class GameManager : MonoBehaviour
{
    public int maxTime = 30; 
    public int CurrentRoundIt {get; private set;}
    private Dictionary< Color, Vector3?[] > colorPositions;

    // Singleton
    public static GameManager Instance {get; private set;}

    void Start()
    {
        CurrentRoundIt = 0;
    }

    private void Awake() {
        // Singleton
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    void FixedUpdate() {
        foreach (var pair in colorPositions) {
            if (pair.Value[CurrentRoundIt] == null) { continue; }

            // update pair.key position using corresponding pair.value
        }

        ++CurrentRoundIt;
    }

    void ColorEnded(Color color, in Vector3?[] positions) {
        colorPositions[color] = positions;
        CurrentRoundIt = 0;
    }
}
