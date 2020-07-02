using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public double currency;
    public double currencyPerPower;
    public double currencyPerSecond;
    public double powerPerSecond;
    public double totalPowerRate;
    public List<PassiveUnitData> passiveUnitDataList = new List<PassiveUnitData>();

    public SaveData(CoolerCookieClicker resources, List<PassiveUnit> passiveUnitList)
    {
        // Serialize each PassiveUnit's data
        for (int i = 0; i < passiveUnitList.Count; i++)
        {
            passiveUnitDataList.Add(new PassiveUnitData(passiveUnitList[i].getDataToSave()));
        }

        // Serialize game resources data
        this.currency = resources.currency;
        this.currencyPerPower = resources.currencyPerPower;
        this.currencyPerSecond = resources.currencyPerSecond;
        this.powerPerSecond = resources.powerPerSecond;
        this.totalPowerRate = resources.totalPowerRate;
    }
}
