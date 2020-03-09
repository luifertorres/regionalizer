using System.Collections.Generic;

namespace Regionalizer.Entities
{
    public class Municipality
    {
        public int MunicipalityId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<RegionMunicipality> RegionMunicipalities { get; set; }
    }
}