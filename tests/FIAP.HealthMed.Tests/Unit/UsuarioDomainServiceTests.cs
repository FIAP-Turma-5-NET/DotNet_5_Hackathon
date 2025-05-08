using FIAP_HealthMed.Domain.Entity;
using FIAP_HealthMed.Domain.Enums;
using FIAP_HealthMed.Domain.Helpers;
using FIAP_HealthMed.Domain.Interface.Repository;
using FIAP_HealthMed.Domain.Service;

using Moq;

namespace FIAP.HealthMed.Tests.Unit
{
    public class UsuarioDomainServiceTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private readonly UsuarioDomainService _service;

        public UsuarioDomainServiceTests()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _service = new UsuarioDomainService(_usuarioRepositoryMock.Object);
        }

        [Fact]
        public async Task CadastrarAsync_DeveRetornarId_QuandoCadastroBemSucedido()
        {
            // Arrange  
            var usuario = new Usuario
            {
                Nome = "João Silva",
                CPF = "123.456.789-00",
                DDD = "11", 
                Telefone = "98765-4321",
                Email = "joao.silva@example.com",
                SenhaHash = "hashed-password",
                Role = Role.Paciente 
            };

            _usuarioRepositoryMock
                .Setup(repo => repo.CadastrarAsync(usuario))
                .ReturnsAsync(1);

            // Act  
            var resultado = await _service.CadastrarAsync(usuario);

            // Assert  
            Assert.Equal(1, resultado);
            _usuarioRepositoryMock.Verify(repo => repo.CadastrarAsync(usuario), Times.Once);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarUsuario_QuandoIdExistir()
        {
            // Arrange  
            var usuario = new Usuario
            {
                Id = 1,
                Nome = "João Silva",
                CPF = "123.456.789-00",
                DDD = "11",
                Telefone = "98765-4321",
                Email = "joao.silva@example.com",
                SenhaHash = "hashed-password",
                Role = Role.Paciente
            };

            _usuarioRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(1))
                .ReturnsAsync(usuario);

            // Act  
            var resultado = await _service.ObterPorIdAsync(1);

            // Assert  
            Assert.NotNull(resultado);
            Assert.Equal("João Silva", resultado?.Nome);
        }

        [Fact]
        public async Task ObterPorIdAsync_DeveRetornarNull_QuandoIdNaoExistir()
        {
            // Arrange
            _usuarioRepositoryMock
                .Setup(repo => repo.ObterPorIdAsync(1))
                .ReturnsAsync((Usuario?)null);

            // Act
            var resultado = await _service.ObterPorIdAsync(1);

            // Assert
            Assert.Null(resultado);
        }

        [Fact]
        public async Task VerificarExistentePorCpfOuEmailAsync_DeveRetornarTrue_QuandoUsuarioExistir()
        {
            // Arrange
            var cpf = "123.456.789-00";
            var email = "joao.silva@example.com";

            _usuarioRepositoryMock
                .Setup(repo => repo.VerificarExistentePorCpfOuEmailAsync(cpf, email))
                .ReturnsAsync(true);

            // Act
            var resultado = await _service.VerificarExistentePorCpfOuEmailAsync(cpf, email);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public async Task VerificarExistentePorCpfOuEmailAsync_DeveRetornarFalse_QuandoUsuarioNaoExistir()
        {
            // Arrange
            var cpf = "123.456.789-00";
            var email = "joao.silva@example.com";

            _usuarioRepositoryMock
                .Setup(repo => repo.VerificarExistentePorCpfOuEmailAsync(cpf, email))
                .ReturnsAsync(false);

            // Act
            var resultado = await _service.VerificarExistentePorCpfOuEmailAsync(cpf, email);

            // Assert
            Assert.False(resultado);
        }

        [Fact]
        public async Task ListarMedicosAsync_DeveRetornarListaDeMedicos_QuandoExistiremMedicos()
        {
            // Arrange  
            var medicos = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = "Dr. João",
                    CPF = "123.456.789-00",
                    DDD = "11",
                    Telefone = "98765-4321",
                    Email = "dr.joao@example.com",
                    SenhaHash = "hashed-password",
                    Role = Role.Medico,
                    CRM = "12345"
                },
                new Usuario
                {
                    Id = 2,
                    Nome = "Dr. Maria",
                    CPF = "987.654.321-00",
                    DDD = "21",
                    Telefone = "91234-5678",
                    Email = "dr.maria@example.com",
                    SenhaHash = "hashed-password",
                    Role = Role.Medico,
                    CRM = "67890"
                }
            };

            _usuarioRepositoryMock
                .Setup(repo => repo.ListarMedicosAsync(null, null, null))
                .ReturnsAsync(medicos);

            // Act  
            var resultado = await _service.ListarMedicosAsync(null, null, null);

            // Assert  
            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public async Task InserirEspecialidadesUsuarioAsync_DeveChamarRepositorio_QuandoDadosSaoValidos()
        {
            // Arrange
            var usuarioId = 1;
            var especialidadeIds = new List<int> { 1, 2, 3 };

            _usuarioRepositoryMock
                .Setup(repo => repo.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds))
                .Returns(Task.CompletedTask);

            // Act
            await _service.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds);

            // Assert
            _usuarioRepositoryMock.Verify(repo => repo.InserirEspecialidadesUsuarioAsync(usuarioId, especialidadeIds), Times.Once);
        }

        [Fact]
        public void VerificarLogin_DeveRetornarTrue_QuandoSenhaForValida()
        {
            // Arrange
            var usuario = new Usuario
            {
                Nome = "João Silva",
                CPF = "123.456.789-00",
                DDD = "11",
                Telefone = "98765-4321",
                Email = "joao.silva@example.com",
                SenhaHash = PasswordHasher.HashPassword("senha123"),
                Role = Role.Paciente
            };
            var senha = "senha123";

            // Act
            var resultado = _service.VerificarLogin(usuario, senha);

            // Assert
            Assert.True(resultado);
        }

        [Fact]
        public void VerificarLogin_DeveRetornarFalse_QuandoSenhaForInvalida()
        {
            // Arrange
            var usuario = new Usuario
            {
                Nome = "João Silva",
                CPF = "123.456.789-00",
                DDD = "11",
                Telefone = "98765-4321",
                Email = "joao.silva@example.com",
                SenhaHash = PasswordHasher.HashPassword("senha123"),
                Role = Role.Paciente
            };
            var senha = "senhaErrada";

            // Act
            var resultado = _service.VerificarLogin(usuario, senha);

            // Assert
            Assert.False(resultado);
        }
    }
}

