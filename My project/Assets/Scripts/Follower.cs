using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Follower : MonoBehaviour
{
    public float speed;
    public GameObject theFollowed;
    public bool awaken;
    public Animator FollowerAnimator;
    Vector2 movement;
    void Start()
    {
        //FollowerAnimator = new Animator;
       
        awaken = false;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        FollowerAnimator.SetFloat("Horizontal", movement.x);
        FollowerAnimator.SetFloat("Vertical", movement.y);
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
        if (this.GetComponent<WakeupScream>().timeUp)
        {
            return;
        }
        theFollowed = ScreamManager.screamManager.currentScreamerChain.Last();
        ScreamManager.screamManager.currentScreamerChain.Add(gameObject);
        this.GetComponent<WakeupScream>().coroutineCanStart = false;
        awaken = true;
        FollowerAnimator.SetBool("Screamed", true);
        
    }
}
