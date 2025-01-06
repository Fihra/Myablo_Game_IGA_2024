using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public enum MusicLayers
{
    Exploration,
    Battle
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] EventInstance musicEventInstance;
    PARAMETER_DESCRIPTION musicDescription;
    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeMusic(MusicEvents.instance.musicEvent);
        musicDescription = GetDescription(musicEventInstance);
        ChangeMusicLayer(MusicLayers.Exploration);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeMusicLayer(MusicLayers layerName)
    {
        //Debug.Log($"Current Layer {layerName}");

        switch (layerName)
        {
            case MusicLayers.Exploration:
                musicEventInstance.setParameterByID(musicDescription.id, 0);
                break;
            case MusicLayers.Battle:
                musicEventInstance.setParameterByID(musicDescription.id, 1);
                break;
        }    
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = RuntimeManager.CreateInstance(musicEventReference);
        musicEventInstance.start();
    }

    private PARAMETER_DESCRIPTION GetDescription(EventInstance musicInstance)
    {
        EventDescription musicDescription;
        musicInstance.getDescription(out musicDescription);

        PARAMETER_DESCRIPTION intensityParameterDescription;
        musicDescription.getParameterDescriptionByName("Intensity", out intensityParameterDescription);

        return intensityParameterDescription;
    }
}
