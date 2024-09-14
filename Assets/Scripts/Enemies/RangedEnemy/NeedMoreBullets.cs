using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedMoreBullets : MonoBehaviour
{
    [SerializeField] float Speed = 4.5f;
    [SerializeField] Animator anim;
    [SerializeField] GameObject myObject;
    [SerializeField] Transform attackPoint;
    [SerializeField] int attackDamage;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask baseLayer;
    [SerializeField] float attackRange;
    float timer;
    float deathTimer = 5f;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
    }

    private void Start()
    {
        StartCoroutine(DestroyAmmo());
    }

    IEnumerator DestroyAmmo()
    {
        yield return new WaitForSeconds(5f);
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);
        
        Collider2D[] hitBase = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, baseLayer);
        foreach (Collider2D player in hitPlayer)
        {
            anim.SetTrigger("Contact");
            Destroy(gameObject, 0.4f);
        }
        foreach (Collider2D construct in hitBase)
        {
            anim.SetTrigger("Contact");
            Destroy(gameObject, 0.4f);
        }

        


    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
