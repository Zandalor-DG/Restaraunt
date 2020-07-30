namespace Restaraunt.UI.Controllers
{
    #region << Using >>

    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Restaraunt.Core;
    using Restaraunt.Entities;
    using Restaraunt.Entities.SortOrder;

    #endregion

    [Authorize]
    public class FoodItemController : Controller
    {
        public IActionResult Index(int id, bool descending, SortFoodItem sortOrder)
        {
            var foodsItemsVM = ViewModel(id, descending, sortOrder);

            return View(foodsItemsVM);
        }

        public IActionResult IndexAJAX(int id, bool descending, SortFoodItem sortOrder)
        {
            var foodsItemsVM = ViewModel(id, descending, sortOrder);

            return Json(foodsItemsVM);
        }

        public FoodsItemsVM ViewModel(int id, bool descending, SortFoodItem sortOrder)
        {
            var foodItem = NHibernateHelperCore.GetEntities<FoodItem>(a => a.FoodCategory.Id == id);

            var foodItemVM = foodItem.Select(a => new FoodItemVM()
                                                  {
                                                          Id = a.Id,
                                                          Name = a.Name,
                                                          Price = a.Price,
                                                          FoodCategoryId = id
                                                  }).ToList();

            foodItemVM = sortOrder switch
            {
                    SortFoodItem.Name when !descending => foodItemVM.OrderBy(s => s.Name).ToList(),
                    SortFoodItem.Name when descending => foodItemVM.OrderByDescending(s => s.Name).ToList(),
                    SortFoodItem.Price when !descending => foodItemVM.OrderBy(s => s.Price).ToList(),
                    SortFoodItem.Price when descending => foodItemVM.OrderByDescending(s => s.Price).ToList(),
                    _ => foodItemVM.OrderByDescending(s => s.Name).ToList(),
            };

            var foodsItemsVM = new FoodsItemsVM()
                               {
                                       FoodsItems = foodItemVM,
                                       FoodCategoryId = id,
                                       Descending = descending,
                                       Sort = sortOrder,
                                       Admin = User.IsInRole(NHibernateHelperCore.RoleAdmin)
                               };

            return foodsItemsVM;
        }

        [HttpGet]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodItem(int? id, int foodCategoryId)
        {
            var foodItem = NHibernateHelperCore.GetSingleOrDefault<FoodItem>(a => a.Id == id);
            FoodItemVM foodItemVm;
            foodItemVm = foodItem == null ?
                                 new FoodItemVM() :
                                 new FoodItemVM()
                                 {
                                         Name = foodItem.Name,
                                         Id = foodItem.Id,
                                         Price = foodItem.Price,
                                         FoodCategoryId = foodCategoryId
                                 };

            return View(foodItemVm);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodItem(FoodItemVM foodItemVm)
        {
            var foodCategory = NHibernateHelperCore.GetSingleOrDefault<FoodCategory>(a => a.Id == foodItemVm.FoodCategoryId);
            var foodItem = NHibernateHelperCore.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemVm.Id) ??
                           new FoodItem()
                           {
                                   Name = foodItemVm.Name,
                                   Id = foodItemVm.Id,
                                   Price = foodItemVm.Price,
                                   FoodCategory = foodCategory
                           };

            foodItem.Name = foodItemVm.Name;
            foodItem.Price = foodItemVm.Price;

            NHibernateHelperCore.SaveOrUpdate(foodItem);

            return RedirectToAction("Index", "FoodItem", new
                                                         {
                                                                 id = foodItemVm.FoodCategoryId
                                                         });
        }

        [HttpGet]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult ConfirmDeleteFoodItem(int id)
        {
            var delFoodItem = NHibernateHelperCore.GetSingleOrDefault<FoodItem>(a => a.Id == id);
            var foodItemVM = new FoodItemVM()
                             {
                                     Name = delFoodItem.Name,
                                     Id = delFoodItem.Id,
                                     Price = delFoodItem.Price,
                                     FoodCategoryId = delFoodItem.FoodCategory.Id
                             };

            return View(foodItemVM);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult DeleteFoodItem(int id, int foodCategoryId)
        {
            var delFoodItem = NHibernateHelperCore.GetSingleOrDefault<FoodItem>(a => a.Id == id);

            NHibernateHelperCore.DeleteEntities<FoodItem>(delFoodItem);

            return RedirectToAction("Index", "FoodItem", new
                                                         {
                                                                 id = foodCategoryId
                                                         });
        }
    }
}