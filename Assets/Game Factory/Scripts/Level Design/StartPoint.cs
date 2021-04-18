using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{

    private void Awake()
    {
        SpawnPlayer();
    }

    // Start is called before the first frame update
    void Start()
    {
        // LevelManager.instance.levelStartPoint = this.transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer()
    {
        Instantiate(GameManager.instance.PlayerPrefab, transform.position, GameManager.instance.PlayerPrefab.transform.rotation);
    }
}
