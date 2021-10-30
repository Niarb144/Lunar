using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip landing;
    [SerializeField] AudioClip crashing;

    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    [SerializeField] float delay = 1f;

    AudioSource audioSource;
    ParticleSystem particles;

    bool isTransitioning = false;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        particles = GetComponent<ParticleSystem>();
    }
    void OnCollisionEnter(Collision other)
    { 
        if (isTransitioning)
        {
            return;
        }
        else 
        {
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Rocket at Launch Pad");
                    break;
                case "Finish":

                    NextSceneSequence();

                    Debug.Log("Reached Landing");
                    break;
                case "Fuel":
                    Debug.Log("Got some Fuel");
                    break;
                default:

                    StartCrashSequence();

                    Debug.Log("Hit an Object");
                    break;

            }
        }
        
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
       
        audioSource.PlayOneShot(crashing);

        particles.Stop();
        particles.Play(crashParticles);   
      

        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
    }

    void ReloadScene()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void NextSceneSequence()
    {
        isTransitioning = true;

        audioSource.Stop();
        audioSource.PlayOneShot(landing);

        particles.Stop();
        particles.Play(successParticles); 
       

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }
   
}
