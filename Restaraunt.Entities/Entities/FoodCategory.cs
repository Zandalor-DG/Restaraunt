namespace Restaraunt.Entities
{
    #region << Using >>

    using FluentNHibernate.Mapping;

    #endregion

    public class FoodCategory : EntityBase
    {
        #region Properties

        public virtual string Name { get; set; }

        #endregion

        #region Nested Classes

        public class Mapping : EntityMapBase<FoodCategory>
        {
            #region Constructors

            public Mapping()
            {
                Map(a => a.Name);
            }

            #endregion
        }

        #endregion
    }
}