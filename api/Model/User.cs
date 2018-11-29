using System;

namespace api.model
{

    public class User 
    {
        private string userName;
        private string passWord;
        private string email;
        private int id;
        public int Id
        {
            get { return id;}
            set { id = value;}
        }
        
        public string Email
        {
            get { return email;}
            set { email = value;}
        }
        
        public string PassWord
        {
            get { return passWord;}
            set { passWord = value;}
        }
        
        public string UserName
        {
            get { return userName;}
            set { userName = value;}
        }

        public User()
        {

        }
    }
}