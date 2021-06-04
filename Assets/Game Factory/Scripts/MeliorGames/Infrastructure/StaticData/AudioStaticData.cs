using System.Collections.Generic;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Infrastructure.StaticData
{
  [CreateAssetMenu(fileName = "SoundData", menuName = "Data/SoundData", order = 0)]
  public class AudioStaticData : ScriptableObject
  {
    public EAudio Type;
    public List<AudioClip> Clips = new List<AudioClip>();
  }
}