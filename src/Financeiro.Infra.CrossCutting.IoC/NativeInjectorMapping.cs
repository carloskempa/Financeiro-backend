using Financeiro.App.App;
using Financeiro.App.AutoMapper;
using Financeiro.App.Commands;
using Financeiro.App.Handlers;
using Financeiro.App.Interfaces;
using Financeiro.Data;
using Financeiro.Data.Queries;
using Financeiro.Data.Repositories;
using Financeiro.Domain.Core.Communication.Mediator;
using Financeiro.Domain.Core.Messages;
using Financeiro.Domain.Entidades;
using Financeiro.Domain.Interfaces.Queries;
using Financeiro.Domain.Interfaces.Respositories;
using Financeiro.Domain.Interfaces.Services;
using Financeiro.Domain.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Financeiro.Infra.CrossCutting.IoC
{
    public class NativeInjectorMapping
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddSingleton(AutoMapperConfiguration.RegisterMappings().CreateMapper());
            services.AddDbContext<FinanceiroContext>(options => options.UseSqlServer(configuration.GetConnectionString("FinanceiroContext")));

            RegisterServicesApplication(services);
            RegisterServicesDomain(services);
            RegisterServicesCommands(services);
            RegisterServicesRepository(services);
            RegiterServicesQueries(services);
        }

        private static void RegiterServicesQueries(IServiceCollection services)
        {
            services.AddScoped<IMovimentoQuery, MovimentoQuery>();
            services.AddScoped<IPessoaQuery, PessoaQuery>();
            services.AddScoped<IFornecedorQuery, FornecedorQuery>();
            services.AddScoped<ICentroCustoQuery, CentroCustoQuery>();
            services.AddScoped<IContaFinanceiraQuery, ContaFinanceiraQuery>();
        }

        private static void RegisterServicesApplication(IServiceCollection services)
        {
            services.AddScoped<IUsuarioApp, UsuarioApp>();
            services.AddScoped<IContaFinanceiraApp, ContaFinanceiraApp>();
            services.AddScoped<IFornecedorApp, FornecedorApp>();
            services.AddScoped<ICentroCustoApp, CentroCustoApp>();
            services.AddScoped<IPessoaApp, PessoaApp>();
            services.AddScoped<IMovimentoApp, MovimentoApp>();
        }

        private static void RegisterServicesDomain(IServiceCollection services)
        {
            services.AddScoped<ICriptografiaService, CriptografiaService>();
        }   

        private static void RegisterServicesCommands(IServiceCollection services)
        {
            services.AddScoped<IRequestHandler<CriarUsuarioCommand, Usuario>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<LogarUsuarioCommand, Usuario>, UsuarioCommandHandler>();

            services.AddScoped<IRequestHandler<CriarContaFinanceiraCommand, ContaFinanceira>, ContaFinanceiraCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarContaFinanceiraCommand, ContaFinanceira>, ContaFinanceiraCommandHandler>();

            services.AddScoped<IRequestHandler<CriarFornecedorCommand, Fornecedor>, FornecedorCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarFornecedorCommand, Fornecedor>, FornecedorCommandHandler>();

            services.AddScoped<IRequestHandler<CriarCentroCustoCommand, CentroCusto>, CentroCustoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarCentroCustoCommand, CentroCusto>, CentroCustoCommandHandler>();

            services.AddScoped<IRequestHandler<CriarPessoaCommand, Pessoa>, PessoaCommandHandler>();

            services.AddScoped<IRequestHandler<CriarMovimentoCommand, Movimento>, MovimentoCommandHandler>();

        }

        private static void RegisterServicesRepository(IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IContaFinanceiraRepository, ContaFinanceiraRepository>();
            services.AddScoped<ICentroCustoRepository, CentroCustoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<IItemMovimentoRepository, ItemMovimentoRepository>();
            services.AddScoped<IMovimentoRepository, MovimentoRepository>();
        }
    }
}
