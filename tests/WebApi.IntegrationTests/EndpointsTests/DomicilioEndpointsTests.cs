using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

public class DomicilioEndpointsTests : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public DomicilioEndpointsTests(CustomWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_Domicilio_For_Existing_User_Should_Work()
    {
        // Usuario base
        var userPost = await _client.PostAsJsonAsync("/api/usuario", new
        {
            nombre = "Con Domicilio",
            email = "condomicilio@example.com",
            domicilios = (object?)null
        });
        var user = await userPost.ReadAsAsync<UsuarioResponseLite>();

        // Crear domicilio para ese usuario
        var domPost = await _client.PostAsJsonAsync("/api/domicilio", new
        {
            usuarioId = user.Id,
            calle = "Corrientes",
            numero = "1234",
            provincia = "Córdoba",
            ciudad = "Capital"
        });
        domPost.StatusCode.Should().Be(HttpStatusCode.Created);

        var listRes = await _client.GetAsync("/api/domicilio");
        listRes.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Put_Domicilio_Should_Validate_Fields()
    {
        // Usuario + domicilio inicial
        var post = await _client.PostAsJsonAsync("/api/usuario", new
        {
            nombre = "Edit Dom",
            email = "edit.dom@example.com",
            domicilios = new object[] { new { calle = "Mitre", numero = "100", provincia = "Córdoba", ciudad = "Capital" } }
        });
        var created = await post.ReadAsAsync<UsuarioResponseLite>();
        var domId = created.Domicilios[0].Id;

        // Update inválido
        var badPut = await _client.PutAsJsonAsync($"/api/domicilio/{domId}", new
        {
            id = domId,
            calle = "",
            numero = "2020",
            provincia = "Córdoba",
            ciudad = "Mendiolaza"
        });
        badPut.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        // Update válido
        var goodPut = await _client.PutAsJsonAsync($"/api/domicilio/{domId}", new
        {
            id = domId,
            calle = "Nueva Calle",
            numero = "2020",
            provincia = "Córdoba",
            ciudad = "Mendiolaza"
        });
        goodPut.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
