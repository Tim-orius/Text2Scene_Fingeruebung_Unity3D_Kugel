using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour{
    
    public GameObject playerObject;
    private Vector3 cameraOffset;

    // Start is called before the first frame update
    void Start(){
        cameraOffset = this.transform.position - playerObject.transform.position;
    }

    // Update is called once per frame
    void Update(){
        //Rotation
        float rotation =0;
        if (Input.GetKey (KeyCode.Q))
            rotation -= 1;
        if (Input.GetKey (KeyCode.E))
            rotation += 1;

        cameraOffset = Quaternion.Euler(0, rotation, 0) * cameraOffset;

        this.transform.position = playerObject.transform.position + cameraOffset;
        transform.LookAt(playerObject.transform.position);
    }

    void LateUpdate(){
        this.transform.position = playerObject.transform.position + cameraOffset;
        transform.LookAt(playerObject.transform.position);
    }
}
