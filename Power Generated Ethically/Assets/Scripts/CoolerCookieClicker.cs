/* central script for managing resources and currency in idle game
 * written by Jason and Will
 * For P & GE, a game for Weekly Game Jam week 155
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolerCookieClicker : SingletonBase<CoolerCookieClicker>
{
    // Text is in reference to the UI element "Text"
    public Text totalDollarsText;
    public double currency;
    public double currencyclickvalue;
    public double currencyPerSecond;
    public double totalPowerRate;
    public double powerpersecond;

    public Text dollarsPerSecond;
    public Text powerPerSecondText;
    public Text ConversationRateText;
    public Text totalPowerText;

    public double treadmillSpeed;               // holds speed of the treadmill
    public double treadmillPowerConstant = 0.0625d;       // multiplier to treadmill speed value

    private double passivePowerGenerationRate;   // rate of passive power gen (units)
    private double activePowerGenerationRate;    // rate of active power gen (treadmill speed * activePowerConstant)
    public double currencyPerPower;             // sell rate power (e.g. $0.12/kWh)

    public double clickUpgrade1Cost;            // cost of first active upgrade
    public int clickUpgradeLevel;               // level of active upgrade?

    // store reference to each generator type that has been purchased
    public Dictionary<string, PassiveUnit> unitList = new Dictionary<string, PassiveUnit>();

    // called before first frame update
    public void Start()
    {
        // set initial  price
        currencyPerPower = 0.12;

        currencyclickvalue += 1;
        currency = 0;
        totalPowerRate = 0;
        clickUpgrade1Cost = 10;

        UpdateUI();
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

        // calculate active power generation
        activePowerGenerationRate = treadmillSpeed * treadmillPowerConstant;

        //currencypersecond new variable
        currencyPerSecond = (passivePowerGenerationRate + activePowerGenerationRate) * currencyPerPower;

        //added by Jason. A variable that will calculate the current KwH/s
        powerpersecond = (passivePowerGenerationRate + activePowerGenerationRate);

        // update UI elements
        UpdateUI();

        //added by jason. Formula for creating the Total power variable: (passive kWh + active kWh) * time
        totalPowerRate += (passivePowerGenerationRate + activePowerGenerationRate) * Time.deltaTime;

        // update currency: total power * $/kWh
        currency += totalPowerRate * currencyPerPower;

    }

    /// <summary>
    /// Update UI elements
    /// </summary>
    private void UpdateUI()
    {
        // added by Jason. A UI field that displays the current CurrencyPerPower rate to the player.
        ConversationRateText.text = "1 kWh = " + currencyPerPower.ToString("F2");

        // added by Jason. UI for total money
        totalDollarsText.text = currency.ToString("F2") + " $";

        //added by Jason. UI Element for the Total Power.
        totalPowerText.text = totalPowerRate.ToString("F2") + " kWh";

        //changed by Jason. Turned currencypersecond to dollars per second. 
        dollarsPerSecond.text = currencyPerSecond.ToString("F2") + "Dollars/s";

        //added by Jason. text field that allows us to see current Kwh / s. 
        powerPerSecondText.text = powerpersecond.ToString("F2") + " kWh/s";
    }
}