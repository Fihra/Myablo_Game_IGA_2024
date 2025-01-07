using UnityEngine;
using FMODUnity;

public class AmbienceEvents : MonoBehaviour
{
    public static AmbienceEvents instance;

    [field: Header("Ambience")]

    [field: Header("Fire")]
    [field: SerializeField] public EventReference fireAmbientEvent { get; private set; }

    [field: Header("Forest")]
    [field: SerializeField] public EventReference forestAmbientEvent { get; private set; }

    [field: Header("Water Stream")]
    [field: SerializeField] public EventReference waterStreamAmbientEvent { get; private set; }

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
