using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManagerController : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManagerController instance;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
        }
    }

    private void Start()
    {


    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.Log("Unable to find audio source: " + name);
            return;
        }

        s.source.Play();
    }

    public void StartMainSceneBackgroundMusic()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "FirstLight");
        s.source.volume = 0;
        s.source.Play();
        for (float i = 0; i < 0.5f; i += 0.1f)
        {
            s.source.volume = i;
        }
    }
}
