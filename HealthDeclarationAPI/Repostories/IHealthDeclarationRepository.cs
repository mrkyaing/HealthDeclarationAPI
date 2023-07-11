using HealthDeclarationAPI.Entities;
using HealthDeclarationAPI.Models;

namespace HealthDeclarationAPI.Repostories {
    public interface IHealthDeclarationRepository {
        Task<HealthDeclarationEntity> Create(HealthDeclarationModel healthDeclarationModel);
        Task<IList<HealthDeclarationModel>> GetAll();
        Task<HealthDeclarationModel> GetById(string Id);
        Task Update(HealthDeclarationModel healthDeclarationModel);
        Task Delete(string Id);
    }
}
