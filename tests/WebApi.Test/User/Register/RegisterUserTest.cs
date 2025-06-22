using CommonTestUltilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.ResponseCompression;
using MyRecipeBook.Exceptions;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using WebApi.Test.InlineData;

namespace WebApi.Test.User.Register;

public class RegisterUserTest : IClassFixture<CustoWebApplicationFactory>
{
    private readonly HttpClient _httpClient;

    public RegisterUserTest ( CustoWebApplicationFactory factory )
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Success ()
    {
        var request = RequestsRegisterUserJsonBuilder.Build();

        var response = await _httpClient.PostAsJsonAsync("User" , request);

        response.StatusCode.Should().Be(HttpStatusCode.Created);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(request.Name);
    }

    [Theory]
    [ClassData(typeof(CultureInlineDataTest))]
    public async Task Error_Empty_Name ( string culture )
    {
        var request = RequestsRegisterUserJsonBuilder.Build();
        request.Name = string.Empty;

        //verifica se no header tem um accept-language e remove, para não haver duplicidade e lançar uma exceção no cliente.
        if (_httpClient.DefaultRequestHeaders.Contains("Accept-Language"))
            _httpClient.DefaultRequestHeaders.Remove("Accept-Language");

        _httpClient.DefaultRequestHeaders.Add("Accept-Language" , culture);

        var response = await _httpClient.PostAsJsonAsync("User" , request);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        await using var responseBody = await response.Content.ReadAsStreamAsync();

        var responseData = await JsonDocument.ParseAsync(responseBody);

        var erros = responseData.RootElement.GetProperty("errors").EnumerateArray();

        var expectedMessage = ResourceMessagesException.ResourceManager.GetString("NAME_EMPTY" , new CultureInfo(culture));

        erros.Should().ContainSingle().And.Contain(error => error.GetString()!.Equals(expectedMessage));
    }
}