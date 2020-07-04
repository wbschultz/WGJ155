/* central script for managing resources and currency in idle game
 * written by Jason and Will
 * For P & GE, a game for Weekly Game Jam week 155
 */

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoolerCookieClicker : SingletonBase<CoolerCookieClicker>
{
    // Text is in reference to the UI element "Text"
    public Text totalDollarsText;
    public double currency;
    public double currencyclickvalue;
    public double currencyPerSecond;
    public double totalPowerRate;
    public double powerPerSecond;
    public double purchasepool;


    public Text dollarsPerSecond;
    public Text powerPerSecondText;
    public Text ConversationRateText;
    public Text totalPowerText;
    public treadmill_controller treadmill;

    private bool gameLoaded = false;

    public float treadmillSpeed;               // holds speed of the treadmill
    public double treadmillPowerConstant = 0.2625d;       // multiplier to treadmill speed value

    private double passivePowerGenerationRate;   // rate of passive power gen (units)
    private double activePowerGenerationRate;    // rate of active power gen (treadmill speed * activePowerConstant)
    public double currencyPerPower;             // sell rate power (e.g. $0.12/kWh)

    public double clickUpgrade1Cost;            // cost of first active upgrade
    public int clickUpgradeLevel;               // level of active upgrade?

    // store reference to each generator type that has been purchased
    private List<PassiveUnit> unitList = new List<PassiveUnit>();

    // called before first frame update
    public void Start()
    {
        // set initial  price
        if (gameLoaded == false)
        {
            currencyPerPower = 0.12;
            purchasepool = 0;
            currencyclickvalue += 1;
            currency = 0;
            totalPowerRate = 0;
            clickUpgrade1Cost = 10;
            passivePowerGenerationRate = 0;
        }

        GameObject[] dataManagers = GameObject.FindGameObjectsWithTag("DataManager");
        foreach(GameObject dataManager in dataManagers)
        {
            PassiveUnit passiveUnit = dataManager.GetComponent<PassiveUnit>();
            if (passiveUnit != null)
            {
                unitList.Add(passiveUnit);
            }
        }


        UpdateUI();
    }

    public void AddToPassive(double value)
    {
        passivePowerGenerationRate += value;
    }
    // called every frame
    public void Update()
    {
        // sum all generation rates
        passivePowerGenerationRate = 0;
       foreach (PassiveUnit passiveUnit in unitList)
       {
            passivePowerGenerationRate += passiveUnit.TotalPowerRate;
       }

        // calculate active power generation
        activePowerGenerationRate = treadmillSpeed * treadmillPowerConstant;

        //currencypersecond new variable
        currencyPerSecond = (passivePowerGenerationRate + activePowerGenerationRate) * currencyPerPower;

        //added by Jason. A variable that will calculate the current KwH/s
        powerPerSecond = (passivePowerGenerationRate + activePowerGenerationRate);

        // update UI elements
        UpdateUI();

        //added by jason. Formula for creating the Total power variable: (passive kWh + active kWh) * time
        totalPowerRate += (passivePowerGenerationRate + activePowerGenerationRate) * Time.deltaTime;

        // update currency: total power * $/kWh
        currency = (totalPowerRate * currencyPerPower) - purchasepool;


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
        powerPerSecondText.text = powerPerSecond.ToString("F2") + " kWh/s";
    }

    public void RestoreDataFromSave(SaveData gameData)
    {
        // Restore data from save data
        this.currency = gameData.currency;
        this.currencyPerPower = gameData.currencyPerPower;
        this.currencyPerSecond = gameData.currencyPerSecond;
        this.powerPerSecond = gameData.powerPerSecond;
        this.totalPowerRate = gameData.totalPowerRate;
        this.treadmillSpeed = gameData.treadmillSpeed;
        this.gameLoaded = true;

        // Rerender UI to match state
        this.treadmill.Set_Speed(this.treadmillSpeed);
        this.UpdateUI();
    }

    public void QuitGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    /**
     * <summary>Save all game data to storage</summary>
     */
    public void SaveGame()
    {
        UnityEngine.Debug.Log("CoolerCookieClicker::SaveGame() ");

        //SaveSystem.SaveAllData(this, unitList);
        //for (int i = 0; i < this.unitList.Count; i++)
        //{
        //    this.unitList[i].SavePassiveUnit();
        //}
    }

    /**
     * <summary>Load all game data from storage</summary>
     */
    public void LoadGame()
    {
        //SaveData gameData = SaveSystem.LoadAllData();

        //this.currency = gameData.currency;
        //this.currencyPerPower = gameData.currencyPerPower;
        //this.currencyPerSecond = gameData.currencyPerSecond;
        //this.powerPerSecond = gameData.powerPerSecond;
        //this.totalPowerRate = gameData.totalPowerRate;

        //for (int i = 0; i < this.unitList.Count; i++)
        //{
        //    this.unitList[i].RestoreDataFromSave(gameData.passiveUnitDataList);
        //}

        //UpdateUI();
        //for (int i = 0; i < this.unitList.Count; i++)
        //{
        //    this.unitList[i].LoadPassiveUnit();
        //}
    }
}