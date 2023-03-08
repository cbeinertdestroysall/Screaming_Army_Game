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

    public bool coroutineCanStart;

    public bool timeUp = false;

    IEnumerator DisableScreenPattern()
    {
        screamPatternDisplay.SetActive(true);
        yield return new WaitForSeconds(1);
        Debug.Log("coroutine has ended");
        screamPatternDisplay.SetActive(false);
        coroutineCanStart = false;


    }


    void Update()
    {
        if (coroutineCanStart)
        {
            StartCoroutine(DisableScreenPattern());
            coroutineCanStart = false;
            
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
            else if (Vector2.Distance(transform.position, player.transform.position) <= 3)
            {
                coroutineCanStart = true;
                StopCoroutine(DisableScreenPattern());
            }
            else
            {
                coroutineCanStart = false;
                screamPatternDisplay.SetActive(false);
                StopCoroutine(DisableScreenPattern());
            }
        }       
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            coroutineCanStart = true;
            
        }
    }*/

}
