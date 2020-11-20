using MalbersAnimations;
using MalbersAnimations.Controller;
using MalbersAnimations.HAP;
using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiTesti : MonoBehaviour
{

    public GameObject aimAtTarget;
    [SerializeField] bool isGunEquipped;
    [SerializeField] Camera aiCamera;
    [SerializeField] LayerMask layerMask;

    RiderCombat riderCombat;
    Aim aim;
    MInventory inventory;
    MAnimalAIControl animalAIControl;

    // Start is called before the first frame update
    void Start()
    {
        aimAtTarget = GameObject.FindGameObjectWithTag("Player");
        riderCombat = GetComponentInChildren<RiderCombat>();
        aim = GetComponentInChildren<Aim>();
        animalAIControl = GetComponentInChildren<MAnimalAIControl>();
        aim.MainCamera = aiCamera;
        inventory = GetComponentInChildren<MInventory>();
        isGunEquipped = false;    
    }

    // Update is called once per frame
    void Update()
    {

        aiCamera.transform.forward = aimAtTarget.transform.forward;

        if(Vector3.Distance(transform.position,aimAtTarget.transform.position) < 20f)
        {
      
            riderCombat.AimMode();
            if (!riderCombat.Aim)
                riderCombat.SetAim(true);
            riderCombat.Aimer.AimOrigin = aimAtTarget.transform;
            aim.AimOrigin = aimAtTarget.transform;
           
            
            if (!isGunEquipped)
            {
                inventory.EquipItem(2);
                riderCombat.Change_Weapon_Holder_Right();
                isGunEquipped = true;
            }


            // riderCombat.MainAttack();
            // riderCombat.MainAttackReleased();
            //riderCombat.ReloadWeapon();
            Debug.Log($"{name} is aiming at {aimAtTarget.name}, distance {Vector3.Distance(transform.position, aimAtTarget.transform.position)} ");

        }
    }

    public void SetClosestWaypoint()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 15f, transform.forward, out hit,15f, layerMask,QueryTriggerInteraction.UseGlobal))
        {
            Debug.Log($"Sphere cast: {hit.transform.name}");
            if (hit.collider.gameObject.GetComponent<MWayPoint>())
            {
                animalAIControl.SetTarget(hit.collider.gameObject);
                Debug.Log($"{name} closest Waypoint is: {hit.collider.name}");
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 15f);
        
    }
}
