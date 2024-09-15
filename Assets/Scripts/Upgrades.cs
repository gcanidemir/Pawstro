using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{
    public GunsLookAt gunsLookAt1;
    public GunsLookAt gunsLookAt2;
    public GunsLookAt gunsLookAt3;
    public GunsLookAt gunsLookAt4;
    public Bullet bullet;
    public BaseHealthDamage BaseHealthDamage;
    public LaserHitbox laserHitbox;
    public player Player;
    public HealthBar healthBar;
    public Health health;
    public HealthBar OxygenBar;
    public Oxygen oxygen;
    public HealthBar FuelBar;
    public HealthBar BaseHealth;
    public Health BaseHp;
    public Fuel fuel;
    public Drill drill;
    public Trigger trigger;
    public PlayerMoney playerMoney;
    public InventoryManager inventoryManager;
    public Transform drillrange;
    public MeteorExplode meteorexplode;
    public Teleport teleport;
    public GameObject forcefield;
    public GameObject companionbed;
    public GameObject BaseChosmetics;
    public int Dashlvl, MaxHealthlvl, Oxygenlvl, HPregenlvl, Speedlvl, Drillvl, Fuellvl, FuelRegenlvl, StackSizelvl, MiningRangelvl, Fortunelvl, BaseTPlvl, ForceShieldlvl,BaseHealthlvl,BaseSheidllvl,OxygenRegenlvl,LaserDefenselvl,Turretlvl,OreProccesorlvl,CoinGeneratorlvl,CompanionBedlvl,BaseOverClocklvl,DeathRaylvl,BaseChosemeticslvl;
    public int Success;
    public bool CoinGenOnline = false;
    public float CoinGenTimer = 20;
    public TextMeshProUGUI DashUpgrade, HealthUpgrade, OxygenUpgrade, HPregenUpgrade, SpeedUpgrade, DrillUpgrade, FuelUpgrade, StackSizeUpgrade, MiningRangeUpgrade, FortuneUpgrade, BaseTPUpgrade, ForcceShieldUpgrade;

    public TextMeshProUGUI FuelRegenUpgrade, BaseHealthUpgrade, BaseShieldUpgrade, OxygenRegenUpgrade, LaserDefenseUpgrade, TurretUpgrade, OreProccessorUpgrade, CoinGeneratorUpgrade, CompanionBedUpgrade, BaseOverclockUpgrade, DeathRayUpgrade, BaseChosmeticsUpgrade;

    public void Update()
    {
        if(CoinGenOnline)
        {
            CoinGenTimer -= Time.deltaTime;
            if (CoinGenTimer < 0)
            {
                CoinGenTimer = 20;
                playerMoney.EarnMoney(10 * CoinGeneratorlvl);
            }
        }
    }
    public void UpgradeDashSpeed()
    {
        Success = playerMoney.SpendMoney(100 * (Dashlvl + 1),2,Dashlvl);
        if (Success == 1)
        {
            Player.multiplier = Player.multiplier + 0.5f;
            Dashlvl = Dashlvl + 1;
            DashUpgrade.text = (100 * (Dashlvl + 1)).ToString();
        }

    }
    public void UpgradeHealth()
    {
        Success = playerMoney.SpendMoney(100 * (MaxHealthlvl + 1), 2, MaxHealthlvl);
        if (Success == 1)
        {
            healthBar.SetMaxHealth(health.maxhealth + 50);
            healthBar.SetHealth(health.currenthealth + 50);
            health.maxhealth = health.maxhealth + 50;
            health.currenthealth = health.currenthealth + 50;
            MaxHealthlvl = MaxHealthlvl + 1;
            HealthUpgrade.text = (100 * (MaxHealthlvl + 1)).ToString();
        }
    }
    public void UpgradeOxygen()
    {
        Success = playerMoney.SpendMoney(100 * (Oxygenlvl + 1), 2, Oxygenlvl);
        if (Success == 1)
        {
            OxygenBar.SetMaxHealth(oxygen.maxhealth + 50);
            OxygenBar.SetHealth(oxygen.currenthealth + 50);
            oxygen.maxhealth = oxygen.maxhealth + 50;
            oxygen.currenthealth = oxygen.currenthealth + 50;

            trigger.oxlast = trigger.oxlast + 0.2f;
            Oxygenlvl = Oxygenlvl + 1;
            OxygenUpgrade.text = (100 * (Oxygenlvl + 1)).ToString();
        }
    }
    public void UpgradeHPregen()
    {
        Success = playerMoney.SpendMoney(100 * (HPregenlvl + 1), 2, HPregenlvl);
        if (Success == 1)
        {
            trigger.HPregen = trigger.HPregen + 0.2f;
            HPregenlvl = HPregenlvl + 1;
            HPregenUpgrade.text = (100 * (HPregenlvl + 1)).ToString();
        }
    }

    public void UpgradeSpeed()
    {
        Success = playerMoney.SpendMoney(100 * (Speedlvl + 1), 2, Speedlvl);
        if (Success == 1)
        {
            Player.speedbonus = Player.speedbonus + 0.5f;
            Speedlvl = Speedlvl + 1;
            SpeedUpgrade.text = (100 * (Speedlvl + 1)).ToString();
        }
    }

    public void UpgradeFuel()
    {
        Success = playerMoney.SpendMoney(100 * (Fuellvl + 1), 3, Fuellvl);
        if (Success == 1)
        {
            FuelBar.SetMaxHealth(oxygen.maxhealth + 50);
            FuelBar.SetHealth(oxygen.currenthealth + 50);
            fuel.maxhealth = fuel.maxhealth + 50;
            fuel.currenthealth = fuel.currenthealth + 50;
            Fuellvl = Fuellvl + 1;
            FuelUpgrade.text = (100 * (Fuellvl + 1)).ToString();

        }

    }
    public void UpgradeDrillPower()
    {
        Success = playerMoney.SpendMoney(100 * (Drillvl + 1), 2, Drillvl);
        if (Success == 1)
        {
            drill.damagemod = drill.damagemod*2;
            Drillvl = Drillvl + 1;
            DrillUpgrade.text = (100 * (Drillvl + 1)).ToString();

        }


    }
    public void Upgradestacksize()
    {
        Success = playerMoney.SpendMoney(100 * (StackSizelvl + 1), 4, StackSizelvl);
        if (Success == 1)
        {
            StackSizelvl = StackSizelvl + 1;
            inventoryManager.maxStack = inventoryManager.maxStack + 1;
            StackSizeUpgrade.text = (100 * (StackSizelvl + 1)).ToString();
        }

    }
    public void UpgradeMiningRange()
    {
        Success = playerMoney.SpendMoney(200 * (MiningRangelvl + 1), 2, MiningRangelvl);
        if (Success == 1)
        {
            float x = 0;
            x = x + 1;
            MiningRangelvl = MiningRangelvl + 1;
            drillrange.localScale = new Vector3(1 + x, 1 + x, 1 + x);
            MiningRangeUpgrade.text = (200 * (MiningRangelvl + 1)).ToString();
        }
    }
    public void UpgradeFortune()
    {
        Success = playerMoney.SpendMoney(200 * (Fortunelvl + 1), 3, Fortunelvl);
        if (Success == 1)
        {
            Fortunelvl = Fortunelvl + 1;
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Meteor");
            foreach (GameObject go in gos)
                meteorexplode.rarity = meteorexplode.rarity + 10;


            FortuneUpgrade.text = (200 * (Fortunelvl + 1)).ToString();
        }
    }
    public void UpgradeTP()
    {
        Success = playerMoney.SpendMoney(2000 * (BaseTPlvl + 1), 1, BaseTPlvl);
        if (Success == 1)
        {
            BaseTPlvl = BaseTPlvl + 1;
            teleport.CanTeleport = true;
            BaseTPUpgrade.text = ("Sold");
        }
    }
    public void UpgradeForceField()
    {
        Success = playerMoney.SpendMoney(2000 * (ForceShieldlvl + 1), 1, ForceShieldlvl);
        if (Success == 1)
        {
            health.shieldmod = health.shieldmod * (8 / 10);
            forcefield.SetActive(true);
            ForceShieldlvl = ForceShieldlvl + 1;
            ForcceShieldUpgrade.text = ("Sold");
        }
    }


    //----------------------------------------------------------------------------------//

    public void UpgradeFuelRegen()
    {
        Success = playerMoney.SpendMoney(100 * (FuelRegenlvl + 1), 2, FuelRegenlvl);
        if (Success == 1)
        {
            Player.fuelmod = Player.fuelmod + 0.2f;
            FuelRegenlvl += 1;
            FuelRegenUpgrade.text = (100 * (FuelRegenlvl + 1)).ToString();
        }
    }
    public void UpgradeBaseHealth()
    {
        Success = playerMoney.SpendMoney(100 * (BaseHealthlvl + 1), 2, BaseHealthlvl);
        if (Success == 1)
        {
            BaseHealth.SetMaxHealth(BaseHp.maxhealth + 50);
            BaseHealth.SetHealth(BaseHp.currenthealth + 50);
            BaseHp.maxhealth = BaseHp.maxhealth + 50;
            BaseHp.currenthealth = BaseHp.currenthealth + 50;
            BaseHealthlvl = BaseHealthlvl + 1;
            BaseHealthUpgrade.text = (100 * (BaseHealthlvl + 1)).ToString();

        }
    }
    public void UpgradeBaseShield()
    {
        Success = playerMoney.SpendMoney(100 * (BaseSheidllvl + 1), 2, BaseSheidllvl);
        if (Success == 1)
        {
            BaseSheidllvl = BaseSheidllvl + 1;
            BaseHp.shieldmod = BaseHp.shieldmod + 1;
            BaseShieldUpgrade.text = (100 * (BaseSheidllvl + 1)).ToString();
        }
    }

    public void UpgradeOxygenRegen()
    {
        Success = playerMoney.SpendMoney(100 * (OxygenRegenlvl + 1), 2, OxygenRegenlvl);
        if (Success == 1)
        {
            OxygenRegenlvl = OxygenRegenlvl + 1;
            trigger.oxregen = trigger.oxregen + 1;
            OxygenRegenUpgrade.text = (100 * (OxygenRegenlvl + 1)).ToString();

        }
    }

    public void UpgradeLaserDefense()
    {
        Success = playerMoney.SpendMoney(100 * (LaserDefenselvl + 1), 2, LaserDefenselvl);
        if (Success == 1)
        {
            LaserDefenselvl = LaserDefenselvl + 1;
            laserHitbox.damagemodifier = laserHitbox.damagemodifier*2;
            LaserDefenseUpgrade.text = (100 * (LaserDefenselvl + 1)).ToString();
        }
    }
    public void UpgradeTurret()
    {
        Success = playerMoney.SpendMoney(100 * (Turretlvl + 1), 4, Turretlvl);
        if (Success == 1)
        {
            Turretlvl = Turretlvl + 1;
            BaseHealthDamage.turretlvl = BaseHealthDamage.turretlvl + 1;
            TurretUpgrade.text = (100 * (Turretlvl + 1)).ToString();
        }
    }

    public void UpgradeOreProccessor()
    {
        Success = playerMoney.SpendMoney(200 * (OreProccesorlvl + 1), 2, OreProccesorlvl);
        if (Success == 1)
        {
            OreProccesorlvl = OreProccesorlvl + 1;
            inventoryManager.proccessmodifier = inventoryManager.proccessmodifier + 0.2f;
            OreProccessorUpgrade.text = (200 * (OreProccesorlvl + 1)).ToString();
        }
    }

    public void UpgradeCoinGenerator()
    {
        Success = playerMoney.SpendMoney(400 * (CoinGeneratorlvl + 1), 2, CoinGeneratorlvl);
        if (Success == 1)
        {
            CoinGeneratorlvl = CoinGeneratorlvl + 1;
            CoinGenOnline = true;
            CoinGeneratorUpgrade.text = (400 * (CoinGeneratorlvl + 1)).ToString();
        }
    }

    public void UpgradeCompanionBed()
    {
        Success = playerMoney.SpendMoney(500 * (CompanionBedlvl + 1), 1, CompanionBedlvl);
        if (Success == 1)
        {
            companionbed.SetActive(true);
            CompanionBedUpgrade.text = ("Sold");
        }
    }

    public void UpgradeBaseOverClock()
    {
        Success = playerMoney.SpendMoney(2000 * (BaseOverClocklvl + 1), 1, BaseOverClocklvl);
        if (Success == 1)
        {
            BaseOverClocklvl = BaseOverClocklvl + 1;
            bullet.bulletmodifier = bullet.bulletmodifier + 1;
            gunsLookAt1.shootspeedmodifier = gunsLookAt1.shootspeedmodifier + 1;
            gunsLookAt2.shootspeedmodifier = gunsLookAt2.shootspeedmodifier + 1;
            gunsLookAt3.shootspeedmodifier = gunsLookAt3.shootspeedmodifier + 1;
            gunsLookAt4.shootspeedmodifier = gunsLookAt4.shootspeedmodifier + 1;
            BaseOverclockUpgrade.text = ("Sold");
        }
    }

    public void UpgradeDeathRay()
    {
        Success = playerMoney.SpendMoney(2000 * (DeathRaylvl + 1), 1, DeathRaylvl);
        if (Success == 1)
        {
            DeathRaylvl = DeathRaylvl + 1;

            DeathRayUpgrade.text = ("Sold");
        }
    }

    public void UpgradeBaseChosmetics()
    {
        Success = playerMoney.SpendMoney(2000 * (BaseChosemeticslvl + 1), 1, BaseChosemeticslvl);
        if (Success == 1)
        {
            BaseChosemeticslvl = BaseChosemeticslvl + 1;
            BaseChosmetics.SetActive(true);
            BaseChosmeticsUpgrade.text = ("Sold");
        }
    }


}
