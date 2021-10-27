using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemRotate : MonoBehaviour{

    public float speed;
    private Vector3 rotation;

    // Start is called before the first frame update
    void Start(){
        rotation = new Vector3(45,15,90);
    }

    // Update is called once per frame
    void Update(){
        this.transform.Rotate(rotation * speed * Time.deltaTime);
    }
}
