using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Diagnostics;

public class GameController : MonoBehaviour
{

    public static double knowledge;
    public static double carbonCredit;
    private int year;
    private int month;
    public TextMeshProUGUI displayKnowledge;
    public TextMeshProUGUI displayCarbonCredit;
    public TextMeshProUGUI displayTrees;
    public TextMeshProUGUI displayYear;
    public TextMeshProUGUI displayMonth;
    private int numberUpdates;
    private int tickRate = 60;

    // Start is called before the first frame update
    void Start()
    {
        carbonCredit = 0;
        knowledge = 0;
        year = 1960;
        month = 1;
        numberUpdates = 1;
    }

    // FixedUpdate is called 60 times per second
    void FixedUpdate()
    {
        if (numberUpdates % (tickRate * 10) == 0)
        {
            numberUpdates = 1;
            month++;
            if (month > 12)
            {
                month = 1;
                year++;
            };
        }
        numberUpdates++;
        carbonCredit += 0.1 / tickRate;
        carbonCredit += 0.1 * Actions.Tree.trees / tickRate;
        knowledge += 1.0 / 5 / tickRate;
    }

    void Update()
    {

        displayCarbonCredit.text = string.Format("{0:0.}", carbonCredit);
        //displayKnowledge.text = string.Format("Conhecimento: {0:0.}", knowledge);
        //displayMonth.text = string.Format("Mês: {0}", month);
        //displayYear.text = string.Format("Ano: {0}", year);
        displayTrees.text = string.Format("{0} m²", Actions.Tree.trees*100);
    }


    public void PlantTree()
    {
            Actions.Tree.PlantTrees();
    }

    public void SetGameSpeed(int newTickRate)
    {
        tickRate = newTickRate;
    }
}