using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    public float MoveSpeed = 5f;
    public float JumpForce = 12f;

    private bool LeftInput => Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed;
    private bool RightInput => Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed;
    private bool UpInput => Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame;

    private Rigidbody2D rb;
    private Animator anim;
    private int input;
    private bool Jumping;
    private bool Grounded;
    private bool FacingForward;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 

    }

    void Update() {
        input = 0;
        Grounded = Physics2D.OverlapBox(GroundCheck.position, GroundCheck.localScale, 0f, GroundLayer);

        if (RightInput) input += 1;
        if (LeftInput) input += -1;
        if (UpInput && Grounded) Jumping = true;

    }

    void FixedUpdate() {
        rb.linearVelocityX = input * MoveSpeed;
        anim.SetFloat("Speed", Mathf.Abs(input));

        if (Jumping) {
            rb.linearVelocityY = 0;
            rb.AddForce(new Vector2(0, JumpForce), ForceMode2D.Impulse);
            Jumping = false;
        }

        if (input > 0 && !FacingForward || input < 0 && FacingForward)
            FlipCharacter();
    }

    void FlipCharacter() {
        FacingForward = !FacingForward;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Jump")) {
            JumpPad pad = other.gameObject.GetComponent<JumpPad>();

            rb.linearVelocityY = 0;
            rb.AddForce(new Vector2(0, pad.Force), ForceMode2D.Impulse);
            pad.Activate();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Jump")) {
            JumpPad pad = other.gameObject.GetComponent<JumpPad>();
            
            StartCoroutine(pad.CloseStaple());
        }
    }
}