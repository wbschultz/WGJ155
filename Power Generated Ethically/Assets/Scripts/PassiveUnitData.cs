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

    public PassiveUnitData(string unitType, int quantity, double powerRatePerQuantity, double priceToPurchase, double priceGrowthRate)
    {
        this.unitType = unitType;
        this.quantity = quantity;
        this.powerRatePerQuantity = powerRatePerQuantity;
        this.priceToPurchase = priceToPurchase;
        this.priceGrowthRate = priceGrowthRate;
    }
}
