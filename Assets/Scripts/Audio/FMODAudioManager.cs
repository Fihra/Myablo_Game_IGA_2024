using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections.Generic;

public class FMODAudioManager : MonoBehaviour
{
    enum Busses
    {
        Master,
        Music,
        SFX,
        Environment
    }

    public static FMODAudioManager instance;

    [SerializeField] List<EventInstance> fmodEventInstances = new List<EventInstance>();


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void PlayOneShot(EventReference sfx, Vector3 worldPosition)
    {
        RuntimeManager.PlayOneShot(sfx, worldPosition);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
