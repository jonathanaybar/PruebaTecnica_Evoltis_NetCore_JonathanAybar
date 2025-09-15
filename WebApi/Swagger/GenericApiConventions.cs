using Microsoft.AspNetCore.Mvc;

namespace WebApi.Swagger;

public static class GenericApiConventions
{
    [ProducesResponseType(StatusCodes.Status200OK)]
    public static void Get() { }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void GetById(int id) { }

    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public static void Post(object body) { }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void Put(int id, object body) { }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public static void Delete(int id) { }
}
