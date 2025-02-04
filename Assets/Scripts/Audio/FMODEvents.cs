using UnityEngine;
using System.Collections.Generic;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    public static FMODEvents instance;

    [field: Header("Player SFX")]
    [field: Header("Player Melee Attack")]
    [field: SerializeField] public EventReference playerAttack { get; private set; }

    [field: Header("Player Receive Damage")]
    [field: SerializeField] public EventReference playerReceiveDamage { get; private set; }

    [field: Header("Player Fire Magic Cast")]
    [field: SerializeField] public EventReference playerFireCast { get; private set; }

    [field: Header("Player Fire Magic Hit Target")]
    [field: SerializeField] public EventReference playerFireHit { get; private set; }

    [field: Header("Player Footsteps")]
    [field: SerializeField] public EventReference grassFootsteps { get; private set; }

    [field: Header("Enemy SFX")]
    [field: Header("Slime Attack")]
    [field: SerializeField] public EventReference slimeAttack { get; private set; }

    [field: Header("Enemy Receive Damage")]
    [field: SerializeField] public EventReference enemyReceiveDamage { get; private set; }

    [field: Header("Enemy Attack")]
    [field: SerializeField] public EventReference enemyAttack { get; private set; }

    [field: Header("UI SFX")]
    
    [field: Header("Level up")]
    [field: SerializeField] public EventReference levelUpUI { get; private set; }

    [field: Header("Confirm")]
    [field: SerializeField] public EventReference confirmUI { get; private set; }

    [field: Header("Cancel")]
    [field: SerializeField] public EventReference cancelUI { get; private set; }


    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
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
