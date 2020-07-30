namespace Restaraunt.Entities
{
    using FluentNHibernate.Mapping;

    public class EntityMapBase<T> : ClassMap<T> where T : EntityBase
    {
        public EntityMapBase()
        {
            Id(a => a.Id);
            Map(a => a.CrDt).Nullable();
        }
    }
}