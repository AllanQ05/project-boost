using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float levelLoadDelay = 2f;
    [SerializeField]AudioClip success;
    [SerializeField]AudioClip crash;

    [SerializeField]ParticleSystem successParticles;
    [SerializeField]ParticleSystem crashPaticles;

    AudioSource audioSource;

    bool isTransitioning =false;
    bool isCollisionDisable = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ResponseToDebugKeys();
    }

    void ResponseToDebugKeys(){
        if(Input.GetKeyDown(KeyCode.L)){
            StartSuccessSequence();
        }else if(Input.GetKeyDown(KeyCode.C)){
            isCollisionDisable = !isCollisionDisable; //toggle collision
        }
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning || isCollisionDisable){
            return;
        }
        switch(other.gameObject.tag){
            case "Friendly":
                Debug.Log("this thing is friendly");
                break;
            case "Finish":
                // if (SceneManager.GetActiveScene().buildIndex == 0){
                //     SceneManager.LoadScene(1);
                // }else{
                //     SceneManager.LoadScene(0);
                // }
                // LoadNextLevel();
                StartSuccessSequence();
                Debug.Log("congrate, you are finished");
                break;
            default:
                // Debug.Log("sorry you blew up!");
                // ReloadLevel();
                // Invoke("StartCrashSequence",1f);   // delay 1 sec to run method
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence(){
        isTransitioning = true;
        successParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        // todo add particle effect upon success
        GetComponent<Movement>().enabled =false;
        Invoke("LoadNextLevel",levelLoadDelay); 
    }

    void StartCrashSequence(){
        isTransitioning = true;
        crashPaticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled =false;
        // ReloadLevel();
        Invoke("ReloadLevel",levelLoadDelay); 
    }

    void LoadNextLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // get current scene
        int nextSceneIndex = currentSceneIndex+1;
        if(nextSceneIndex==SceneManager.sceneCountInBuildSettings){ // == last scene
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel(){
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;   // get current scene
        // SceneManager.LoadScene(0);
        // SceneManager.LoadScene("Sandbox");
        SceneManager.LoadScene(currentSceneIndex);
    }
}
