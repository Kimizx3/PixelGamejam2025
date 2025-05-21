using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    [Header("Sound Effects")] 
    [SerializeField] private List<Sound> _sounds;

    private Dictionary<string, AudioClip> _soundDictionary;
    private AudioSource _audioSource;

    private static AudioManager _instance;
    public static AudioManager Instance => _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _audioSource = GetComponent<AudioSource>();
        _soundDictionary = new Dictionary<string, AudioClip>();

        foreach (var sound in _sounds)
        {
            _soundDictionary.Add(sound.name, sound.clip);
        }
    }

    public void PlaySound(string soundName)
    {
        if (_soundDictionary.ContainsKey(soundName))
        {
            _audioSource.PlayOneShot(_soundDictionary[soundName]);
        }
    }
}
