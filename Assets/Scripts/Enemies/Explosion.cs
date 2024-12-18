using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] float radius = 1.5f;
    [SerializeField] int damage = 5;

    const string PLAYER_STRING = "Player";
    void Start()
    {
        Explode();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        if(colliders.Length == 0) return;

        foreach(Collider collider in colliders)
        {
            if(collider.CompareTag(PLAYER_STRING))
            {
            GameObject player = collider.gameObject; 
            player.GetComponent<PlayerHealth>().TakeDamage(damage);
            break;
            }
        }
    }
}
