using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Vector3 setPosition;
    public Vector3 setRotaion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveToTransform()
    {
        this.transform.position = setPosition;
        this.transform.rotation = Quaternion.Euler(setRotaion);
    }
}
