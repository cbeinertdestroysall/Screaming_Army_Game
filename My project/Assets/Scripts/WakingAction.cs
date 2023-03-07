using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WakingAction : MonoBehaviour
{
    public KeyCode scream;
    public List<KeyCode> patternToWake = new List<KeyCode>();

    int currentWakeupIndex;

    [SerializeField]
    int currentAsleepScreamer;
    [SerializeField]
    AudioSource audioSource;

    int screamIndex;

    void Start()
    {
        currentWakeupIndex = 0;
    }

    void Update()
    {
        
        if (Input.anyKey)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(vKey))
                {
                    //Debug.Log(vKey);
                    List<KeyCode> screamKeys = gameObject.GetComponent<ScreamManager>().screamKeys;
                    for (int i = 0; i < screamKeys.Count; i++)
                    {
                        if (screamKeys[i] == vKey)
                        {
                            scream = gameObject.GetComponent<ScreamManager>().screamKeys[i];
                            screamIndex = i;
                            //Debug.Log("scream code is: " + scream);
                            break;
                        }
                    }
                }
            }
        }
        if (Input.GetKeyDown(scream))
        {
            Debug.Log("screaming key = " + scream);
            patternToWake = gameObject.GetComponent<ScreamManager>().asleepScreamers[currentAsleepScreamer].GetComponent<WakeupScream>().screamToWake;
            if (patternToWake != null)
            {
                MatchPattern();
            }

        }
    }

    void MatchPattern()
    {
        //Debug.Log("matching...");
        audioSource.clip = gameObject.GetComponent<ScreamManager>().screamAudios[screamIndex];
        audioSource.Play();
        if (patternToWake[currentWakeupIndex] == scream)
        {
            Debug.Log("correct scream");
            
            scream = KeyCode.None;
            currentWakeupIndex++;
            if (currentWakeupIndex == patternToWake.Count)
            {
                //wake the person up
                Debug.Log("WOKE UP!");
                gameObject.GetComponent<ScreamManager>().asleepScreamers[currentAsleepScreamer].GetComponent<Follower>().AwakeScreamer();
                gameObject.GetComponent<ScreamManager>().asleepScreamers[currentAsleepScreamer].GetComponent<Image>().color = new Color(255, 255, 255);

                currentWakeupIndex = 0;

                currentAsleepScreamer++;
            }
        }
        else
        {
            Debug.Log("wrong scream");
            currentWakeupIndex = 0;
        }
    }
}
