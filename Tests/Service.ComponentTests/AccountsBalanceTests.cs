// <copyright file="AccountsBalanceTests.cs" company="Microsoft">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>

namespace Service.App.ComponentTests
{
    using System.Threading.Tasks;
    using FluentAssertions;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Service.Core.Models.Banking;
    using Service.Core.Providers;
    using Xunit;
    using static System.Net.HttpStatusCode;

    public class AccountsBalanceTests : ComponentTest
    {
        private const string ApiHeaderName = "api-version";
        private Mock<IBankAccountsRepository> _banksAccountsRepository;

        public AccountsBalanceTests()
        {
            _httpClient.DefaultRequestHeaders.Add(ApiHeaderName, "1.0");
        }

        [Fact]
        public async Task Calling_service_with_invalid_version_should_return_bad_request_error()
        {
            // No api-version case
            _httpClient.DefaultRequestHeaders.Remove(ApiHeaderName);
            var response = await _httpClient.GetAsync($"/api/banks/1/balance/euros");
            response.StatusCode.Should().Be(BadRequest);

            // Not supported version case
            _httpClient.DefaultRequestHeaders.Add(ApiHeaderName, "0.0");
            response = await _httpClient.GetAsync($"/api/banks/1/balance/euros");
            response.StatusCode.Should().Be(BadRequest);
        }

        [Fact]
        public async Task Get_balance_for_available_bank_account_in_euro()
        {
            var balanceInEuroToReturn = 9.23;
            var bankAccountId = 2;

            _banksAccountsRepository.Setup(br => br.Get(bankAccountId))
                .Returns(new BankAccount
                {
                    Balance = new Balance
                    {
                        Euros = balanceInEuroToReturn,
                    },
                });

            var response = await _httpClient.GetAsync($"/api/banks/{bankAccountId}/balance/euros");
            var value = await response.Content.ReadAsStringAsync();

            value.Should().Be($"{balanceInEuroToReturn}");
        }

        [Fact]
        public async Task Should_return_not_found_if_id_type_not_valid()
        {
            var response = await _httpClient.GetAsync("/api/banks/myBankId/balance/euros");

            response.StatusCode.Should().Be(NotFound);
        }

        protected override void OverrideServices(IServiceCollection services)
        {
            _banksAccountsRepository = new Mock<IBankAccountsRepository>();

            services.AddSingleton(_banksAccountsRepository.Object);
        }
    }
}
