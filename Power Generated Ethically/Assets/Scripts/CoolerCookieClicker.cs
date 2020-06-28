﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolerCookieClicker : SingletonBase<CoolerCookieClicker>
{
    // Text is in reference to the UI element "Text"
    public Text currencytext;
    public double currency;
    public double currencyclickvalue;
    public double currencypersecond;

    public Text currencyPerSecondText;
    public Text clickUpgrade1Text;

    public double passivePowerGenerationRate;   // rate of passive power gen (units)
    public double activePowerGenerationRate;    // rate of active power gen (treadmill)
    public double currencyPerPower;             // sell rate power (e.g. $0.12/kWh)

    public double clickUpgrade1Cost;            // cost of first active upgrade
    public int clickUpgradeLevel;               // level of active upgrade?

    public double productionUpgradeCost;
    public int productionUpgradeLevel = 0;

    // store reference to each generator type that has been purchased
    public Dictionary<string, PassiveUnit> unitList = new Dictionary<string, PassiveUnit>();

    // called before first frame update
    public void Start()
    {
        // TODO: make active power gen look at treadmill game
        activePowerGenerationRate = 0.2;

        // set initial  price
        currencyPerPower = 0.12;

        currencyclickvalue += 1;
        currency = 0;
        productionUpgradeCost = 25;
        clickUpgrade1Cost = 10;
    }

    // called every frame
    public void Update()
    {
        // sum all generation rates
        passivePowerGenerationRate = 0;
        foreach (KeyValuePair<string, PassiveUnit> kvp in unitList)
        {
            passivePowerGenerationRate += kvp.Value.TotalPowerRate;
        }

        //currencypersecond new variable
        currencypersecond = (passivePowerGenerationRate + activePowerGenerationRate) * currencyPerPower;

        // TODO seperate this code onto the menus themselves, or at least separate function
        // update UI
        currencytext.text = "Currency:  " + currency.ToString("F2");

        //currencyPerSecondText.text = currencyPerPower.ToString("F0") + "currency/s";
        currencyPerSecondText.text = currencypersecond.ToString("F2") + "currency/s";
        clickUpgrade1Text.text = "Click Upgrade 1\nCost:" + clickUpgrade1Cost.ToString("F2") + "currency\nPower: +1 Click\nLevel: " + clickUpgradeLevel;
        //productionUpgrade1Text.text = "Production Upgrade 1\nCost: " + productionUpgradeCost.ToString("F2") + "currency\nPower: +1 Currency/s\nLevel: " + productionUpgradeLevel;

        // update currency (passive kWh + active kWh) * $/kWh * time
        currency += (passivePowerGenerationRate + activePowerGenerationRate) * currencyPerPower * Time.deltaTime;
    }

    // =============================================temporary stuff=================================================================

    // TODO: Change this to a rate linked to the jumping game
    /// <summary>
    /// Generate currency on click
    /// </summary>
    public void ActiveGenerate()
    {
        currency += currencyclickvalue;
    }

    /// <summary>
    /// Linear increase to the active generation (clicks for now)
    /// </summary>
    public void BuyClickUpgrade()
    {
        // if player has enough currency, buy upgrade and increase cost
        if (currency >= clickUpgrade1Cost)
        {
            currency -= clickUpgrade1Cost;
            clickUpgradeLevel++;
            clickUpgrade1Cost *= 1.07;
            currencyclickvalue++;
        }
    }

    /// <summary>
    /// Basic linearly increasing unit
    /// </summary>
    public void BuyUnitUpgrade()
    {
        if (currency >= productionUpgradeCost)
        {
            // increase up
            productionUpgradeLevel++;
            currency -= productionUpgradeCost;
            productionUpgradeCost *= 1.07;

        }
    }

}
