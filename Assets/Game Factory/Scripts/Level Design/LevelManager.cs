using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [Header("Level Info")]
    [SerializeField] string levelName = "Level ";
    [SerializeField] int currentLevelIndex = 1;
    [Header("Scene Assets")]
    [SerializeField] StartPoint startPoint;
    [SerializeField] List<GameObject> paths;
    [SerializeField] List<EnemySpawnPoint> enemySpawnPoints;

    public int CurretLevelIndex
    {
        get { return currentLevelIndex; }
    }

    public string CurrentLevelName
    {
        get { return levelName + currentLevelIndex; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        Debug.Log($"Level Manager: Scene count {SceneManager.sceneCountInBuildSettings}");
    }
    void Start()
    {
        
    }

    public void ResetLevel()
    {
        //GameManager.instance.ResetGameManager();
        Destroy(GameManager.instance.player.gameObject);
        startPoint.SpawnPlayer();
       // UiManager.instance.UpdateReferences();
        Debug.Log("Level Manager: Reset Level");
    }


    public string NextLevel()
    {

        if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
            currentLevelIndex++;
        else
            currentLevelIndex = 1;

        Debug.Log($"Level Manager: Current level index {currentLevelIndex}");
        return levelName + currentLevelIndex;
    }

}
