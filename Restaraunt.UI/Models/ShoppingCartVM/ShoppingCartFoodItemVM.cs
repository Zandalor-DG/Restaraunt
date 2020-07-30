namespace Restaraunt.Entities
{
    public class ShoppingCartFoodItemVM
    {
        #region Properties

        public int Id { get; set; }

        public string FoodName { get; set; }

        public int FoodItemId { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public decimal Price { get; set; }

        public int Count { get; set; }

        #endregion
    }
}