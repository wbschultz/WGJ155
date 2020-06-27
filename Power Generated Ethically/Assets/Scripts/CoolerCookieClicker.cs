using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolerCookieClicker : MonoBehaviour
{
    // Text is in reference to the UI element "Text"
    public Text currencytext;
    public double currency;
    public double currencyclickvalue;


    public Text CurrencypersecondText;
    public Text ClickUpgrade1Text;
    public Text productionupgrade1text;

    public double CurrencyPerSecond;
    public double clickUpgrade1Cost;
    public int clickupgradelevel;

    public double productionupgradecost;
    public int productionupgradelevel;

    public void Start()
    {
        currencyclickvalue += 1;
        currency = 0;
        productionupgradecost = 25;
        clickUpgrade1Cost = 10;
    }

    public void Update()
    {
        CurrencyPerSecond = productionupgradelevel;
        currencytext.text = "Currency:  " + currency.ToString("F0");
        CurrencypersecondText.text = CurrencyPerSecond.ToString("F0") + "currency/s";
        ClickUpgrade1Text.text = "Click Upgrade 1\nCost:" + clickUpgrade1Cost.ToString("F0") + "currency\nPower: +1 Click\nLevel: " + clickupgradelevel;
        productionupgrade1text.text = "Production Uprage 1\nCost: " + productionupgradecost.ToString("F0") + "currency\nPower: +1 Currency/s\nLevel: " + productionupgradelevel;
        currency += CurrencyPerSecond * Time.deltaTime;
    }

    public void Click()
    {
        currency += currencyclickvalue;
    }

    public void BuyClickUpgrade()
    {
        if (currency >= clickUpgrade1Cost)
        {
            clickupgradelevel++;
            currency -= clickUpgrade1Cost;
            clickUpgrade1Cost *= 1.07;
            currencyclickvalue++;
        }
    }
    public void BuyProductionUpgrade()
    {
        if (currency >= productionupgradecost)
        {
            productionupgradelevel++;
            currency -= productionupgradecost;
            productionupgradecost *= 1.07;

        }
    }

}
