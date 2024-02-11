using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Author: Liz
/// Description: Contains specific information for each audio clip used.
/// </summary>
[System.Serializable]
public class AudioFile
{
    public string Name;
    [Space]
    public AudioClip Clip;
    public float Volume = 0.25f;
    public float Pitch = 1f;
    //The pitch (+ or -) that an audio file will be randomly deviated by. Great for SFX.
    [Range(0, 1)] public float PitchDeviation = 0;
    [Space]
    public bool Loop;

    [HideInInspector] public AudioSource Source;
}

/// <summary>
/// Author: Liz
/// Description: Plays music at the start of 
/// </summary>
[System.Serializable]
public class SceneMusic
{
    public int SceneBuildIndex;
    public string MusicToPlay;
}

/// <summary>
/// Author: Liz
/// Description: 
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Files")]
    [SerializeField] private List<AudioFile> _music = new List<AudioFile>();
    [SerializeField] private List<AudioFile> _soundEffects = new List<AudioFile>();

    [Header("Mixing")]
    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerGroup _soundEffectGroup;
    [Space]
    [SerializeField] private List<SceneMusic> _sceneMusic;

    private AudioFile _currentMusic;

    /// <summary>
    /// Adds each Source to the object.
    /// </summary>
    private void Awake()
    {
        SetUpAudio(_music, _musicGroup);
        SetUpAudio(_soundEffects, _soundEffectGroup);
    }

    private void Start()
    {
        PlaySceneMusic();


    }

    public void PlaySceneMusic()
    {
        foreach (SceneMusic sceneMusic in _sceneMusic)
        {
            if (sceneMusic.SceneBuildIndex == SceneTransitions.Instance.GetBuildIndex())
            {
                PlayMusic(sceneMusic.MusicToPlay);
            }
        }
    }

    /// <summary>
    /// Sets up AudioFiles.
    /// </summary>
    /// <param name="audioList">Audio files are from this List.</param>
    /// <param name="mixerGroup">The mixer group to use.</param>
    private void SetUpAudio(List<AudioFile> audioList, AudioMixerGroup mixerGroup)
    {
        foreach (AudioFile af in audioList)
        {
            af.Source = gameObject.AddComponent<AudioSource>();
            af.Source.clip = af.Clip;

            af.Source.volume = af.Volume;
            af.Source.pitch = af.Pitch;
            af.Source.loop = af.Loop;
            af.Source.playOnAwake = false;

            af.Source.outputAudioMixerGroup = mixerGroup;
        }
    }



    /// <summary>
    /// Plays a sound effect.
    /// </summary>
    /// <param name="name">Its name</param>
    public void PlaySoundEffect(string name)
    {
        AudioFile a = GetSoundEffect(name);

        if (a == null)
        {
            Debug.LogWarning($"No sound found with name {name}");
            return;
        }

        float newPitch = Random.Range(1 - a.PitchDeviation, 1 + a.PitchDeviation);

        if (a.Source.isActiveAndEnabled)
        {
            a.Source.pitch = newPitch;
            a.Source.Play();
        }
        else
        {
            Debug.LogWarning($"{a.Source.name} is not enabled! Cannot play audio file.");
        }
    }

    /// <summary>
    /// Switches current music to a new track.
    /// </summary>
    /// <param name="name">Music name</param>
    public void PlayMusic(string name)
    {
        AudioFile a = GetMusic(name);

        if (a == null)
        {
            Debug.LogWarning($"No sound found with name {name}");
            return;
        }

        if (_currentMusic == a)
        {
            return;
        }

        if (_currentMusic != null)
        {
            _currentMusic.Source.Stop();
            _currentMusic.Source.playOnAwake = false;
        }

        if (a.Source.isActiveAndEnabled)
        {
            a.Source.Play();
            a.Source.playOnAwake = true;
        }
        else
        {
            Debug.LogWarning($"{a.Source.name} is not enabled! Cannot play audio file.");
        }

        _currentMusic = a;
    }

    public void StopCurrentMusic()
    {
        _currentMusic.Source.Stop();
    }

    public AudioFile GetMusic(string name)
    {
        foreach (AudioFile af in _music)
        {
            if (af.Name == name)
            {
                return af;
            }
        }

        return null;
    }

    public AudioFile GetSoundEffect(string name)
    {
        foreach (AudioFile af in _soundEffects)
        {
            if (af.Name == name)
            {
                return af;
            }
        }

        return null;
    }

    public void SetMusicVolume(float newVolume)
    {
        _musicGroup.audioMixer.SetFloat("M_Volume", Mathf.Log10(newVolume) * 20);
    }

    public void SetSFXVolume(float newVolume)
    {
        _soundEffectGroup.audioMixer.SetFloat("S_Volume", Mathf.Log10(newVolume) * 20);
    }
}