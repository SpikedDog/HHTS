using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCarSound : MonoBehaviour
{
    [SerializeField] float minPitch = 0.4f;
    [SerializeField] float maxPitch = 1.5f;
    [SerializeField] float maxSpeed = 70f;

    [SerializeField] AudioClip engineSound;
    [SerializeField] AudioClip hitSound;
    [SerializeField] AudioClip interactionSound;
    [SerializeField] AudioClip endGameSound;
    [SerializeField] AudioClip deliverSound;
    [SerializeField] AudioClip tickerSound;
    [SerializeField] AudioClip starterSound;

    public float audioPitch = 1;
    //https://youtu.be/IHY3sAPz7Pk

    private AudioSource engineSource;
    private AudioSource sfxSource;
    private AudioSource pickUpSource;
    private AudioSource endGameSource;
    private AudioSource deliverSource;
    private AudioSource tickerSource;
    private AudioSource starterSource;

    private GoofyNewControls carController;

    void Awake()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        engineSource = audioSources[0];
        sfxSource = audioSources[1];
        pickUpSource = audioSources[2];
        endGameSource = audioSources[3];
        deliverSource = audioSources[4];
        tickerSource = audioSources[5];
        starterSource = audioSources[6];
    }

    // Start is called before the first frame update
    void Start()
    {
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
        engineSource.volume = 0.6f;
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
        //sfxSource.volume = 0.5f;
        sfxSource.PlayOneShot(hitSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Prop")
        {
            PlayHitSound();
        }
    }

    public void PickUpSound()
    {
        //pickUpSource.volume = 0.5f;
        pickUpSource.PlayOneShot(interactionSound);
    }

    public void PlayEndGameSound()
    {
        endGameSource.volume = 0.2f;
        endGameSource.PlayOneShot(endGameSound);
    }

    public void PlayDeliverSound()
    {
        //deliverSource.volume = 0.5f;
        deliverSource.PlayOneShot(deliverSound);
    }

    public void TickerSound()
    {
        tickerSource.PlayOneShot(tickerSound);
    }

    public void StartSound()
    {
        starterSource.PlayOneShot(starterSound);
    }
}
