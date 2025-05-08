using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Interfaces;
using FIAP_HealthMed.Application.Mapper;
using FIAP_HealthMed.Application.Service;
using FIAP_HealthMed.Application.Strategies;
using FIAP_HealthMed.Data.Repository;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Interface.Services;
using FIAP_HealthMed.Domain.Service;
using FIAP_HealthMed.Producer.Interface;
using FIAP_HealthMed.Producer.Producer;
using FIAP_HealthMed.Producer.Service;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP_HealthMed.CrossCutting
{
    public static class Bootstrapper
    {
        public static IServiceCollection AddRegisterCommonServices (this IServiceCollection services)
        {
            services.AddSingleton(MapperConfiguration.RegisterMapping());

            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IHorarioDisponivelRepository,HorarioDisponivelRepository>();
            services.AddScoped<IEspecialidadeRepository, EspecialidadeRepository>();

            return services;
        }

        public static IServiceCollection AddRegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioDomainService, UsuarioDomainService>();
            services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();

            services.AddScoped<IConsultaDomainService, ConsultaDomainService>();
            services.AddScoped<IConsultaApplicationService, ConsultaApplicationService>();

            services.AddScoped<IHorarioDisponivelDomainService, HorarioDisponivelDomainService>();
            services.AddScoped<IHorarioDisponivelApplicationService, HorarioDisponivelApplicationService>();

            services.AddScoped<IEspecialidadeDomainService, EspecialidadeDomainService>();
            services.AddScoped<IEspecialidadeApplicationService, EspecialidadeApplicationService>();

            services.AddScoped<ILoginStrategy, LoginEmailStrategy>();
            services.AddScoped<ILoginStrategy, LoginCpfStrategy>();
            services.AddScoped<ILoginStrategy, LoginCrmStrategy>();
            services.AddScoped<ILoginStrategyResolver, LoginStrategyResolver>();
            services.AddScoped<IAuthApplicationService, AuthApplicationService>();
            services.AddSingleton<ITokenService, TokenService>();
            services.AddScoped<IProducerService, ProducerService>();
            services.AddScoped<IUsuarioProducer, UsuarioProducer>();


            //services.AddScoped<IUsuarioApplicationService, UsuarioApplicationService>();



            //services.AddScoped<IProducerService, ProducerService>();
            //services.AddScoped<IContatoProducer, ContatoProducer>();

            return services;
        }
    }
}
