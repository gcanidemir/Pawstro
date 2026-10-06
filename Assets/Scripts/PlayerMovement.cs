using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;

public class player : MonoBehaviour
{

    [Header("Upgrade Stats")]
    public float multiplier = 1; // sprint/dash upgrade
    public float speedbonus = 1; // speed upgrade
    public float fuelmod = 1; // fuel efficiency upgrade
    public bool CanTeleport = false;

    [Header("Movement")]
    //replaces old 'acceleration'
    [SerializeField] private float walkAcceleration = 1f;
    //replaces old 'acceleration' when sprinting
    [SerializeField] private float sprintAcceleration = 3f;
    //public -> [SerializeField] private
    [SerializeField] private float deceleration = 1f;
    //public -> [SerializeField] private
    [SerializeField] private float Speedtolerance = 0.1f;
    //replaces hardcoded '/ 5' in OnCollisionEnter2D
    [SerializeField] private float bounceDamping = 5f;

    [Header("Fuel")]
    //replaces 0.01 per frame (now frame-rate independent, same as 60 FPS)
    [SerializeField] private float moveFuelPerSecond = 0.6f;
    //replaces 0.05 per frame (now frame-rate independent, same as 60 FPS)
    [SerializeField] private float sprintFuelPerSecond = 3f;
    //public -> [SerializeField] private
    [SerializeField] private Fuel fuel;

    [Header("Teleport")]
    //replaces hardcoded '15' in tp()
    [SerializeField] private float teleportCooldownTime = 15f;
    //public -> [SerializeField] private
    [SerializeField] private HealthBar TPBar;
    //public -> [SerializeField] private
    [SerializeField] private GameObject TpHud;
    //public -> [SerializeField] private
    [SerializeField] private Transform _player;

    [Header("Sprites & Animation")]
    //public -> [SerializeField] private
    [SerializeField] private Transform PlayerSprite;
    //public -> [SerializeField] private
    [SerializeField] private Transform DrillSprite;
    //public -> [SerializeField] private
    [SerializeField] private GameObject Drill;
    //public -> [SerializeField] private
    [SerializeField] private Animator anim;

    [Header("Collision")]
    //public -> [SerializeField] private
    [SerializeField] private Transform bouncebox;
    //public -> [SerializeField] private
    [SerializeField] private GameObject playerpos;

    [Header("Audio")]
    //public -> [SerializeField] private (currently unused in this script)
    [SerializeField] private AudioManager audioManager;

    // ── Constants ──
    //replaces private 'speedConstant' (was set to 10)
    private const float BaseSpeed = 10f;
    //replaces hardcoded '/ 10' when fuel is empty
    private const float EmptyFuelSlowdown = 10f;

    // ── Private Fields ──
    //6 copy-pasted if-blocks replaced by a single array of tags
    private static readonly string[] PickupTags = { "CommonOre", "CommonGem", "RareOre", "RareGem", "LegendaryOre", "LegendaryGem" };

    private DemoScript demoScript;
    Rigidbody2D rb;
    private Vector2 input;
    private Vector2 currentSpeed;
    private float teleportCooldown;

    // CHANGED: removed 'speedConstant = 10' (now const BaseSpeed)
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        demoScript = GetComponent<DemoScript>();
        TpHud.SetActive(false);
    }

    // CHANGED: was ~150 lines doing everything; now reads input once and calls 4 methods
    private void Update()
    {
        //one Vector2 instead of separate speedx / speedy
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        bool isMoving = input != Vector2.zero;
        //replaces 'FuelEmpty' field; '> 0' instead of '== 0' (safer with floats)
        bool hasFuel = fuel.currenthealth > 0f;
        // FIX: sprint now requires fuel AND movement (old code kept sprint speed when Shift was held at 0 fuel)
        bool isSprinting = isMoving && hasFuel && Input.GetKey(KeyCode.LeftShift);

        HandleFuel(isMoving, isSprinting);
        HandleMovement(isSprinting, hasFuel);
        HandleFacing();
        HandleTeleport();
    }

    // ── Fuel ──

    //extracted from Update()
    private void HandleFuel(bool isMoving, bool isSprinting)
    {
        float drain = 0f;
        if (isMoving) drain += moveFuelPerSecond;
        // FIX: sprint only drains while moving (old code drained when standing with Shift held)
        if (isSprinting) drain += sprintFuelPerSecond;

        // FIX: multiplied by Time.deltaTime (old code drained per frame, so higher FPS = faster fuel loss)
        if (drain > 0f)
            fuel.takedamage(drain * Time.deltaTime / fuelmod);
    }

    // ── Movement ──

    //Extracted from Update(); replaces targetspeedX/Y logic and the shift / non-shift if-blocks
    private void HandleMovement(bool isSprinting, bool hasFuel)
    {
        // Same values as before: walk = 10 * speedbonus, sprint = 20 * multiplier * speedbonus
        float targetSpeed = isSprinting
            ? 2f * BaseSpeed * multiplier * speedbonus
            : BaseSpeed * speedbonus;

        // Same values as before: walk = 1 * speedbonus, sprint = 3 * speedbonus
        float accel = (isSprinting ? sprintAcceleration : walkAcceleration) * speedbonus;

        //8 near-identical if-blocks replaced by one helper called per axis
        currentSpeed.x = Approach(currentSpeed.x, input.x, targetSpeed, accel);
        currentSpeed.y = Approach(currentSpeed.y, input.y, targetSpeed, accel);

        //4 rb.velocity branches merged into 2 simple modifiers
        Vector2 velocity = currentSpeed;
        if (input.x != 0f && input.y != 0f) velocity /= Mathf.Sqrt(2f); // diagonal
        // FIX: old empty-fuel diagonal was '/ 10 * Sqrt(2)' = (x / 10) * 1.41, which made diagonal FASTER than straight
        if (!hasFuel) velocity /= EmptyFuelSlowdown;

        rb.velocity = velocity;
    }

    // replaces the 8 acceleration/deceleration if-blocks (same behavior)
    //  Below max speed -> accelerate, above -> decelerate. No input -> slow to 0.
    private float Approach(float current, float axisInput, float speed, float accel)
    {
        float max = axisInput != 0f ? speed : 0f;
        float target = Mathf.Sign(axisInput) * max;
        float rate = Mathf.Abs(current) <= max ? accel : deceleration;
        return Mathf.Lerp(current, target, rate * Time.deltaTime);
    }

    // ── Sprite facing ──

    // extracted from Update(); 3 if-blocks with 6 duplicated scale lines
    //  merged into one decision + one assignment (same behavior)
    private void HandleFacing()
    {
        bool faceRight;

        if (currentSpeed.x > Speedtolerance)
            faceRight = true;
        else if (currentSpeed.x < -Speedtolerance)
            faceRight = false;
        else
            // UNCHANGED logic: rotation.z is a quaternion component, not an angle
            // (0.9 ≈ 128°). For an exact 90° check use: Drill.transform.right.x >= 0f
            faceRight = Mathf.Abs(Drill.transform.rotation.z) <= 0.9f;

        Vector3 scale = faceRight ? Vector3.one : new Vector3(-1f, 1f, 1f);
        PlayerSprite.localScale = scale;
        DrillSprite.localScale = scale;
    }

    // ── Teleport ──

    // extracted from Update()
    private void HandleTeleport()
    {
        // CHANGED: only calls SetActive when the value changes (was every frame)
        if (TpHud.activeSelf != CanTeleport)
            TpHud.SetActive(CanTeleport);

        // CHANGED: two if-blocks merged; Mathf.Max clamps at 0
        if (teleportCooldown > 0f)
        {
            teleportCooldown = Mathf.Max(0f, teleportCooldown - Time.deltaTime);
            TPBar.SetHealth(teleportCooldown);
        }

        // CHANGED: nested ifs merged into one condition
        if (CanTeleport && teleportCooldown == 0f && Input.GetKey(KeyCode.B))
        {

            anim.SetBool("isTP", true);
        }
    }

    public void tp()
    {
        _player.localPosition = Vector3.zero;
        // hardcoded 15 -> teleportCooldownTime
        teleportCooldown = teleportCooldownTime;
        TPBar.SetMaxHealth(teleportCooldownTime);
        TPBar.SetHealth(teleportCooldownTime);
        anim.SetBool("isTP", false);
    }

    // ── Collisions ──

    // made private (Unity calls it either way)
    private void OnCollisionEnter2D(Collision2D col)
    {
        // early exit instead of wrapping everything in 'if (contacts.Length > 0)'
        if (col.contactCount == 0) return;

        // bouncepoint / playerposition / bounceboxposition fields -> one local
        bouncebox.localPosition = currentSpeed;
        Vector2 direction = bouncebox.position - playerpos.transform.position;
        // FIX: GetContact(0) instead of allocating a new array on every collision
        Vector2 normal = col.GetContact(0).normal;

        // hardcoded '/ 5' -> bounceDamping; '-1 *' simplified to '-'
        if (Mathf.Abs(normal.x) > Mathf.Abs(normal.y))
            currentSpeed.x = -direction.x / bounceDamping;
        else if (Mathf.Abs(normal.x) < Mathf.Abs(normal.y))
            currentSpeed.y = -direction.y / bounceDamping;
    }

    // 6 copy-pasted if-blocks replaced by a loop over PickupTags
    //  (array index = item id, same as before: 0 = CommonOre ... 5 = LegendaryGem)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int i = 0; i < PickupTags.Length; i++)
        {
            if (collision.CompareTag(PickupTags[i]))
            {
                demoScript.PickItem(i);
                // CHANGED: 'pickedItem' field removed, uses collision.gameObject
                Destroy(collision.gameObject);
                // NEW: stop after a match (old code checked all 6 tags every time)
                return;
            }
        }
    }

}