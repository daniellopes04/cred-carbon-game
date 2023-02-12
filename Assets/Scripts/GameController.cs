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
    public List<Region> Regions;

    // Start is called before the first frame update
    void Start()
    {
        Regions = new List<Region>();
        carbonCredit = 0;
        knowledge = 0;
        year = 1960;
        month = 1;
        numberUpdates = 1;
        for (int i = 0; i < 7; i++)
        {
            Regions.Add(new Region());

        }
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
        carbonCredit += 0.1 * Regions[0].tree.trees / tickRate;
        knowledge += 1.0 / 5 / tickRate;
    }

    void Update()
    {

        displayCarbonCredit.text = string.Format("{0:0.}", carbonCredit);
        PlantTree(0);
        //displayKnowledge.text = string.Format("Conhecimento: {0:0.}", knowledge);
        //displayMonth.text = string.Format("Mês: {0}", month);
        //displayYear.text = string.Format("Ano: {0}", year);
        //displayTrees.text = string.Format("Arvores: {0}", Tree.trees);
    }


    public void PlantTree(int n)
    {
        if (n >= 0 && n < 7)
            Regions[n].tree.PlantTrees();
    }

    public void SetGameSpeed(int newTickRate)
    {
        tickRate = newTickRate;
    }
}