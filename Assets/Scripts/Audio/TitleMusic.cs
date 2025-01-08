using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using UnityEngine.SceneManagement;

public class TitleMusic : MonoBehaviour
{
    [SerializeField] public EventReference titleMusicReference;
    EventInstance titleMusicEventInstance;

    public static TitleMusic instance;

    public PLAYBACK_STATE musicState;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        titleMusicEventInstance = RuntimeManager.CreateInstance(titleMusicReference);
        titleMusicEventInstance.start();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu")
        {
            titleMusicEventInstance.getPlaybackState(out musicState);

            if (musicState == PLAYBACK_STATE.STOPPED)
                titleMusicEventInstance.start();
        }

        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            titleMusicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
