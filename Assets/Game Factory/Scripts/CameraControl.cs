using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector3 setPosition;
    public Vector3 setRotaion;

    public void MoveToTransform()
    {
        Debug.Log("Camera Control: Move");
        transform.parent = null;
        this.transform.position = setPosition;
        this.transform.rotation = Quaternion.Euler(setRotaion);
    }
}
