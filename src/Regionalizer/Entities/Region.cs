using System.Collections.Generic;

namespace Regionalizer.Entities
{
    public class Region
    {
        public int RegionId { get; set; }
        public string Name { get; set; }
        public List<RegionMunicipality> RegionMunicipalities { get; set; }
    }
}