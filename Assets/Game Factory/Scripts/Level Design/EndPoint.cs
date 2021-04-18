using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPoint : MonoBehaviour
{
    Transform playerTransform;

    void Start()
    {

    }

    void Update()
    {
        if(playerTransform == null)
            playerTransform = GameManager.instance.player.transform;

        if (Vector3.Distance(transform.position,playerTransform.position) <= 10 && playerTransform != null)
        {
            if (!GameManager.instance.IsPlayerReachedEnd)
            {
                GameManager.instance.PlayerReachedEnd();
            }
        }
    }
}
