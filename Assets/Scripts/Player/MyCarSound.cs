using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarSound : MonoBehaviour
{
    [SerializeField] float minPitch = 0.6f;
    [SerializeField] float maxPitch = 1.5f;
    [SerializeField] float maxSpeed = 85f;

    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip hitSound;
    public float audioPitch = 1;
    //https://youtu.be/IHY3sAPz7Pk

    private AudioSource engineSource;
    private AudioSource sfxSource;

    private GoofyNewControls carController;

    // Start is called before the first frame update
    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        engineSource = audioSources[0];
        sfxSource = audioSources[1];

        PlayEngineSound();

        carController = GetComponent<GoofyNewControls>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateEngineSound();
    }

    private void PlayEngineSound()
    {
        engineSource.clip = engineSound;
        engineSource.volume = 0.2f;
        engineSource.loop = true;
        engineSource.Play();
    }

    private void UpdateEngineSound()
    {
        float speed = carController.currSpeed;
        float normalizedSpeed = speed / maxSpeed;
        normalizedSpeed = Mathf.Clamp01(normalizedSpeed);
        engineSource.pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
    }

    public void PlayHitSound()
    {
        sfxSource.volume = 0.5f;
        sfxSource.PlayOneShot(hitSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Prop")
        {
            PlayHitSound();
        }
    }
}
