using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    private bool LeftInput => Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed;
    private bool RightInput => Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed;
    private bool UpInput => Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame;

    private float speed = 5f;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 input;
    private bool FacingForward;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
    }

    void Update() {
        input = Vector2.zero;

        if (RightInput) input.x += 1;
        if (LeftInput) input.x -= 1;
        if (UpInput) input.y += 1;
    }

    void FixedUpdate() {
        rb.linearVelocity = new Vector2(input.x * speed, rb.linearVelocity.y);
        float horizontalSpeed = Mathf.Abs(input.x);
        anim.SetFloat("Speed", horizontalSpeed);

        if (input.x > 0 && !FacingForward || input.x < 0 && FacingForward) {
            FlipCharacter();
        }
    }

    void FlipCharacter() {
        FacingForward = !FacingForward;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }
}
