using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound sound in sounds)
        {
            sound.audSrc = gameObject.AddComponent<AudioSource>();
            sound.audSrc.clip = sound.clip;
            sound.audSrc.volume = sound.volume;
            sound.audSrc.loop = sound.isLoop;
        }
    }

    private void Start()
    {
        // AudioManager.instance.Play("playground");
    }

    public void Play(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        s.audSrc.Play();
    }

    public void Stop(string soundName)
    {
        Sound s = Array.Find(sounds, sound => sound.name == soundName);
        if (s != null && s.audSrc.isPlaying)
        {
            s.audSrc.Stop();
        }
    }
}
