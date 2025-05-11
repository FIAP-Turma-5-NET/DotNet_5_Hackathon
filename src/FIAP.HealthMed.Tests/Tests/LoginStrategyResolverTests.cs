using FIAP_HealthMed.Application.Enums;
using FIAP_HealthMed.Application.Interface;
using FIAP_HealthMed.Application.Service;
using Moq;


namespace FIAP.HealthMed.Tests.Unit
{
    public class LoginStrategyResolverTests
    {
        [Fact]
        public void Resolver_DeveRetornarEstrategiaCorreta_QuandoTipoLoginEhValido()
        {
            // Arrange
            var tipoLogin = TipoLogin.Email;
            var mockStrategy = new Mock<ILoginStrategy>();
            mockStrategy.Setup(s => s.TipoLogin).Returns(tipoLogin);

            var estrategias = new List<ILoginStrategy> { mockStrategy.Object };
            var resolver = new LoginStrategyResolver(estrategias);

            // Act
            var result = resolver.Resolver(tipoLogin);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockStrategy.Object, result);
        }

        [Fact]
        public void Resolver_DeveLancarExcecao_QuandoTipoLoginNaoEhSuportado()
        {
            // Arrange
            var tipoLogin = TipoLogin.CPF;
            var mockStrategy = new Mock<ILoginStrategy>();
            mockStrategy.Setup(s => s.TipoLogin).Returns(TipoLogin.Email);

            var estrategias = new List<ILoginStrategy> { mockStrategy.Object };
            var resolver = new LoginStrategyResolver(estrategias);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => resolver.Resolver(tipoLogin));
            Assert.Equal($"Tipo de login não suportado: {tipoLogin}", exception.Message);
        }

        [Fact]
        public void Resolver_DeveLancarExcecao_QuandoNaoExistemEstrategias()
        {
            // Arrange
            var tipoLogin = TipoLogin.Email;
            var resolver = new LoginStrategyResolver(new List<ILoginStrategy>());

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => resolver.Resolver(tipoLogin));
            Assert.Equal($"Tipo de login não suportado: {tipoLogin}", exception.Message);
        }
    }

}
