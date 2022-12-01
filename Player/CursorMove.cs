using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
 
        if(Physics.Raycast(ray, out RaycastHit raycastHit)){
            transform.position = raycastHit.point;
    
            Vector3 direction = transform.position - OlimarMain.olimarRigidbody.transform.position;
            Quaternion bounce =  Quaternion.LookRotation(new Vector3( raycastHit.normal.x, raycastHit.normal.y, raycastHit.normal.z), -direction) * Quaternion.Euler(90, 0, 0);
            transform.rotation = bounce;
        }
        
    }
}
