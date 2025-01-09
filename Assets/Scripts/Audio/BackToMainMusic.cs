using UnityEngine;
using FMODUnity;
using FMOD.Studio;
public class BackToMainMusic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PLAYBACK_STATE bossMusicState;

            BossMusicTrigger.instance.GetBossMusicInstance().getPlaybackState(out bossMusicState);

            if (bossMusicState == PLAYBACK_STATE.PLAYING)
            {
                BossMusicTrigger.instance.GetBossMusicInstance().stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                MusicManager.instance.GetMusicInstance().start();
            }
        }
    }
}
