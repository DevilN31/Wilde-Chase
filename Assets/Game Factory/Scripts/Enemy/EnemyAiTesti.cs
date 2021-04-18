using MalbersAnimations;
using MalbersAnimations.Controller;
using MalbersAnimations.HAP;
using MalbersAnimations.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EnemyAiTesti : MonoBehaviour
{

    [SerializeField] LayerMask layerMask;
    [SerializeField] float sphereCastRadius = 15f;
    [SerializeField] bool isHorseRunning = false;
    [SerializeField] bool isRestartedAi = false;

    RiderCombat riderCombat;
    MAnimalAIControl animalAIControl;
    MAnimal mAnimal;
    MRider rider;

    public float SphereCastRadius
    {
        get { return sphereCastRadius; }
    }

    public bool IsHorseRunning
    {
        get { return isHorseRunning; }
    }

    void Start()
    {
        riderCombat = GetComponentInChildren<RiderCombat>();
        animalAIControl = GetComponentInChildren<MAnimalAIControl>();
        mAnimal = GetComponentInChildren<MAnimal>();
        rider = GetComponentInChildren<MRider>();
         //animalAIControl.enabled = false;
      //  SetClosestWaypoint();
    }
    
    private void Update()
    {
        if (GameManager.instance != null)
        {
            if (Vector3.Distance(GameManager.instance.player.transform.position, transform.position) <= sphereCastRadius && animalAIControl.Target == null)
            {
                animalAIControl.enabled = true;
                SetClosestWaypoint();
            }

            if (Vector3.Distance(GameManager.instance.player.transform.position, transform.position) > sphereCastRadius * 3 && animalAIControl.Target != null && isHorseRunning)
            {

                 StartCoroutine(ResetAnimalAi());
            }
        }

    }
     

    public void SetClosestWaypoint()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, sphereCastRadius, transform.forward, out hit,100f, layerMask,QueryTriggerInteraction.UseGlobal))
        {

            if (hit.collider.gameObject.GetComponent<MWayPoint>())
            {
                animalAIControl.SetTarget(hit.collider.gameObject.transform,true);
                animalAIControl.enabled = true;
                Debug.Log($"{name} closest Waypoint is: {hit.collider.name}");
            }
        }
        else
        {
            Debug.Log($"EnemyAiTest: {name} can't find closest waypoint, hit: {hit.collider.name}");
        }
    }

    public void SpeedUpHorse(float speed)
    {
        isHorseRunning = true;
        mAnimal.SpeedSet_Get("Ground").Speeds[2].Vertical.Value += speed;
    }
    public void SlowDownHorse(float speed)
    {
        mAnimal.SpeedSet_Get("Ground").Speeds[2].Vertical.Value -= speed;
        isHorseRunning = false;
    }

    public IEnumerator StopHorse()
    {
        while (Vector3.Distance(transform.position, GameManager.instance.player.transform.position) > sphereCastRadius / 2)
            yield return null;

        mAnimal.SpeedSet_Get("Ground").Speeds[2].Vertical.Value = 0;
        animalAIControl.enabled = false;
    }

    public void StartHorseRunAway()
    {
        StartCoroutine(HorseRunAway());
    }

     IEnumerator HorseRunAway()
    {
        yield return null;

        animalAIControl.enabled = false;

        Collider[] test = mAnimal.gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider co in test)
        {
            co.isTrigger = true;
        }
        SpeedUpHorse(10);
        if ((mAnimal.transform.position - GameManager.instance.player.transform.position).normalized.z >= 0)
        {
            mAnimal.gameObject.transform.Rotate(Vector3.up * 50);
        }
        else
        {
            mAnimal.gameObject.transform.Rotate(Vector3.up * -50);
        }
        Destroy(this.gameObject, 2f);
    }

    public void UnmountHorse()
    {
        rider.DismountAnimal();
    }

    IEnumerator ResetAnimalAi()
    {
        isRestartedAi = true;
        animalAIControl.enabled = false;
        yield return new WaitForSeconds(0.2f);
        animalAIControl.enabled = true;
    }

#if UNITY_EDITOR    
    private void OnDrawGizmos()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, Vector3.up, sphereCastRadius);
        
    }
#endif
}
