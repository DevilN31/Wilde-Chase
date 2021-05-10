using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure.AssetManagement
{
  public class AssetProvider
  {
    public GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }

    public GameObject Instantiate(string path, Vector3 at, Quaternion rotation)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, rotation);
    }
  }
}