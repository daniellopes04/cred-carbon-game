using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.Security.AccessControl;

public class GameController : MonoBehaviour
{

    public static double knowledge;
    public static double carbonCredit;
    private int year;
    private int month;
    private int actionID;
    public TextMeshProUGUI displayKnowledge;
    public TextMeshProUGUI displayCarbonCredit;
    public TextMeshProUGUI displayTrees;
    public TextMeshProUGUI displayYear;
    public TextMeshProUGUI displayMonth;
    private int numberUpdates;
    private int tickRate = 50;

    // Start is called before the first frame update
    void Start()
    {
        carbonCredit = 0;
        knowledge = 0;
        year = 1997;
        month = 1;
        numberUpdates = 1;
        actionID = 0;
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        if (numberUpdates % (tickRate * 10) == 0)  // Time Calculations
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

        switch (actionID)  // Action Calculations
        {
            case 0: // Idle
                break;

            case 1: // Plant Trees
                Actions.Tree.PlantTrees( 50 / tickRate);
                break;

            case 2: // Build Factories 
                Actions.Thermal.BuildFactory(50 / tickRate);
                break;

            case 3: // Build Solar Panels
                Actions.Solar.BuildPanels(50 / tickRate);
                break;

            case 4: // Build Wind Turbines 
                Actions.Wind.BuildTurbine( 50 / tickRate);
                break;

        }
 
        
        // Resource Calculations
        carbonCredit += 0.1 / tickRate;  // Ganho passivo
        carbonCredit += 0.1 * Actions.Tree.trees / tickRate; // Ganho por árvores
        carbonCredit -= 0.3 * Actions.Thermal.factories / tickRate; // Perda por termelétricas
        carbonCredit -= 0.1 * Actions.Solar.panels / tickRate; // Perda por paineis de energia solar
        carbonCredit -= 0.025 * Actions.Wind.turbines / tickRate; // Perda por energia eólica

        knowledge += 0.1 / tickRate;
        knowledge += 0.3 * Actions.Thermal.factories / tickRate;
        knowledge += 0.1 * Actions.Solar.panels / tickRate;
        knowledge += 0.2 * Actions.Wind.turbines / tickRate;

    }

    void Update()
    {

        displayCarbonCredit.text = string.Format("{0:0.}", carbonCredit);
        //displayKnowledge.text = string.Format("Conhecimento: {0:0.}", knowledge);
        //displayMonth.text = string.Format("Mês: {0}", month);
        //displayYear.text = string.Format("Ano: {0}", year);
        displayTrees.text = string.Format("{0} m²", Actions.Tree.trees*100);
    }


    public void SetAction(int n)
    {
        actionID = n;
    }

    public void SetGameSpeed(int newTickRate)
    {
        tickRate = newTickRate;
    }
}