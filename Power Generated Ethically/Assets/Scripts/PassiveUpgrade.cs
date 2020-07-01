/*
 * 
 * Controls Upgrades for passive units:
 * Takes the form of a repeatable upgrade to
 * either increase power generation rate or decrease price
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveUpgrade : MonoBehaviour
{
    private int quantity = 0;

    [SerializeField]
    string upgradeType = "Hamster Wheel Oil";

    [SerializeField]
    List<PassiveUnit> unitsToUpgrade = new List<PassiveUnit>();

    [Header("Unit Upgrade Parameters")]
    [SerializeField]
    private int maxQuantity = 20;           // maximum number of times this can be purchased
    [SerializeField]
    private double priceToUpgrade = 10d;         // price to upgrade the unit
    [SerializeField]
    private double upgradePriceGrowthRate = 1.5d;   // upgrade price increase multiplier
    [SerializeField]
    private float powerRateModifier = 2f;          // multiplier to unit's power rate
    [SerializeField]
    private float priceModifier = 1f;                   // multiplier to unit's price (discounts etc)

    /// <summary>
    /// Apply rate and price modifier, subtact currency, and increase price
    /// </summary>
    public void ApplyUpgrade()
    {
        if (CoolerCookieClicker.Instance.currency >= priceToUpgrade && quantity < maxQuantity)
        {
            CoolerCookieClicker.Instance.currency -= priceToUpgrade;
            quantity++;
            foreach (PassiveUnit unit in unitsToUpgrade)
            {
                if (priceModifier != 1)
                {
                    // price up
                    unit.PriceMultiply(priceModifier);
                }
                // rate up
                unit.PowerRateUp(powerRateModifier);
            }
            // increase buy price
            priceToUpgrade *= upgradePriceGrowthRate;
        }
    }
}
