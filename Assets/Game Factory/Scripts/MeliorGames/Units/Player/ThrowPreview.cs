using System;
using System.Collections.Generic;
using Game_Factory.Scripts.MeliorGames.Projectiles;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Units.Player
{
  public class ThrowPreview : MonoBehaviour
  {
    public PlayerShoot PlayerShoot;
    
    public List<ProjectilePreview> ProjectilePreviews;
    
    private ProjectilePreview pickedProjectilePreview;

    private bool projectileIsPicked;

    private void Start()
    {
      PlayerShoot.ProjectilePicked += OnProjectilePicked;
      PlayerShoot.ProjectileThrown += OnProjectileThrown;
    }

    private void Update()
    {
      if (projectileIsPicked)
      {
        //pickedProjectilePreview.transform.eulerAngles += Vector3.up * 2;
        //pickedProjectilePreview.gameObject.SetActive(true);
      }

      #if UNITY_EDITOR
      if (Input.GetKeyDown(KeyCode.Escape))
      {
        UnityEditor.EditorApplication.isPaused = !UnityEditor.EditorApplication.isPaused;
      }
      #endif
    }

    public void OnDeath()
    {
      OnProjectileThrown();
    }

    private void OnProjectilePicked(Projectile projectile)
    {
      var nextProjectilePreview = ProjectilePreviews.Find(
        preview => preview.ProjectileType == projectile.ProjectileType);

      if (pickedProjectilePreview != nextProjectilePreview && pickedProjectilePreview != null)
      {
        pickedProjectilePreview.gameObject.SetActive(false);
        pickedProjectilePreview = nextProjectilePreview;
      }
      else
      {
        pickedProjectilePreview = nextProjectilePreview;
      }

      projectileIsPicked = true;

      pickedProjectilePreview.gameObject.SetActive(true);
    }

    private void OnProjectileThrown()
    {
      projectileIsPicked = false;
      pickedProjectilePreview.gameObject.SetActive(false);
    }
  }
}