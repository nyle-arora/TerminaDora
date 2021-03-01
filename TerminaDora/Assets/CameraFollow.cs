using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform doraTransform;

    // Start is called before the first frame update
    void Start()
    {
        doraTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //we store current camera's position in variable temp
        Vector3 temp = transform.position; 
        //set camera's position x to be equal to player's position x
        temp.x = doraTransform.position.x; 
        temp.y = doraTransform.position.y; 

        //set back camera's temp posn to camera's current posn
        transform.position = temp; 
    }
}
