using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using CroixRouge.Model;
using CroixRouge.Model.Exceptions;
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

        public async Task<IEnumerable<CroixRouge.Model.Alerte>> GetAlertesAsync(int? pageIndex, int? pageSize, string nom)
        {
             return await _context.Alerte
            .Where(alerte => nom == null || alerte.Nom.Contains(nom))
            .OrderBy(alerte => alerte.Id)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
        }

        public async Task AddAlerteAsync(Alerte alerte)
        {
            if (alerte != null)
            {
                _context.Alerte.Add(alerte);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAlerteAsync(CroixRouge.Model.Alerte alerte, AlerteDTO dto)
        {
            alerte.Nom = dto.Nom;
            alerte.Contenu = dto.Contenu;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(alerte).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAlerteAsync(Alerte alerte)
        {
            _context.Alerte.Remove(alerte);
            await _context.SaveChangesAsync();
        }

        public Task<CroixRouge.Model.Alerte> FindAlerteById(int id)
        {
            return _context.Alerte.FindAsync(id);
        }

        public async Task<IEnumerable<Collecte>> GetCollectesAsync(int? pageIndex, int? pageSize, bool horairesAJour)
        {
            var collectes = await _context.Collecte
            .OrderBy(collecte => collecte.Id)
            .Include(c => c.Jourouverture)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();

            if (horairesAJour)
            {
                foreach(var collecte in collectes)
                {
                    collecte.Jourouverture = collecte.Jourouverture.Where(j => j.Date == null || j.Date >= DateTime.Now).ToList();
                }
            }

            return collectes;
        }

        public async Task AddCollecteAsync(Collecte collecte)
        {
            if (collecte != null)
            {
                _context.Collecte.Add(collecte);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCollecteAsync(Collecte collecte, CollecteDTO dto)
        {
            collecte.Nom = dto.Nom;
            collecte.Latitude = dto.Latitude;
            collecte.Longitude = dto.Longitude;
            collecte.Telephone = dto.Telephone;

            _context.Entry(collecte).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveCollecteAsync(Collecte collecte)
        {
            //suppression des joursouvertures lié
            

            _context.Collecte.Remove(collecte);
            await _context.SaveChangesAsync();
        }

        public Task<Collecte> FindCollecteById(int id)
        {
            return _context.Collecte
            .Include(c => c.Jourouverture)
            .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Groupesanguin>> GetGroupesanguinsAsync()
        {
            return await _context.Groupesanguin
            .OrderBy(g => g.Nom)
            .ToArrayAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _context.Role
            .ToArrayAsync();
        }

        public async Task<IEnumerable<Utilisateur>> GetUtilisateursAsync(int? pageIndex, int? pageSize, string login)
        {
             return await _context.Utilisateur
            .Where(u => login == null || u.Login.Contains(login))
            .OrderBy(u => u.Login)
            .Take(pageSize.Value)
            .Skip(pageIndex.Value * pageSize.Value)
            .ToArrayAsync();
        }

        public async Task AddUtilisateurAsync(Utilisateur utilisateur)
        {
            if (utilisateur != null)
            {
                _context.Utilisateur.Add(utilisateur);
                await _context.SaveChangesAsync();
            }
            //lancer exception
        }

        public async Task UpdateUtilisateurAsync(Utilisateur utilisateur, UtilisateurDTO dto)
        {   
            //Pas top avec le mapper car il remplace tous les champs    
            utilisateur.Nom = dto.Nom;
            utilisateur.Mail = dto.Mail;
            utilisateur.Prenom = dto.Prenom;
            utilisateur.NumGsm = dto.NumGsm;
            utilisateur.DateNaissance = dto.DateNaissance;
            utilisateur.IsMale = dto.IsMale;
            utilisateur.Score = dto.Score;
            utilisateur.Rue = dto.Rue;
            utilisateur.Numero = dto.Numero;

            if(dto.FkGroupesanguin != null){
                Console.WriteLine(dto.FkGroupesanguin);
                utilisateur.FkGroupesanguin = dto.FkGroupesanguin;
            }else{
                utilisateur.FkGroupesanguin = null;
            }

            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(utilisateur).OriginalValues["Rv"] = dto.Rv;

            //_context.Entry(utilisateur).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUtilisateurAsync(Utilisateur utilisateur)
        {
            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();
        }

        public async Task<Utilisateur> FindUtilisateurByLogin(string login)
        {
            //return _context.Utilisateur.FindAsync(login);
             return await _context.Utilisateur
                    .Include(u=>u.FkGroupesanguinNavigation)
                    .FirstOrDefaultAsync(c => c.Login == login);

        }

        public async Task<IEnumerable<Information>> GetInformationsAsync()
        {
            return await _context.Information
            .OrderBy(i => i.Id)
            .ToArrayAsync();
        }

        public async Task addDonAsync(Don don)
        {
            if(don != null)
            {
                _context.Don.Add(don);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Jourouverture>> GetJoursouverturesAsync()
        {
             return await _context.Jourouverture
            .OrderBy(j => j.Id)
            .ToArrayAsync();
        }

        public async Task AddJourouvertureAsync(Jourouverture jourouverture)
        {
            if (jourouverture != null)
            {
                verificationHoraire(jourouverture.HeureDebut, jourouverture.HeureFin);
                _context.Jourouverture.Add(jourouverture);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateJourouvertureAsync(Jourouverture jourouverture, JourouvertureDTO dto)
        {   
            if (jourouverture != null)
            {
                _context.Entry(jourouverture).OriginalValues["Rv"] = dto.Rv;
                await _context.SaveChangesAsync();
            }
            //exception
        }

        public async Task RemoveJourouvertureAsync(Jourouverture jourouverture)
        {
            _context.Jourouverture.Remove(jourouverture);
            await _context.SaveChangesAsync();
        }

        public Task<Jourouverture> FindJourouvertureById(int id)
        {
            return _context.Jourouverture.FindAsync(id);
        }

        public void verificationHoraire(TimeSpan h1, TimeSpan h2) 
        {
            if (h2 <= h1)
                throw new HoraireInvalideException();
        }
    }
}