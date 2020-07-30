namespace Restaraunt.Entities
{
    #region << Using >>

    #endregion

    public class FoodItemToExtra : EntityBase
    {
        #region Properties

        public virtual FoodItem FoodItem { get; set; }

        public virtual FoodItemExtra FoodItemExtra { get; set; }

        #endregion

        #region Nested Classes

        public class Mapping : EntityMapBase<FoodItemToExtra>
        {
            #region Constructors

            public Mapping()
            {
                References(a => a.FoodItem);
                References(a => a.FoodItemExtra);
            }

            #endregion
        }

        #endregion
    }
}