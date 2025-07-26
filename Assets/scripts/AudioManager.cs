using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource IdleMusicSource;
    [SerializeField] AudioSource sfxSource;

    [SerializeField] AudioClip whooshClip;
    [SerializeField] AudioClip[] sliceClips;

    public static AudioManager Ins;

    void Awake()
    {
        if(Ins == null)
        {
            Ins = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void PlayIdleMusic()
    {
        IdleMusicSource.Play();
    }

    public void StopIdleMusic()
    {
        IdleMusicSource.Stop();
    }

    public void PlayWhoosh(Vector3 pos)
    {
        sfxSource.PlayOneShot(whooshClip, 0.8f);
    }

    public void PlaySlice(Vector3 pos)
    {
        AudioClip clip = sliceClips[Random.Range(0, sliceClips.Length)];

        sfxSource.transform.position = pos;

        float pitch = Random.Range(.92f, 1.08f); //changing to avoid audio fatigue
        sfxSource.pitch = pitch;

        float vol = Random.Range(0.85f, 1f);

        float startOffset = Random.Range(0f, 0.02f);
        sfxSource.time = startOffset;
        
        sfxSource.PlayOneShot(clip, vol);
    }

    void PlayOneShot(AudioClip clip, Vector3 pos, float vol = 1f)
    {
        sfxSource.transform.position = pos;
        sfxSource.pitch = 1f;
        sfxSource.PlayOneShot(clip, vol);
    }

}
