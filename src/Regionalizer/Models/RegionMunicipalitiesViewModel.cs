using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Regionalizer.Entities;

namespace Regionalizer.Models
{
    public class RegionMunicipalitiesViewModel
    {
        public Region Region { get; set; }

        public IEnumerable<Municipality> AllMunicipalities { get; set; }

        public SelectList ActiveMunicipalities
        {
            get
            {
                var municipalities = AllMunicipalities
                    .Where(m => m.IsActive)
                    .Except(Municipalities);

                return new SelectList(municipalities, "MunicipalityId", "Name");
            }
        }

        public SelectListItem SelectedMunicipality { get; set; }

        public IEnumerable<Municipality> Municipalities
        {
            get
            {
                return Region.RegionMunicipalities
                    .Select(rm => rm.Municipality);
            }
        }
    }
}