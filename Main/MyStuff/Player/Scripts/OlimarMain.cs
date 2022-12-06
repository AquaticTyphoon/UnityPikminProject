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
    public static OlimarControl olimarControl;
    public static Vector2 inputVector;

    public GameObject olimarCam;
    public Vector3 camOffset;

    public bool controlEnabled = true;

    public bool strafeControls = false;

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
      olimarCam.transform.position = Vector3.SlerpUnclamped(olimarCam.transform.position, newPos, 0.03f);

      Vector3 localTarget = new Vector3(inputVector.x, 0, inputVector.y) * (speed * 2.2f);

      float forceApplied = 1f;

      if(controlEnabled == true){
        if(strafeControls && CursorMove.setTarget.transform.position != Vector3.zero){
            olimarRigidbody.AddForce(transform.TransformDirection(localTarget) / 1.2f);
      
        }else{
            olimarRigidbody.AddForce(localTarget * forceApplied);
        }
      }

      

    }

    private void Update(){
      if(controlEnabled == true){
          inputVector= olimarControl.Player.WASD.ReadValue<Vector2>();


          if(olimarControl.Player.Target.inProgress){
            strafeControls = true;
          }else{
            strafeControls = false;
          }

          if(olimarControl.Player.Interact.inProgress & CursorMove.setTarget != null){
            speed = Mathf.Clamp(cursorToOlimarDistance * 2, 1.3f, 1.45f);
          }else{
            speed = 1.5f;
          }

          cursorToOlimarDistance = Vector3.Distance (pikminCursor.transform.position, olimarRigidbody.transform.position);
          Vector3 target = new Vector3(pikminCursor.transform.position.x, transform.position.y, pikminCursor.transform.position.z);

          
          if(strafeControls){
              transform.LookAt(target);
          }

          if(cursorToOlimarDistance >= 0.2f && inputVector != Vector2.zero && (pikminCursor.transform.rotation.normalized.y  != transform.rotation.normalized.y)){
            if(!strafeControls){
              transform.LookAt(transform.position + new Vector3(inputVector.x, transform.position.y, inputVector.y), Vector3.up);
            }
          }
        
        if(olimarControl.Player.Interact.inProgress){
          if(cursorToOlimarDistance >= 0.125f && (pikminCursor.transform.rotation.normalized.y  != transform.rotation.normalized.y)){
            transform.LookAt(target);

          }    
        }
      }
      
      animator.SetFloat("Speed", speed);
      if(inputVector != Vector2.zero){
        animator.SetBool("IsMoving", true);
      }else{
        animator.SetBool("IsMoving", false);
      }
    }
    
}
