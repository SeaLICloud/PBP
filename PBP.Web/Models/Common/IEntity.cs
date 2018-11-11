using System;

namespace PBP.Web.Models.Common
{
    public interface IEntity
    {
        Guid Guid { get; set; }
        DateTime CreateTime { get; set; }
        DateTime UpdateTime { get; set; }
    }
}
