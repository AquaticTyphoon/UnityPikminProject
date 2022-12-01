using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OlimarMain : MonoBehaviour
{
    public static Rigidbody olimarRigidbody;
    private PlayerInput playerInput;
    private OlimarControl olimarControl;
    void Start()
    {
      olimarRigidbody = GetComponent<Rigidbody>();
      playerInput = GetComponent<PlayerInput>();
      olimarControl = new OlimarControl();
      olimarControl.Player.Enable();

    }

    private void Update(){
      Vector2 inputVector= olimarControl.Player.WASD.ReadValue<Vector2>();
      float speed = 1f;
      olimarRigidbody.velocity = new Vector3(inputVector.x, olimarRigidbody.velocity.y, inputVector.y);
    }
}
