using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform turretHead;
    [SerializeField] Transform playerTargetPoint;
    [SerializeField] Transform projectileSpawnPoint;
    [SerializeField] float fireRate = 2f;

    PlayerHealth player;

    void Start() 
    {
        player = FindFirstObjectByType<PlayerHealth>();
        StartCoroutine(FireRoutine());   
    }
    void Update() 
    {
        if(playerTargetPoint)
        {
        turretHead.LookAt(playerTargetPoint.position);
        }
        
    }     
   
   IEnumerator FireRoutine()
   {
    while(player)
    {
        yield return new WaitForSeconds(fireRate);
        Projectile projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.transform.LookAt(playerTargetPoint);
    }
   }
}
