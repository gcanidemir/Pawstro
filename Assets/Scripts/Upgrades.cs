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
    public Trigger trigger;
    public PlayerMoney playerMoney;
    public int Dash, MaxHealth, OxygenUp, HPregen, Speed;
    public int Success;
    public TextMeshProUGUI DashUpgrade, HealthUpgrade, OxygenUpgrade, HPregenUpgrade, SpeedUpgrade;
    public void UpgradeDashSpeed()
    {
        Success = playerMoney.SpendMoney(100*(Dash + 1));
        if (Success == 1)
        {
            Player.multiplier = Player.multiplier + 0.5f;
            Dash = Dash + 1;
            DashUpgrade.text = (100*(Dash+1)).ToString();
        }
        
    }
    public void UpgradeHealth()
    {
        Success = playerMoney.SpendMoney(100 * (MaxHealth + 1));
        if (Success == 1)
        {
            healthBar.SetMaxHealth(health.maxhealth + 50);
            healthBar.SetHealth(health.currenthealth + 50);
            health.maxhealth = health.maxhealth + 50;
            health.currenthealth = health.currenthealth + 50;
            MaxHealth = MaxHealth + 1;
            HealthUpgrade.text = (100 * (MaxHealth + 1)).ToString();
        }
    }
    public void UpgradeOxygen()
    {
        Success = playerMoney.SpendMoney(100 * (OxygenUp + 1));
        if (Success == 1)
        {
            OxygenBar.SetMaxHealth(oxygen.maxhealth + 50);
            OxygenBar.SetHealth(oxygen.currenthealth + 50);
            oxygen.maxhealth = oxygen.maxhealth + 50;
            oxygen.currenthealth = oxygen.currenthealth + 50;

            trigger.oxregen = trigger.oxregen + 0.2f;
            trigger.oxlast = trigger.oxlast + 0.2f;
            OxygenUp = OxygenUp + 1;
            OxygenUpgrade.text = (100 * (OxygenUp + 1)).ToString();
        }
    }
    public void UpgradeHPregen()
    {
        Success = playerMoney.SpendMoney(100 * (HPregen + 1));
        if (Success == 1)
        {
            trigger.HPregen = trigger.HPregen + 0.2f;
            HPregen = HPregen + 1;
            HPregenUpgrade.text = (100 * (HPregen + 1)).ToString();
        }
    }

    public void UpgradeSpeed()
    {
        Success = playerMoney.SpendMoney(100 * (Speed + 1));
        if (Success == 1)
        {
            Player.speedbonus = Player.speedbonus + 0.5f;
            Speed = Speed + 1;
            SpeedUpgrade.text = (100 * (Speed + 1)).ToString();
        }
    }
}
