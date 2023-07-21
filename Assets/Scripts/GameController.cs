using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
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
    private static int actionID;
    private static bool actionInProgress = false;    
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
    public TextMeshProUGUI displaySolarPanelsUnlockCost;
    public TextMeshProUGUI displayWindTurbinesUpgradeCost;
    public TextMeshProUGUI displayWindTurbinesUnlockCost;
    public TextMeshProUGUI displayPlantTreesCCIncome;
    public TextMeshProUGUI displayThermalCCIncome;
    public TextMeshProUGUI displaySolarPanelsCCIncome;
    public TextMeshProUGUI displayWindTurbinesCCIncome;
    public TextMeshProUGUI displayThermalKnowledgeIncome;
    public TextMeshProUGUI displaySolarPanelsKnowledgeIncome;
    public TextMeshProUGUI displayWindTurbinesKnowledgeIncome;
    public TextMeshProUGUI displayPlantTreesUpgradeLevel;
    public TextMeshProUGUI displayThermalUpgradeLevel;
    public TextMeshProUGUI displaySolarPanelssUpgradeLevel;
    public TextMeshProUGUI displayWindTurbineUpgradeLevel;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public List<Button> actionButtons;

    private int numberUpdates;
    private int tickRate = 50;

    // Start is called before the first frame update
    public void Start()
    {
        progress = 50;
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
        Actions.Tree.ResetValues();
        Actions.Thermal.ResetValues();
        Actions.Solar.ResetValues();
        Actions.Wind.ResetValues();
    }

    // FixedUpdate is called 50 times per second
    void FixedUpdate()
    {
        if (gameEnded == 0)
        {
            if (year >= 2050 || progress <= -50|| carbonCredit < 0)
            {
                gameEnded = 2; // perdeu
                gameOverPanel.SetActive(!gameOverPanel.activeSelf);
            }

            if (totalCarbonCredit>= carbonCreditGoal)
            {
                gameEnded = 1; // ganhou
                winPanel.SetActive(!winPanel.activeSelf);
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

            actionButtons[0].interactable = Actions.Thermal.CanAfford() ? true : false;
            actionButtons[1].interactable = Actions.Solar.CanAfford() ? true : false;
            actionButtons[2].interactable = Actions.Wind.CanAfford() ? true : false;


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

            progress = (int) Math.Min(((carbonCreditGain*tickRate / ObjectiveFunction((year - 1997) * 12 + month)-1)*100),50);

            //Debug.Log("Progress: " + GameController.progress);
            //Debug.Log("CC " + carbonCreditGain*tickRate);

            //Debug.Log("OBJ: " + ObjectiveFunction((year - 1997) * 12 + month) );

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
        displayThermalValue.text = string.Format("{0}", Actions.Thermal.factories);
        displaySolarPanelsValue.text = Actions.Solar.panels.ToString();
        displayWindTurbinesValue.text = Actions.Wind.turbines.ToString();
        displayPlantTreesUpgradeCost.text = Actions.Tree.UpgradeCost().ToString();
        displayThermalUpgradeCost.text = Actions.Thermal.UpgradeCost().ToString();
        displaySolarPanelsUpgradeCost.text = Actions.Solar.UpgradeCost().ToString();
        displaySolarPanelsUnlockCost.text = Actions.Solar.UpgradeCost().ToString();
        displayWindTurbinesUpgradeCost.text = Actions.Wind.UpgradeCost().ToString();
        displayWindTurbinesUnlockCost.text = Actions.Wind.UpgradeCost().ToString();
        displayPlantTreesCCIncome.text = "+" + Actions.Tree.getCarbonCreditIncome().ToString("F2") + " CC/s" ;
        displayThermalCCIncome.text = Actions.Thermal.getCarbonCreditIncome().ToString("F2") + " CC/s";
        displaySolarPanelsCCIncome.text = Actions.Solar.getCarbonCreditIncome().ToString("F2") + " CC /s";
        displayWindTurbinesCCIncome.text = Actions.Wind.getCarbonCreditIncome().ToString("F2") + " CC/s";
        displayThermalKnowledgeIncome.text = "+" + Actions.Thermal.getKnowledgeIncome().ToString("F2") + " Knowledge/s";
        displaySolarPanelsKnowledgeIncome.text = "+" + Actions.Solar.getKnowledgeIncome().ToString("F2") + " Knowledge/s";
        displayWindTurbinesKnowledgeIncome.text = "+" + Actions.Wind.getKnowledgeIncome().ToString("F2") + " Knowledge/s";
        displayPlantTreesUpgradeLevel.text = "Level: " + (Actions.Tree.upgradeCount + 1).ToString();
        displayThermalUpgradeLevel.text = "Level: " + (Actions.Thermal.upgradeCount + 1).ToString();
        displaySolarPanelssUpgradeLevel.text = "Level: " + Actions.Solar.upgradeCount.ToString();
        displayWindTurbineUpgradeLevel.text = "Level: " + Actions.Wind.upgradeCount.ToString();
    }


    public static void SetAction(int n)
    {
        //Debug.Log(actionID + " trocando para " + n);
        if (actionID == n) actionID = 0;
        else actionID = n;
    }

    public static int GetAction()
    {
        return actionID;
    }

    public static bool GetActionInProgress()
    {
        return actionInProgress;
    }

    public static void SetActionInProgress(bool value)
    {
        actionInProgress = value;
    }

    public void SetGameSpeed(int newTickRate)
    {
        tickRate = newTickRate;
    }

    public double ObjectiveFunction(double n)    // Fun��o quadr�tica que serve para c�lculo do progresso do jogador | Objetivo = 100k CC
    {
        return Math.Abs(CCgoalDifficulty * (0.0012214 * n * n - 0.0221486 * n));
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