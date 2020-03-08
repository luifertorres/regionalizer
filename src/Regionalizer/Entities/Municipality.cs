using System.Collections.Generic;

namespace Regionalizer.Entities
{
    public class Municipality
    {
        public int MunicipalityId { get; set; }
        public string Name { get; set; }
        public MunicipalityStatus Status { get; set; }
        public List<RegionMunicipality> RegionMunicipalities { get; set; }
    }

    public enum MunicipalityStatus
    {
        Active,
        Inactive
    }
}