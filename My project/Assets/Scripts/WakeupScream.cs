using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WakeupScream : MonoBehaviour
{

    [SerializeField]
    KeyCode scream; //this character's scream key

    [SerializeField]
    public List<KeyCode> screamToWake = new List<KeyCode>(); //the pattern of scream to wake this character up

    [SerializeField]
    public List<GameObject> numbers = new List<GameObject>();

    public GameObject screamPatternDisplay;
    public GameObject player;


    public bool coroutineCanStart;

    public bool timeUp = false;

    bool poped = false;

    bool timerCanStart;
    
    public float timeLeft;

    public GameObject timer;

    IEnumerator DisableScreenPattern()
    {
        Debug.Log("coroutine started");
        screamPatternDisplay.SetActive(true);
        yield return new WaitForSeconds(timeLeft);
        Debug.Log("coroutine has ended");
        screamPatternDisplay.SetActive(false);
        coroutineCanStart = false;
        poped = true;

    }


    void Update()
    {
        if (timer != null)
        {
            timer.GetComponent<TMP_Text>().text = "Time left to view pattern: " + timeLeft;
        }
        if (timerCanStart && timer != null)
        {
            if (timeLeft > 0)
                timeLeft -= Time.deltaTime;
            else if (timeLeft <= 0)
                timeLeft = 0;
        }
        
        if (coroutineCanStart == false)
        {
            StopCoroutine(DisableScreenPattern());
        }

        if(player != null)
        {
            if (gameObject.GetComponent<Follower>().awaken)
            {
                screamPatternDisplay.SetActive(false);
            }
            else if (Vector2.Distance(transform.position, player.transform.position) <= 3 && !poped)
            {
                timerCanStart = true;

                StartCoroutine(DisableScreenPattern());               
            }
            else
            {
                coroutineCanStart = false;
                screamPatternDisplay.SetActive(false);
                //poped = false;
                StopCoroutine(DisableScreenPattern());
            }
        }       
    }

}
