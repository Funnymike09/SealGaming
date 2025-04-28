using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    public static AudioManager singleton { get; private set; }

    private void Awake()
    {
        if (singleton != null && singleton != this)
        {
            Destroy(this);
        }
        else
        {
            singleton = this;
        }
    }

    public void PlaySound(AudioClip clip)
    {
        AudioSource source = new GameObject().AddComponent<AudioSource>();
        source.gameObject.transform.SetParent(transform);
        source.clip = clip;
        source.Play();
        StartCoroutine(DestroyInstance(source.gameObject, source.clip.length));
    }

    public void PlaySoundSpatial(AudioClip clip, GameObject go)
    {
        AudioSource source = go.AddComponent<AudioSource>();
        source.clip = clip;
        source.spatialBlend = 1.0f;
        source.maxDistance = 20f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.Play();
        StartCoroutine(RemoveComponent(source, source.clip.length));
    }
    public void PlaySoundList( GameObject go)
    {
        AudioSource source = go.GetComponent<AudioSource>();
       // source.spatialBlend = 1.0f;
       // source.maxDistance = 20f;
       // source.rolloffMode = AudioRolloffMode.Linear;
        source.Play();
        //StartCoroutine(RemoveComponent(source, source.clip.length));
    }
    public void PlaySoundListR(AudioResource clip)
    {
        AudioSource source = gameObject.GetComponent<AudioSource>();
        // source.spatialBlend = 1.0f;
        // source.maxDistance = 20f;
        // source.rolloffMode = AudioRolloffMode.Linear;
        source.resource = clip;
        source.Play();
        //StartCoroutine(RemoveComponent(source, source.clip.length));
    }
    public void PlaySoundListOnce(GameObject go, AudioResource clip)
    {
        AudioSource source = go.GetComponent<AudioSource>();
        // source.spatialBlend = 1.0f;
        // source.maxDistance = 20f;
        // source.rolloffMode = AudioRolloffMode.Linear;
        source.Play();
        source.resource = clip;
        //StartCoroutine(RemoveComponent(source, source.clip.length));
    }
    public void PlaySoundListSpatialClip(GameObject go, AudioResource clip )
    {
        AudioSource source = go.AddComponent<AudioSource>();
        source.resource = clip;
        source.spatialBlend = 1.0f;
        source.Play();
       
    }


    IEnumerator DestroyInstance(GameObject go, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(go);
    }

    IEnumerator RemoveComponent(AudioSource source, float time)
    {
        yield return new WaitForSeconds(time);

        Destroy(source);
    }


}
