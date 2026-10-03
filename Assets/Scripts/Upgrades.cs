using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private GunsLookAt gunsLookAt1;
    [SerializeField] private GunsLookAt gunsLookAt2;
    [SerializeField] private GunsLookAt gunsLookAt3;
    [SerializeField] private GunsLookAt gunsLookAt4;
    [SerializeField] private Bullet bullet;
    [SerializeField] private BaseHealthDamage BaseHealthDamage;
    [SerializeField] private LaserHitbox laserHitbox;
    [SerializeField] private player Player;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private Health health;
    [SerializeField] private HealthBar OxygenBar;
    [SerializeField] private Oxygen oxygen;
    [SerializeField] private HealthBar FuelBar;
    [SerializeField] private Fuel fuel;
    [SerializeField] private HealthBar BaseHealth;
    [SerializeField] private Health BaseHp;
    [SerializeField] private Drill drill;
    [SerializeField] private Trigger trigger;
    [SerializeField] private PlayerMoney playerMoney;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private Transform drillrange;
    [SerializeField] private MeteorExplode meteorexplode;
    [SerializeField] private Teleport teleport;
    [SerializeField] private GameObject forcefield;
    [SerializeField] private GameObject companionbed;
    [SerializeField] private GameObject BaseChosmetics;
    private int Dashlvl, MaxHealthlvl, Oxygenlvl, HPregenlvl, Speedlvl, Drillvl, Fuellvl, FuelRegenlvl, StackSizelvl, MiningRangelvl, Fortunelvl, BaseTPlvl, ForceShieldlvl, BaseHealthlvl, BaseSheidllvl, OxygenRegenlvl, LaserDefenselvl, Turretlvl, OreProccesorlvl, CoinGeneratorlvl, CompanionBedlvl, BaseOverClocklvl, DeathRaylvl, BaseChosemeticslvl;
    private bool CoinGenOnline = false;
    private float CoinGenTimer = 20;
    [SerializeField] private TextMeshProUGUI DashUpgrade, HealthUpgrade, OxygenUpgrade, HPregenUpgrade, SpeedUpgrade, DrillUpgrade, FuelUpgrade, StackSizeUpgrade, MiningRangeUpgrade, FortuneUpgrade, BaseTPUpgrade, ForcceShieldUpgrade;

    [SerializeField] private TextMeshProUGUI FuelRegenUpgrade, BaseHealthUpgrade, BaseShieldUpgrade, OxygenRegenUpgrade, LaserDefenseUpgrade, TurretUpgrade, OreProccessorUpgrade, CoinGeneratorUpgrade, CompanionBedUpgrade, BaseOverclockUpgrade, DeathRayUpgrade, BaseChosmeticsUpgrade;

    public void Update()
    {
        if (CoinGenOnline)
        {
            CoinGenTimer -= Time.deltaTime;
            if (CoinGenTimer < 0)
            {
                CoinGenTimer = 20;
                playerMoney.EarnMoney(10 * CoinGeneratorlvl);
            }
        }
    }

    private bool TryBuy(ref int level, int baseCost, int maxLevel, TextMeshProUGUI label)
    {
        int cost = baseCost * (level + 1);
        if (!playerMoney.SpendMoney(cost, maxLevel, level))
            return false;

        level++;
        label.text = level >= maxLevel ? "Sold" : (baseCost * (level + 1)).ToString();
        return true;
    }

    public void UpgradeDashSpeed()
    {
        if (TryBuy(ref Dashlvl, 100, 2, DashUpgrade))
            Player.multiplier = Player.multiplier + 0.5f;
    }
    public void UpgradeHealth()
    {
        if (TryBuy(ref MaxHealthlvl, 100, 2, HealthUpgrade))
        {
            healthBar.SetMaxHealth(health.maxhealth + 50);
            healthBar.SetHealth(health.currenthealth + 50);
            health.maxhealth = health.maxhealth + 50;
            health.currenthealth = health.currenthealth + 50;
        }
    }
    public void UpgradeOxygen()
    {
        if (TryBuy(ref Oxygenlvl, 100, 2, OxygenUpgrade))
        {
            OxygenBar.SetMaxHealth(oxygen.maxhealth + 50);
            OxygenBar.SetHealth(oxygen.currenthealth + 50);
            oxygen.maxhealth = oxygen.maxhealth + 50;
            oxygen.currenthealth = oxygen.currenthealth + 50;
            trigger.oxlast = trigger.oxlast + 0.2f;
        }
    }
    public void UpgradeHPregen()
    {
        if (TryBuy(ref HPregenlvl, 100, 2, HPregenUpgrade))
            trigger.HPregen = trigger.HPregen + 0.2f;
    }

    public void UpgradeSpeed()
    {
        if (TryBuy(ref Speedlvl, 100, 2, SpeedUpgrade))
            Player.multiplier = Player.multiplier + 0.5f;

    }

    public void UpgradeFuel()
    {
        if (TryBuy(ref Fuellvl, 100, 2, FuelUpgrade))
        {
            FuelBar.SetMaxHealth(oxygen.maxhealth + 50);
            FuelBar.SetHealth(oxygen.currenthealth + 50);
            fuel.maxhealth = fuel.maxhealth + 50;
            fuel.currenthealth = fuel.currenthealth + 50;

        }
    }
    public void UpgradeDrillPower()
    {
        if (TryBuy(ref Drillvl, 100, 2, DrillUpgrade))
            drill.damagemod = drill.damagemod * 2;
    }
    public void Upgradestacksize()
    {
        if (TryBuy(ref StackSizelvl, 100, 2, StackSizeUpgrade))
            inventoryManager.maxStack = inventoryManager.maxStack + 1;
    }
    public void UpgradeMiningRange()
    {
        if (TryBuy(ref MiningRangelvl, 200, 2, MiningRangeUpgrade))
            drillrange.localScale = Vector3.one * (1 + MiningRangelvl);

    }
    public void UpgradeFortune()
    {
        if (TryBuy(ref Fortunelvl, 200, 3, FortuneUpgrade))
            meteorexplode.rarity = meteorexplode.rarity + 10;

    }
    public void UpgradeTP()
    {
        if (TryBuy(ref BaseTPlvl, 2000, 1, BaseTPUpgrade))
            teleport.CanTeleport = true;
    }
    public void UpgradeForceField()
    {
        if (TryBuy(ref ForceShieldlvl, 2000, 1, ForcceShieldUpgrade))
        {
            health.shieldmod *= 0.8f;
            forcefield.SetActive(true);
        }
    }


    //----------------------------------------------------------------------------------//

    public void UpgradeFuelRegen()
    {
        if (TryBuy(ref FuelRegenlvl, 100, 2, FuelRegenUpgrade))
            Player.fuelmod = Player.fuelmod + 0.2f;
    }
    public void UpgradeBaseHealth()
    {
        if (TryBuy(ref BaseHealthlvl, 100, 2, BaseHealthUpgrade))
        {
            BaseHealth.SetMaxHealth(BaseHp.maxhealth + 50);
            BaseHealth.SetHealth(BaseHp.currenthealth + 50);
            BaseHp.maxhealth = BaseHp.maxhealth + 50;
            BaseHp.currenthealth = BaseHp.currenthealth + 50;
        }
    }
    public void UpgradeBaseShield()
    {
        if (TryBuy(ref BaseSheidllvl, 100, 2, BaseShieldUpgrade))
            BaseHp.shieldmod = BaseHp.shieldmod + 1;
    }

    public void UpgradeOxygenRegen()
    {
        if (TryBuy(ref OxygenRegenlvl, 100, 2, OxygenRegenUpgrade))
            trigger.oxregen += 1;
    }

    public void UpgradeLaserDefense()
    {
        if (TryBuy(ref LaserDefenselvl, 100, 2, LaserDefenseUpgrade))
            laserHitbox.damagemodifier = laserHitbox.damagemodifier * 2;
    }
    public void UpgradeTurret()
    {
        if (TryBuy(ref Turretlvl, 100, 4, TurretUpgrade))
            BaseHealthDamage.turretlvl += 1;
    }

    public void UpgradeOreProccessor()
    {
        if (TryBuy(ref OreProccesorlvl, 200, 2, OreProccessorUpgrade))
        {
            inventoryManager.proccessmodifier = inventoryManager.proccessmodifier + 0.2f;
        }
    }

    public void UpgradeCoinGenerator()
    {
        if (TryBuy(ref CoinGeneratorlvl, 400, 2, CoinGeneratorUpgrade))
            CoinGenOnline = true;
    }

    public void UpgradeCompanionBed()
    {
        if (TryBuy(ref CompanionBedlvl, 500, 1, CompanionBedUpgrade))
            companionbed.SetActive(true);
    }

    public void UpgradeBaseOverClock()
    {
        if (TryBuy(ref BaseOverClocklvl, 2000, 1, BaseOverclockUpgrade))
        {

            bullet.bulletmodifier += 1;
            gunsLookAt1.shootspeedmodifier += 1;
            gunsLookAt2.shootspeedmodifier += 1;
            gunsLookAt3.shootspeedmodifier += 1;
            gunsLookAt4.shootspeedmodifier += 1;
        }
    }

    public void UpgradeDeathRay()
    {
        if (TryBuy(ref DeathRaylvl, 2000, 1, DeathRayUpgrade))
        {
            //death ray upgrade logic here
        }
    }

    public void UpgradeBaseChosmetics()
    {
         if (TryBuy(ref BaseChosemeticslvl, 2000, 1, BaseChosmeticsUpgrade))
        {
            BaseChosmetics.SetActive(true);
        }
    }


}
