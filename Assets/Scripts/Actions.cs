using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Actions
{

    public static class Tree
    {
        public static int trees = 0;
        public static int CarbonCost = 1;
        public static int actionProgress = 0;

        public static bool CanAfford()
        {
            return true;
        }

        public static void PlantTrees(int ticks)
        {
            if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= 50)
                {
                    trees++;
                    actionProgress = 0;
                }
            }
        }
    }

    public static class Thermal
    {
        public static int factories = 0;
        public static int CarbonCost = 25;
        public static int actionProgress = 0;

        public static bool CanAfford()
        {
            if (CarbonCost <= GameController.carbonCredit) return true;
            else return false;
        }

        public static void BuildFactory(int ticks)
        {
            if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                    GameController.carbonCredit -= CarbonCost;
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= 5 * 50)  // 5s
                {
                    factories++;
                    actionProgress = 0;
                }
            }
        }
    }
    public static class Solar 
    {
        public static int panels = 0;
        public static int CarbonCost = 100;
        public static int actionProgress = 0;

        public static bool CanAfford()
        {
            if (CarbonCost <= GameController.carbonCredit && GameController.knowledge >= 100) return true;
            else return false;
        }

        public static void BuildPanels(int ticks)
        {
            if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                    GameController.carbonCredit -= CarbonCost;
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= 3 * 50)  // 10s
                {
                    panels++;
                    actionProgress = 0;
                }
            }
        }
    }

    public static class Wind
    {
        public static int turbines = 0;
        public static int CarbonCost = 500;
        public static int actionProgress = 0;

        public static bool CanAfford()
        {
            if (CarbonCost <= GameController.carbonCredit && GameController.knowledge >= 500) return true;
            else return false;
        }

        public static void BuildTurbine(int ticks)
        {
            if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                    GameController.carbonCredit -= CarbonCost;
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= 10 * 50)  // 10s
                {
                    turbines++;
                    actionProgress = 0;
                }
            }
        }
    }

}