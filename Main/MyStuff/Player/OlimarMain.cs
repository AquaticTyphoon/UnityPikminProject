using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OlimarMain : MonoBehaviour
{   float speed;
    private Animator animator;
    public GameObject olimarModelObject;
    public static float cursorToOlimarDistance;
    public GameObject pikminCursor;
    public static Rigidbody olimarRigidbody;
    private PlayerInput playerInput;
    public OlimarControl olimarControl;
    public static Vector2 inputVector;

    public GameObject olimarCam;
    public Vector3 camOffset;

    void Start()
    {
      animator = GetComponent<Animator>();
      olimarRigidbody = GetComponent<Rigidbody>();
      playerInput = GetComponent<PlayerInput>();
      olimarControl = new OlimarControl();
      olimarControl.Player.Enable();

      olimarCam.transform.position = olimarRigidbody.position + olimarCam.transform.position;
      camOffset = olimarCam.transform.position - olimarRigidbody.position;
    }

    void LateUpdate(){
      Vector3 newPos = olimarRigidbody.position + camOffset;
      olimarCam.transform.position = Vector3.SlerpUnclamped(olimarCam.transform.position, newPos, 0.015f);
    }

    private void Update(){

      inputVector= olimarControl.Player.WASD.ReadValue<Vector2>();
      Vector3 localTarget = new Vector3(inputVector.x, olimarRigidbody.velocity.y, inputVector.y) * speed;

      olimarRigidbody.velocity =  transform.TransformDirection(localTarget);

      cursorToOlimarDistance = Vector3.Distance (pikminCursor.transform.position, olimarRigidbody.transform.position);

      if(cursorToOlimarDistance >= 0.2f && inputVector != Vector2.zero && (pikminCursor.transform.rotation.normalized.y  != transform.rotation.normalized.y)){
        Vector3 target = new Vector3(pikminCursor.transform.position.x, transform.position.y, pikminCursor.transform.position.z);
        transform.LookAt(target);

      }

      speed = Mathf.Clamp(cursorToOlimarDistance, 0.7f, 1.5f);

      animator.SetFloat("Speed", speed);

      if(inputVector != Vector2.zero){
        animator.SetBool("IsMoving", true);
      }else{
        animator.SetBool("IsMoving", false);
      }
      
      

      
    }
    
}
