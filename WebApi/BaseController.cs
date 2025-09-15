using Domain;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using WebApi.Swagger;

//Controller base generico
namespace WebApi
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiConventionType(typeof(GenericApiConventions))]

    public abstract class BaseController<TEntity, TRepository> : ControllerBase
       where TEntity : class, IEntity
       where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;

        protected BaseController(TRepository repository)
        {
            _repository = repository;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync()
        {
            var entities = await _repository.GetAll();
            return Ok(entities);
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetByIdAsync(int id)
        {
            var entity = await _repository.Get(id);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> CreateAsync(TEntity entity)
        {
            var created = await _repository.Add(entity);

            var id = ((IEntity)created).Id;

            var controller = ControllerContext.ActionDescriptor.ControllerName.ToLowerInvariant();

            return Created($"/api/{controller}/{id}", created);
        }

        // PUT: api/[controller]/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<TEntity>> UpdateAsync(int id, TEntity entity)
        {
            if ((entity as IEntity)?.Id != id)
                return BadRequest("ID mismatch");

            var updated = await _repository.Update(entity);
            return Ok(updated);
        }

        // DELETE: api/[controller]/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> DeleteAsync(int id)
        {
            var deleted = await _repository.Delete(id);
            if (deleted == null)
                return NotFound();

            return Ok(deleted);
        }
    }
}
