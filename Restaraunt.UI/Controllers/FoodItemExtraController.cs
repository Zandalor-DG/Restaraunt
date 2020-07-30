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
    public class FoodItemExtraController : Controller
    {
        public IActionResult Index(int id, bool descending, SortFoodItemExtra sortOrder)
        {
            var foodsItemsExtraVm = ViewModel(id, descending, sortOrder);

            return View(foodsItemsExtraVm);
        }

        public IActionResult IndexAJAX(int id, bool descending, SortFoodItemExtra sortOrder)
        {
            var foodsItemsExtraVm = ViewModel(id, descending, sortOrder);

            return Json(foodsItemsExtraVm);
        }

        public FoodsItemsExtraVM ViewModel(int id, bool descending, SortFoodItemExtra sortOrder)
        {
            var foodItemExtra = NHibernateHelperCore.GetEntities<FoodItemExtra>(a => a.FoodCategory.Id == id);

            var foodItemExtraVM = foodItemExtra.Select(a => new FoodItemExtraVM()
                                                            {
                                                                    Id = a.Id,
                                                                    Name = a.Name,
                                                                    Price = a.Price,
                                                                    FoodCategoryId = id
                                                            }).ToList();

            foodItemExtraVM = sortOrder switch
            {
                    SortFoodItemExtra.Name when !descending => foodItemExtraVM.OrderBy(s => s.Name).ToList(),
                    SortFoodItemExtra.Name when descending => foodItemExtraVM.OrderByDescending(s => s.Name).ToList(),
                    SortFoodItemExtra.Price when !descending => foodItemExtraVM.OrderBy(s => s.Price).ToList(),
                    SortFoodItemExtra.Price when descending => foodItemExtraVM.OrderByDescending(s => s.Price).ToList(),
                    _ => foodItemExtraVM.OrderByDescending(s => s.Name).ToList(),
            };

            var foodsItemsExtraVm = new FoodsItemsExtraVM()
                                    {
                                            FoodsItemsExtra = foodItemExtraVM,
                                            FoodCategoryId = id,
                                            Descending = descending,
                                            Sort = sortOrder,
                                            Admin = User.IsInRole(NHibernateHelperCore.RoleAdmin)
                                    };

            return foodsItemsExtraVm;
        }

        [HttpGet]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodItemExtra(int? id, int foodCategoryId)
        {
            var foodItemExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemExtra>(a => a.Id == id);
            FoodItemExtraVM foodItemExtraVm;
            foodItemExtraVm = foodItemExtra == null ?
                                      new FoodItemExtraVM() :
                                      new FoodItemExtraVM()
                                      {
                                              Name = foodItemExtra.Name,
                                              Id = foodItemExtra.Id,
                                              Price = foodItemExtra.Price,
                                              FoodCategoryId = foodCategoryId
                                      };

            return View(foodItemExtraVm);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult CreateOrUpdateFoodItemExtra(FoodItemExtraVM foodItemExtraVm)
        {
            var foodCategory = NHibernateHelperCore.GetSingleOrDefault<FoodCategory>(a => a.Id == foodItemExtraVm.FoodCategoryId);
            var foodItemExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemExtra>(a => a.Id == foodItemExtraVm.Id) ??
                                new FoodItemExtra()
                                {
                                        Name = foodItemExtraVm.Name,
                                        Id = foodItemExtraVm.Id,
                                        Price = foodItemExtraVm.Price,
                                        FoodCategory = foodCategory
                                };

            foodItemExtra.Name = foodItemExtraVm.Name;
            foodItemExtra.Price = foodItemExtraVm.Price;

            NHibernateHelperCore.SaveOrUpdate(foodItemExtra);

            return RedirectToAction("Index", "FoodItemExtra", new
                                                              {
                                                                      id = foodItemExtraVm.FoodCategoryId
                                                              });
        }

        [HttpGet]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult ConfirmDeleteFoodItemExtra(int id)
        {
            var delFoodItemExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemExtra>(a => a.Id == id);
            var foodItemExtraVM = new FoodItemExtraVM()
                                  {
                                          Name = delFoodItemExtra.Name,
                                          Id = delFoodItemExtra.Id,
                                          Price = delFoodItemExtra.Price,
                                          FoodCategoryId = delFoodItemExtra.FoodCategory.Id
                                  };

            return View(foodItemExtraVM);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public IActionResult DeleteFoodItemExtra(int id, int foodCategoryId)
        {
            var delFoodItemExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemExtra>(a => a.Id == id);

            NHibernateHelperCore.DeleteEntities<FoodItemExtra>(delFoodItemExtra);

            return RedirectToAction("Index", "FoodItemExtra", new
                                                              {
                                                                      id = foodCategoryId
                                                              });
        }
    }
}