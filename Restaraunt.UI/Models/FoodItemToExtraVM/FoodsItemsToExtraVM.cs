namespace Restaraunt.Entities
{
    #region << Using >>

    using System.Collections.Generic;

    #endregion

    public class FoodsItemsToExtraVM
    {
        #region Properties

        public List<FoodItemToExtraVM> FoodItemToExtraVm { get; set; }

        public int FoodItemId { get; set; }

        public int FoodItemExtraId { get; set; }

        public bool Admin { get; set; }

        #endregion

        #region Constructors

        public FoodsItemsToExtraVM()
        {
            FoodItemToExtraVm = new List<FoodItemToExtraVM>();
        }

        #endregion
    }
}