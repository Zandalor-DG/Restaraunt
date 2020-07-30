namespace Restaraunt.Entities
{
    #region << Using >>

    #endregion

    public class FoodItem : EntityBase
    {
        #region Properties

        public virtual string Name { get; set; }

        public virtual decimal Price { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }

        #endregion

        #region Nested Classes

        public class Mapping : EntityMapBase<FoodItem>
        {
            #region Constructors

            public Mapping()
            {
                Map(a => a.Name);
                Map(a => a.Price);
                References(a => a.FoodCategory);
            }

            #endregion
        }

        #endregion
    }
}