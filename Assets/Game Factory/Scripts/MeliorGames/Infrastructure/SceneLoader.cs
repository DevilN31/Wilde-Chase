﻿using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure
{
  public class SceneLoader : MonoBehaviour
  {
    public void Load(string name, Action onLoaded = null) =>
      StartCoroutine(LoadScene(name, onLoaded));

    private IEnumerator LoadScene(string name, Action onLoaded = null)
    {
      /*if (SceneManager.GetActiveScene().name == name)
      {
        onLoaded?.Invoke();
        yield break;
      }*/
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(name);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}