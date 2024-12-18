using System.Collections;
using UnityEngine;

public class SpawnGate : MonoBehaviour
{
    [SerializeField] GameObject enemyRobot;
    [SerializeField] GameObject SpawnPoint;
    [SerializeField] float delay = 5f;
    PlayerHealth player;
   
    void Start()
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(SpawnRobot()); 
    }

    
    void Update()
    {
       
    }

    IEnumerator SpawnRobot()
    {
        while(player)
        {
        Instantiate(enemyRobot, SpawnPoint.transform.position, SpawnPoint.transform.rotation);
        yield return new WaitForSeconds(delay);
        }
       
    }
}
