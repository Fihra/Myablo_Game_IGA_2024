using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public enum Phases
{
    Phase_One,
    Phase_Two
}
public class BossMusicTrigger : MonoBehaviour
{
    public static BossMusicTrigger instance;

    [SerializeField] public EventReference bossMusicReference;
    EventInstance bossMusicEventInstance;

    PARAMETER_DESCRIPTION bossDescription;
    PLAYBACK_STATE musicState;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bossMusicEventInstance = RuntimeManager.CreateInstance(bossMusicReference);
        bossDescription = GetDescription(bossMusicEventInstance);
    }

    public EventInstance GetBossMusicInstance()
    {
        return bossMusicEventInstance;
    }

    public void ChangeMusicPhase(Phases currentPhase)
    {
        switch(currentPhase)
        {
            case Phases.Phase_One:
                bossMusicEventInstance.setParameterByID(bossDescription.id, 0);
                break;
            case Phases.Phase_Two:
                bossMusicEventInstance.setParameterByID(bossDescription.id, 100f);
                break;
        }
    }

    public void RestartMusic()
    {
        bossMusicEventInstance.getPlaybackState(out musicState);

        if (musicState == PLAYBACK_STATE.PLAYING)
            bossMusicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        StartCoroutine(RestartingMusic());

        bossMusicEventInstance.start();
        ChangeMusicPhase(Phases.Phase_One);
    }

    IEnumerator RestartingMusic()
    {
        yield return new WaitForSeconds(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public PARAMETER_DESCRIPTION GetDescription(EventInstance musicInstance)
    {
        EventDescription musicDescription;
        musicInstance.getDescription(out musicDescription);

        PARAMETER_DESCRIPTION intensityParameterDescription;
        musicDescription.getParameterDescriptionByName("Phase", out intensityParameterDescription);

        return intensityParameterDescription;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.Log("hi?");
            //if (musicState == PLAYBACK_STATE.PLAYING) return;
            //MusicManager.instance.GetMusicInstance().getPlaybackState(out musicState);

            EventInstance mainMusicInstance = MusicManager.instance.GetMusicInstance();

            mainMusicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            bossMusicEventInstance.start();

        }
    }
}
