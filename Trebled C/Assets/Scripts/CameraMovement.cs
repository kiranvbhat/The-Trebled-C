using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cameraObj;
    
    public void UpdateCamera(Vector3 cameraPos) {
        cameraObj.transform.position = cameraPos;
    }
}
