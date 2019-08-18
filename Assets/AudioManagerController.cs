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
            s.source.priority = s.priority;
        }

        StartCoroutine("StartStartSceneBackgroundMusic");
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

    public IEnumerator StartMainSceneBackgroundMusic()
    {
        StartCoroutine("StopStartSceneBackgroundMusic");
        Sound s = Array.Find(sounds, sound => sound.name == "Andromeda-EnterGame");
        if (s == null)
        {
            Debug.Log("Unable to find audio source: Andromeda-EnterGame");

        }
        else
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();
                s.source.volume = 0;
                for (float i = 0; i < 0.4f; i += 0.1f)
                {
                    s.source.volume = i;
                    yield return new WaitForSeconds(1.0f);
                }

                Debug.Log("Andromeda-EnterGame is playing");
            }
        }
        
    }

    public IEnumerator StopMainSceneBackgroundMusic()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Andromeda-EnterGame");
        Debug.Log("Andromeda-EnterGame is stopping");
        for (float i = s.source.volume; i > 0; i -= 0.1f)
        {
            s.source.volume = i;
            yield return new WaitForSeconds(1.0f);
        }

        s.source.Stop();


    }

    public IEnumerator StartStartSceneBackgroundMusic()
    {
        
        Sound s = Array.Find(sounds, sound => sound.name == "Freeing-Intro");
        if (s == null)
        {
            Debug.Log("Unable to find audio source: Freeing-Intro");
        }
        else
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();
                s.source.volume = 0;
                for (float i = 0; i < 0.4f; i += 0.1f)
                {
                    s.source.volume = i;
                    yield return new WaitForSeconds(0.1f);
                }

                Debug.Log("Freeing-Intro is playing");
            }
        }
        
    }

    public IEnumerator StopStartSceneBackgroundMusic()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Freeing-Intro");

        Debug.Log("Freeing-Intro is stopping");
        for (float i = s.source.volume; i > 0; i -= 0.1f)
        {
            s.source.volume = i;
            yield return new WaitForSeconds(1.0f);
        }

        s.source.Stop();


    }

    public IEnumerator StartEnterBossBackgroundMusic()
    {
        StartCoroutine("StopMainSceneBackgroundMusic");
        Sound s = Array.Find(sounds, sound => sound.name == "Blight-BossStage");
        if (s == null)
        {
            Debug.Log("Unable to find audio source: Blight-BossStage");
        }
        else
        {
            if (!s.source.isPlaying)
            {
                s.source.Play();
                s.source.volume = 0;
                for (float i = 0; i < 0.8f; i += 0.1f)
                {
                    s.source.volume = i;
                    yield return new WaitForSeconds(0.1f);
                }

                Debug.Log("Blight-BossStage is playing");
            }
            
        }

    }

    public IEnumerator StopEnterBossBackgroundMusic()
    {
        Sound s = Array.Find(sounds, sound => sound.name == "Blight-BossStage");

        Debug.Log("Blight-BossStage is stopping");
        for (float i = s.source.volume; i > 0; i -= 0.1f)
        {
            s.source.volume = i;
            yield return new WaitForSeconds(1.0f);
        }

        s.source.Stop();


    }

    public IEnumerator StartAfterSealedBackgroundMusic()
    {
        StartCoroutine("StopEnterBossBackgroundMusic");
        Sound s = Array.Find(sounds, sound => sound.name == "FirstLight-AfterSealed");
        if (s == null)
        {
            Debug.Log("Unable to find audio source: FirstLight-AfterSealed");
        }
        else
        {

            s.source.Play();
            s.source.volume = 0;
            for (float i = 0; i < 0.4f; i += 0.1f)
            {
                s.source.volume = i;
                yield return new WaitForSeconds(0.1f);
            }

            Debug.Log("FirstLight-AfterSealed is playing");
        }
    }


}
