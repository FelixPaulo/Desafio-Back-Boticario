using AutoMapper;
using Cashbot.Application.AutoMapper;
using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.Services;
using Cashbot.Application.ViewModels.Dealer;
using Cashbot.Domain.Interfaces;
using Cashbot.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cashbot.Application.UnitTests
{
    public class DealerApplicationTests
    {
        private readonly DealerApplication _dealerApplication;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly Mock<ApplicationNotificationHandler> _notifications;
        private readonly Mock<IDealerRepository> _dealerRepository;

        public DealerApplicationTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<ApplicationNotificationHandler>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _dealerRepository = new Mock<IDealerRepository>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();

            _dealerApplication = new DealerApplication(mapper, _unitOfWork.Object, _mediator.Object,
                                                                        _httpContextAccessor.Object,
                                                                        _dealerRepository.Object, _notifications.Object);
        }

        [Fact]
        public async Task ShouldReturnDealerViewModelIfAddADealerWithSucess()
        {
            //Arrange
            var addDealerViewModel = new AddDealerViewModel { Cpf = "000.111.222-77", Email = "teste@grupoboticario.com", Name = "teste", Password = "Teste@123"};

            //Act
            var result = await _dealerApplication.Add(addDealerViewModel);

            //Assert
            _mediator.Invocations.Count().Should().Be(1);
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task ShouldReturnNullIfAddADealerWithDomainInvalid()
        {
            //Arrange
            var addDealerViewModel = new AddDealerViewModel();

            //Act
            var result = await _dealerApplication.Add(addDealerViewModel);

            //Assert
            _mediator.Invocations.Count().Should().Be(4);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "O nome é obrigatório")));
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "O cpf é obrigatório")));
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "A senha é obrigatório")));
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "E-mail é obrigatório")));
            result.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnNullIfAddADealerWithEmailInvalid()
        {
            //Arrange
            var addDealerViewModel = new AddDealerViewModel { Cpf = "000.111.222-77", Email = "teste.com", Name = "teste", Password = "Teste@123" };

            //Act
            var result = await _dealerApplication.Add(addDealerViewModel);

            //Assert
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "E-mail inválido")));
            result.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnDealerIfFindByCpf()
        {
            //Arrange
            var dealer = new Dealer ("teste", "000.111.222-77", "teste@grupoboticario.com", "Teste@123");
            _dealerRepository.Setup(x => x.GetByCpf(It.IsAny<string>())).ReturnsAsync(dealer);

            //Act
            var result = await _dealerApplication.GetByCpf("000.111.222-77");

            //Assert
            _mediator.Invocations.Count().Should().Be(0);
            result.Should().NotBeNull();
            result.Cpf.Should().Be("000.111.222-77");
        }

        [Fact]
        public async Task ShouldReturnDealerIfFindByLogin()
        {
            //Arrange
            var dealer = new Dealer("teste", "000.111.222-77", "teste@grupoboticario.com", "Teste@123");
            _dealerRepository.Setup(x => x.GetByEmailAndPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(dealer);

            //Act
            var result = await _dealerApplication.GetByEmailAndPassword("teste@grupoboticario.com", "Teste@123");

            //Assert
            _mediator.Invocations.Count().Should().Be(0);
            result.Should().NotBeNull();
            result.Email.Should().Be("teste@grupoboticario.com");
            result.Password.Should().Be("Teste@123");
        }
    }
}
