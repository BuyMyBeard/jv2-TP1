using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputs : MonoBehaviour
{
    [SerializeField] float jumpBuffer = 0.1f;
    ActionMap inputs;
    InputAction runAction, jumpAction;
    public Vector2 MoveInput
    {
        get => inputs.PlayerControls.Move.ReadValue<Vector2>();
    }
    public Vector2 LookDelta
    {
        get => inputs.PlayerControls.Look.ReadValue<Vector2>();
    }

    public bool RunInput { get; private set; } = false;
    public bool JumpPress { get; private set; } = false;

    public bool JumpHold { get; private set; } = false;
    void Awake()
    {
        inputs = new ActionMap();
        runAction = inputs.FindAction("Run");
        jumpAction = inputs.FindAction("Jump");
    }

    private void OnEnable()
    {
        inputs.Enable();
        runAction.started += (_) => RunInput = true;
        runAction.canceled += (_) => RunInput = false;
        jumpAction.started += (_) => { StartCoroutine(BufferJump()); JumpHold = true; };
        jumpAction.canceled += (_) => JumpHold = false;
    }

    private IEnumerator BufferJump()
    {
        JumpPress = true;
        yield return new WaitForSeconds(jumpBuffer);
        JumpPress = false;
    }

}
