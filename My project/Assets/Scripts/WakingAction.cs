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
    public WakeupScream wakeup;

    void Start()
    {
        currentWakeupScreamIndex = 0;
        screamManager = gameObject.GetComponent<ScreamManager>();
        //wakeup = GetComponent<WakeupScream>();
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
                    List<KeyCode> screamKeysAlt = screamManager.screamKeysAlt;
                    for (int i = 0; i < screamManager.currentScreamerChain.Count; i++)
                    {
                        if (screamKeys[i] == vKey)
                        {
                            scream = screamManager.screamKeys[i];
                            screamIndex = i;
                            break;
                        }
                        else if(screamKeysAlt[i] == vKey)
                        {
                            scream = screamManager.screamKeysAlt[i];
                            screamIndex = i;
                            break;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(scream))
        {
            scream = ConvertInputKey(scream);
            print(scream);
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

            GameObject go = screamManager.asleepScreamers[currentAsleepScreamer].GetComponent<WakeupScream>().screamPatternDisplay.transform.GetChild(currentWakeupScreamIndex).gameObject;
            go.GetComponent<SpriteRenderer>().color = new Color32(246, 246, 176, 255);
            
            scream = KeyCode.None;
            currentWakeupScreamIndex++;

            //successfully wake the person up
            if (currentWakeupScreamIndex == patternToWake.Count && wakeup.timeUp == false)
            {               
                Debug.Log("WOKE UP!");
                screamManager.asleepScreamers[currentAsleepScreamer].GetComponent<Follower>().AwakeScreamer();
                screamManager.asleepScreamers[currentAsleepScreamer].GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);

                currentWakeupScreamIndex = 0;
                currentAsleepScreamer++;
            }
        }
        else
        {
            Debug.Log("wrong scream");
            audioSource.clip = screamManager.snoreAudio;
            audioSource.Play();
            foreach (Transform child in screamManager.asleepScreamers[currentAsleepScreamer].GetComponent<WakeupScream>().screamPatternDisplay.transform)
            {
                if(child.gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer t))
                {
                    t.color = new Color32(255, 255, 255, 255);
                }
            }
            
            currentWakeupScreamIndex = 0;
        }
    }

    KeyCode ConvertInputKey(KeyCode k)
    {
        if(k == KeyCode.Alpha0)
        {
            return KeyCode.Keypad0;
        }
        else if (k == KeyCode.Alpha1)
        {
            return KeyCode.Keypad1;
        }
        else if (k == KeyCode.Alpha2)
        {
            return KeyCode.Keypad2;
        }
        else if (k == KeyCode.Alpha3)
        {
            return KeyCode.Keypad3;
        }
        else if (k == KeyCode.Alpha4)
        {
            return KeyCode.Keypad4;
        }
        else if (k == KeyCode.Alpha5)
        {
            return KeyCode.Keypad5;
        }
        return k;
    }
}
