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
    [field: SerializeField] public int MaxTime {get; private set;} = 30;
    [field: SerializeField] public float FPS {get; private set;} = 30.0f;
    [field: SerializeField] public Vector3 RespawnPoint {get; private set;}

    public int CurrentRoundIt {get; private set;}
    private Dictionary< Color, Vector3?[] > Positions;

    // Singleton
    public static GameManager Instance {get; private set;}

    void Start()
    {
        Positions = new Dictionary<Color, Vector3?[]>();
        CurrentRoundIt = 0;

        Time.fixedDeltaTime = 1.0f / FPS;
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
