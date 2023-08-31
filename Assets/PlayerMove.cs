using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1;
    [SerializeField] float camMinClamp = -15;
    [SerializeField] float camMaxClamp = 90;
    [SerializeField] float walkSpeed = 10;
    [SerializeField] float runMultiplier = 3;
    [SerializeField] float jumpImpulse = 20;
    [SerializeField] float groundCheckDistance = 1;
    [SerializeField] Vector3 gravity = Physics.gravity;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform head;

    PlayerInputs inputs;
    Camera cam;
    CharacterController characterController;
    CapsuleCollider cc;
    Animator animator;

    Vector3 velocity = Vector3.zero;
    float verticalRotation = 0;
    bool isGrounded = false;

    Vector3 GroundCheckOrigin
    {
        get => transform.position + new Vector3(0, (-cc.height / 2) + 0.05f, 0);
    }

    // Start is called before the first frame update
    void Awake()
    {
        characterController = GetComponent< CharacterController>();
        inputs = GetComponent<PlayerInputs>();
        cc = GetComponent<CapsuleCollider>();
        cam = Camera.main;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        MoveCamera();
        MoveCharacter();
    }

    private void MoveCharacter()
    {
        velocity += gravity * Time.deltaTime;

        Vector3 movement = new(inputs.MoveInput.x, 0, inputs.MoveInput.y);
        if (inputs.RunInput && movement.z > 0)
        {
            movement.z *= runMultiplier;
            animator.SetBool("IsRunning", true);
        }
        else
            animator.SetBool("IsRunning", false);

        animator.SetBool("IsWalking", movement.z != 0);

        animator.SetBool("IsStrafingLeft", movement.x < 0 && movement.z == 0);
        animator.SetBool("IsStrafingRight", movement.x > 0 && movement.z == 0);

        movement = (movement.z * transform.forward + movement.x * transform.right) * -1;

        animator.SetBool("IsJumping", !characterController.isGrounded);
        if (inputs.JumpPress && characterController.isGrounded)
            velocity.y = jumpImpulse;

        characterController.Move((walkSpeed * movement + velocity) * Time.deltaTime);
    }

    private void MoveCamera()
    {
        Vector2 delta = mouseSensitivity * Time.deltaTime * inputs.LookDelta;
        float prevVerticalRotation = verticalRotation;
        verticalRotation -= delta.y;
        verticalRotation = Mathf.Clamp(verticalRotation, camMinClamp, camMaxClamp);
        head.transform.Rotate(new Vector3(verticalRotation - prevVerticalRotation, 0, 0));
        transform.Rotate(Vector3.up * delta.x);
    }
}
