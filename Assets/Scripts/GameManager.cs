using System.Collections.Generic;
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
    private Dictionary< Color, List<Vector3> > colorPositions;

    void Start() {
        colorPositions = new Dictionary<Color, List<Vector3>>(4);
    }

    void Update() {
        foreach (var pair in colorPositions) {
            // update pair.key position using corresponding pair.value
        }
    }

    void ColorEnded(Color color, in List<Vector3> positions) {
        colorPositions[color] = positions;
    }
}
