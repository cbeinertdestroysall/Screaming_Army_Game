using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupScream : MonoBehaviour
{
    [SerializeField]
    KeyCode scream; //this character's scream key

    [SerializeField]
    public List<KeyCode> screamToWake = new List<KeyCode>(); //the pattern of scream to wake this character up

    public GameObject screamPatternDisplay;
    public GameObject player;

    void Update()
    {
        if(player != null)
        {
            if (gameObject.GetComponent<Follower>().awaken)
            {
                screamPatternDisplay.SetActive(false);
            }
            else if (Vector2.Distance(transform.position, player.transform.position) <= 3)
            {
                screamPatternDisplay.SetActive(true);
            }
            else
            {
                screamPatternDisplay.SetActive(false);
            }
        }       
    }

}
