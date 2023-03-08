using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //the players transform that follows player, shows in inspector
    public Transform followTransform;


    // Update is called once per frame
    void FixedUpdate()
    {
        //x and y axis for the 2d space, 
        this.transform.position = new Vector3(followTransform.position.x, followTransform.position.y, this.transform.position.z);


    }
}