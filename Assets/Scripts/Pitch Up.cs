using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PitchUp : MonoBehaviour
{
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;

    [SerializeField]
    private float pitch = 1.0f;

    private void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SetPitchUp();
        }
    }

    private void SetPitchUp()
    {
        source.PlayOneShot(clip);
        pitch += 0.1f;
        source.pitch = pitch;
    }
}