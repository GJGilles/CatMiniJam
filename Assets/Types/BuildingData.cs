using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Types
{
    public enum BuildingEnum
    {
        None = -1,
        Default
    }

    [Serializable]
    public class BuildingData
    {
        public BuildingEnum id = BuildingEnum.Default;
        public string name = "";
        public GameObject building;
        public List<ItemCost> cost = new List<ItemCost>();
    }
}