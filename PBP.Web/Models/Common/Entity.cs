using System;
using System.ComponentModel.DataAnnotations;

namespace PBP.Web.Models.Common
{
    public class Entity<TEntity>:IEntity where TEntity:IEntity
    {
        [Required]
        public Guid Guid { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }
    }
}
