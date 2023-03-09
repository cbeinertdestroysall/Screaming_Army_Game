using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> instructions = new List<GameObject>();
    public GameObject instructionMenu;
    public GameObject player;
    public GameObject screamManager;
    int currentIndex;


    void Start()
    {
        currentIndex = 0;
        player.SetActive(false);
        screamManager.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            instructions[currentIndex].SetActive(false);
            if(currentIndex < instructions.Count-1)
            {
                instructions[currentIndex + 1].SetActive(true);
                currentIndex++;
            }
            else
            {
                instructionMenu.SetActive(false);
                player.SetActive(true);
                screamManager.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            instructions[currentIndex].SetActive(false);
            if (currentIndex > 0)
            {
                instructions[currentIndex - 1].SetActive(true);
                currentIndex--;
            }
            
        }
    }

}
