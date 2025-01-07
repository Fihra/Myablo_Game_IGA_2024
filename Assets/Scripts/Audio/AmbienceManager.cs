using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AmbienceManager : MonoBehaviour
{
    public static AmbienceManager instance;

    private EventInstance ambientEventInstance;

    private void InitializeAmbience(EventReference ambientEventReference)
    {
        ambientEventInstance = RuntimeManager.CreateInstance(ambientEventReference);
        ambientEventInstance.start();
    }

    public EventInstance FireAmbienceEvent()
    {
        return ambientEventInstance;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeAmbience(AmbienceEvents.instance.fireAmbientEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
