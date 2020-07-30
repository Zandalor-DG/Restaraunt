namespace Restaraunt.Entities
{
    #region << Using >>

    #endregion

    public class ShoppingCartFoodItemExtra : EntityBase
    {
        #region Properties

        public virtual ShoppingCartFoodItem ShoppingCartFoodItem { get; set; }

        public virtual FoodItemExtra FoodItemExtra { get; set; }

        public virtual int Count { get; set; }

        #endregion

        #region Nested Classes

        public class Mapping : EntityMapBase<ShoppingCartFoodItemExtra>
        {
            #region Constructors

            public Mapping()
            {
                Map(a => a.Count);
                References(a => a.ShoppingCartFoodItem);
                References(a => a.FoodItemExtra);
            }

            #endregion
        }

        #endregion
    }
}