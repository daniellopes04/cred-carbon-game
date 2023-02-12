using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Actions
{

    public class Tree
    {
        public int trees;
        public int treesCarbonCost;
        public int treesKnowledgeCost;


        public Tree()
        {
            this.trees = 0;
            this.treesCarbonCost = 1;
            this.treesKnowledgeCost = 1;
        }
        public bool CanAfford()
        {
            if (treesCarbonCost <= GameController.carbonCredit && treesKnowledgeCost <= GameController.knowledge) return true;
            else return false;
        }

        public void PlantTrees()
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