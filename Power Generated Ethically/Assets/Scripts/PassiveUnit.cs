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
    Text unitButtonText;
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
            if (!resourceManager.unitList.ContainsKey(unitType))
            {
                resourceManager.unitList.Add(unitType, this);
            }
            // update the UI
            UpdateUI();
        }
    }

    /// <summary>
    /// multiply price by rate modifier
    /// </summary>
    /// <param name="modifier">decimal multiplier for price</param>
    void PriceMultiply(float modifier)
    {
        priceToPurchase *= modifier;
    }

    /// <summary>
    /// multiply power generation by rate modifier
    /// </summary>
    /// <param name="modifier">decimal modifier for power generation</param>
    void PowerRateUp(float modifier)
    {
        powerRatePerQuantity *= modifier;
    }

    /// <summary>
    /// update the UI display for this unit
    /// </summary>
    public void UpdateUI()
    {
        //created by Jason. Got rid of old method, and added in new fields
        quantityText.text = quantity.ToString("F0") + " Units";
        powerRatePerQuantityText.text = powerRatePerQuantity.ToString("F2") + " kWh / s";
        priceToPurchaseText.text = priceToPurchase.ToString("F2") + " $";
        TotalPowerRateText.text = TotalPowerRate.ToString("F2") + " kWh / s";
    }
}
