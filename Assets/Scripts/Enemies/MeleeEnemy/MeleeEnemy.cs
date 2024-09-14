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
    int currentHealth;
    private float timer;
    private bool canAttack = true;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
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
            }
            foreach (Collider2D construct in hitBase)
            {
                construct.GetComponent<Health>().takedamage(attackDamage);
            }
            canAttack = false;
            anim.SetTrigger("Bite");

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
}
