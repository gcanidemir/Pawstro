using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
    public Enemy enemy;
    public bool canHit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
            return;

        if (collision.gameObject.transform.parent.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();
            canHit = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision == null)
            return;

        if (collision.gameObject.transform.parent.gameObject.CompareTag("Enemy"))
        {
            enemy = collision.gameObject.transform.parent.gameObject.GetComponent<Enemy>();
            canHit = false;

        }
    }

    private IEnumerator HitTimer()
    {
        canHit = false;
        enemy.TakeDamage(25);
        yield return new WaitForSeconds(1f);
        canHit = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.F) && canHit)
            StartCoroutine(HitTimer());
    }
}
