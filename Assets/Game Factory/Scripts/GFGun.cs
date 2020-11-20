using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFGun : MonoBehaviour
{
    Aim aim;

    void Start()
    {
        aim = GetComponentInParent<Aim>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(aim.AimTarget);
        Debug.DrawRay(transform.position, transform.forward * 25, Color.red);
    }
}
