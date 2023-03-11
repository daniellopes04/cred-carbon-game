using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Actions
{

    public static class Tree
    {
        public static int trees = 0;
        public static int treesCarbonCost = 1;
        public static int treesKnowledgeCost = 1;

        public static bool CanAfford()
        {
            if (treesCarbonCost <= GameController.carbonCredit && treesKnowledgeCost <= GameController.knowledge) return true;
            else return false;
        }

        public static void PlantTrees()
        {
            if (CanAfford())
            {
                GameController.carbonCredit -= treesCarbonCost;
                GameController.knowledge -= treesKnowledgeCost;
                trees++;
                treesCarbonCost = (int)Mathf.Pow(trees, 1.8f);
                treesKnowledgeCost = (int)Mathf.Pow(trees, 1.1f);
            }
        }
    }

}