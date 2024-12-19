using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public UnityEvent<float> onExperienceGranted;
    public UnityEvent<float> onExperienceUpdated;
    public UnityEvent<float> onHealthChanged;
    public UnityEvent<float> onManaChanged;
    public UnityEvent onPlayerDied;
    public UnityEvent onPlayerRevived;
    public UnityEvent onPlayerLeveledUp;
    public UnityEvent onStatPointSpent;
    public UnityEvent onSkillPointSpent;
    public UnityEvent<ClassSkill> onNewAbility2Equipped;

    public UnityEvent onDialogueStarted;
    public UnityEvent onDialogueEnded;

    public static EventsManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}

