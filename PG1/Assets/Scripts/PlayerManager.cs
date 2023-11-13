using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int damage;

    public int getDamage(){
        return damage;
    }
    void OnTriggerEnter(Collider col){
        EnemyManager enemyCol = col.gameObject.GetComponent<EnemyManager>();

        if (enemyCol != null){
            int enemyDamage = enemyCol.getDamage();
            TakeDamage(enemyDamage);
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
