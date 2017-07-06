using G4S.Entities.Pocos;
using System;
using System.Collections.Generic;

namespace G4S.Business.Helpers

{
    public class EntityResult<TEntity> : EntityResult where TEntity : EntityBase
    {
        public TEntity Entity { get; set; }

        public EntityResult(ResultCode result, string[] validationMessages, TEntity entity) : base(result, validationMessages)
        {
            this.Entity = entity;
        }

        public EntityResult(ResultCode result, TEntity entity) : base(result)
        {
            this.Entity = entity;
        }

        public EntityResult(ResultCode result) : base(result)
        {
        }

        public EntityResult(ResultCode result, string[] validationMessages) : base(result, validationMessages)
        {
        }
    }
}