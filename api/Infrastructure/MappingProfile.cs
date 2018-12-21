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
            CreateMap<CroixRouge.Model.Role,RoleModel>();

            CreateMap<RoleModel,CroixRouge.Model.Role>();

            CreateMap<CroixRouge.Model.Alerte,AlerteModel>();

            CreateMap<AlerteModel, CroixRouge.Model.Alerte>();

            CreateMap<CroixRouge.Model.Groupesanguin, GroupesanguinModel>();

            CreateMap<CroixRouge.Model.Utilisateur, UtilisateurModel>()
            .ForMember(u => u.Password, opt => opt.Ignore());

            CreateMap<UtilisateurModel, CroixRouge.Model.Utilisateur>()
            .ForMember(u => u.Password, opt => opt.Ignore());

            CreateMap<CroixRouge.Model.Adresse, CroixRouge.DTO.AdresseModel>();

            CreateMap<CroixRouge.DTO.AdresseModel, CroixRouge.Model.Adresse>();

            CreateMap<CroixRouge.DTO.CollecteModel, CroixRouge.Model.Collecte>();

            CreateMap<CroixRouge.Model.Collecte, CroixRouge.DTO.CollecteModel>();

            CreateMap<Information, InformationModel>();

            CreateMap<InformationModel, Information>();
        }
    } 
}