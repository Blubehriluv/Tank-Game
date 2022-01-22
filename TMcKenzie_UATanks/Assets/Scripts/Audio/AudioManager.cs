using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Slider effectSlider;
    public Slider musicSlider;
    public Sound[] Effects;
    public Sound[] Music;

    public static AudioManager instance;
    private float musicVolume;
    private float effectsVolume;

    void Start()
    {
        effectSlider.onValueChanged.AddListener(delegate { EffectsVolumeChanged(); });
        musicSlider.onValueChanged.AddListener(delegate { MusicVolumeChanged(); });
        PlayMusic("Mythica");
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("DELETING EXTRA AUDIO MANAGER.");
            Destroy(gameObject);
        }


        createAudioSources(Effects, effectsVolume);     // create sources for effects
        createAudioSources(Music, musicVolume);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void createAudioSources(Sound[] sounds, float volume)
    {
        foreach (Sound s in sounds)
        {   // loop through each music/effect
            s.source = gameObject.AddComponent<AudioSource>(); // create anew audio source(where the sound splays from in the world)
            s.source.clip = s.clip;     // the actual music/effect clip
        }
    }

    public void PlayMusic(string name)
    {
        // here we get the Sound from our array with the name passed in the methods parameters
        Sound s = System.Array.Find(Music, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Unable to play music " + name);
            return;
        }
        // This will play the sound
        s.source.Play();
    }

    public void StopMusic(string name)
    {
        // here we get the Sound from our array with the name passed in the methods parameters
        Sound s = System.Array.Find(Music, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Unable to stop music " + name);
            return;
        }
        // This will play the sound
        s.source.Stop();
    }

    public void PlaySound(string name)
    {
        // here we get the Sound from our array with the name passed in the methods parameters
        Sound s = System.Array.Find(Effects, sound => sound.name == name);
        if (s == null)
        {
            Debug.Log("Unable to play sound " + name);
            return;
        }
        // This will play the sound
        s.source.Play();
    }

   
    public void EffectsVolumeChanged()
    {
        effectsVolume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        foreach (Sound s in Effects)
        {
            s.source.volume = s.volume * effectsVolume * effectSlider.value;
        }
        Effects[0].source.Play(); // play an effect so user can her effect volume
    }

    public void MusicVolumeChanged()
    {
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        foreach (Sound s in Music)
        {
            s.source.volume = s.volume * musicVolume * musicSlider.value;
        }
    }
}


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
    public bool loop = false;
}
