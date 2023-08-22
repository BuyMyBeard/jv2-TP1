using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputs))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 1;
    [SerializeField] float verticalMinClamp = -15;
    [SerializeField] float verticalMaxClamp = 90;
    [SerializeField] float walkSpeed = 10;
    [SerializeField] float runMultiplier = 3;
    [SerializeField] float jumpImpulse = 20;
    [SerializeField] float groundCheckDistance = 1;
    [SerializeField] LayerMask groundLayer;
    float verticalRotation = 0;
    PlayerInputs inputs;
    Rigidbody rb;
    Camera cam;
    CapsuleCollider cc;

    bool isGrounded = false;

    Vector3 GroundCheckOrigin
    {
        get => transform.position + new Vector3(0, -cc.height / 2, 0);
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = GetComponent<PlayerInputs>();
        cc = GetComponent<CapsuleCollider>();
        cam = Camera.main;
    }

    void Update()
    {
        Vector2 delta = mouseSensitivity * Time.deltaTime * inputs.LookDelta;
        float prevVerticalRotation = verticalRotation;
        verticalRotation -= delta.y;
        verticalRotation = Mathf.Clamp(verticalRotation, verticalMinClamp, verticalMaxClamp);
        cam.transform.RotateAround(transform.position, transform.TransformDirection(Vector3.right), verticalRotation - prevVerticalRotation) ;
        isGrounded = Physics.Raycast(GroundCheckOrigin, Vector3.down, groundCheckDistance, groundLayer);
        transform.Rotate(Vector3.up * delta.x);
        //Vector3 lookDirection = new Vector3(0, cam.transform.eulerAngles.y - 6.72f, 0);
        //transform.eulerAngles = lookDirection;
        Vector3 movement = new Vector3(inputs.MoveInput.x, 0, inputs.MoveInput.y);
        if (inputs.RunInput && movement.z > 0)
            movement.z *= runMultiplier;

        movement = cam.transform.forward * movement.z + cam.transform.right * movement.x;

        movement *= walkSpeed;
        movement.y = rb.velocity.y;

        if (inputs.JumpPress && isGrounded)
            movement.y = jumpImpulse;
        Debug.Log(rb.velocity);
        rb.velocity = movement;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(GroundCheckOrigin, GroundCheckOrigin + Vector3.down * groundCheckDistance);
    }
}
