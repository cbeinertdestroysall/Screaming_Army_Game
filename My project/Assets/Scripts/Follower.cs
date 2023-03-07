using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Follower : MonoBehaviour
{

    public float speed;
    public int distance = 20;
    public GameObject theFollowed;

    bool awaken;


    void Start()
    {
        awaken = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (awaken)
        {
            if (Vector2.Distance(transform.position, theFollowed.transform.position) > 110)
            {
                transform.position = Vector3.MoveTowards(transform.position, theFollowed.transform.position, speed * Time.deltaTime);
            }
        }      
    }

    public void AwakeScreamer()
    {
        theFollowed = ScreamManager.screamManager.currentScreamerChain.Last();
        ScreamManager.screamManager.currentScreamerChain.Add(gameObject);
        awaken = true;
    }
}
