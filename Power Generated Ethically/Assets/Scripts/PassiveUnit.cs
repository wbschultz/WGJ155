using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassiveUnit : MonoBehaviour
{
    public string unitType = "Hamster";

    private int quantity = 0; // number of upgrades purchased
    [SerializeField]
    private double powerRatePerQuantity = 0.01d; // rate of power generation per unit
    [SerializeField]
    private double priceToPurchase = 5d;         // price to buy next unit
    [SerializeField]
    private double priceGrowthRate = 1.07d;      // price increase multiplier

    public double TotalPowerRate { get; private set; } // total rate of power generation for all units

    [Header("UI Elements")]
    [SerializeField]
    Text unitButtonText;    // text element of UI button

    // Start is called before the first frame update
    void Start()
    {
        // initialize passive generation rate
        TotalPowerRate = powerRatePerQuantity * quantity;
        updateUI();
    }

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
            priceToPurchase *= 1.07d;
            // update the power gen rate
            TotalPowerRate = powerRatePerQuantity * quantity;
            // if there isn't a reference to that unit yet, add it
            if (!resourceManager.unitList.ContainsKey(unitType))
            {
                resourceManager.unitList.Add(unitType, this);
            }
            // update the UI
            updateUI();
        }
    }

    /// <summary>
    /// update the UI display for this unit
    /// </summary>
    public void updateUI()
    {
        unitButtonText.text = unitType + 
            "\nCost: $" + priceToPurchase.ToString("F2") + 
            "\nPower Generation: +" + powerRatePerQuantity + 
            "\nQuantity: " + quantity;
    }
}
