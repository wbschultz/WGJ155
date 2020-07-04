/*
 * 
 * Controls Upgrades for passive units:
 * Takes the form of a repeatable upgrade to
 * either increase power generation rate or decrease price
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUpgrade : MonoBehaviour
{
    //UI fields for the upgrades

    public Text upgradequantityText;
    public Text upgradepriceText;
    public Text upgrademodifierText;
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
    /// 

    public void Start()
    {
        UpdateUI();
    }
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
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        upgradequantityText.text = quantity.ToString("F0") + " Upgrade level";
        if(quantity == 20)
        {
            upgradequantityText.text = "Maximum Upgrade level";
        }

        upgradepriceText.text = priceToUpgrade.ToString("F2") + " $ to upgrade";

        upgrademodifierText.text = powerRateModifier.ToString("F2") + "x";
    }

    /***************************************************************************
     * Save/Load player upgrade progress
     ***************************************************************************/
    public PassiveUpgradeProps getDataToSave()
    {
        PassiveUpgradeProps passiveUpgradeProps = new PassiveUpgradeProps(upgradeType, quantity, priceToUpgrade, unitsToUpgrade);
        return passiveUpgradeProps;
    }

    public void RestoreDataFromSave(SaveData saveData)
    {
        List<PassiveUpgradeData> passiveUpgradeDataList = saveData.passiveUpgradeDataList;
        for (int i = 0; i < passiveUpgradeDataList.Count; i++)
        {
            if (passiveUpgradeDataList[i].upgradeType == this.upgradeType)
            {
                PassiveUpgradeData passiveUpgradeData = passiveUpgradeDataList[i];
                this.quantity = passiveUpgradeData.quantity;
                this.priceToUpgrade = passiveUpgradeData.priceToUpgrade;
                break;
            }
        }

        UpdateUI();
    }
}
