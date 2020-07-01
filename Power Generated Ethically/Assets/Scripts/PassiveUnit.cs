﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUnit : MonoBehaviour
{
    public string unitType = "Hamster";
    public int quantity = 0; // number of upgrades purchased
    public double TotalPowerRate { get; private set; } // total rate of power generation for all units

    [SerializeField]
    private double powerRatePerQuantity = 0.01d; // rate of power generation per unit
    [SerializeField]
    private double priceToPurchase = 5d;         // price to buy next unit
    [SerializeField]
    private double priceGrowthRate = 1.07d;      // unit buy price increase multiplier

    [Header("UI Elements")]
    // Added by Jason. UI elements for quantity, powerRatePerQuantity, priceToPurchase, and TotalPowerRate.
    public Text powerRatePerQuantityText;
    public Text quantityText;
    public Text priceToPurchaseText;
    public Text TotalPowerRateText;

    // text element of UI button

    // Start is called before the first frame update
    void Start()
    {
        // initialize passive generation rate
        TotalPowerRate = powerRatePerQuantity * quantity;
        UpdateUI();
    }

    /***************************************************************************
     * PassiveUnit state changes
     **************************************************************************/
    /// <summary>
    /// Buys one more of this unit type
    /// </summary>
    public void PurchaseUnit()
    {
        CoolerCookieClicker resourceManager = CoolerCookieClicker.Instance;

        if (resourceManager.currency >= priceToPurchase)
        {
            // buy unit
            resourceManager.currency -= priceToPurchase;
            quantity++;
            // increase price
            priceToPurchase *= priceGrowthRate;
            // update the power gen rate
            TotalPowerRate = powerRatePerQuantity * quantity;
            // if there isn't a reference to that unit yet, add it
            //if (!resourceManager.unitList.ContainsKey(unitType))
            //{
            //    resourceManager.unitList.Add(unitType, this);
            //}
            // update the UI
            UpdateUI();
        }
    }

    /// <summary>
    /// multiply price by rate modifier
    /// </summary>
    /// <param name="modifier">decimal multiplier for price</param>
    public void PriceMultiply(float modifier)
    {
        priceToPurchase *= modifier;
    }

    /// <summary>
    /// multiply power generation by rate modifier
    /// </summary>
    /// <param name="modifier">decimal modifier for power generation</param>
    public void PowerRateUp(float modifier)
    {
        powerRatePerQuantity *= modifier;
    }

    /***************************************************************************
     * Update UI
     ***************************************************************************/
    /// <summary>
    /// update the UI display for this unit
    /// </summary>
    public void UpdateUI()
    {
        //created by Jason. Got rid of old method, and added in new fields
        quantityText.text = quantity.ToString("F0") + " Units";
        powerRatePerQuantityText.text = powerRatePerQuantity.ToString("F2") + " kWh / s";
        priceToPurchaseText.text = "Cost: " + priceToPurchase.ToString("F2") + " $";
        TotalPowerRateText.text = TotalPowerRate.ToString("F2") + " kWh / s";
    }

    /***************************************************************************
     * Save/Load player upgrade progress
     ***************************************************************************/
    /**
     * <summary>Serializes passive unit data and saves it to storage</summary>
     */
    public void SavePassiveUnit ()
    {
        SaveSystem.SavePassiveUnit(this.unitType, this.quantity, this.powerRatePerQuantity, this.priceToPurchase, this.priceGrowthRate);
    }

    /**
     * <summary>Deserializes and loads passive unit data from storage</summary>
     */
    public void LoadPassiveUnit ()
    {
        PassiveUnitData loadedData = SaveSystem.LoadPassiveUnit(this.unitType);
        UnityEngine.Debug.Log("PassiveUnit::LoadPassiveUnit() unitType: " + loadedData.unitType + ",quantity: " + loadedData.quantity);
        this.quantity = loadedData.quantity;
        this.powerRatePerQuantity = loadedData.powerRatePerQuantity;
        this.priceToPurchase = loadedData.priceToPurchase;
        this.priceGrowthRate = loadedData.priceGrowthRate;

        UpdateUI();
    }
}
