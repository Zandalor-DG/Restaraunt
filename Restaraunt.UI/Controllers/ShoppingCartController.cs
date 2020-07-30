namespace Restaraunt.UI.Controllers
{
    #region << Using >>

    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Restaraunt.Core;
    using Restaraunt.Entities;
    using Restaraunt.Entities.ViewModel;

    #endregion

    [Authorize]
    public class ShoppingCartController : Controller
    {
        public IActionResult Index()
        {
            var shoppingCartItemVM = ViewModel();
            return View(shoppingCartItemVM);
        }

        public IActionResult IndexJson()
        {
            var shoppingCartItemVM = ViewModel();
            return Json(shoppingCartItemVM);
        }

        public ShoppingsCartFoodsItemsVM ViewModel()
        {
            var id = int.Parse(User.Identity.Name);
            
            ShoppingsCartFoodsItemsVM shoppingCartItemVM;
            using (var session = NHibernateHelperCore.OpenSession())
            {
                var items = session.Query<ShoppingCartFoodItem>()
                                   .Where(a => a.User.Id == id)
                                   .Select(a => new ShoppingCartFoodItemVM()
                                                {
                                                        Id = a.Id,
                                                        Count = a.Count,
                                                        UserId = a.User.Id,
                                                        UserName = a.User.Name,
                                                        Price = a.FoodItem.Price,
                                                        FoodItemId = a.FoodItem.Id,
                                                        FoodName = a.FoodItem.Name
                                                }).ToList();


                shoppingCartItemVM = new ShoppingsCartFoodsItemsVM()
                                     {
                                             ShoppingCartFoodItem = items,
                                             Admin = User.IsInRole(NHibernateHelperCore.RoleAdmin),
                                             AllCountFoodsItems = items.Sum(a => a.Count)
                                     };
            }

            return shoppingCartItemVM;
        }

        [HttpGet]
        public IActionResult AddFoodItemToShoppingCart(int foodItemId)
        {
            var id = int.Parse(User.Identity.Name);
            var foodItem = NHibernateHelperCore.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemId);
            var user = NHibernateHelperCore.GetSingleOrDefault<User>(a => a.Id == id);

            var shoppingCartFoodItem = NHibernateHelperCore.GetSingleOrDefault<ShoppingCartFoodItem>(a => a.FoodItem.Id == foodItem.Id && a.User.Id == user.Id) ??
                                       new ShoppingCartFoodItem
                                       {
                                               FoodItem = foodItem,
                                               User = user
                                       };

            shoppingCartFoodItem.Count += 1;
            NHibernateHelperCore.SaveOrUpdate(shoppingCartFoodItem);

            return Ok();
        }

        public IActionResult RemoveItemToShoppingCart(int foodItemId)
        {
            var id = int.Parse(User.Identity.Name);
            var foodItem = NHibernateHelperCore.GetSingleOrDefault<FoodItem>(a => a.Id == foodItemId);
            var user = NHibernateHelperCore.GetSingleOrDefault<User>(a => a.Id == id);
            if (foodItem == null || user == null)
                return BadRequest();

            var shoppingCartFoodItem = NHibernateHelperCore.GetSingleOrDefault<ShoppingCartFoodItem>(a => a.FoodItem.Id == foodItem.Id && a.User.Id == user.Id);
            if (shoppingCartFoodItem.Count == 1)
                return DeleteItemToShoppingCart(shoppingCartFoodItem.Id);

            shoppingCartFoodItem.Count -= 1;

            NHibernateHelperCore.SaveOrUpdate(shoppingCartFoodItem);

            return Ok();
        }

        public IActionResult DeleteItemToShoppingCart(int shoppingCartFoodItemId)
        {
            var shoppingCartFoodItem = NHibernateHelperCore.GetSingleOrDefault<ShoppingCartFoodItem>(a => a.Id == shoppingCartFoodItemId);
            NHibernateHelperCore.DeleteEntities(shoppingCartFoodItem);
            return Ok();
        }
    }
}