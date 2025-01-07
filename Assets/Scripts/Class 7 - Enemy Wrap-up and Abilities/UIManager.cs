using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerHUD;
    [SerializeField] GameObject characterStatPanel;
    [SerializeField] GameObject skillTree;

    public static UIManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        // HideAll();
    }

    public void HideAll()
    {
        // Hide everything
        HidePlayerHUD();
        HideCharacterStatsPanel();
        HideSkillTree();
    }

    #region PlayerHUD
    public void ShowPlayerHUD()
    {
        playerHUD.SetActive(true);
    }
    public void HidePlayerHUD()
    {
        playerHUD.SetActive(false);
    }
    #endregion

    #region Character Stats Panel
    public void ShowCharacterStatsPanel()
    {
        characterStatPanel.SetActive(true);
    }
    public void HideCharacterStatsPanel()
    {
        characterStatPanel.SetActive(false);
    }
    public void ToggleCharacterStatsPanel()
    {
        if (characterStatPanel.activeInHierarchy)
        {
            HideCharacterStatsPanel();
            FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.cancelUI, transform.position);
        }
        else
        {
            ShowCharacterStatsPanel();
            FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.confirmUI, transform.position);
        }
            
    }

    #endregion

    #region Skill Tree
    public void ShowSkillTree()
    {
        skillTree.SetActive(true);
    }

    public void HideSkillTree()
    {
        skillTree.SetActive(false);
    }

    public void ToggleSkillTree()
    {
        if (skillTree.activeInHierarchy)
        {
            HideSkillTree();
            FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.cancelUI, transform.position);
        }
        else
        {
            ShowSkillTree();
            FMODAudioManager.instance.PlayOneShot(FMODEvents.instance.confirmUI, transform.position);
        }
            
    }

    #endregion
}
