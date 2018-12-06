using System;
using System.Collections.Generic;
using CroixRouge.Model;
using CroixRouge.Dal;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CroixRouge.api.Infrastructure
{
    public class AuthenticationRepository
    {

        private CroixRouge.Dal.bdCroixRougeContext _context;
        public AuthenticationRepository(CroixRouge.Dal.bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Utilisateur>> GetUsers()
        {
            return await this._context.Utilisateur.ToArrayAsync();
        }
    }
}
