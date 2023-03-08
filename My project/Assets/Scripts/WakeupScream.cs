using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    
    public float timeStart = 5;
    public float timeLeft = 5;

    public bool timerCanStart;

    public bool timeUp = false;

    /*IEnumerator TimeLeft()
    {
        yield return new WaitForSeconds(1);
        timeLeft -= Time.deltaTime;
        timerCanStart = false;
    }*/


    void Update()
    {
        if (timerCanStart)
        {
            if (timeLeft > 0)
                timeLeft -= Time.deltaTime;
            else if (timeLeft <= 0)
                timeLeft = 0;
                timeUp = true;
        }
        else 
        {
            timeLeft = timeStart;
        }

        if(player != null)
        {
            if (gameObject.GetComponent<Follower>().awaken)
            {
                screamPatternDisplay.SetActive(false);
            }
            else if (Vector2.Distance(transform.position, player.transform.position) <= 3)
            {
                timerCanStart = true;
                screamPatternDisplay.SetActive(true);
            }
            else
            {
                timerCanStart = false;
                screamPatternDisplay.SetActive(false);
            }
        }       
    }

}
