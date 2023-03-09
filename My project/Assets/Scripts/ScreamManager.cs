using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreamManager : MonoBehaviour
{
    public static ScreamManager screamManager;

    [SerializeField]
    public List<GameObject> screamers = new List<GameObject>();
    [SerializeField]
    public List<KeyCode> screamKeys = new List<KeyCode>();
    [SerializeField]
    public List<KeyCode> screamKeysAlt = new List<KeyCode>();
    [SerializeField]
    public List<GameObject> asleepScreamers = new List<GameObject>();
    [SerializeField]
    public List<AudioClip> screamAudios = new List<AudioClip>();
    [SerializeField]
    public AudioClip snoreAudio;

    public List<GameObject> currentScreamerChain = new List<GameObject>();

    void Start()
    {
        screamManager = this;
    }


}
