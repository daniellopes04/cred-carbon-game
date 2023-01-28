using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Currency : MonoBehaviour
{
    public static class Tree
    {
        public static int trees;
        public static int treesCarbonCost;
        public static int treesKnowledgeCost;

        public static bool CanAfford()
        {
            if (treesCarbonCost <= carbonCredit && treesKnowledgeCost <= knowledge) return true;
            else return false;
        }

        public static void PlantTrees()
        {
            if (CanAfford())
            {
                carbonCredit -= treesCarbonCost;
                knowledge -= treesKnowledgeCost;
                trees++;
                treesCarbonCost = (int) Mathf.Pow(trees, 1.8f);
                treesKnowledgeCost = (int) Mathf.Pow(trees, 1.1f);
            }
        }
    }

    private static double knowledge;
    private static double carbonCredit;
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
        Tree.trees = 0;
        Tree.treesCarbonCost = 1;
        Tree.treesKnowledgeCost = 1;
    }

    // FixedUpdate is called 60 times per second
    void FixedUpdate()
    {
        if (numberUpdates % (tickRate*10) == 0)
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
        carbonCredit += 1.0 / tickRate;
        carbonCredit += 1.0 * Tree.trees / tickRate;
        knowledge += 1.0 / 5 / tickRate;
    }

    void Update()
    {

        displayCarbonCredit.text = string.Format("Créditos de Carbono: {0:0.}", carbonCredit);
        displayKnowledge.text = string.Format("Conhecimento: {0:0.}", knowledge);
        displayMonth.text = string.Format("Mês: {0}", month);
        displayYear.text = string.Format("Ano: {0}", year);
        displayTrees.text = string.Format("Arvores: {0}", Tree.trees);
    }


    public void PlantTree()
    {
        Tree.PlantTrees();
    }

    public void SetGameSpeed(int newTickRate)
    {
        tickRate = newTickRate;
    }
}
