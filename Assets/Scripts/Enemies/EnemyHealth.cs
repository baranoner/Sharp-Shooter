using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
  [SerializeField] GameObject robotExplosionVFX;
  [SerializeField] int maxHealth = 3;
   int currentHealth;

   GameManager gameManager;

   void Start()
   {
    gameManager = FindFirstObjectByType<GameManager>();
    gameManager.AdjustEnemiesLeft(1);
    currentHealth = maxHealth;
   }
   public void TakeDamage(int damage)
   {
    currentHealth -= Mathf.Abs(damage);

    if(currentHealth <= 0)
    {
      SelfDestruct();
    }   
    
   }
   public void SelfDestruct()
   {
    gameManager.AdjustEnemiesLeft(-1);
    Instantiate(robotExplosionVFX, transform.position, Quaternion.identity);  
    Destroy(gameObject);

   }
}
