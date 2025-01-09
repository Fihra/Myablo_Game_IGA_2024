using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Collections;

public class RestartBossMusic : MonoBehaviour
{
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            BossMusicTrigger.instance.RestartMusic();
        }
    }
}
