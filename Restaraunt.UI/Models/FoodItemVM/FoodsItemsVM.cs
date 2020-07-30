namespace Restaraunt.Entities
{
    #region << Using >>

    using System.Collections.Generic;
    using Restaraunt.Entities.SortOrder;

    #endregion

    public class FoodsItemsVM
    {
        #region Properties

        public List<FoodItemVM> FoodsItems { get; set; }

        public int FoodCategoryId { get; set; }

        public bool Descending { get; set; }

        public SortFoodItem Sort { get; set; }

        public bool Admin { get; set; }

        #endregion

        #region Constructors

        public FoodsItemsVM()
        {
            FoodsItems = new List<FoodItemVM>();
        }

        #endregion
    }
}