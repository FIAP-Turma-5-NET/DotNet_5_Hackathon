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
            .ForMember(dest => dest.Especialidades, opt => opt.Ignore()) 
            .ForMember(dest => dest.Telefone, opt => opt.Ignore()) 
            .ForMember(dest => dest.DDD, opt => opt.Ignore());     

            CreateMap<ConsultaModelRequest, Consulta>();

            CreateMap<EspecialidadeModelRequest, Especialidade>();

            CreateMap<HorarioDisponivelModelRequest, HorarioDisponivel>();

        }
    }
}
