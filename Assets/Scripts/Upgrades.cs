using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Upgrades : MonoBehaviour
{

    public player Player;
    public HealthBar healthBar;
    public Health health;
    public HealthBar OxygenBar;
    public Oxygen oxygen;
    public HealthBar FuelBar;
    public Fuel fuel;
    public Drill drill;
    public Trigger trigger;
    public PlayerMoney playerMoney;
    public int Dashlvl, MaxHealthlvl, Oxygenlvl, HPregenlvl, Speedlvl, Drillvl, Fuellvl;
    public int Success;
    [Header("------------------Player Upgrades------------------")]
    public TextMeshProUGUI DashUpgrade, HealthUpgrade, OxygenUpgrade, HPregenUpgrade, SpeedUpgrade,DrillUpgrade,FuelUpgrade;


    [Header("------------------Base Upgrades------------------")]
    public TextMeshProUGUI FuelRegen;
    public void UpgradeDashSpeed()
    {
        Success = playerMoney.SpendMoney(100*(Dashlvl + 1));
        if (Success == 1)
        {
            Player.multiplier = Player.multiplier + 0.5f;
            Dashlvl = Dashlvl + 1;
            DashUpgrade.text = (100*(Dashlvl+1)).ToString();
        }
        
    }
    public void UpgradeHealth()
    {
        Success = playerMoney.SpendMoney(100 * (MaxHealthlvl + 1));
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
        Success = playerMoney.SpendMoney(100 * (Oxygenlvl + 1));
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
        Success = playerMoney.SpendMoney(100 * (HPregenlvl + 1));
        if (Success == 1)
        {
            trigger.HPregen = trigger.HPregen + 0.2f;
            HPregenlvl = HPregenlvl + 1;
            HPregenUpgrade.text = (100 * (HPregenlvl + 1)).ToString();
        }
    }

    public void UpgradeSpeed()
    {
        Success = playerMoney.SpendMoney(100 * (Speedlvl + 1));
        if (Success == 1)
        {
            Player.speedbonus = Player.speedbonus + 0.5f;
            Speedlvl = Speedlvl + 1;
            SpeedUpgrade.text = (100 * (Speedlvl + 1)).ToString();
        }
    }

    public void UpgradeFuel()
    {
        Success = playerMoney.SpendMoney(100 * (Fuellvl + 1));
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
        Success = playerMoney.SpendMoney(100 * (Drillvl + 1));
        if (Success == 1)
        {
            drill.damagemod = drill.damagemod + 1;
            Drillvl = Drillvl + 1;
            DrillUpgrade.text = (100 * (Drillvl + 1)).ToString();

        }


    }

    public void UpgradeFuelRegen()
    {
        Player.fuelmod = Player.fuelmod + 0.2f;
    }
}
