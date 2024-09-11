using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHitbox : MonoBehaviour
{
    private bool inArea = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            inArea = true;
            StartCoroutine(HitTimer());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            inArea = false;
        }
    }

    private IEnumerator HitTimer()
    {
        while (inArea)
        {
            Debug.Log("Enemy Hit");
            yield return new WaitForSeconds(3f);
        }
    }
}
