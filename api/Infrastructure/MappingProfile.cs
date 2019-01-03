using System;
using System.Collections.Generic;
using System.Linq;
using CroixRouge.Model;
using CroixRouge.DTO;
using AutoMapper;

namespace CroixRouge.api.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Role,RoleDTO>();
            CreateMap<RoleDTO,Role>();

            CreateMap<Alerte,AlerteDTO>();
            CreateMap<AlerteDTO, Alerte>();

            CreateMap<Groupesanguin, GroupesanguinDTO>();

            CreateMap<Utilisateur, UtilisateurDTO>();
            //.ForMember(u => u.Password, opt => opt.Ignore());
            CreateMap<UtilisateurDTO, Utilisateur>();
            //.ForMember(u => u.Password, opt => opt.Ignore());

            CreateMap<CollecteDTO, Collecte>();
            CreateMap<Collecte, CollecteDTO>();

            CreateMap<InformationDTO, Information>();
            CreateMap<Information, InformationDTO>();

            CreateMap<Don, DonDTO>();
            CreateMap<DonDTO, Don>();

            CreateMap<Jourouverture, JourouvertureDTO>();
            CreateMap<JourouvertureDTO, Jourouverture>();

            CreateMap<Lanceralerte, LanceralerteDTO>();
            CreateMap<LanceralerteDTO, Lanceralerte>();
        }
    } 
}