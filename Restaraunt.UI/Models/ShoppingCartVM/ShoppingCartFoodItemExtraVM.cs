using System;
using System.Collections.Generic;
using System.Text;

namespace Restaraunt.Entities.ViewModel
{
    public class ShoppingCartFoodsItemsVM
    {
        public  int Id { get; set; }

        public  UserVM User { get; set; }

        public FoodItemExtra FoodItemExtra { get; set; }

        public int Count { get; set; }
    }
}
