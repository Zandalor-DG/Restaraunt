namespace Restaraunt.Models.SearchAllEntities
{
    #region << Using >>

    using System.Collections.Generic;
    using Restaraunt.Entities;

    #endregion

    public class AllItemsVM
    {
        #region Properties

        public List<FoodItemVM> FoodsItems { get; set; }

        public List<FoodItemExtraVM> FoodsItemsExtraVM { get; set; }

        #endregion

        #region Constructors

        public AllItemsVM()
        {
            FoodsItems = new List<FoodItemVM>();
            FoodsItemsExtraVM = new List<FoodItemExtraVM>();
        }

        #endregion
    }
}