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

        public Task<CroixRouge.Model.Adresse> FindAdresseById(int id)
        {
            return _context.Adresse.FindAsync(id);
        }

        public async Task<IEnumerable<CroixRouge.Model.Alerte>> GetAlertesAsync(int? pageIndex=0, int? pageSize = 10, string nom = null)
        {
             return await _context.Alerte
            .Where(alerte => nom == null || alerte.Nom.Contains(nom))
            .OrderBy(alerte => alerte.Id)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
        }

        public async Task AddAlerteAsync(CroixRouge.Model.Alerte alerte)
        {
            if (alerte != null)
            {
                _context.Alerte.Add(alerte);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAlerteAsync(CroixRouge.Model.Alerte alerte, CroixRouge.DTO.AlerteModel dto)
        {
            alerte.Nom = dto.Nom;
            alerte.Contenu = dto.Contenu;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(alerte).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAlerteAsync(CroixRouge.Model.Alerte alerte)
        {
            _context.Alerte.Remove(alerte);
            await _context.SaveChangesAsync();
        }

        public Task<CroixRouge.Model.Alerte> FindAlerteById(int id)
        {
            return _context.Alerte.FindAsync(id);
        }
    }
}