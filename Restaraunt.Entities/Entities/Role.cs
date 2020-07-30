namespace Restaraunt.Entities
{
    #region << Using >>

    using System.Collections.Generic;
    using FluentNHibernate.Mapping;

    #endregion

    public enum Role
    {
        Admin = 1,
        User = 2
    }
}