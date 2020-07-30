namespace Restaraunt.Entities
{
    #region << Using >>

    #endregion

    public class FoodItemExtra : EntityBase
    {
        #region Properties

        public virtual string Name { get; set; }

        public virtual decimal Price { get; set; }

        public virtual FoodCategory FoodCategory { get; set; }

        #endregion

        #region Nested Classes

        public class Mapping : EntityMapBase<FoodItemExtra>
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