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
            else
                throw new NotFoundException("Alerte");
        }

        public async Task UpdateAlerteAsync(CroixRouge.Model.Alerte alerte, AlerteDTO dto)
        {
            if (alerte == null || dto == null)
                throw new NotFoundException("Alerte");

            alerte.Nom = dto.Nom;
            alerte.Contenu = dto.Contenu;
            //fixme: le premier RowVersion n'a pas d'impact. 
            //Accès concurrents
            _context.Entry(alerte).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveAlerteAsync(Alerte alerte)
        {
            if (alerte == null)
                throw new NotFoundException("Alerte");
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
            else    
                throw new NotFoundException("Collecte");
        }

        public async Task UpdateCollecteAsync(Collecte collecte, CollecteDTO dto)
        {
            if (collecte == null || dto == null)
                throw new NotFoundException("Collecte");

            collecte.Nom = dto.Nom;
            collecte.Latitude = dto.Latitude;
            collecte.Longitude = dto.Longitude;
            collecte.Telephone = dto.Telephone;

            _context.Entry(collecte).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveCollecteAsync(Collecte collecte,bool suppressionHorraire)
        {
            if (collecte == null)
                throw new NotFoundException("Collecte");
            //suppression des joursouvertures lié
            
            _context.Collecte.Remove(collecte);
            if(suppressionHorraire)
            {
                foreach(Jourouverture jourouverture in collecte.Jourouverture)
                {
                    _context.Jourouverture.Remove(jourouverture);
                }
                _context.Collecte.Remove(collecte);
            }
            else{
                _context.Collecte.Remove(collecte);
            }
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
            else
                throw new NotFoundException("Utilisateur");
        }

        public async Task UpdateUtilisateurAsync(Utilisateur utilisateur, UtilisateurDTO dto)
        {   
            if (utilisateur == null || dto == null)
                throw new NotFoundException("Utilisateur");
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

            _context.Entry(utilisateur).OriginalValues["Rv"] = dto.Rv;

            await _context.SaveChangesAsync();
        }

        public async Task RemoveUtilisateurAsync(Utilisateur utilisateur)
        {
            if (utilisateur == null)
                throw new NotFoundException("Utilisateur");

            _context.Utilisateur.Remove(utilisateur);
            await _context.SaveChangesAsync();
        }

        public async Task<Utilisateur> FindUtilisateurByLogin(string login)
        {
            if (login == null)
                throw new NotFoundException("Utilisateur");
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
            else
                throw new NotFoundException("Don");
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
                verificationChevauchementHoraires(jourouverture.FkCollecteNavigation, jourouverture);
                _context.Jourouverture.Add(jourouverture);
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("Jourouverture");
        }

        public async Task UpdateJourouvertureAsync(Jourouverture jourouverture, JourouvertureDTO dto)
        {   
            if (jourouverture != null && dto != null)
            {
                _context.Entry(jourouverture).OriginalValues["Rv"] = dto.Rv;
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("JourOuverture");
        }

        public async Task RemoveJourouvertureAsync(Jourouverture jourouverture)
        {
            if (jourouverture == null)
                throw new NotFoundException("Jourouverture");

            _context.Jourouverture.Remove(jourouverture);
            await _context.SaveChangesAsync();
        }
    
        public Task<Jourouverture> FindJourouvertureById(int id)
        {
            return _context.Jourouverture.FindAsync(id);
        }

        public async Task<IEnumerable<Lanceralerte>> GetLanceralertesAsync(string login)
        {
             return await _context.Lanceralerte
            .Where(lA => login == null || lA.FkUtilisateur.Contains(login))
            .OrderBy(l => l.Id)
            .ToArrayAsync();
        }

        public async Task AddLanceralerteAsync(Lanceralerte lanceralerte)
        {
            if (lanceralerte != null)
            {
                _context.Lanceralerte.Add(lanceralerte);
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("Lanceralerte");
        }

        public async Task UpdateLanceralerteAsync(Lanceralerte lanceralerte)
        {   
            if (lanceralerte != null)
            {
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("Lanceralerte");
        }

        public async Task RemoveLanceralerteAsync(Lanceralerte lanceralerte)
        {
            if (lanceralerte == null)
                throw new NotFoundException("Lanceralerte");

            _context.Lanceralerte.Remove(lanceralerte);
            await _context.SaveChangesAsync();
        }

        public Task<Lanceralerte> FindLanceralerteById(int id)
        {
            return _context.Lanceralerte.FindAsync(id);
        }

        public async Task<IEnumerable<Imagepromotion>> GetImagepromotionAsync()
        {
             return await _context.Imagepromotion
            .OrderBy(iP => iP.Id)
            .ToArrayAsync();
        }

        public async Task AddImagepromotionAsync(Imagepromotion imagepromotion)
        {
            if (imagepromotion != null)
            {
                _context.Imagepromotion.Add(imagepromotion);
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("Imagepromotion");
        }

        public async Task UpdateImagePromotionAsync(Imagepromotion imagepromotion, ImagepromotionDTO dto)
        {   
            if (imagepromotion != null && dto != null)
            {
                _context.Entry(imagepromotion).OriginalValues["Rv"] = dto.Rv;
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("Imagepromotion");
        }

        public async Task RemoveImagepromotionAsync(Imagepromotion imagepromotion)
        {
            if (imagepromotion == null)
                throw new NotFoundException("Imagepromotion");

            _context.Imagepromotion.Remove(imagepromotion);
            await _context.SaveChangesAsync();
        }

        public Task<Imagepromotion> FindImagepromotionById(int id)
        {
            return _context.Imagepromotion.FindAsync(id);
        }

        public async Task<IEnumerable<Diffuserimage>> GetDiffuserimageAsync()
        {
             return await _context.Diffuserimage
            .OrderBy(dI => dI.Id)
            .ToArrayAsync();
        }

        public async Task AddDiffuserimageAsync(Diffuserimage diffuserimage)
        {
            if (diffuserimage != null)
            {
                _context.Diffuserimage.Add(diffuserimage);
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("Diffuserimage");
        }

        public async Task UpdateDiffuserimageAsync(Diffuserimage diffuserimage)
        {   
            if (diffuserimage != null)
            {
                await _context.SaveChangesAsync();
            }
            else
                throw new NotFoundException("Diffuserimage");
        }

        public async Task RemoveDiffuserimageAsync(Diffuserimage diffuserimage)
        {
            if (diffuserimage == null)
                throw new NotFoundException("Diffuserimage");

            _context.Diffuserimage.Remove(diffuserimage);
            await _context.SaveChangesAsync();
        }

        public Task<Diffuserimage> FindDiffuserimageById(int id)
        {
            return _context.Diffuserimage.FindAsync(id);
        }

        public void verificationHoraire(TimeSpan h1, TimeSpan h2) 
        {
            if (h2 <= h1)
                throw new HoraireInvalideException();
        }

        public void verificationChevauchementHoraires(Collecte collecte, Jourouverture nouveauJourOuverture)
        {
            if (nouveauJourOuverture.Jour.HasValue)
            {
                if (nouveauJourOuverture.Date.HasValue)
                    throw new HoraireDoubleException();

                if (collecte.Jourouverture.Any(
                    jourOuvertureExistant => jourOuvertureExistant.Jour == nouveauJourOuverture.Jour && 
                    horaireSeChevauchent(nouveauJourOuverture, jourOuvertureExistant)
                ))
                    throw new ChevauchementHorairesException();
            }
            else
            {
                if (nouveauJourOuverture.Date.HasValue)
                {
                    DateTime maintenantSansHeures = DateTime.Now.Date;
                    int resultatDate = DateTime.Compare(nouveauJourOuverture.Date.Value, maintenantSansHeures);
                    if (resultatDate < 0)
                        throw new HorairePlusValideException();

                    if (collecte.Jourouverture.Any(
                        jourOuvertureExistant => jourOuvertureExistant.Date == nouveauJourOuverture.Date && 
                        horaireSeChevauchent(nouveauJourOuverture, jourOuvertureExistant)
                    ))
                        throw new ChevauchementHorairesException();
                }
                else
                    throw new HoraireManquantException();
            }
        }

        public bool horaireSeChevauchent(Jourouverture nouveauJourOuverture, Jourouverture jourOuvertureExistant)
        {
            return ((nouveauJourOuverture.HeureFin >= jourOuvertureExistant.HeureDebut && nouveauJourOuverture.HeureFin <= jourOuvertureExistant.HeureFin) ||
                    (nouveauJourOuverture.HeureDebut >= jourOuvertureExistant.HeureDebut && nouveauJourOuverture.HeureDebut <= jourOuvertureExistant.HeureFin) ||
                    (nouveauJourOuverture.HeureDebut <= jourOuvertureExistant.HeureDebut && nouveauJourOuverture.HeureFin >= jourOuvertureExistant.HeureFin));
        }
    }
}