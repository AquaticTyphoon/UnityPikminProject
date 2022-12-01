using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CursorMove : MonoBehaviour
{
    Camera cam;
    public LayerMask playerMask;

    void Awake(){
        cam = Camera.main;
    }
    
    void Update()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
 
        if(Physics.Raycast(ray, out RaycastHit raycastHit, playerMask)){
            transform.position = raycastHit.point;
    
            Vector3 direction = transform.position - OlimarMain.olimarRigidbody.transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
        
    }
}
