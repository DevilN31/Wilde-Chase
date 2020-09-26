using MalbersAnimations;
using MalbersAnimations.HAP;
using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiTesti : MonoBehaviour
{

    public GameObject aimAtTarget;
    RiderCombat riderCombat;
    Aim aim;
    MInventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        riderCombat = GetComponent<RiderCombat>();
        aim = GetComponent<Aim>();
        inventory = GetComponent<MInventory>();
        inventory.EquipItem(2);
       // riderCombat.Change_Weapon_Holder_Right();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,aimAtTarget.transform.position) < 20f)
        {
            riderCombat.AimMode();

            if(aim.AimOrigin == null)
            aim.SetTarget(aimAtTarget.transform);

            riderCombat.SetAim(true);
            
           // riderCombat.MainAttack();
            Debug.Log($"{name} is aiming at {aimAtTarget.name}, distance {Vector3.Distance(transform.position, aimAtTarget.transform.position)} ");

        }
    }
}
