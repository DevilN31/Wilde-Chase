using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    public List<GameObject> enemiesInLevel;
    public GameObject player;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] bool isPlayerReachedEnd;
    [SerializeField] bool isFirstRun = true;
    [SerializeField] string levelName = "Level ";
    [SerializeField] int currentLevelIndex = 1;
    [SerializeField] string currentLevelName;

    public float TestAverageAngle = 0;
    public float TempAngle;

    public PlayerController PlayerController
    {
        get { return player.GetComponent<PlayerController>(); }
    }

    public bool IsPlayerReachedEnd
    {
        get { return isPlayerReachedEnd; }
    }

    public GameObject PlayerPrefab
    {
        get { return playerPrefab; }
    }

    public string CurrentLevelName
    {
        get { return currentLevelName; }
    }
    private void OnEnable()
    {
        SceneManager.sceneUnloaded += ResetGameManager;
        

    }

    private void OnDisable()
    {
        SceneManager.sceneUnloaded -= ResetGameManager;
    }

    private void Awake()
    {
        
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        
        if (DataManager.instance.GetPlayerPrefsBool("FirstRun") != -1)
        {
            if (DataManager.instance.GetPlayerPrefsBool("FirstRun") == 1)
                isFirstRun = true;
            else
                isFirstRun = false;
        }

        if (isFirstRun)
        {
            
            DataManager.instance.ResetPlayerPrefs();
            isFirstRun = false;
            DataManager.instance.SetPlayerPrefsBool("FirstRun", isFirstRun);
        }
        currentLevelName = levelName + currentLevelIndex;
        Time.timeScale = 1;

    }
    void Start()
    {

    }

    
    void Update()
    {
        if (enemiesInLevel.Count <= 0)
        {
            UiManager.instance.StartFadeIn();
        }

        if(enemiesInLevel.Count > 0)
        {
            TempAngle = 0;
            foreach(GameObject enemy in enemiesInLevel)
            {
               TempAngle += enemy.GetComponentInChildren<EnemyAnimationController>().AngleToTarget;
            }
            TestAverageAngle = TempAngle / enemiesInLevel.Count;           
        }
    }

    public void PlayerReachedEnd()
    {
        Debug.Log("PLAYER REACHED END");
        isPlayerReachedEnd = true;
        foreach(GameObject enemy in enemiesInLevel)
        {
            enemy.GetComponentInParent<EnemyAiTesti>().StartCoroutine(enemy.GetComponentInParent<EnemyAiTesti>().StopHorse());
        }
        PlayerController.StartCoroutine("PlayerEndDeath");
    }

    public void ResetGameManager(Scene scene)
    {
        isPlayerReachedEnd = false;
        enemiesInLevel.Clear();
        UiManager.instance.DisabledScripts.Clear();
    }

    public string NextLevel()
    {
        if (currentLevelIndex < SceneManager.sceneCountInBuildSettings)
            currentLevelIndex++;
        else
            currentLevelIndex = 1;

        Debug.Log($"Level Manager: Current level index {currentLevelIndex}");
        currentLevelName = levelName + currentLevelIndex;
        return currentLevelName;
    }
}
