using HealthDeclarationAPI.Models;
using HealthDeclarationAPI.Repostories;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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

        [HttpGet]
        public async Task<IList<HealthDeclarationModel>> Get() {
          return await  _healthDeclarationRepository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<HealthDeclarationModel> Get(string id) {
            var item = await _healthDeclarationRepository.GetById(id);
            if(item == null) {
                throw new ArgumentNullException("not found ");
            }
            return item;
        }

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
                _logger.LogError($"Error {e.Message}");
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(HealthDeclarationModel healthDeclarationModel) {
            try {
                _logger.LogInformation($"start executed: {DateTime.Now}");
                _logger.LogInformation($"requested payload :{healthDeclarationModel}");
                 await _healthDeclarationRepository.Update(healthDeclarationModel);
                _logger.LogInformation($"Executed successfully.");
                return Ok(new ErrorDetailsModel { StatusCode = 200, Message = "updated successfully." });
            }
            catch (Exception e) {
                _logger.LogError($"Error {e.Message}");
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id) {
            try {   
                _logger.LogInformation($"start executed: {DateTime.Now}");
                _logger.LogInformation($"requested Id :{id}");
                 await _healthDeclarationRepository.Delete(id);
                _logger.LogInformation($"Executed successfully.");
                return Ok(new ErrorDetailsModel { StatusCode = 200, Message = "deleted successfully." });
            }
            catch (Exception e) {
                _logger.LogError($"Error {e.Message}");
                throw;
            }
        }
    }
}
