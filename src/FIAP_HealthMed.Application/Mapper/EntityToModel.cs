using AutoMapper;
using FIAP_HealthMed.Application.Model.Consulta;
using FIAP_HealthMed.Application.Model.Especialidade;
using FIAP_HealthMed.Application.Model.HorarioDisponivel;
using FIAP_HealthMed.Application.Model.Usuario;
using FIAP_HealthMed.Domain.Entity;
using Shared.Model;

namespace FIAP_HealthMed.Application.Mapper
{
    public class EntityToModel : Profile
    {
        public EntityToModel()
        {
            CreateMap<Usuario, UsuarioModelResponse>()
                .ForMember(dest => dest.Especialidades, opt => opt.MapFrom(src => src.Especialidades));

            CreateMap<Especialidade, EspecialidadeModelResponse>();

            CreateMap<Consulta, ConsultaModelResponse>()
              .ForMember(dest => dest.NomeMedico, opt => opt.MapFrom(src => src.MedicoNome))
              .ForMember(dest => dest.NomePaciente, opt => opt.MapFrom(src => src.PacienteNome))
              .ForMember(dest => dest.NomeEspecialidade, opt => opt.MapFrom(src => src.NomeEspecialidade));

            CreateMap<HorarioDisponivel, HorarioDisponivelModelResponse>();

            CreateMap<Usuario, UsuarioMensagem>()
            .ForMember(dest => dest.Telefone, opt => opt.MapFrom(src =>
             src.Telefone.Length == 9
                 ? $"({src.DDD}) {src.Telefone.Substring(0, 5)}-{src.Telefone.Substring(5)}"
                 : $"({src.DDD}) {src.Telefone.Substring(0, 4)}-{src.Telefone.Substring(4)}"
         ));
        }
    }
}
