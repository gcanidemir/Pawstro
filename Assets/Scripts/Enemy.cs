using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    /* Enemy Variables */
    enum Type
    {
        BOSS,
        MELEE,
        MINI_BOSS,
        RANGED
    }


    [SerializeField] private int maxhealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private int attackDamage;
    [SerializeField] private int attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask baseLayer;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Animator anim;
    [SerializeField] private bool canAttack;
    [SerializeField] private Type type;
    public SpriteRenderer sprite;
    private float timer;
    private Vector3 targetLocation;
    private Transform targetTransform;
    private int currenthealth;
    /* --------------- */

    /* Enemy stats are set */
    private void EnemyInit()
    {
        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }
    /* ------------------- */

    /* Chase mechanic for the enemies. Uses NavMeshPlus package. */
    private void ChaseInit()
    {
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Chase()
    {
        DecideWhichToFollow();
        navMeshAgent.SetDestination(targetLocation);
    }

    private void DecideWhichToFollow()
    {
        /* To follow player when gets too close while enemy is attacking the building */
        float offset = 3f;

        Vector3 enemyLocation = this.transform.position;
        Vector3 playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;

        /* Takes closest point on the surface between base and enemy */
        Vector3 buildingLocation = GameObject.FindGameObjectWithTag("Building").GetComponent<Collider2D>().ClosestPoint(enemyLocation);

        /*Distance between player and enemy*/
        float distPlayerEnemy = CommonUtils.Distance2D(buildingLocation, enemyLocation);
        /*Distance between building and enemy*/
        float distBuildingEnemy = CommonUtils.Distance2D(playerLocation, enemyLocation);

        if (distPlayerEnemy + offset < distBuildingEnemy)
        {
            targetLocation = buildingLocation;
            switch(type)
            {
                case Type.MELEE:
                    navMeshAgent.stoppingDistance = 2.75f;
                    break;
                case Type.BOSS:
                    navMeshAgent.stoppingDistance = 3f;
                    break;
                case Type.MINI_BOSS:
                    navMeshAgent.stoppingDistance = 4.5f;
                    break;
            }
            
        }
        else
        {
            targetLocation = playerLocation;
            switch (type)
            {
                case Type.MELEE:
                    navMeshAgent.stoppingDistance = 4f;
                    break;
                case Type.BOSS:
                    navMeshAgent.stoppingDistance = 4.5f;
                    break;
                case Type.MINI_BOSS:
                    navMeshAgent.stoppingDistance = 6f;
                    break;
            }
        }
                
        if(type == Type.MELEE)
        {
            Vector3 look = this.transform.GetChild(0).InverseTransformPoint(targetLocation);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 90;
            transform.GetChild(0).Rotate(0, 0, angle);
        } else if (type == Type.BOSS)
        {
            Vector3 look = this.transform.GetChild(0).InverseTransformPoint(targetLocation);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 180;
            transform.GetChild(0).Rotate(0, 0, angle);
        } else if (type == Type.MINI_BOSS)
        {
            Vector3 look = this.transform.GetChild(0).InverseTransformPoint(targetLocation);
            float angle = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg - 180;
            transform.GetChild(0).Rotate(0, 0, angle);
        }
    }
    /* --------------------------------------------------------- */

    public void TakeDamage(int damage)
    {
        this.transform.Find("Canvas").Find("Healthbar").gameObject.SetActive(true);

        if (damage < currenthealth)
        {
            currenthealth -= damage;
            StartCoroutine(FlashRed());

        }
        else
        {
            currenthealth = 0;
            die();
        }
        healthBar.SetHealth(currenthealth);
    }

    public IEnumerator FlashRed()
    {
        sprite.color = new Color32(233, 108, 108, 255);
        yield return new WaitForSeconds(0.5f);
        sprite.color = Color.white;
    }

    void die()
    {
        if (currenthealth <= 0)
        {
            Destroy(gameObject, 0.8f);
        }
    }
    void enemyAttack()
    {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    private void Start()
    {
        ChaseInit();
        EnemyInit();
    }

    private void Update()
    {
        Chase();
        enemyAttack();
    }
}
