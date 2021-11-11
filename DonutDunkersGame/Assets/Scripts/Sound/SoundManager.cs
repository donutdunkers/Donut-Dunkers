using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _Instance;
	
    public static SoundManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                _Instance = FindObjectOfType<SoundManager>();
            }
            return _Instance;
        }
    }
	
    private void Update()
    {
        AudioSource currentSource;
        if (isMusicSourceA)
        {
            currentSource = musicSourceA;
        }
        else
        {
            currentSource = musicSourceB;
        }
    }
	
    public void CrossFade(AudioClip audio, float volume, float fadetime)
    {
		this.StartCoroutine(this.Fade(audio, volume, fadetime));
    }
	
    private Coroutine currentSourceroutine; //assign it to run a numerator
    private Coroutine newSourceroutine;
	
    private IEnumerator Fade(AudioClip audio, float volume, float fadetime)
    {
        AudioSource currentSource;
        AudioSource newSource;

        //currentSource = (isMusicSourceA) ? musicSourceA : musicSourceB;
        if (isMusicSourceA)
        {
            currentSource = musicSourceA;
        }
        else
        {
            currentSource = musicSourceB;
        }

        if (currentSource.clip == audio)
        {
            currentSource.volume = volume;
            yield break;
        }

        if (isMusicSourceA)
        {
            newSource = musicSourceB;
        }
        else
        {
            newSource = musicSourceA;
        }

        newSource.clip = audio;
        newSource.Play();
        newSource.volume = 0f;

        if (currentSourceroutine != null)
        {
            StopCoroutine(currentSourceroutine);
        }
        if (newSourceroutine != null)
        {
            StopCoroutine(newSourceroutine);
        }

        currentSourceroutine = StartCoroutine(FadeSource(currentSource, currentSource.volume, 0, fadetime));

        newSourceroutine = StartCoroutine(FadeSource(newSource, newSource.volume, volume, fadetime));
       
	   isMusicSourceA = !isMusicSourceA;
    }

    private IEnumerator FadeSource(AudioSource source, float startV, float endV, float duration)
    {
        float startTime = Time.unscaledTime;
        while (true)
        {
            float elapsed = Time.unscaledTime - startTime;
            source.volume = Mathf.Clamp01(Mathf.Lerp(startV, endV, elapsed / duration));
            if (source.volume == endV)
            {
                break;
            }
            yield return null;
        }
    }

    //plays music
    public AudioSource musicSourceA;
    public AudioSource musicSourceB;
    private bool isMusicSourceA; 

    //plays sound effects
    public AudioSource soundSource;




}
