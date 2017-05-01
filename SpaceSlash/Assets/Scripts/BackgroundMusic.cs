using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour {
    private AudioSource[] audioSources;
    public float fadeSpeed = 0.5f;
    public float fadeInOutMultiplier = 0.2f;
    public bool isPlaying;

    public string playingTrackName = "Nothing";
    public int playingTrackIndex;
    public float playingTrackVolume = 0.000f;

    public string lastTrackName = "Nothing";
    public int lastTrackIndex;
    public float lastTrackVolume = 0.000f;

    public IEnumerator FadeOutOldMusic_FadeInNewMusic()
    {
        audioSources[playingTrackIndex].volume = 0.000f;

        audioSources[playingTrackIndex].Play();

        while (audioSources[playingTrackIndex].volume < 1f)
        {
            audioSources[lastTrackIndex].volume -= fadeSpeed * fadeInOutMultiplier;

            audioSources[playingTrackIndex].volume += fadeSpeed * fadeInOutMultiplier;

            Debug.Log("Fade: " + lastTrackName + " " + audioSources[lastTrackIndex].volume.ToString() + " Rise: " + playingTrackName + " " + audioSources[playingTrackIndex].volume.ToString());

            yield return new WaitForSeconds(0.001f);

            lastTrackVolume = audioSources[lastTrackIndex].volume;

            playingTrackVolume = audioSources[playingTrackIndex].volume;
        }

        audioSources[lastTrackIndex].volume = 0.000f; // Just In Case....

        audioSources[lastTrackIndex].Stop();

        lastTrackIndex = playingTrackIndex;

        lastTrackName = playingTrackName;

        isPlaying = true;
    }

    public IEnumerator FadeInNewMusic()
    {
        audioSources[playingTrackIndex].volume = 0.000f;

        audioSources[playingTrackIndex].Play();

        while (audioSources[playingTrackIndex].volume < 1f)
        {
            audioSources[playingTrackIndex].volume += fadeSpeed * 2;

            Debug.Log("Fading In: " + audioSources[playingTrackIndex].volume.ToString());

            yield return new WaitForSeconds(0.001f);

            playingTrackVolume = audioSources[playingTrackIndex].volume;
        }

        lastTrackIndex = playingTrackIndex;

        lastTrackName = playingTrackName;

        isPlaying = true;
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);

        audioSources = GetComponentsInChildren<AudioSource>();
    }

    public void PlayMusic(string transformName)
    {
        for (int a = 0; a < audioSources.Length; a++)
        {
            if (audioSources[a].name == transformName)
            {
                Debug.Log("Found Track Name (" + transformName + ") at Index(" + a.ToString() + ")");
                playingTrackIndex = a;
                playingTrackName = transformName;
                StartCoroutine(FadeInNewMusic());
                break;
            }
        }
        if (playingTrackIndex == lastTrackIndex)
        {
            Debug.Log("Same Track Selected");
            return;
        }
        else
        {
            if (isPlaying)
            {
                Debug.Log("Fading in new music - Fading out old music");
                StartCoroutine(FadeOutOldMusic_FadeInNewMusic());
            }
            else
            {
                Debug.Log("Fading in new music");
                StartCoroutine(FadeInNewMusic());
            }
        }
    }
}
