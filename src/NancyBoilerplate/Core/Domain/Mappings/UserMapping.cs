using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using System;

namespace NancyBoilerplate.Core.Domain.Mappings
{
    public class UserMapping : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Table("Users");

            mapping.Id(x => x.Id, "Id").GeneratedBy.Identity();
        }
    }
}