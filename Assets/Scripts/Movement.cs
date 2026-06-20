using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour {
    public Transform GroundCheck;
    public LayerMask GroundLayer;
    public float MoveSpeed = 5f;
    public float JumpForce = 12f;

    private IInteractable Interactable; 
    private bool InteractInput => Keyboard.current.eKey.wasPressedThisFrame;
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
        if (LeftInput) input -= 1;
        if (UpInput && Grounded) Jumping = true;

        if (InteractInput && Interactable != null) {
            GameManager.Instance.AddInteraction(Interactable.Interact);
            Interactable.Interact();
        }
    }

    void FixedUpdate() {
        rb.linearVelocity = new Vector2(input * MoveSpeed, rb.linearVelocity.y);
        float horizontalSpeed = Mathf.Abs(input);
        anim.SetFloat("Speed", horizontalSpeed);

        if (Jumping) {
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

    private void OnTriggerEnter2D(Collider2D collision) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null) {
            Interactable = interactable;
        }
        if (collision.CompareTag("Respawn")) {
            string NextLvl = "lv" + (int.Parse(SceneManager.GetActiveScene().name[2..]) + 1);
            SceneManager.LoadScene(NextLvl);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (Interactable != null && interactable == Interactable) {
            Interactable = null;
        }
    }
}
