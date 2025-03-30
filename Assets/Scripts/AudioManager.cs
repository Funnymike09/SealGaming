using System;
using System.Collections;
using UnityEngine;

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
        source.maxDistance = 5f;
        source.rolloffMode = AudioRolloffMode.Linear;
        source.Play();
        StartCoroutine(RemoveComponent(source, source.clip.length));
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
