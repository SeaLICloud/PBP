using System;

namespace PBP.Web.Models.Common
{
    public class Entity<TEntity>:IEntity where TEntity:IEntity
    {
        public Guid Guid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
