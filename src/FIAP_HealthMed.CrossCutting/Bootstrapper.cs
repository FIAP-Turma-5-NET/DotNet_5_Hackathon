using FIAP_HealthMed.Data.Repository;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Interface.Services;
using FIAP_HealthMed.Domain.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP_HealthMed.CrossCutting
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRegisterCommonServices (this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IHorarioDisponivelRepository,HorarioDisponivelRepository>();
            services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();

            return services;
        }

        public static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioDomainService, UsuarioDomainService>();
            services.AddScoped<IConsultaDomainService, ConsultaDomainService>();
            services.AddScoped<IHorarioDisponivelDomainService, HorarioDisponivelDomainService>();
            services.AddScoped<IEspecialidadeDomainService, EspecialidadeDomainService>();
           
            
            //services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();
           
            
            
            //services.AddScoped<IProducerService, ProducerService>();
            //services.AddScoped<IContatoProducer, ContatoProducer>();

            return services;
        }
    }
}
