namespace Restaraunt.Entities
{
    #region << Using >>

    #endregion

    public class ShoppingCartFoodItem : EntityBase
    {
        #region Properties

        public virtual User User { get; set; }

        public virtual FoodItem FoodItem { get; set; }

        public virtual int Count { get; set; }

        #endregion

        #region Nested Classes

        public class Mapping : EntityMapBase<ShoppingCartFoodItem>
        {
            #region Constructors

            public Mapping()
            {
                References(a => a.User);
                References(a => a.FoodItem);
                Map(a => a.Count);
            }

            #endregion
        }

        #endregion
    }
}