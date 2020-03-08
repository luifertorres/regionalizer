namespace Regionalizer.Entities
{
    public class RegionMunicipality
    {
        public int RegionId { get; set; }
        public Region Region { get; set; }

        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
    }
}