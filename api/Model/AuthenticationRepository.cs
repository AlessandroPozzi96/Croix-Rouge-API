using System;
using System.Collections.Generic;

namespace api.model
{
    public class AuthenticationRepository
    {
        private User[] _users = new User[]
        {
            new User() {UserName= "janedoe", Email="jane@doe.com", Id=1, PassWord="123"}, 
            new User() {UserName= "johndoe", Email="john@doe.com", Id=2, PassWord="456"}
            //test commit
        };

        public IEnumerable<User> GetUsers()
        {
            return _users;
        }
        
    }
}
