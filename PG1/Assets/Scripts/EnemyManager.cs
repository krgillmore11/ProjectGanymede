using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    //public Camera cameraObject;

    [SerializeField] int health = 100;
    [SerializeField] int damage = 100;
    //[SerializeField] float shootRange = 100f;
    EnemyController ec;

    void Start(){
        ec = GetComponent<EnemyController>();
    }

    /*public void Shoot(){
            Ray ray = new Ray(cameraObject.transform.position, cameraObject.transform.forward);//ray shooting directly from camera
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, shootRange)){
                PlayerManager player = hit.collider.GetComponent<PlayerManager>();

                if (player != null){
                    player.TakeDamage(damage);
                }
            }
        }*/

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

    public void TakeDamage(int damage){
        health -= damage;
        ec.StartChase();//if enemy takes damage chase

        if (health <= 0){
            Die();
        }
    }

    void Die(){
        Destroy(gameObject);
    }
}
