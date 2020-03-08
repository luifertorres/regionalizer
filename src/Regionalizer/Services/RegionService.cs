using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Regionalizer.Entities;
using System;

namespace Regionalizer.Services
{

    public class RegionService : IRegionService
    {
        private readonly RegionalizerDbContext _context;

        public RegionService(RegionalizerDbContext context)
        {
            _context = context;
        }

        public async Task<Region> Get(int id)
        {
            return await _context.Regions
                .Include(r => r.RegionMunicipalities.Select(rm => rm.Municipality))
                .FirstOrDefaultAsync(r => r.RegionId == id);
        }

        public async Task<IEnumerable<Region>> GetAll()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region> Add(Region region)
        {
            _context.Regions.Add(region);
            await _context.SaveChangesAsync();

            return region;
        }

        public async Task Update(Region region)
        {
            var regionToUpdate = await _context.Regions.FindAsync(region.RegionId);

            Merge(regionToUpdate, with: region);
            await _context.SaveChangesAsync();
        }

        private void Merge(Region regionToUpdate, Region with)
        {
            regionToUpdate.Name = with.Name;
        }

        public async Task<Region> Remove(int id)
        {
            var region = await _context.Regions.FindAsync(id);

            _context.Regions.Remove(region);
            await _context.SaveChangesAsync();

            return region;
        }
    }
}