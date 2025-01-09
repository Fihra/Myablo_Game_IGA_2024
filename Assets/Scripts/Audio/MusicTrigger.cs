using UnityEngine;

public class MusicTrigger : MonoBehaviour
{

    public static int enemyCounter = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Enemy Counter: {enemyCounter}");
        if (enemyCounter < 1) MusicManager.instance.ChangeMusicLayer(MusicLayers.Exploration);
        else MusicManager.instance.ChangeMusicLayer(MusicLayers.Battle);
    }

    private string GetAliveStatus(Collider other)
    {
        return $"{other.name} is {other.GetComponent<BasicAI>().GetAlive()}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //Debug.Log(GetAliveStatus(other));
            if (other.GetComponent<BasicAI>().GetAlive())
            {
                enemyCounter++;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<BasicAI>().GetAlive())
                enemyCounter--;
        }
    }
}
