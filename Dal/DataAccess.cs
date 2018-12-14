using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CroixRouge.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CroixRouge.DTO;


namespace CroixRouge.Dal
{
    public class DataAccess 
    {
        private bdCroixRougeContext _context;

        public DataAccess(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<CroixRouge.Model.Adresse>> GetAdressesAsync(string ville = null)
        {
            return await _context.Adresse
            .Where(adr => ville == null || adr.Ville.Contains(ville))
            .OrderBy(adr => adr.Id)
            .ToArrayAsync();
        }

        public async Task AddAdresseAsync(CroixRouge.Model.Adresse adresse)
        {
            if (adresse != null)
            {
                _context.Adresse.Add(adresse);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAdresseAsync(CroixRouge.Model.Adresse adresse, CroixRouge.DTO.AdresseModel dto)
        {
            adresse.Ville = dto.Ville;
            adresse.Rue = dto.Rue; 
            adresse.Numero = dto.Numero;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(adresse).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAdresseAsync(CroixRouge.Model.Adresse adresse)
        {
            _context.Adresse.Remove(adresse);
            await _context.SaveChangesAsync();
        }

        public Task<CroixRouge.Model.Adresse> FindAdresseByIdAsync(int id)
        {
            return _context.Adresse.FindAsync(id);
        }
    }
}