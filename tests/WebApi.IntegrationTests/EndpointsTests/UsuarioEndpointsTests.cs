using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

public class UsuarioEndpointsTests : IClassFixture<CustomWebAppFactory>
{
    private readonly HttpClient _client;

    public UsuarioEndpointsTests(CustomWebAppFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Post_And_GetById_Should_Work()
    {
        var create = new
        {
            nombre = "Ana Perez",
            email = "ana.perez@example.com",
            domicilios = new object[]
            {
                new { calle = "San Martín", numero = "456", provincia = "Córdoba", ciudad = "Capital" }
            }
        };

        var post = await _client.PostAsJsonAsync("/api/usuario", create);
        post.StatusCode.Should().Be(HttpStatusCode.Created);

        var created = await post.ReadAsAsync<UsuarioResponseLite>();
        created.Nombre.Should().Be("Ana Perez");
        created.Email.Should().Be("ana.perez@example.com");
        created.Domicilios.Should().HaveCount(1);

        var get = await _client.GetAsync($"/api/usuario/{created.Id}");
        get.StatusCode.Should().Be(HttpStatusCode.OK);

        var fetched = await get.ReadAsAsync<UsuarioResponseLite>();
        fetched.Id.Should().Be(created.Id);
    }

    [Fact]
    public async Task Post_Should_Return_400_When_Invalid_Email()
    {
        var create = new { nombre = "Bad Email", email = "not-an-email", domicilios = (object?)null };

        var post = await _client.PostAsJsonAsync("/api/usuario", create);
        post.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Put_Should_Edit_And_Add_Domicilios()
    {
        // Crear base
        var post = await _client.PostAsJsonAsync("/api/usuario", new
        {
            nombre = "Juan Gomez",
            email = "juan.gomez@example.com",
            domicilios = new object[]
            {
                new { calle = "Mitre", numero = "100", provincia = "Córdoba", ciudad = "Capital" }
            }
        });
        var created = await post.ReadAsAsync<UsuarioResponseLite>();

        // Update: editar y agregar
        var update = new
        {
            id = created.Id,
            nombre = "Juan G. Actualizado",
            email = "juan.g.actualizado@example.com",
            domicilios = new object[]
            {
                new { id = created.Domicilios[0].Id, calle = "Rivadavia", numero = "789", provincia = "Córdoba", ciudad = "Capital" },
                new { id = (int?)null, calle = "Belgrano", numero = "1500", provincia = "Córdoba", ciudad = "Villa Allende" }
            }
        };

        var put = await _client.PutAsJsonAsync($"/api/usuario/{created.Id}", update);
        put.StatusCode.Should().Be(HttpStatusCode.OK);

        var updated = await put.ReadAsAsync<UsuarioResponseLite>();
        updated.Nombre.Should().Be("Juan G. Actualizado");
        updated.Domicilios.Should().HaveCount(2);
    }

    [Fact]
    public async Task Search_By_Provincia_Should_Return_Results()
    {
        await _client.PostAsJsonAsync("/api/usuario", new
        {
            nombre = "Cordi",
            email = "cordi@example.com",
            domicilios = new object[] { new { calle = "X", numero = "1", provincia = "Córdoba", ciudad = "Capital" } }
        });

        var res = await _client.GetAsync("/api/usuario/search?provincia=Córdoba");
        res.StatusCode.Should().Be(HttpStatusCode.OK);

        var list = await res.Content.ReadFromJsonAsync<UsuarioResponseLite[]>();
        list!.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Delete_Should_Return_204()
    {
        var post = await _client.PostAsJsonAsync("/api/usuario", new
        {
            nombre = "Borrar",
            email = "borrar@example.com",
            domicilios = (object?)null
        });
        var created = await post.ReadAsAsync<UsuarioResponseLite>();

        var del = await _client.DeleteAsync($"/api/usuario/{created.Id}");
        del.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var get = await _client.GetAsync($"/api/usuario/{created.Id}");
        get.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}
