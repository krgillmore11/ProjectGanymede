using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;

    public int getDamage(){
        return damage;
    }
    void OnTriggerEnter(Collider col){
        PlayerManager playerCol = col.gameObject.GetComponent<PlayerManager>();

        if (playerCol != null){
            int playerDamage = playerCol.getDamage();
            TakeDamage(playerDamage);
        }
    }

    void TakeDamage(int damage){
        health -= damage;

        if (health <= 0){
            Die();
        }
    }

    void Die(){
        
    }
}
