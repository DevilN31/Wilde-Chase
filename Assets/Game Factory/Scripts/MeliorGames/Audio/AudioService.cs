using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game_Factory.Scripts.MeliorGames.Extensions;
using Game_Factory.Scripts.MeliorGames.Infrastructure.Data;
using Game_Factory.Scripts.MeliorGames.Infrastructure.StaticData;
using UnityEngine;

namespace Game_Factory.Scripts.MeliorGames.Audio
{
  public class AudioService : MonoBehaviour
  {
    public static AudioService Instance;

    public AudioSource SoundsAudioSource;
    public AudioSource MusicAudioSource;
    public AudioSource BackgroundSoundsAudioSource;

    private const string AudioDataPath = "StaticData/Audio";
    private Dictionary<EAudio, AudioStaticData> audioClips;

    private EAudio previousSoundType = EAudio.Undefined;

    private void Awake()
    {
      Instance = this;
      
      LoadAudio();
    }

    public void Init()
    {
      SoundsAudioSource.volume = SaveLoadService.Instance.GameSettings.SoundsVolume;
      MusicAudioSource.volume = SaveLoadService.Instance.GameSettings.MusicVolume;
    }

    private void Start()
    {
      PlayMusic(EAudio.BackgroundMusic);
      //PlayBackgroundSounds(EAudio.BackgroundSounds);
    }

    private void LoadAudio()
    {
      audioClips = Resources.LoadAll<AudioStaticData>(AudioDataPath).ToDictionary(x => x.Type, x => x);
    }


    public void PlaySound(EAudio audioType)
    {
      AudioStaticData audioData = 
        audioClips.TryGetValue(audioType, out AudioStaticData audioStaticData) ? audioStaticData : null;
      
      if(audioData == null)
        return;
      
      if(SoundsAudioSource.isPlaying && previousSoundType == audioType)
        return;

      AudioClip clip = audioData.Clips.GetRandom();

      previousSoundType = audioType;
      SoundsAudioSource.PlayOneShot(clip, SoundsAudioSource.volume);
    }

    public void PlayMusic(EAudio audio)
    {
      AudioStaticData audioData = 
        audioClips.TryGetValue(audio, out AudioStaticData audioStaticData) ? audioStaticData : null;
      
      if(audioData == null)
        return;

      AudioClip clip = audioData.Clips.GetRandom();
      MusicAudioSource.clip = clip;
      MusicAudioSource.loop = true;
      MusicAudioSource.Play();
    }

    public void PlayBackgroundSounds(EAudio audioType)
    {
      AudioStaticData audioData = 
        audioClips.TryGetValue(audioType, out AudioStaticData audioStaticData) ? audioStaticData : null;
      
      if(audioData == null)
        return;

      AudioClip clip = audioData.Clips.GetRandom();
      
      BackgroundSoundsAudioSource.clip = clip;
      BackgroundSoundsAudioSource.Play();
      StartCoroutine(BackgroundSoundsCoroutine(clip.length, audioType));
    }

    private IEnumerator BackgroundSoundsCoroutine(float previousSoundDuration, EAudio audioType)
    {
      yield return new WaitForSeconds(previousSoundDuration);
      PlayBackgroundSounds(audioType);
    }
  }
}