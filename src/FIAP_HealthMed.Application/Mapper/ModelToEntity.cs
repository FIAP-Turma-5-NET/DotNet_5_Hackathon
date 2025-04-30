using AutoMapper;
using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Application.Model.Especialidade;
using FIAP_HealthMed.Application.Model.HorarioDisponivel;
using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Domain.Entity;

namespace FIAP_HealthMed.Application.Mapper
{
    public class ModelToEntity : Profile
    {
        public ModelToEntity() 
        {
            CreateMap<UsuarioModelRequest, Usuario>()
                .ForMember(dest => dest.SenhaHash, opt => opt.Ignore()) 
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role))
                .ForMember(dest => dest.EspecialidadeId, opt => opt.MapFrom(src => src.EspecialidadeId))
                .ForMember(dest => dest.CRM, opt => opt.MapFrom(src => src.CRM));

            CreateMap<ConsultaModelRequest, Consulta>();

            CreateMap<EspecialidadeModelRequest, Especialidade>();

            CreateMap<HorarioDisponivelModelRequest, HorarioDisponivel>();



        }
    }
}
