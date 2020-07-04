using AutoMapper;
using Cashbot.Application.AutoMapper;
using Cashbot.Application.Interfaces;
using Cashbot.Application.Notifications;
using Cashbot.Application.Services;
using Cashbot.Application.ViewModels.Purchase;
using Cashbot.Domain.Interfaces;
using Cashbot.Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Cashbot.Application.UnitTests
{
    public class PurchaseApplicationTests
    {
        private readonly PurchaseApplication _purchaseApplication;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IMediatorHandler> _mediator;
        private readonly Mock<IHttpContextAccessor> _httpContextAccessor;
        private readonly Mock<ApplicationNotificationHandler> _notifications;
        private readonly Mock<IPurchaseRepository> _purchaseRepository;
        private readonly Mock<IDealerApplication> _dealerApplication;

        public PurchaseApplicationTests()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _mediator = new Mock<IMediatorHandler>();
            _notifications = new Mock<ApplicationNotificationHandler>();
            _httpContextAccessor = new Mock<IHttpContextAccessor>();
            _purchaseRepository = new Mock<IPurchaseRepository>();
            _dealerApplication = new Mock<IDealerApplication>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            var mapper = mockMapper.CreateMapper();

            _purchaseApplication = new PurchaseApplication(mapper, _unitOfWork.Object, _mediator.Object,
                                                                        _httpContextAccessor.Object,
                                                                        _dealerApplication.Object,
                                                                        _purchaseRepository.Object,
                                                                         _notifications.Object);
        }

        [Fact]
        public async Task ShouldReturnPurchaseViewModelIfAddAPurchaseWithSucess()
        {
            //Arrange
            var addPurchaseViewModel = new AddPurchaseViewModel { Code = "CodeTeste", Value = 100.00, Cpf = "000.111.222-77", Date = DateTime.Now };
            var dealer = new Dealer("teste", "000.111.222-77", "teste@grupoboticario.com", "Teste@123");
            _dealerApplication.Setup(x => x.GetByCpf(It.IsAny<string>())).ReturnsAsync(dealer);

            //Act
            var result = await _purchaseApplication.Add(addPurchaseViewModel);

            //Assert
            _mediator.Invocations.Count().Should().Be(1);
            result.Should().NotBeNull();
            result.Status.Should().Be("Em validação");
        }

        [Fact]
        public async Task ShouldReturnNullIfNotFoundPurchaseByCpfDealer()
        {
            //Arrange
            var addPurchaseViewModel = new AddPurchaseViewModel { Code = "CodeTeste", Value = 100.00, Cpf = "000.111.222-77", Date = DateTime.Now };

            //Act
            var result = await _purchaseApplication.Add(addPurchaseViewModel);

            //Assert
            _mediator.Invocations.Count().Should().Be(1);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "Não foi encontrado nenhum revendedor com o cpf: 000.111.222-77")));
            result.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnPurchaseWithStatusAproved()
        {
            //Arrange
            var addPurchaseViewModel = new AddPurchaseViewModel { Code = "CodeTeste", Value = 100.00, Cpf = "153.509.460-56", Date = DateTime.Now };
            var dealer = new Dealer("teste", "153.509.460-56", "teste@grupoboticario.com", "Teste@123");
            _dealerApplication.Setup(x => x.GetByCpf(It.IsAny<string>())).ReturnsAsync(dealer);


            //Act
            var result = await _purchaseApplication.Add(addPurchaseViewModel);

            //Assert
            _mediator.Invocations.Count().Should().Be(1);
            result.Should().NotBeNull();
            result.Status.Should().Be("Aprovado");
        }

        [Fact]
        public async Task ShouldReturnNullIfPurchaseDomainInvalid()
        {
            //Arrange
            var addPurchaseViewModel = new AddPurchaseViewModel { Cpf = "000.111.222-77" };
            var dealer = new Dealer("teste", "000.111.222-77", "teste@grupoboticario.com", "Teste@123");
            _dealerApplication.Setup(x => x.GetByCpf(It.IsAny<string>())).ReturnsAsync(dealer);

            //Act
            var result = await _purchaseApplication.Add(addPurchaseViewModel);

            //Assert
            _mediator.Invocations.Count().Should().Be(3);
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "O código é obrigatório")));
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "O valor é obrigatório")));
            _mediator.Verify(x => x.PublishEvent(It.Is<ApplicationNotification>(q => q.Value == "A data é obrigatório")));
            result.Should().BeNull();
        }

        [Fact]
        public async Task ShouldReturnListPurchaseWithCachBack()
        {
            //Arrange
            var purchase1 = new Purchase(1, "code1", 1000.0, "em validação", DateTime.Now);
            var purchase2 = new Purchase(1, "code1", 1000.1, "em validação", DateTime.Now);
            var purchase3 = new Purchase(1, "code1", 1510, "em validação", DateTime.Now);

            var lstPurchase = new List<Purchase>();
            lstPurchase.Add(purchase1);
            lstPurchase.Add(purchase2);
            lstPurchase.Add(purchase3);

            _purchaseRepository.Setup(x => x.GetAll()).ReturnsAsync(lstPurchase);

             //Act
             var result = await _purchaseApplication.ListPurchase();

            //Assert
            _mediator.Invocations.Count().Should().Be(0);
            result.Should().NotBeNull();
            result.Count().Should().Be(3);
            result.ElementAt(0).PercentCashback.Should().Be("10%");
            result.ElementAt(0).ValueCashback.Should().Be(100);
            result.ElementAt(1).PercentCashback.Should().Be("15%");
            result.ElementAt(1).ValueCashback.Should().Be(150.015);
            result.ElementAt(2).PercentCashback.Should().Be("20%");
            result.ElementAt(2).ValueCashback.Should().Be(302);
        }
    }
}
