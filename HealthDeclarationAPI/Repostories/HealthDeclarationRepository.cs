using AutoMapper;
using HealthDeclarationAPI.DAO;
using HealthDeclarationAPI.Entities;
using HealthDeclarationAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthDeclarationAPI.Repostories {
    public class HealthDeclarationRepository : IHealthDeclarationRepository {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public HealthDeclarationRepository(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<HealthDeclarationEntity> Create(HealthDeclarationModel healthDeclarationModel) {
            try {
                // var healthdeclare = _mapper.Map<HealthDeclarationEntity>(healthDeclarationModel);
                var healthdeclare = BindModelToEntity(healthDeclarationModel);
                _appDbContext.HealthDeclarations.Add(healthdeclare.Item1);
                _appDbContext.HealthDeclarationItems.Add(healthdeclare.Item2);
                await _appDbContext.SaveChangesAsync();
                return healthdeclare.Item1;
            }
            catch (Exception ex) {
                throw;
            }
        }

        public Task Delete(string Id) {
            throw new NotImplementedException();
        }

        public async Task<IList<HealthDeclarationModel>> GetAll() {
            var items =await (from h in _appDbContext.HealthDeclarations
                         join i in _appDbContext.HealthDeclarationItems
                         on h.Id equals i.HeaderId
                         select new HealthDeclarationModel
                         {
                             Id = h.Id,
                             DeviceId=h.DeviceId,
                             DeviceType = h.DeviceType,
                             DeviceName = h.DeviceName,
                             GroupId = h.GroupId,
                             DataType = h.DataType,
                             Data=new Data
                             { 
                                 FullPowerMode = i.FullPowerMode,
                                 ActivePowerControl = i.ActivePowerControl,
                                 FirmwareVersion = i.FirmwareVersion,
                                 Temperature = i.Temperature,
                                 Humidity = i.Humidity,
                                 Version = i.Version,
                                 MessageType = i.MessageType,
                                 Occupancy = i.Occupancy,
                                 StateChanged = i.StateChanged,
                             } 
                         }).ToListAsync();
            return items;
        }

        public Task Update(HealthDeclarationModel healthDeclarationModel) {
            throw new NotImplementedException();
        }

        private (HealthDeclarationEntity, HealthDeclarationItemEntity) BindModelToEntity(HealthDeclarationModel model) {
           
            var entity = new HealthDeclarationEntity()
            {
                Id = Guid.NewGuid().ToString(),
                DeviceId=model.DeviceId,
                DeviceType=model.DeviceType,
                DeviceName=model.DeviceName,
                GroupId=model.GroupId,
                DataType=model.DataType,
                Timestamp = DateTime.Now.Ticks
        };
            var item = new HealthDeclarationItemEntity()
            {
                Id = Guid.NewGuid().ToString(),
                FullPowerMode = model.Data.FullPowerMode,
                ActivePowerControl = model.Data.ActivePowerControl,
                FirmwareVersion = model.Data.FirmwareVersion,
                Temperature = model.Data.Temperature,
                Humidity = model.Data.Humidity,
                Version = model.Data.Version,
                MessageType = model.Data.MessageType,
                Occupancy = model.Data.Occupancy,
                StateChanged = model.Data.StateChanged,
                HeaderId=entity.Id
            };
            return (entity,item);
        }
    }
}
