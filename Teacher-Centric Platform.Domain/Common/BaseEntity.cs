using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Teacher_Centric_Platform.Domain.Common
{
    public abstract class BaseEntity
    {
        public Guid Id { get; protected set; } = Guid.NewGuid();
        public bool IsDeleted { get;  set; } = false;

    }
}
