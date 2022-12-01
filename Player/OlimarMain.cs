using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OlimarMain : MonoBehaviour
{
    public static Rigidbody olimarRigidbody;
    private PlayerInput playerInput;
    private OlimarControl olimarControl;
    private Vector2 inputVector;
    void Start()
    {
      olimarRigidbody = GetComponent<Rigidbody>();
      playerInput = GetComponent<PlayerInput>();
      olimarControl = new OlimarControl();
      olimarControl.Player.Enable();
    }

    private void Update(){
      Vector2 inputVector= olimarControl.Player.WASD.ReadValue<Vector2>();
      olimarRigidbody.velocity = new Vector3(inputVector.x, olimarRigidbody.velocity.y, inputVector.y);
    }
}
