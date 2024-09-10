using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorExplode : MonoBehaviour
{
    public GameObject drop;
    public GameObject rareDrop;
    public int dropCount = 10;
    public float spread = 2f;
    public int rarity = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explode()
    {
        while (dropCount > 0)
        {
        int randomIndex = Random.Range(0,100);
            if (randomIndex > rarity) { 
            dropCount--;
            Vector3 pos = transform.position;
            pos.x += spread * UnityEngine.Random.value - spread / 2;
            pos.y += spread * UnityEngine.Random.value - spread / 2;
            GameObject go = Instantiate(drop);
            go.transform.position = pos;
            }
            else
            {
                dropCount--;
                Vector3 pos = transform.position;
                pos.x += spread * UnityEngine.Random.value - spread / 2;
                pos.y += spread * UnityEngine.Random.value - spread / 2;
                GameObject go = Instantiate(rareDrop);
                go.transform.position = pos;
            }
        }

        Destroy(gameObject);
    }
}
