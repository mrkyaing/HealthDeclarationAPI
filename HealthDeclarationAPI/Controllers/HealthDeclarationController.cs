using HealthDeclarationAPI.Entities;
using HealthDeclarationAPI.Models;
using HealthDeclarationAPI.Repostories;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HealthDeclarationAPI.Controllers {
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class HealthDeclarationController : ControllerBase {
        private readonly IHealthDeclarationRepository _healthDeclarationRepository;
        private readonly ILogger<HealthDeclarationController> _logger;

        public HealthDeclarationController(IHealthDeclarationRepository healthDeclarationRepository, ILogger<HealthDeclarationController> logger)
        {
            this._healthDeclarationRepository = healthDeclarationRepository;
            this._logger = logger;
        }

        // GET: api/<HealthDeclarationController>
        [HttpGet]
        public async Task<IList<HealthDeclarationModel>> Get() {
          return await  _healthDeclarationRepository.GetAll();
        }

        // GET api/<HealthDeclarationController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<HealthDeclarationController>
        [HttpPost]
        public async Task<IActionResult> Post(HealthDeclarationModel healthDeclarationModel) {
            try {
                _logger.LogInformation($"start executed: {DateTime.Now}");
                _logger.LogInformation($"requested payload :{healthDeclarationModel}");
                var healthcareDeclareEntity =await _healthDeclarationRepository.Create(healthDeclarationModel);
                _logger.LogInformation($"Executed successfully.");
                return Ok(new ErrorDetailsModel { StatusCode=200,Message= "created successfully." });
            }
            catch (Exception e) {
                throw;
            }
        }

        // PUT api/<HealthDeclarationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<HealthDeclarationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
