using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    public class DomicilioController : BaseController<Domicilio,IDomicilioRepository>
    {
        public DomicilioController(IDomicilioRepository repository) : base(repository)
        { 
        }
    }
}
