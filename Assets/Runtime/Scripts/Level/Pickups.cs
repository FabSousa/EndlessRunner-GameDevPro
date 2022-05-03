using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickups : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private GameObject cherryRender;
    private AudioSource audioSource;

    private float rotationSpeed = 100;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        cherryRender.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(audioClip != null) AudioUtility.PlayAudioCue(audioSource, audioClip);

        GameMode.CherryCount++;

        cherryRender.SetActive(false);

        Destroy(gameObject, audioClip.length);
    }
}
