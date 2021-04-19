using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    private void Awake()
    {
        SpawnPlayer();
    }

    public void SpawnPlayer()
    {
        Instantiate(GameManager.instance.PlayerPrefab, transform.position, GameManager.instance.PlayerPrefab.transform.rotation);
    }
}
