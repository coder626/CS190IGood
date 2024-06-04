using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public AudioClip audioclip;
    public AudioClip missLeftClip; // Specific audio clip for MissLeft
    public AudioClip missRightClip; // Specific audio clip for MissRight
    public AudioClip missShortClip; // Specific audio clip for MissShort
    public AudioClip missBehindClip; // Specific audio clip for MissBehind
    public AudioClip madeShotClip;
    public AudioSource audioSource;
    public int counter = 0;
    public int totalShots = 0;
    // Target position to reset the ball to
    private Vector3 resetPosition = new Vector3(2.5876f, -2.0f, -0.587f);
    private Rigidbody rb;

    void Start(){
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = audioclip;
        rb = GetComponent<Rigidbody>();
        if(rb == null){
            Debug.LogError("Rigidbody not found on ");
        }
    }

    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("PickUp")){
            Debug.Log("Touched");
            counter = counter + 1;
            StartCoroutine(PlayClipsSequentially(audioclip, madeShotClip));
            totalShots++;
        }
        if(other.gameObject.CompareTag("MissBehind")){
            Debug.Log("MissBehind");
            if(totalShots % 4 == 0){
                audioSource.clip = missBehindClip; // Set to MissBehind audio clip
                audioSource.Play();
            }
            totalShots++;
        }
        if(other.gameObject.CompareTag("MissShort")){
            Debug.Log("MissShort");
            if(totalShots % 4 == 0){
                audioSource.clip = missShortClip; // Set to MissShort audio clip
                audioSource.Play();
            }
            totalShots++;
        }
        if(other.gameObject.CompareTag("MissLeft")){
            Debug.Log("MissLeft");
            if(totalShots % 4 == 0){
                audioSource.clip = missLeftClip; // Set to MissLeft audio clip
                audioSource.Play();
            }
            totalShots++;
        }
        if(other.gameObject.CompareTag("MissRight")){
            Debug.Log("MissRight");
            if(totalShots % 4 == 0){
                audioSource.clip = missRightClip; // Set to MissRight audio clip
                audioSource.Play();
            }
            totalShots++;
        }
    }
    IEnumerator PlayClipsSequentially(AudioClip firstClip, AudioClip secondClip)
    {
        // Play the first audio clip
        audioSource.clip = firstClip;
        audioSource.Play();

        // Wait for the duration of the first audio clip
        yield return new WaitForSeconds(firstClip.length);

        // Play the second audio clip
        audioSource.clip = secondClip;
        audioSource.Play();
    }
    void Update()
    {
        // Check if the y position is less than or equal to -0.031
        if(transform.position.y <= -2.88f)
        {
            // Reset the ball's position
            transform.position = resetPosition;
            if(rb != null){
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            }
    }
}
