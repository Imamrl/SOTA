using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraObject;
    public Transform pivot;
    float lerpSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lerpSpeed = (transform.position - cameraObject.position).magnitude * 20f;
        transform.position = Vector3.Lerp(transform.position, cameraObject.position, Time.deltaTime*lerpSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, cameraObject.rotation, Time.deltaTime*lerpSpeed*2);
    }
    /*public void RotateZ(float rotation) 
    {
        pivotRot = rotation;
        pivotRot = Mathf.Clamp(pivotRot, -40, 40);
        pivot.transform.rotation *= Quaternion.AxisAngle(pivot.transform.right, pivotRot);
    }*/
}
