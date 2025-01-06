using UnityEngine;
using FMODUnity;

public class MusicEvents : MonoBehaviour
{
    public static MusicEvents instance;

    [field: Header("Music")]
    [field: SerializeField] public EventReference musicEvent { get; private set; }

    [field: Header("Exploration")]
    [field: SerializeField] public MusicLayers explorationLayer { get; private set; }

    [field: Header("Battle")]
    [field: SerializeField] public MusicLayers battleLayer { get; private set; }

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
