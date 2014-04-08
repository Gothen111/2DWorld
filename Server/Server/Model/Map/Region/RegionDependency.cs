using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Map.Region
{
    class RegionDependency
    {
        public static RegionDependency regionDependency = new RegionDependency();
        private Dictionary<RegionEnum, List<Enum>> layer;

        private RegionDependency()
        {
            layer = new Dictionary<RegionEnum, List<Enum>>();
            List<Enum> var_Layer_Grassland = new List<Enum>();

            var_Layer_Grassland.Add(Block.BlockEnum.Gras);
            var_Layer_Grassland.Add(Block.BlockEnum.Dirt);
            layer.Add(RegionEnum.Grassland, var_Layer_Grassland);
        }

        public List<Enum> getLayer(RegionEnum _RegionEnum)
        {
            return layer[_RegionEnum];
        }
    }
}
