using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*******************************************************************************
 * Main data type that aggregates all save/load data together
 ******************************************************************************/
[System.Serializable]
public class SaveData
{
    public double currency;
    public double currencyPerPower;
    public double currencyPerSecond;
    public double powerPerSecond;
    public double totalPowerRate;
    public double purchasepool;
    public float treadmillSpeed;
    public List<PassiveUnitData> passiveUnitDataList = new List<PassiveUnitData>();
    public List<PassiveUpgradeData> passiveUpgradeDataList = new List<PassiveUpgradeData>();

    public SaveData(CoolerCookieClicker resources, List<PassiveUnit> passiveUnitList, List<PassiveUpgrade> passiveUpgradeList)
    {
        // Serialize each PassiveUnit's data
        foreach (PassiveUnit passiveUnit in passiveUnitList)
        {
            passiveUnitDataList.Add(new PassiveUnitData(passiveUnit.getDataToSave()));
        }

        // Serialize each PassiveUpgrade's data
        foreach (PassiveUpgrade passiveUpgrade in passiveUpgradeList)
        {
            passiveUpgradeDataList.Add(new PassiveUpgradeData(passiveUpgrade.getDataToSave()));
        }

        // Serialize game resources data
        this.currency = resources.currency;
        this.currencyPerPower = resources.currencyPerPower;
        this.currencyPerSecond = resources.currencyPerSecond;
        this.powerPerSecond = resources.powerPerSecond;
        this.totalPowerRate = resources.totalPowerRate;
        this.purchasepool = resources.purchasepool;
        this.treadmillSpeed = resources.treadmillSpeed;
    }
}

/*******************************************************************************
 * Game data types to store/load
 ******************************************************************************/
public struct PassiveUpgradeProps
{
    public string upgradeType;
    public int quantity;
    public double priceToUpgrade;
    public List<PassiveUnit> unitsToUpgrade;

    public PassiveUpgradeProps(string upgradeType, int quantity, double priceToUpgrade, List<PassiveUnit> unitsToUpgrade)
    {
        this.upgradeType = upgradeType;
        this.quantity = quantity;
        this.priceToUpgrade = priceToUpgrade;
        this.unitsToUpgrade = unitsToUpgrade;
    }
}

[System.Serializable]
public class PassiveUpgradeData
{
    public string upgradeType;
    public int quantity;
    public double priceToUpgrade;
    public List<string> unitsToUpgrade = new List<string>();

    public PassiveUpgradeData(PassiveUpgradeProps passiveUpgradeProps)
    {
        this.upgradeType = passiveUpgradeProps.upgradeType;
        this.quantity = passiveUpgradeProps.quantity;
        this.priceToUpgrade = passiveUpgradeProps.priceToUpgrade;

        for (int i = 0; i < passiveUpgradeProps.unitsToUpgrade.Count; i++)
        {
            unitsToUpgrade.Add(passiveUpgradeProps.unitsToUpgrade[i].unitType);
        }
    }
}