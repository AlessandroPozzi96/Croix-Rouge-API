using System;
using System.Collections.Generic;
using CroixRouge.Model;
using CroixRouge.Dal;
namespace CroixRouge.api.Infrastructure
{
    public class AuthenticationRepository
    {

        private bdCroixRougeContext _context;
        public AuthenticationRepository(bdCroixRougeContext context)
        {
            this._context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Utilisateur> GetUsers()
        {
            return _context.Utilisateur;
        }
    }
}
