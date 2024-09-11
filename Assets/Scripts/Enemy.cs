using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private Type type;
    public float maxhealth;
    public float currenthealth;
    public HealthBar healthBar;
    private int damage;
    private Vector3 targetLocation;
    private NavMeshAgent navMeshAgent;
    /* --------------- */

    /* Enemy stats are set */
    private void EnemyInit()
    {

        //TODO: Change magic numbers later. 
        switch (type)
        {
            case Type.BOSS:
                maxhealth = 50;
                damage = 20;
                break;

            case Type.MELEE:
                maxhealth = 100;
                damage = 50;
                break;

            case Type.MINI_BOSS:
                maxhealth = 100;
                damage = 70;
                break;

            case Type.RANGED:
                navMeshAgent.stoppingDistance = 10;
                maxhealth = 100;
                damage = 40;
                break;
        }

        currenthealth = maxhealth;
        healthBar.SetMaxHealth(maxhealth);
    }
    /* ------------------- */

    /* Chase mechanic for the enemies. Uses NavMeshPlus package. */
    private void ChaseInit()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
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
        float offset = 1.5f;

        Vector3 enemyLocation = this.transform.position;
        Vector3 playerLocation = GameObject.FindGameObjectWithTag("Player").transform.position;

        /* Takes closest point on the surface between base and enemy */
        Vector3 buildingLocation = GameObject.FindGameObjectWithTag("Building").GetComponent<Collider2D>().ClosestPoint(enemyLocation);

        /*Distance between player and enemy*/
        float distPlayerEnemy = CommonUtils.Distance2D(buildingLocation, enemyLocation);
        /*Distance between building and enemy*/
        float distBuildingEnemy = CommonUtils.Distance2D(playerLocation, enemyLocation);

        if (distPlayerEnemy + offset < distBuildingEnemy)
            targetLocation = buildingLocation;
        else
            targetLocation = playerLocation;
    }
    /* --------------------------------------------------------- */

    private void TakeDamage(float damage)
    {
        this.transform.Find("Canvas").Find("Healthbar").gameObject.SetActive(true);

        if (damage < currenthealth)
            currenthealth -= damage;
        else
            currenthealth = 0;
        healthBar.SetHealth(currenthealth);
    }

    private void Start()
    {
        ChaseInit();
        EnemyInit();
    }

    private void Update()
    {
        Chase();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(25f);
        }
    }

}
