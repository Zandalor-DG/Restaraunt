namespace Restaraunt.UI.Controllers
{
    #region << Using >>

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Restaraunt.Core;
    using Restaraunt.Entities;

    #endregion

    [Authorize]
    public class FoodCategoryController : Controller
    {
        [HttpGet]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodCategory(int? id)
        {
            var foodCategory = NHibernateHelperCore.GetSingleOrDefault<FoodCategory>(a => a.Id == id);
            FoodCategoryVM categoryVM;
            categoryVM = foodCategory == null ?
                                 new FoodCategoryVM() :
                                 new FoodCategoryVM()
                                 {
                                         Name = foodCategory.Name, Id = foodCategory.Id
                                 };

            return View(categoryVM);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodCategory(FoodCategoryVM foodCategoryVm)
        {
            var foodCategory = NHibernateHelperCore.GetSingleOrDefault<FoodCategory>(a => a.Id == foodCategoryVm.Id) ??
                               new FoodCategory()
                               {
                                       Name = foodCategoryVm.Name,
                                       Id = foodCategoryVm.Id
                               };

            foodCategory.Name = foodCategoryVm.Name;
            NHibernateHelperCore.SaveOrUpdate<FoodCategory>(foodCategory);

            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult ConfirmDeleteFoodCategory(int id)
        {
            var delFoodCategory = NHibernateHelperCore.GetSingleOrDefault<FoodCategory>(a => a.Id == id);
            var foodCategoryVM = new FoodCategoryVM()
                                 {
                                         Name = delFoodCategory.Name,
                                         Id = delFoodCategory.Id
                                 };

            return View(foodCategoryVM);
        }

        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult DeleteFoodCategory(int id)
        {
            var delFoodCategory = NHibernateHelperCore.GetSingleOrDefault<FoodCategory>(a => a.Id == id);
            var foodItem = NHibernateHelperCore.GetEntities<FoodItem>(a => a.FoodCategory.Id == id);
            var foodItemExtra = NHibernateHelperCore.GetEntities<FoodItemExtra>(a => a.FoodCategory.Id == id);

            if (foodItem.Count != 0 && foodItemExtra.Count != 0)
            {
                foreach (var delFoodItem in foodItem)
                    NHibernateHelperCore.DeleteEntities<FoodItem>(delFoodItem);

                foreach (var delFoodItemExtra in foodItemExtra)
                    NHibernateHelperCore.DeleteEntities<FoodItemExtra>(delFoodItemExtra);
            }

            NHibernateHelperCore.DeleteEntities<FoodCategory>(delFoodCategory);

            return RedirectToAction("Index", "Home");
        }
    }
}