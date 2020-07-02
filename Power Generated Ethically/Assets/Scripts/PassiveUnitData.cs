using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PassiveUnitData
{
    public string unitType;
    public int quantity;
    public double powerRatePerQuantity;
    public double priceToPurchase;
    public double priceGrowthRate;

    public PassiveUnitData(PassiveUnitProps passiveUnitProps)
    {
        this.unitType = passiveUnitProps.unitType;
        this.quantity = passiveUnitProps.quantity;
        this.powerRatePerQuantity = passiveUnitProps.powerRatePerQuantity;
        this.priceToPurchase = passiveUnitProps.priceToPurchase;
        this.priceGrowthRate = passiveUnitProps.priceGrowthRate;
    }
}
