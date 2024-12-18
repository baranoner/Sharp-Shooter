using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [SerializeField] int damage = 2;
    [SerializeField] GameObject projectileHitVFX;
    Rigidbody myRigidbody;
    PlayerHealth player;
    
    void Awake() 
    {
      myRigidbody = GetComponent<Rigidbody>();  
    }
    void Start()
    {
       myRigidbody.linearVelocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other) 
    {
        player = other.GetComponent<PlayerHealth>();
        
        player?.TakeDamage(damage);
        
        Instantiate(projectileHitVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
