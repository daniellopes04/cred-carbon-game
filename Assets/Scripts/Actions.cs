using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions 
{

    public static class Tree
    {
        public static int trees = 0;
        public static int CarbonCost = 1;
        public static int actionProgress = 0;
        public static double constructionTime = 1;
        public static double upgradeCount = 0;
        public static double upgradeCost = 15;

        public static bool CanAfford()
        {
            return true;
        }

        public static bool CanAffordUpgrade()
        {
            if (GameController.knowledge >= UpgradeCost()) return true;
            else return false;
        }

        public static int UpgradeCost()
        {
            return (int)Math.Floor(upgradeCost * Math.Pow(1.1, upgradeCount));
        }

        public static void Upgrade()
        {
            if (CanAffordUpgrade())
            {
                GameController.knowledge -= UpgradeCost();
                upgradeCount++;
            }
        }
        public static double getCarbonCreditIncome()
        {
            return 0.1 * trees * (1+0.2*upgradeCount);
        }


        public static void PlantTrees(int ticks)
        {
            if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                    if (!CanvasController.IsBadgeOpen()) {
                        GameController.SetActionInProgress(true);
                    }
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= constructionTime * 50)
                {
                    trees++;
                    actionProgress = 0;
                    GameController.SetActionInProgress(false);
                }
            }
        }

    }

    public static class Thermal
    {
        public static int factories = 0;
        public static int CarbonCost = 25;
        public static int actionProgress = 0;
        public static double constructionTime = 5;
        public static double upgradeCount = 0;
        public static double upgradeCost = 30;

        public static bool CanAfford()
        {
            if (CarbonCost <= GameController.carbonCredit) return true;
            else return false;
        }

        public static bool CanAffordUpgrade()
        {
            if (GameController.knowledge >= UpgradeCost()) return true;
            else return false;
        }

        public static int UpgradeCost()
        {
            return (int)Math.Floor(upgradeCost * Math.Pow(1.1, upgradeCount));
        }

        public static void Upgrade()
        {
            if (CanAffordUpgrade())
            {
                GameController.knowledge -= UpgradeCost();
                upgradeCount++;
            }
        }
        public static double getCarbonCreditIncome()
        {
            return -0.3 * factories;
        }
        public static double getKnowledgeIncome()
        {
            return 0.3 * factories * (1 + 0.2 * upgradeCount);
        }

        public static void BuildFactory(int ticks)
        {
            if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                    GameController.carbonCredit -= CarbonCost;
                    if (!CanvasController.IsBadgeOpen()) {
                        GameController.SetActionInProgress(true);
                    }
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= constructionTime * 50)  // 5s
                {
                    factories++;
                    actionProgress = 0;
                    GameController.SetActionInProgress(false);
                }
            }
        }
    }
    public static class Solar 
    {
        public static int panels = 0;
        public static int CarbonCost = 100;
        public static int actionProgress = 0;
        public static int knowledgeCost = 300;
        public static double constructionTime = 3;
        public static double upgradeCount = 0;
        public static double upgradeCost = 50;
        public static bool unlocked = false;

        public static bool CanAfford()
        {
            if (CarbonCost <= GameController.carbonCredit && unlocked) return true;
            else return false;
        }

        public static void Unlock()
        {
            if (GameController.knowledge > knowledgeCost)
            {
                GameController.knowledge -= knowledgeCost;
                unlocked = true;
            }
        }

        public static bool CanAffordUpgrade()
        {
            if (GameController.knowledge >= UpgradeCost()) return true;
            else return false;
        }

        public static int UpgradeCost()
        {
            return (int)Math.Floor(upgradeCost * Math.Pow(1.1, upgradeCount));
        }

        public static void Upgrade()
        {
            if (CanAffordUpgrade())
            {
                GameController.knowledge -= UpgradeCost();
                upgradeCount++;
            }
        }
        public static double getCarbonCreditIncome()
        {
            return -0.1 * panels / (1 + 0.15 * upgradeCount);
        }
        public static double getKnowledgeIncome()
        {
            return 0.1 * panels * (1 + 0.15 * upgradeCount);
        }

        public static void BuildPanels(int ticks)
        {
            if (!unlocked)
            {
                Unlock();
            }
            else if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                    GameController.carbonCredit -= CarbonCost;
                    if (!CanvasController.IsBadgeOpen()) {
                        GameController.SetActionInProgress(true);
                    }
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= constructionTime * 50)  // 3s
                {
                    panels++;
                    actionProgress = 0;
                    GameController.SetActionInProgress(false);
                }
            }
        }
    }

    public static class Wind
    {
        public static int turbines = 0;
        public static int CarbonCost = 500;
        public static int actionProgress = 0;
        public static int knowledgeCost = 1000;
        public static double constructionTime = 10;
        public static double upgradeCount = 0;
        public static double upgradeCost = 100;
        public static bool unlocked = false;

        public static bool CanAfford()
        {
            if (CarbonCost <= GameController.carbonCredit && unlocked) return true;
            else return false;
        }

        public static void Unlock()
        {
            if (GameController.knowledge > knowledgeCost)
            {
                GameController.knowledge -= knowledgeCost;
                unlocked = true;
            }
        }

        public static bool CanAffordUpgrade()
        {
            if (GameController.knowledge >= UpgradeCost()) return true;
            else return false;
        }

        public static int UpgradeCost()
        {
            return (int)Math.Floor(upgradeCost * Math.Pow(1.1, upgradeCount));
        }

        public static void Upgrade()
        {
            if (CanAffordUpgrade())
            {
                GameController.knowledge -= UpgradeCost();
                upgradeCount++;
                constructionTime *= 0.8;
            }
        }
        public static double getCarbonCreditIncome()
        {
            return -0.025 * turbines;
        }
        public static double getKnowledgeIncome()
        {
            return 0.2 * turbines * 1 ;
        }

        public static void BuildTurbine(int ticks)
        {
            if (!unlocked)
            {
                Unlock();
            }
            else if (actionProgress == 0)
            {
                if (CanAfford())
                {
                    actionProgress += ticks;
                    GameController.carbonCredit -= CarbonCost;
                    if (!CanvasController.IsBadgeOpen()) {
                        GameController.SetActionInProgress(true);
                    }
                }
            }
            else
            {
                actionProgress += ticks;
                if (actionProgress >= constructionTime * 50)  // 10s
                {
                    turbines++;
                    actionProgress = 0;
                    GameController.SetActionInProgress(false);
                }
            }
        }
    }

}