using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public bool canFire;
    private float timer;
    public float timeBetweenFire;
    public LayerMask enemyLayer;
    public LayerMask meteorLayer;
    public Transform attackPoint;
    public float attackRange;
    public int attackDamage = 1;
    public int damagemod = 1;

    [Header("Laser")]
    [SerializeField] private LineRenderer laserLineRenderer;
    [SerializeField] private float laserRange;

    AudioManager audioManager;
    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        laserLineRenderer.positionCount = 2;
        laserLineRenderer.useWorldSpace = true;
        laserLineRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        look();
        laser();
    }

    private void look()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void laser()
    {
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer >= timeBetweenFire)
            {
                canFire = true;
                timer = 0;
            }
        }

        bool isFiring = Input.GetMouseButton(0);
        laserLineRenderer.enabled = isFiring;
        if(!isFiring)
        {
            return;
        }   
        
        Vector2 origin = attackPoint.position;
        Vector2 direction = transform.right;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, laserRange, meteorLayer);//şimdilik enemy vurmuyo

        Vector2 endPosition = hit.collider != null ? hit.point : origin + direction * laserRange;
        laserLineRenderer.SetPosition(0, origin);
        laserLineRenderer.SetPosition(1, endPosition);
        //damage part
        if (hit.collider != null && canFire)
        {
            MeteorExplode meteorexplode = hit.collider.GetComponent<MeteorExplode>();
            if (meteorexplode != null)
            {
                meteorexplode.takeDamage(attackDamage * damagemod);
                audioManager.PlaySFX(audioManager.rockhit);
            }
            canFire = false;
        }


    }
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
            Gizmos.color = Color.red;
        Gizmos.DrawLine(attackPoint.position, attackPoint.position + transform.right * laserRange);
    }
    public void UpgradeRange(float amount)
    {
        laserRange += amount;
    }
}
