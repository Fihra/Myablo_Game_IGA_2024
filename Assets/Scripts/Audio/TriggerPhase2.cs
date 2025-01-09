using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class TriggerPhase2 : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BossMusicTrigger.instance.ChangeMusicPhase(Phases.Phase_Two);
        }
    }


}
