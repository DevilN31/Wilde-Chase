using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] List<MonoBehaviour> disableScripts;
    [SerializeField] GameObject touchFieldPanel;
    [SerializeField] Text levelNameText;
    [SerializeField] Text versionText;
    public Toggle slingshotToggle;
    [Header("Settings")]
    [SerializeField] float maxSensetivity = 3;
    [SerializeField] float minSensetivity = 0.5f;
    [SerializeField] Slider sensetivitySlider;
    [SerializeField] Text sensetivityText;
    [Header("Fade In/Out Effect")]
    [SerializeField] Image fadeImage;
    [SerializeField] float fadeEffectDuration;
    [SerializeField] float jumpInterval;
    [SerializeField] bool isResetLevel = false;

    public FixedTouchField TouchField
    {
        get { return touchFieldPanel.GetComponent<FixedTouchField>(); }
    }

    public GameObject MenuPanel
    {
        get { return menuPanel; }
    }

    public List<MonoBehaviour> DisabledScripts
    {
        get { return disableScripts; }
    }
    private void OnEnable()
    {
         SceneManager.sceneLoaded += UpdateReferences;
    }

    private void OnDisable()
    {
         SceneManager.sceneLoaded -= UpdateReferences;
    }

    
    private void Awake()
    {    
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

    }
    

    private void Start()
    {
        // UpdateReferences();
        // StartFadeOut();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // For PC tests
        {
            ShowMenu();
        }

        sensetivityText.text = sensetivitySlider.value.ToString();
    }

    void UpdateReferences(Scene scene,LoadSceneMode mode) // Find all references in scene after scene loaded
    {
        
        AddListenersToMenuButtons();
        fadeImage = GameObject.Find("FadeImage").GetComponent<Image>();
        StartFadeOut();
        menuPanel = GameObject.Find("MenuPanel");
        settingsPanel = GameObject.Find("SettingsPanel");
        touchFieldPanel = GameObject.Find("TouchFieldPanel");
        sensetivitySlider = GameObject.Find("SensetivitySlider").GetComponentInChildren<Slider>();
        sensetivityText = GameObject.Find("SensetivityText").GetComponent<Text>();
        slingshotToggle = GameObject.Find("SlingshotControllToggle").GetComponent<Toggle>();
        levelNameText = GameObject.Find("LevelNameText").GetComponent<Text>();
        versionText = GameObject.Find("VersionText").GetComponent<Text>();

        sensetivitySlider.onValueChanged.AddListener(delegate { OnSensetivitySliderValueChange(); });
        slingshotToggle.onValueChanged.AddListener(delegate { OnSlingshotToggleValueChanged(); });
        levelNameText.text = GameManager.instance.CurrentLevelName;
        versionText.text = $"Version: {Application.version}";
        menuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        

        if (DataManager.instance.GetPlayerPrefsfloat("Sensetivity") != -1f)
        {
            sensetivitySlider.value = DataManager.instance.GetPlayerPrefsfloat("Sensetivity");
            GameManager.instance.PlayerController.ChangeSensetivity(sensetivitySlider.value);

        }
        else
        {
            GameManager.instance.PlayerController.ChangeSensetivity(1f);
        }

        if (DataManager.instance.GetPlayerPrefsBool("SlingshotControl") != -1)
        {
            if (DataManager.instance.GetPlayerPrefsBool("SlingshotControl") == 1)
                slingshotToggle.isOn = true;
            else
                slingshotToggle.isOn = false;
        }
    }

    void AddListenersToMenuButtons()
    {
        Button resume = GameObject.Find("ResumeButton").GetComponent<Button>();
        resume.onClick.AddListener(ResumeGame);

        Button openMenu = GameObject.Find("OpenMenuButton").GetComponent<Button>();
        openMenu.onClick.AddListener(ShowMenu);

        Button quitGame = GameObject.Find("QuitGameButton").GetComponent<Button>();
        quitGame.onClick.AddListener(QuitGame);

        Button resetLevel = GameObject.Find("ResetLevelButton").GetComponent<Button>();
        resetLevel.onClick.AddListener(ResetLevel);
    }

    public void ShowMenu()
    {
        Time.timeScale = 0;
        menuPanel.SetActive(true);
        DisableScripts(false);
    }

    public void ResumeGame()
    {
        menuPanel.SetActive(false);
        DisableScripts(true);
        Time.timeScale = 1;
    }

    public void ResetLevel()
    {
        isResetLevel = true;
        StartFadeIn();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void AddSelfToDisabledList(MonoBehaviour script) // Used for other scripts to add them to disableScripts list
    {
        disableScripts.Add(script);
    }

    void DisableScripts(bool isEnabled) // disables/enables all the scripts in disableScripts list
    {
        foreach(MonoBehaviour script in disableScripts)
        {
            script.enabled = isEnabled;
        }
    }

    void OnSensetivitySliderValueChange() // updates sensetivity in player
    {
        if (GameManager.instance.PlayerController != null)
        {
            GameManager.instance.PlayerController.ChangeSensetivity(sensetivitySlider.value);
            string temp = string.Format("{0:f1}", GameManager.instance.PlayerController.Sensetivity);
            Debug.Log($"Sensetivity: {temp}");
            sensetivityText.text = temp;
        }
           
    }

    void OnSlingshotToggleValueChanged() // updates control type 
    {
        if (GameManager.instance.PlayerController != null)
        {
            GameManager.instance.PlayerController.slingshotControl = slingshotToggle.isOn;
        }
        if(DataManager.instance != null)
        {
            DataManager.instance.SetPlayerPrefsBool("SlingshotControl", slingshotToggle.isOn);
        }
    }

    public void StartFadeOut() // usued to call FadeOut from other scripts
    {
        StopAllCoroutines();
        StartCoroutine(FadeOut());
    }
    public void StartFadeIn() // usued to call FadeIn from other scripts
    {
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut() 
    {
        Color C = fadeImage.color;
        float Timer = 0;

        while (Timer <= fadeEffectDuration)
        {
            yield return null;
            Timer += Time.deltaTime;
            C.a = 1 - Mathf.Clamp01(Timer / fadeEffectDuration);

            fadeImage.color = C;
        }
        Debug.Log(" UI Manager: Fade out");
    }

    IEnumerator FadeIn()
    {
        Debug.Log(" UI Manager: Fade in");
        
        Color C = fadeImage.color;
        float Timer = 0;

        while (Timer <= fadeEffectDuration)
        {
            Timer += Time.unscaledDeltaTime;
            C.a = Mathf.Clamp01(Timer / fadeEffectDuration);

            fadeImage.color = C;
            yield return null;
        }
        Debug.Log("UI Manager: Reset Level");

        if (isResetLevel)
        {
            isResetLevel = false;
            SceneManager.LoadScene(GameManager.instance.CurrentLevelName);
        }
        else
            SceneManager.LoadScene(GameManager.instance.NextLevel());

    }
}
