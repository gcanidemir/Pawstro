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
        BOMBER,
        BOSS,
        DEMOLISHER,
        LONG_RANGED,
        MELEE,
        MID_RANGED,
        MINI_BOSS,
        TANK
    }

    [SerializeField] private int maxHealth;
    private int currentHealth;
    [SerializeField] private int damage;
    [SerializeField] private Type type;

    [SerializeField] private Transform targetLocation;
    private NavMeshAgent navMeshAgent;
    /* --------------- */


    /* Enemy stats are set */
    private void EnemyInit()
    {
        //TODO: Change magic numbers later. 
        switch (type)
        {
            case Type.BOMBER:
                maxHealth = 10;
                damage = 10;
                break;

            case Type.BOSS:
                maxHealth = 20;
                damage = 20;
                break;

            case Type.DEMOLISHER:
                maxHealth = 30;
                damage = 30;
                break;

            case Type.LONG_RANGED:
                maxHealth = 40;
                damage = 40;
                break;

            case Type.MELEE:
                maxHealth = 50;
                damage = 50;
                break;

            case Type.MID_RANGED:
                maxHealth = 60;
                damage = 60;
                break;

            case Type.MINI_BOSS:
                maxHealth = 70;
                damage = 70;
                break;

            case Type.TANK:
                maxHealth = 80;
                damage = 80;
                break;
        }

        currentHealth = maxHealth;
    }

    /* Chase mechanic for the enemies. Uses NavMeshPlus package. */
    private void ChaseInit()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
    }

    private void Chase()
    {
        navMeshAgent.SetDestination(targetLocation.position);
    }
    /* --------------------------------------------------------- */

    private void Start()
    {
        EnemyInit();
        ChaseInit();
    }

    private void Update()
    {
        Chase();
    }

}
