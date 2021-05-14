using System;
using UnityEngine;

namespace Assets.Types
{
    [Serializable]
    public class BuildingOption
    {
        public BuildingEnum building = BuildingEnum.Default;
        public Vector2 position = new Vector2();
    }
}