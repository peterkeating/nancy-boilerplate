using NancyBoilerplate.Core.Domain.Mappings;
using System;

namespace NancyBoilerplate.Core.Domain
{
    public class User : IMappable
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
    }
}