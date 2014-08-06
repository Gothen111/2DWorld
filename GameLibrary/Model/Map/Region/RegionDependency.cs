using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.Region
{
    public class RegionDependency
    {
        public static RegionDependency regionDependency = new RegionDependency();
        private Dictionary<RegionEnum, List<Enum>> layer;

        private RegionDependency()
        {
            this.layer = new Dictionary<RegionEnum, List<Enum>>();

            List<Enum> var_Layer_Grassland = new List<Enum>();
            var_Layer_Grassland.Add(Block.BlockEnum.Ground1);
            var_Layer_Grassland.Add(Block.BlockEnum.Ground2);
            this.layer.Add(RegionEnum.Grassland, var_Layer_Grassland);

            List<Enum> var_Layer_Snowland = new List<Enum>();
            var_Layer_Snowland.Add(Block.BlockEnum.Ground1);
            var_Layer_Snowland.Add(Block.BlockEnum.Ground2);
            this.layer.Add(RegionEnum.Snowland, var_Layer_Snowland);
        }

        public List<Enum> getLayer(RegionEnum _RegionEnum)
        {
            return layer[_RegionEnum];
        }
    }
}
