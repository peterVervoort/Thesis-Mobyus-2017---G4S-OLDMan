using G4S.Business.Helpers;
using G4S.DataAccess.UnitOfWork;
using G4S.Entities.Pocos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace G4S.Business.Writers
{
    public class UserRoleGroupWriter : Writer<UserRoleGroup>, IUserRoleGroupWriter
    {
        private readonly IUowProvider _uowProvider;

        public UserRoleGroupWriter(IUowProvider uowProvider)
        {
            _uowProvider = uowProvider;
        }

        public async Task<EntityResult<StateChange>> AddUserRoleGroupToStateChange(int userRoleGroupId, int stateChangeId)
        {
            EntityResult<StateChange> result = new EntityResult<StateChange>(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup stateChange
                    var scRepo = uow.GetRepository<StateChange>();
                    StateChange stateChange = await scRepo.GetAsync(stateChangeId);
                    if (stateChange == null) result.Exception = new System.Exception("StateChange not found in database");

                    //lookup group
                    var urgRepo = uow.GetRepository<UserRoleGroup>();
                    UserRoleGroup userRoleGroup = await urgRepo.GetAsync(userRoleGroupId);
                    if (userRoleGroup == null) result.Exception = new System.Exception("UserRoleGroup not found in database");

                    //add role to group
                    stateChange.AcceptedRoleGroups.Add(userRoleGroup);
                    await uow.SaveChangesAsync();
                    result.Entity = await scRepo.GetAsync(stateChangeId);
                    result.Code = ResultCode.Success;
                }
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public async Task<EntityResult> RemoveUserRoleGroupFromStateChange(int userRoleGroupId, int stateChangeId) {
            EntityResult result = new EntityResult(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup stateChange
                    var scRepo = uow.GetRepository<StateChange>();
                    StateChange stateChange = await scRepo.GetAsync(stateChangeId);
                    if (stateChange == null) result.Exception = new System.Exception("StateChange not found in database");

                    var group = stateChange.AcceptedRoleGroups.FirstOrDefault(urg => urg.Id == userRoleGroupId);
                    if (group == null) result.Exception = new System.Exception($"StateChange does not contain group with id {stateChangeId}");

                    //add role to group
                    stateChange.AcceptedRoleGroups.Remove(group);
                    await uow.SaveChangesAsync();
                    result.Code = ResultCode.Success;
                }
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }



        //Orderstatechange
        public async Task<EntityResult<OrderStateChange>> AddUserRoleGroupToOrderStateChange(int userRoleGroupId, int orderstateChangeId)
        {
            EntityResult<OrderStateChange> result = new EntityResult<OrderStateChange>(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup stateChange
                    var oscRepo = uow.GetRepository<OrderStateChange>();
                    OrderStateChange orderstateChange = await oscRepo.GetAsync(orderstateChangeId);
                    if (orderstateChange == null) result.Exception = new System.Exception("OrderStateChange not found in database");

                    //lookup group
                    var urgRepo = uow.GetRepository<UserRoleGroup>();
                    UserRoleGroup userRoleGroup = await urgRepo.GetAsync(userRoleGroupId);
                    if (userRoleGroup == null) result.Exception = new System.Exception("UserRoleGroup not found in database");

                    //add role to group
                    orderstateChange.AcceptedRoleGroups.Add(userRoleGroup);
                    await uow.SaveChangesAsync();
                    result.Entity = await oscRepo.GetAsync(orderstateChangeId);
                    result.Code = ResultCode.Success;
                }
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public async Task<EntityResult> RemoveUserRoleGroupFromOrderStateChange(int userRoleGroupId, int orderstateChangeId)
        {
            EntityResult result = new EntityResult(ResultCode.Failed);
            try
            {
                using (var uow = _uowProvider.CreateUnitOfWork())
                {
                    //lookup stateChange
                    var oscRepo = uow.GetRepository<OrderStateChange>();
                    OrderStateChange orderStateChange = await oscRepo.GetAsync(orderstateChangeId);
                    if (orderStateChange == null) result.Exception = new System.Exception("OrderStateChange not found in database");

                    var group = orderStateChange.AcceptedRoleGroups.FirstOrDefault(urg => urg.Id == userRoleGroupId);
                    if (group == null) result.Exception = new System.Exception($"StateChange does not contain group with id {orderstateChangeId}");

                    //add role to group
                    orderStateChange.AcceptedRoleGroups.Remove(group);
                    await uow.SaveChangesAsync();
                    result.Code = ResultCode.Success;
                }
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }
    }
}
