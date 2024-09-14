using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask baseLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    int currentHealth;
    private float timer;
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        enemyAttack();
    }
    // Update is called once per frame

    void enemyAttack() { 
        if (canAttack)
        {
            Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
            Collider2D[] hitBase = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, baseLayer);
            foreach (Collider2D player in hitPlayer)
            {
                player.GetComponent<Health>().takedamage(attackDamage);
                anim.SetTrigger("Bite");
            }
            foreach (Collider2D construct in hitBase)
            {
                construct.GetComponent<Health>().takedamage(attackDamage);
                anim.SetTrigger("Bite");
            }
            canAttack = false;
            

        }
        else
        {
            timer += Time.deltaTime;
            if (timer > attackSpeed)
            {
                canAttack = true;
                timer = 0;
            }
        }

    }

    void takeDamage(int damage)
    {
        currentHealth -= damage;

        die();
    }

    void die()
    {
        if (currentHealth <= 0){

            Destroy(gameObject, 0.8f);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
    private void look()
    {

        Vector2 rotation = player.position - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
