using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CursorMove : MonoBehaviour
{
    [SerializeField]
    private TagInit _tagcheck;

    public static Camera cam;
    public LayerMask cursorMask;
    
    public GameObject pikminCursor;
    public static GameObject setTarget;

    bool noTarget = false;

    bool inTargetRange = false;
    Vector3 cursorPointAwayTarget;
    
    void Awake(){
        cam = Camera.main;
    }
    
    void Update()
    {
        bool rayHit = Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out RaycastHit raycastHit, Mathf.Infinity, ~cursorMask);

        if(rayHit && noTarget){
            transform.position = raycastHit.point;
        }

        if(inTargetRange && OlimarMain.olimarControl.Player.Target.inProgress){
            
            bool olimarToTarget = Physics.Raycast(OlimarMain.olimarRigidbody.transform.position, cursorPointAwayTarget, out RaycastHit rayFromOliHit, Mathf.Infinity, ~cursorMask);
            if(olimarToTarget){
                transform.position = rayFromOliHit.point;
                noTarget = false;
            }
        }else{
            setTarget = pikminCursor;   
            noTarget = true;
        }

        cursorPointAwayTarget = transform.position - OlimarMain.olimarRigidbody.transform.position;
        if(noTarget){
            Quaternion bounce = Quaternion.LookRotation(new Vector3( raycastHit.normal.x, raycastHit.normal.y, raycastHit.normal.z), -cursorPointAwayTarget) * Quaternion.Euler(90, 0, 0);
            transform.rotation = bounce;
        }
        

    }

    private void OnTriggerExit(Collider collider){
        inTargetRange = false;
    }

    private void OnTriggerStay(Collider collission){
        inTargetRange = true;
        
        if(collission.TryGetComponent<TagTester>(out var TagName)){
            if(TagName.HasTag("ENEMY")){
                setTarget = collission.gameObject;
            }
        }
    }
}
