namespace Restaraunt.Entities
{
    using System;

    public class EntityBase
    {
        public virtual int Id { get; set; }

        public virtual DateTime? CrDt { get; }

        public EntityBase()
        {
            CrDt = new DateTime?(DateTime.UtcNow);
        }
    }
}