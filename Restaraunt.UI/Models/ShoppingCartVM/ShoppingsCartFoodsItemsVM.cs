namespace Restaraunt.Entities.ViewModel
{
    #region << Using >>

    using System.Collections.Generic;

    #endregion

    public class ShoppingsCartFoodsItemsVM
    {
        #region Properties

        public List<ShoppingCartFoodItemVM> ShoppingCartFoodItem { get; set; }

        public int AllCountFoodsItems { get; set; }

        public bool Admin { get; set; }

        #endregion

        #region Constructors

        public ShoppingsCartFoodsItemsVM()
        {
            ShoppingCartFoodItem = new List<ShoppingCartFoodItemVM>();
        }

        #endregion
    }
}