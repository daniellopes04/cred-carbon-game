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
    public static double knowledgeGain;
    public static double carbonCredit;
    public static double CCgoalDifficulty;
    public static double totalCarbonCredit;
    public static double carbonCreditGoal;
    public static double carbonCreditGain;
    public static int progress;
    public static int gameEnded;
    public static int year;
    private int goalYear;
    private int month;
    private int actionID;
    public TextMeshProUGUI displayKnowledge;
    public TextMeshProUGUI displayCarbonCredit;
    public TextMeshProUGUI displayTrees;
    public TextMeshProUGUI displayThermalValue;
    public TextMeshProUGUI displaySolarPanelsValue;
    public TextMeshProUGUI displayWindTurbinesValue;
    public TextMeshProUGUI displayYear;
    public TextMeshProUGUI displayMonth;
    public TextMeshProUGUI displayPlantTreesUpgradeCost;
    public TextMeshProUGUI displayThermalUpgradeCost;
    public TextMeshProUGUI displaySolarPanelsUpgradeCost;
    public TextMeshProUGUI displayWindTurbinesUpgradeCost;
    private int numberUpdates;
    private int tickRate = 50;

    // Start is called before the first frame update
    void Start()
    {
        gameEnded = 0;
        goalYear = 2050;
        carbonCredit = 0;
        knowledge = 0;
        year = 1997;
        month = 1;
        numberUpdates = 1;
        actionID = 0;
        CCgoalDifficulty = 1;
        totalCarbonCredit = 50;
        carbonCreditGoal = 100000 * CCgoalDifficulty;
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        if (gameEnded == 0)
        {
            if (year >= 2050 || progress >= 100 || carbonCredit < 0)
            {
                gameEnded = 2; // perdeu
            }

            if (totalCarbonCredit>= carbonCreditGoal)
            {
                gameEnded = 1; // ganhou
            }


            if (numberUpdates % (tickRate * 2) == 0)  // 1 m�s a cada 2s
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
                    Actions.Tree.PlantTrees(50 / tickRate);
                    break;

                case 2: // Build Factories 
                    Actions.Thermal.BuildFactory(50 / tickRate);
                    break;

                case 3: // Build Solar Panels
                    Actions.Solar.BuildPanels(50 / tickRate);
                    break;

                case 4: // Build Wind Turbines 
                    Actions.Wind.BuildTurbine(50 / tickRate);
                    break;

            }


            // Resource Calculations
            carbonCreditGain = (
                0.1 + // Ganho passivo
                Actions.Tree.getCarbonCreditIncome() + // Ganho por �rvores
                Actions.Thermal.getCarbonCreditIncome() + // Ganho por termel�tricas
                Actions.Solar.getCarbonCreditIncome() + // Ganho por pain�is solares
                Actions.Wind.getCarbonCreditIncome() // Ganho por turbinas e�licas
                ) / tickRate;
            carbonCredit += carbonCreditGain;

            totalCarbonCredit += carbonCreditGain ; // Calculando o cr�dito de carbono total

            progress = (int) Math.Max((100 - totalCarbonCredit / ObjectiveFunction((year - 1997) * 12 + month)*100)*1.2,0);


            knowledgeGain = (
                0.01 +
                Actions.Thermal.getKnowledgeIncome() +
                Actions.Solar.getKnowledgeIncome() +
                Actions.Wind.getKnowledgeIncome()
                ) / tickRate;

            knowledge += knowledgeGain;

        }
    }

    void Update()
    {

        displayCarbonCredit.text = string.Format("{0:0.}", carbonCredit);
        displayKnowledge.text = string.Format("{0:0.}", knowledge);
        //displayMonth.text = string.Format("Mes: {0}", month);
        displayYear.text = string.Format("Year: {0}", year);
        displayTrees.text = string.Format("{0} m2", Actions.Tree.trees*100);
        displayThermalValue.text = Actions.Thermal.factories.ToString();
        displaySolarPanelsValue.text = Actions.Solar.panels.ToString();
        displayWindTurbinesValue.text = Actions.Wind.turbines.ToString();
        displayPlantTreesUpgradeCost.text = Actions.Tree.UpgradeCost().ToString();
        displayThermalUpgradeCost.text = Actions.Thermal.UpgradeCost().ToString();
        displaySolarPanelsUpgradeCost.text = Actions.Solar.UpgradeCost().ToString();
        displayWindTurbinesUpgradeCost.text = Actions.Wind.UpgradeCost().ToString();
    }


    public void SetAction(int n)
    {
        actionID = n;
    }

    public void SetGameSpeed(int newTickRate)
    {
        tickRate = newTickRate;
    }

    public double ObjectiveFunction(double n)    // Fun��o quadr�tica que serve para c�lculo do progresso do jogador | Objetivo = 100k CC
    {
        return CCgoalDifficulty * 0.2472 * n * n;
    }

    public void UpgradeAction(int n)
    {
        switch (n)  // Action Calculations
        {
            case 0: // Idle
                break;

            case 1: // Plant Trees
                Actions.Tree.Upgrade();
                break;

            case 2: // Build Factories 
                Actions.Thermal.Upgrade();
                break;

            case 3: // Build Solar Panels
                Actions.Solar.Upgrade();
                break;

            case 4: // Build Wind Turbines 
                Actions.Wind.Upgrade();
                break;

        }
    }
}