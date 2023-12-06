using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Camera cameraObject;
    [SerializeField] int health;
    [SerializeField] int Maxhealth = 100;
    [SerializeField] int damage = 100;
    [SerializeField] float shootRange = 100f;
    [SerializeField] float punchRange = 10f;
    [SerializeField] Transform gunBarrellT;
    [SerializeField] GameObject shootParticle;

    [SerializeField] HealthBar hb;

    void Start(){
        health = Maxhealth;
        hb.SetMax(Maxhealth);
    }

    public void Shoot(){
        Ray ray = new Ray(cameraObject.transform.position, cameraObject.transform.forward);//ray shooting directly from camera
        RaycastHit hit;
        Instantiate(shootParticle, gunBarrellT.position, Quaternion.identity);
        if (Physics.Raycast(ray, out hit, shootRange)){
            EnemyManager enemy = hit.collider.GetComponent<EnemyManager>();
            Explodable ex = hit.collider.GetComponent<Explodable>();
            if (enemy != null){
                enemy.TakeDamage(damage);
            }
            if (ex != null){
                ex.Explode();
            }
        }
    }

    public void Punch(){
        Ray ray = new Ray(cameraObject.transform.position, cameraObject.transform.forward);//ray shooting directly from camera
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, punchRange)){
            EnemyManager enemy = hit.collider.GetComponent<EnemyManager>();

            if (enemy != null){
                enemy.TakeDamage(damage);
            }
        }
    }

    public int getDamage(){
        return damage;
    }
    /*void OnTriggerEnter(Collider col){
        EnemyManager enemyCol = col.gameObject.GetComponent<EnemyManager>();

        if (enemyCol != null){
            int enemyDamage = enemyCol.getDamage();
            TakeDamage(enemyDamage);
        }
    }*/

    public void TakeDamage(int damage){
        health -= damage;
        hb.SetHealth(health);

        if (health <= 0){
            Die();
        }
    }

    void Die(){
        //Debug.Log("you dead");
    }
}
