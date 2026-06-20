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
    private Dictionary< Color, Vector3?[] > Positions;
    public Vector3 RespawnPoint {get; private set;}

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
        foreach (var pair in Positions) {
            if (pair.Value[CurrentRoundIt] == null) { continue; }

            // update pair.key position using corresponding pair.value
            // pair.Key.transform.position = (Vector3)(pair.Value[CurrentRoundIt]);
        }

        ++CurrentRoundIt;
    }

    public void ResetColor(Color color, in Vector3?[] positions) {
        Positions[color] = positions;
        CurrentRoundIt = 0;
    }

}
