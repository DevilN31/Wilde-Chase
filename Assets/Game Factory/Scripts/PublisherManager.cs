using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YsoCorp.GameUtils;
using Game_Factory.Scripts.MeliorGames.LevelManagement.Progress;
using Game_Factory.Scripts.MeliorGames.Infrastructure;

public class PublisherManager : MonoBehaviour
{
    [SerializeField] LevelContainer LevelContainer;

    private void Awake()
    {
        foreach(Level level in LevelContainer.Levels)
        {
            level.Finished += FinishedLevelUpdate;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void FinishedLevelUpdate(Level level)
    {
        YCManager.instance.OnGameFinished(true);
    }
}
