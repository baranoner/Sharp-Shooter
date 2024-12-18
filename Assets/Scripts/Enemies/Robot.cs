using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;
    EnemyHealth enemyHealth;

    const string PLAYER_STRING = "Player";

    void Awake(){
        agent = GetComponent<NavMeshAgent>();
        enemyHealth = GetComponent<EnemyHealth>();
    }
    
    void Start()
    {
        player = FindFirstObjectByType<FirstPersonController>();
    }

    
    void Update()
    {
        if(!player) return;
        agent.SetDestination(player.transform.position);  
    }

    void OnTriggerEnter(Collider other) 
    {
       if(other.CompareTag(PLAYER_STRING))
       {
        enemyHealth.SelfDestruct();
       } 
    }
}
