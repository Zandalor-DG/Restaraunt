namespace Restaraunt.Entities
{
    #region << Using >>

    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Rendering;

    #endregion

    public class CreateOrUpdateFoodItemToExtraVM : FoodItemToExtraVM
    {
        #region Properties

        public List<SelectListItem> FoodItems { get; set; }

        public List<SelectListItem> FoodItemExtras { get; set; }

        #endregion
    }
}