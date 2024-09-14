using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [SerializeField] private int attackDamage;
    [SerializeField] private int maxHealth;
    [SerializeField] private int attackSpeed;
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void enemyAttack()
    {
        if (canAttack)
        {
            

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
}
