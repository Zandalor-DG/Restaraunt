namespace Restaraunt.UI.Controllers
{
    #region << Using >>

    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Restaraunt.Core;
    using Restaraunt.Entities;

    #endregion

    [Authorize]
    public class FoodItemToExtraController : Controller
    {
        public ActionResult Index()
        {
            var foodItemToExtra = NHibernateHelperCore.GetEntities<FoodItemToExtra>();
            var foodItemToExtraVM = foodItemToExtra.Select(a => new FoodItemToExtraVM()
                                                                {
                                                                        Id = a.Id
                                                                }).ToList();

            return View(foodItemToExtraVM);
        }

        [HttpGet]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public ActionResult CreateOrUpdateFoodItemToExtra(int id)
        {
            var foodItemToExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == id);

            var foodItems = NHibernateHelperCore.GetEntities<FoodItem>()
                                                .Select(a => new SelectListItem()
                                                             {
                                                                     Value = a.Id.ToString(),
                                                                     Text = a.Name
                                                             }).ToList();

            var foodItemExtras = NHibernateHelperCore.GetEntities<FoodItemExtra>()
                                                     .Select(a => new SelectListItem()
                                                                  {
                                                                          Value = a.Id.ToString(),
                                                                          Text = a.Name
                                                                  }).ToList();

            var foodItemToExtraVM = new CreateOrUpdateFoodItemToExtraVM()
                                    {
                                            Id = foodItemToExtra?.Id,
                                            FoodItemId = foodItemToExtra?.FoodItem.Id,
                                            FoodItemExtraId = foodItemToExtra?.FoodItemExtra.Id,
                                            FoodItems = foodItems,
                                            FoodItemExtras = foodItemExtras
                                    };

            return View(foodItemToExtraVM);
        }

        [HttpPost]
        [Authorize(Roles = NHibernateHelperCore.RoleAdmin)]
        public ActionResult CreateOrUpdateFoodItemToExtra(FoodItemToExtraVM foodItemToExtraVm)
        {
            var foodItemToExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == foodItemToExtraVm.Id) ?? new FoodItemToExtra();

            var foodItem = NHibernateHelperCore.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemToExtraVm.FoodItemId);
            var foodItemExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemExtra>(a => a.Id == foodItemToExtraVm.FoodItemExtraId);

            foodItemToExtra.FoodItem = foodItem;
            foodItemToExtra.FoodItemExtra = foodItemExtra;

            NHibernateHelperCore.SaveOrUpdate(foodItemToExtra);

            return RedirectToAction("Index", "FoodItemToExtra");
        }

        public ActionResult ConfirmDeleteFoodItemToExtra(int id)
        {
            var delFoodItemToExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == id);
            var foodItemToExtraVm = new FoodItemToExtraVM()
                                    {
                                            Id = delFoodItemToExtra.Id
                                    };

            return View(foodItemToExtraVm);
        }

        [HttpPost]
        public ActionResult DeleteFoodItemToExtra(int id)
        {
            var delFoodItemToExtra = NHibernateHelperCore.GetSingleOrDefault<FoodItemToExtra>(a => a.Id == id);

            NHibernateHelperCore.DeleteEntities<FoodItemToExtra>(delFoodItemToExtra);

            return RedirectToAction("Index", "FoodItemToExtra");
        }
    }
}