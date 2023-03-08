using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakingAction : MonoBehaviour
{
    KeyCode scream; //input scream key
    List<KeyCode> patternToWake = new List<KeyCode>(); //keys to wake the person
    int currentWakeupScreamIndex; //current screaming progress of the pattern
    int currentAsleepScreamer; //the top asleep screamer in the sleeping screamers list
    [SerializeField]
    AudioSource audioSource; //scream audio
    int screamIndex; //input scream's audio index
    ScreamManager screamManager;

    void Start()
    {
        currentWakeupScreamIndex = 0;
        screamManager = gameObject.GetComponent<ScreamManager>();
    }

    void Update()
    {
        if (Input.anyKey)
        {
            //get which key pressed
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    //store the key if it matches the scream input key
                    List<KeyCode> screamKeys = screamManager.screamKeys;
                    for (int i = 0; i < screamManager.currentScreamerChain.Count; i++)
                    {
                        if (screamKeys[i] == vKey)
                        {
                            scream = screamManager.screamKeys[i];
                            screamIndex = i;
                            break;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(scream))
        {
            Debug.Log("screaming key input = " + scream);
            //when all screamers are awake, play audio without checking other conditions
            if (currentAsleepScreamer >= screamManager.asleepScreamers.Count)
            {
                audioSource.clip = screamManager.screamAudios[screamIndex];
                audioSource.Play();
            }
            else
            {
                patternToWake = screamManager.asleepScreamers[currentAsleepScreamer].GetComponent<WakeupScream>().screamToWake;
                if (patternToWake != null)
                {
                    MatchPattern();
                }
            }
        }
    }

    void MatchPattern()
    {
        audioSource.clip = screamManager.screamAudios[screamIndex];
        audioSource.Play();

        //scream input matches
        if (patternToWake[currentWakeupScreamIndex] == scream)
        {
            Debug.Log("correct scream");
            
            scream = KeyCode.None;
            currentWakeupScreamIndex++;

            //successfully wake the person up
            if (currentWakeupScreamIndex == patternToWake.Count)
            {               
                Debug.Log("WOKE UP!");
                screamManager.asleepScreamers[currentAsleepScreamer].GetComponent<Follower>().AwakeScreamer();
                screamManager.asleepScreamers[currentAsleepScreamer].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255);

                currentWakeupScreamIndex = 0;
                currentAsleepScreamer++;
            }
        }
        else
        {
            Debug.Log("wrong scream");
            currentWakeupScreamIndex = 0;
        }
    }
}
