using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodable : MonoBehaviour
{
    [SerializeField] float explosionRadius = 8f;
    [SerializeField] int damageAmount = 20;
    [SerializeField] AudioClip explosionSound;
    [SerializeField] GameObject explosionParticle;

    public void Explode(){
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider hitCollider in hitColliders){
            EnemyManager em = hitCollider.GetComponent<EnemyManager>();
            PlayerManager pm = hitCollider.GetComponent<PlayerManager>();

            if (em != null){
                em.TakeDamage(damageAmount);
            }
            if (pm != null){
                pm.TakeDamage(damageAmount);
            }
        }

        if (explosionSound != null){
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }

        if (explosionParticle != null){
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
