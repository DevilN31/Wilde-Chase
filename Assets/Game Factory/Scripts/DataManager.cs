using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

    }

    #region PlayerPrefs
    // Set methods for Player Prefs.
    public void SetPlayerPrefsInt(string name, int value)
    {
        PlayerPrefs.SetInt(name, value);
    }
    public void SetPlayerPrefsfloat(string name, float value)
    {
        PlayerPrefs.SetFloat(name, value);
    }

    public void SetPlayerPrefsString(string name, string value)
    {
        PlayerPrefs.SetString(name, value);
    }

    public void SetPlayerPrefsBool(string name, bool value)
    {
        if (value)
            SetPlayerPrefsInt(name, 1);
        else
            SetPlayerPrefsInt(name, 0);
    }

    // Get methods for Player Prefs.
    public int GetPlayerPrefsInt(string name)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetInt(name);
        else
        {
            Debug.LogError($"Key int {name} not found!");
            return -1;
        }
    }
    public float GetPlayerPrefsfloat(string name)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetFloat(name);
        else
        {
            Debug.LogError($"Key float {name} not found!");
            return -1;
        }
    }

    public string GetPlayerPrefsString(string name)
    {
        if (PlayerPrefs.HasKey(name))
            return PlayerPrefs.GetString(name);
        else
        {
            Debug.LogError($"Key string {name} not found!");
            return "";
        }
    }

    public int GetPlayerPrefsBool(string name)
    {
        if (PlayerPrefs.HasKey(name))
        {
            return GetPlayerPrefsInt(name);
            
        }
        else
        {
            Debug.LogError($"Key bool {name} not found!");
            return -1;
        }
    }

    [ContextMenu("Reset Player Prefs")] // Get access to method in editor rather then running the game to reset data.
    public void ResetPlayerPrefs()
    {
        SetPlayerPrefsBool("SlingshotControl", true);
        SetPlayerPrefsfloat("Sensetivity", 1f);
        SetPlayerPrefsBool("FirstRun", true);
        Debug.Log("Reset PlayerPrefs data");
    }
    #endregion
}
