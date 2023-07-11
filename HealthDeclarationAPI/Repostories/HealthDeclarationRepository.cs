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
                var healthdeclare = BindModelToEntity(healthDeclarationModel,false);
                _appDbContext.HealthDeclarations.Add(healthdeclare.Item1);
                _appDbContext.HealthDeclarationItems.Add(healthdeclare.Item2);
                await _appDbContext.SaveChangesAsync();
                return healthdeclare.Item1;
        }

        public async Task Delete(string Id) {
            var header =(_appDbContext.HealthDeclarations.Where(x=>x.Id==Id)).SingleOrDefault();
            var items = (_appDbContext.HealthDeclarationItems.Where(x => x.HeaderId== Id)).SingleOrDefault();
            if (items != null && header != null) {
                _appDbContext.HealthDeclarationItems.Remove(items);
                _appDbContext.HealthDeclarations.Remove(header);
                await _appDbContext.SaveChangesAsync();
            }
            else {
                throw new Exception($"not found requested Id :{Id}");
            }
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

        public async Task<HealthDeclarationModel> GetById(string id) {
            var item=await (from h in _appDbContext.HealthDeclarations
                              join i in _appDbContext.HealthDeclarationItems
                              on h.Id equals i.HeaderId
                              where h.Id == id
                              select new HealthDeclarationModel
                              {
                                  Id = h.Id,
                                  DeviceId = h.DeviceId,
                                  DeviceType = h.DeviceType,
                                  DeviceName = h.DeviceName,
                                  GroupId = h.GroupId,
                                  DataType = h.DataType,
                                  Data = new Data
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
                              }).SingleOrDefaultAsync();
            return item;
        }

        public async Task Update(HealthDeclarationModel healthDeclarationModel) {
            var healthdeclare = BindModelToEntity(healthDeclarationModel, true);
          
            _appDbContext.Entry(healthdeclare.Item1).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            _appDbContext.ChangeTracker.Clear();
            _appDbContext.Entry(healthdeclare.Item2).State = EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
        }
      
        private (HealthDeclarationEntity, HealthDeclarationItemEntity) BindModelToEntity(HealthDeclarationModel model,bool IsUpdate) {
            string  headerId = Guid.NewGuid().ToString();
            string itemId = Guid.NewGuid().ToString();
            if (IsUpdate) {
                headerId = model.Id;
                var o = _appDbContext.HealthDeclarationItems.Where(x => x.HeaderId == model.Id).SingleOrDefault();
                if(o!=null)
                itemId =o.Id;
            }
           
            var entity = new HealthDeclarationEntity()
            {
                Id = headerId,
                DeviceId =model.DeviceId,
                DeviceType=model.DeviceType,
                DeviceName=model.DeviceName,
                GroupId=model.GroupId,
                DataType=model.DataType,
                Timestamp = DateTime.Now.Ticks
        };
            var item = new HealthDeclarationItemEntity()
            {
                Id =itemId, 
                FullPowerMode = model.Data.FullPowerMode,
                ActivePowerControl = model.Data.ActivePowerControl,
                FirmwareVersion = model.Data.FirmwareVersion,
                Temperature = model.Data.Temperature,
                Humidity = model.Data.Humidity,
                Version = model.Data.Version,
                MessageType = model.Data.MessageType,
                Occupancy = model.Data.Occupancy,
                StateChanged = model.Data.StateChanged,
                HeaderId= headerId
            };
            return (entity,item);
        }
    }
}
