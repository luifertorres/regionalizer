using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Regionalizer.Entities;

namespace Regionalizer.Services
{

    public class MunicipalityService : IMunicipalityService
    {
        private readonly RegionalizerDbContext _context;

        public MunicipalityService(RegionalizerDbContext context)
        {
            _context = context;
        }

        public async Task<Municipality> Get(int id)
        {
            return await _context.Municipalities
                .Include(r => r.RegionMunicipalities)
                .ThenInclude(rm => rm.Region)
                .FirstOrDefaultAsync(r => r.MunicipalityId == id);
        }

        public async Task<IEnumerable<Municipality>> GetAll()
        {
            return await _context.Municipalities.ToListAsync();
        }

        public async Task<Municipality> Add(Municipality municipality)
        {
            _context.Municipalities.Add(municipality);
            await _context.SaveChangesAsync();

            return municipality;
        }

        public async Task Update(Municipality municipality)
        {
            var oldMunicipality = await _context.Municipalities.FindAsync(municipality.MunicipalityId);

            if (oldMunicipality is null)
            {
                throw new ArgumentException("Municipality not found", nameof(municipality));
            }

            Merge(oldMunicipality, municipality);
            await _context.SaveChangesAsync();
        }

        private void Merge(Municipality oldMunicipality, Municipality newMunicipality)
        {
            oldMunicipality.Name = newMunicipality.Name;
            oldMunicipality.IsActive = newMunicipality.IsActive;
        }

        public async Task<Municipality> Remove(int id)
        {
            var municipality = await _context.Municipalities.FindAsync(id);

            _context.Municipalities.Remove(municipality);
            await _context.SaveChangesAsync();

            return municipality;
        }
    }
}