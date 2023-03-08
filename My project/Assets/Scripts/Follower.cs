using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Follower : MonoBehaviour
{
    public float speed;
    public GameObject theFollowed;
    public bool awaken;

    void Start()
    {
        awaken = false;
    }

    void Update()
    {
        if (awaken)
        {
            if (Vector2.Distance(transform.position, theFollowed.transform.position) > 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, theFollowed.transform.position, speed * Time.deltaTime);
            }
        }      
    }

    public void AwakeScreamer()
    {
        theFollowed = ScreamManager.screamManager.currentScreamerChain.Last();
        ScreamManager.screamManager.currentScreamerChain.Add(gameObject);
        this.GetComponent<WakeupScream>().timerCanStart = false;
        awaken = true;
    }
}
