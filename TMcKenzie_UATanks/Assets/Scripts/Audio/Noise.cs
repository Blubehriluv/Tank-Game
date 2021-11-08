using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Noise : MonoBehaviour
{
    [SerializeField] AudioSource audio;
    public AudioClip[] sources;
    [SerializeField] SphereCollider cd;
    [SerializeField] bool awakeEnabled;
    [SerializeField] int range;

    void Start()
    {
        CheckForNull();
        cd = gameObject.GetComponent<SphereCollider>();

        // Allows for the object to be set to play on awake.
        if (awakeEnabled)
        {
            audio.playOnAwake = true;
        }
        
        
        SetSoundSize(range);
    }

    // Ensures the audio was picked up.
    void CheckForNull()
    {
        if (audio == null)
        {
            audio = gameObject.GetComponent<AudioSource>();
        }
    }

    // This will set the sound's range. Independent of each sound object.
    public void SetSoundSize(int soundSize)
    {
        cd.radius = soundSize;
    }

    // If the sound comes into contact with something else, pass data.
    // Data accuracy depends on reciever (?)
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hit the thing: " + other);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
