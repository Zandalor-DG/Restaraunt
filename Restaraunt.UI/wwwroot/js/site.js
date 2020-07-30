// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function() {
    updateShoppingCartCount();
});

let delayTimer;

function searchInAllItems(value) {
    clearTimeout(delayTimer);
    delayTimer = setTimeout(function() {
            $.ajax({
                    url : "/Home/SearchAllEntities",
                    data : { txt : value },
                    success : function(content) {
                        $(".table").remove();

                        let tableHead = `<table class="table"><thead id="tableHeader">` +
                            `<tr class="searchAllItems"><th>Images</th>` +
                            `<th>Название</th><th>Цена</th><th>Купить</th>`;
                        if (content.Admin) {
                            tableHead += `<th>Редактировать</th><th>Удалить</th>`;
                        }
                        tableHead += `</tr></thead>`;
                        $(".searchAllItems").after(tableHead);

                        let tableBody = `<tbody>`;
                        $("#tableHeader").after(tableBody);
                        for (let i = 0; i < content.foodsItems.length; i++) {

                            let strFoodItems = `<tr class="dataRow">` +
                                `<td class="thumbnail-img"><a href="#">` +
                                `<img alt="" class="img-fluid" src="/images/img-pro-01.jpg"></a></td>` +
                                `<td class="total-pr">${content.foodsItems[i].name}</td>` +
                                `<td class="total-pr">${content.foodsItems[i].price}</td>` +
                                `<td class="name-pr">` +
                                `<a class="btn hvr-hover" href="/ShoppingCart/AddFoodItemToShoppingCart?foodItemId=${
                                content
                                .foodsItems[i].id}">В корзину</a>`;
                            if (content.admin) {
                                strFoodItems +=
                                    `</td><td class="remove-pr"><a href="/FoodItem/CreateOrUpdateFoodItem/${
                                    content
                                    .foodsItems[i].id}">Редактировать</a></td>` +
                                    `<td class="remove-pr"><a href="/FoodItem/ConfirmDeleteFoodItem/${
                                    content.foodsItems[i].id}"><i class="fas fa-times"></i></a>`;

                            }
                            strFoodItems += `</td></tr>`;
                            $("#tableHeader").after(strFoodItems);
                        }

                        for (let j = 0; j < content.foodsItemsExtraVM.length; j++) {
                            let strFoodItemsExtra = `<tr class="dataRow">` +
                                `<td class="thumbnail-img"><a href="#">` +
                                `<img alt="" class="img-fluid" src="/images/img-pro-01.jpg"></a></td>` +
                                `<td class="total-pr">${content.foodsItemsExtraVM[j].name}</td>` +
                                `<td class="total-pr">${content.foodsItemsExtraVM[j].price}</td>` +
                                `<td class="name-pr">` +
                                `<a class="btn hvr-hover" href="/ShoppingCart/AddFoodItemToShoppingCart?foodItemExtraId=${
                                content.foodsItemsExtraVM[j].id
                                }">В корзину</a>`;
                            if (content.admin) {
                                strFoodItemsExtra +=
                                    `</td><td class="remove-pr"><a href="/FoodItem/CreateOrUpdateFoodItem/${
                                    content.foodsItemsExtraVM[j].id}">Редактировать</a></td>` +
                                    `<td class="remove-pr"><a href="/FoodItem/ConfirmDeleteFoodItem/${
                                    content.foodsItemsExtraVM[j].id}"><i class="fas fa-times"></i></a>`;

                            }
                            strFoodItemsExtra += `</td></tr>`;
                            $("#tableHeader").after(strFoodItemsExtra);
                        }

                        tableBody = `</tbody></table>`;
                        $("#tableHeader").after(tableBody);
                    }

                });

        },
        1000);
}

function AddFoodItemToShoppingCart(modelId) {

    $.ajax({
            url : "/ShoppingCart/AddFoodItemToShoppingCart",
            data : { foodItemId : modelId },
            success : function() {
                updateShoppingCartCount();
                $.ajax({
                        url : "/ShoppingCart/IndexJson",
                        success : function(value) {
                            renderShoppingCart(value);
                        }
                    });
            }
        });

};

function AddFoodItemToShoppingCartView(modelId) {

    $.ajax({
            url : "/ShoppingCart/AddFoodItemToShoppingCart",
            data : { foodItemId : modelId },
            success : function() {
                updateShoppingCartCount();
            }
        });

};

function updateShoppingCartCount() {
    $.ajax({
            url : "/ShoppingCart/IndexJson",
            success : function(content) {
                $("#shoppingCart").remove();
                let str =
                    `<span id="shoppingCart" class="badge">${content.allCountFoodsItems}</span>`;
                $("#shoppingCartView").after(str);
            }
        });
};

function RemoveItemToShoppingCart(modelId) {

    $.ajax({
            url : "/ShoppingCart/RemoveItemToShoppingCart",
            data : { foodItemId : modelId },
            success : function() {
                updateShoppingCartCount();
                $.ajax({
                        url : "/ShoppingCart/IndexJson",
                        success : function(value) {
                            renderShoppingCart(value);
                        }
                    });
            }
        });

};

function DeleteItemToShoppingCart(modelId) {

    $.ajax({
            url : "/ShoppingCart/DeleteItemToShoppingCart",
            data : { shoppingCartFoodItemId : modelId },
            success : function() {
                updateShoppingCartCount();
                $.ajax({
                        url : "/ShoppingCart/IndexJson",
                        success : function(value) {
                            renderShoppingCart(value);
                        }
                    });
            }
        });

};

function renderShoppingCart(content) {
    $(".dataRow").remove();

    for (let i = 0; i < content.shoppingCartFoodItem.length; i++) {

        let str =
            `<tr class="dataRow"><td class="thumbnail-img"><a href="#"><img alt="" class="img-fluid" src="/images/img-pro-01.jpg">` +
                `</a></td><td class="name-pr"><p>${content.shoppingCartFoodItem[i].foodName
                }</p></td><td class="name-pr">` +
                `<a class="sortOrderClass" href="#" onclick="AddFoodItemToShoppingCart('${content
                .shoppingCartFoodItem[i].foodItemId}')">` +
                `+</a></td><td class="name-pr">${content.shoppingCartFoodItem[i].count
                }</td><td class="remove-pr"><a class="sortOrderClass" href="#" onclick="RemoveItemToShoppingCart('${
                content.shoppingCartFoodItem[i].foodItemId}')">-</a>` +
                `</td><td class="remove-pr"><a class="sortOrderClass" href="#" onclick="DeleteItemToShoppingCart('${
                content.shoppingCartFoodItem[i].id}')">` +
                `<i class="fas fa-times"></i></a></td></tr>`;
        $("#tableHeader").after(str);
    }

};

function sortTableFoodCategory(sortOrder, orderId, link) {
    let orderElement = $("#" + orderId);
    let descending = orderElement.val();
    let arrowSpan = $(link).find(`span`);
    $.ajax({
            url : "Home/IndexAJAX",
            data : { sortOrder : sortOrder, descending : descending },
            success : function(content) {
                $(".dataRow").remove();

                for (let i = 0; i < content.foodsCategories.length; i++) {
                    let str = `<tr class="dataRow">` +
                        `<td class="thumbnail-img"><a href="#">` +
                        `<img alt="" class="img-fluid" src="/images/img-pro-01.jpg"></a></td>` +
                        `<td class="total-pr">${content.foodsCategories[i].name}</td>` +
                        `<td class="name-pr">` +
                        `<a href="/FoodItem/Index/${content.foodsCategories[i].id}">Посмотреть</a></td>` +
                        `<td class="name-pr">` +
                        `<a href="/FoodItemExtra/Index/${content.foodsCategories[i].id}">Допы</a>`;
                    if (content.admin) {
                        str += `</td><td class="remove-pr"><a href="/FoodCategory/CreateOrUpdateFoodCategory/${content
                            .foodsCategories[i].id}">Редактировать</a></td>` +
                            `<td class="remove-pr"><a href="/FoodCategory/ConfirmDeleteFoodCategory/${
                            content.foodsCategories[i].id}"><i class="fas fa-times"></i></a>`;

                    }
                    str += `</td></tr>`;
                    $("#tableHeader").after(str);
                }

                let orderValue = !(descending === "true");
                $(orderElement).val(orderValue);
                $(".spanArrow").text(" ");
                if (orderValue == true) {
                    $(arrowSpan).text(`▲`);
                }
                else {
                    $(arrowSpan).text(`▼`);
                }
            }

        });
}

function sortTableFoodItem(modelId, sortOrder, orderId, link) {
    let orderElement = $("#" + orderId);
    let descending = orderElement.val();
    let arrowSpan = $(link).find(`span`);
    $.ajax({
            url : "/FoodItem/IndexAJAX",
            data : { sortOrder : sortOrder, descending : descending, id : modelId },
            success : function(content) {
                $(".dataRow").remove();

                for (let i = 0; i < content.foodsItems.length; i++) {
                    let str = `<tr class="dataRow">` +
                        `<td class="thumbnail-img"><a href="#">` +
                        `<img alt="" class="img-fluid" src="/images/img-pro-01.jpg"></a></td>` +
                        `<td class="total-pr">${content.foodsItems[i].name}</td>` +
                        `<td class="total-pr">${content.foodsItems[i].price}</td>` +
                        `<td class="name-pr">` +
                        `<a class="btn hvr-hover" href="/ShoppingCart/AddFoodItemToShoppingCart?foodItemId=${content
                        .foodsItems[i]
                        .id}">В корзину</a>`;
                    if (content.admin) {
                        str += `</td><td class="remove-pr"><a href="/FoodItem/CreateOrUpdateFoodItem/${content
                            .foodsItems[i].id}">Редактировать</a></td>` +
                            `<td class="remove-pr"><a href="/FoodItem/ConfirmDeleteFoodItem/${
                            content.foodsItems[i].id}"><i class="fas fa-times"></i></a>`;

                    }
                    str += `</td></tr>`;
                    $("#tableHeader").after(str);
                }

                let orderValue = !(descending === "true");
                $(orderElement).val(orderValue);
                $(".spanArrow").text(" ");
                if (orderValue == true) {
                    $(arrowSpan).text(`▲`);
                }
                else {
                    $(arrowSpan).text(`▼`);
                }
            }
        });
}

function sortTableFoodItemExtra(modelId, sortOrder, orderId, link) {
    let orderElement = $("#" + orderId);
    let descending = orderElement.val();
    let arrowSpan = $(link).find(`span`);
    $.ajax({
            url : "/FoodItemExtra/IndexAJAX",
            data : { sortOrder : sortOrder, descending : descending, id : modelId },
            success : function(content) {
                $(".dataRow").remove();

                for (let i = 0; i < content.foodsItemsExtra.length; i++) {
                    let str = `<tr class="dataRow">` +
                        `<td class="thumbnail-img"><a href="#">` +
                        `<img alt="" class="img-fluid" src="/images/img-pro-01.jpg"></a></td>` +
                        `<td class="total-pr">${content.foodsItemsExtra[i].name}</td>` +
                        `<td class="total-pr">${content.foodsItemsExtra[i].price}</td>` +
                        `<td class="name-pr">` +
                        `<a class="btn hvr-hover" href="/ShoppingCart/AddFoodItemToShoppingCart?foodItemExtraId=${
                        content.foodsItemsExtra[i].id
                        }">В корзину</a>`;
                    if (content.admin) {
                        str += `</td><td class="remove-pr"><a href="/FoodItem/CreateOrUpdateFoodItem/${content
                            .foodsItemsExtra[i].id}">Редактировать</a></td>` +
                            `<td class="remove-pr"><a href="/FoodItem/ConfirmDeleteFoodItem/${
                            content.foodsItemsExtra[i].id}"><i class="fas fa-times"></i></a>`;

                    }
                    str += `</td></tr>`;
                    $("#tableHeader").after(str);
                }

                let orderValue = !(descending === "true");
                $(orderElement).val(orderValue);
                $(".spanArrow").text(" ");
                if (orderValue == true) {
                    $(arrowSpan).text(`▲`);
                }
                else {
                    $(arrowSpan).text(`▼`);
                }
            }

        });
}