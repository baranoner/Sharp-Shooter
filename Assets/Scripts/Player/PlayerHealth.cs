using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] int maxHealth = 10;
    [SerializeField] CinemachineVirtualCamera deathVirtualCamera;
    [SerializeField] Transform weaponCamera;
    [SerializeField] Image[] shieldBars;
    [SerializeField] GameObject gameOverContainer;
    int currentHealth;
    int gameOverVirtualCameraPriority = 20;

   void Start()
   {
    
    currentHealth = maxHealth;
    AdjustShieldUI();
   }
   public void TakeDamage(int damage)
   {
    currentHealth -= Mathf.Abs(damage);
    AdjustShieldUI();

    if(currentHealth <= 0)
        {
            PlayerGameOver();
        }

    }

    void PlayerGameOver()
    {
        weaponCamera.parent = null;
        deathVirtualCamera.Priority = gameOverVirtualCameraPriority;
        gameOverContainer.SetActive(true);
        StarterAssetsInputs starterAssetsInputs = FindFirstObjectByType<StarterAssetsInputs>();
        starterAssetsInputs.SetCursorState(false);
        Destroy(gameObject);
    }

    void AdjustShieldUI()
{
    
    for(int i = 0; i < shieldBars.Length; i++)
    {
      if(i < currentHealth)
      {
        shieldBars[i].gameObject.SetActive(true);
      }
      else
      {
        shieldBars[i].gameObject.SetActive(false);
      }
    }
}
}
