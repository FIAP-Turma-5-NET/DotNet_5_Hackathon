﻿using AutoMapper;
using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Application.Model.Especialidade;
using FIAP_HealthMed.Application.Model.HorarioDisponivel;
using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Application.Mapper
{
    public class EntityToModel : Profile
    {
        public EntityToModel() 
        {
            CreateMap<Usuario, UsuarioModelResponse>()
                .ForMember(dest => dest.Especialidade, opt => opt.MapFrom(src => src.Especialidade != null ? src.Especialidade.Nome : null));

            CreateMap<Consulta, ConsultaModelResponse>();

            CreateMap<HorarioDisponivel, HorarioDisponivelModelResponse>();

            CreateMap<Especialidade, EspecialidadeModelResponse>();
        }
    }
}
