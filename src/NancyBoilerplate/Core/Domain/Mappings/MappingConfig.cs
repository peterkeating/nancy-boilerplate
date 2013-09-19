using FluentNHibernate.Automapping;
using System;

namespace NancyBoilerplate.Core.Domain.Mappings
{
    public class MappingConfig : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return typeof(IMappable).IsAssignableFrom(type);
        }
    }
}