using System;

namespace api.model
{
    
    public class Alerte 
    {
        private int id;
        public int Id
        {
            get { return id;}
            set { id = value;}
        }
        private string nom;
        public string Nom
        {
            get { return nom;}
            set { nom = value;}
        }

        private string contenu;
        public string Contenu
        {
            get { return contenu;}
            set { contenu = value;}
        }
        
        public Alerte(int id, string nom, string contenu)
        {
            Id = id; 
            Nom = nom; 
            Contenu = contenu;
        }
        
        public Alerte ()
        {

        }
    }
}