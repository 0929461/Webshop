$(document).ready(function () {
    var x = 0;
    var s = "";

    console.log("Hello");


    var theForm = $("#theForm");
    theForm.hide();

    var buyButton = $("#buyButton");
    buyButton.on("click", function () {
        console.log("Buying Item");
    });

    var productInfo = $(".product-props li");
    productInfo.on("click", function () {
        console.log("You Clicked on " + $(this).text());
    });

    var $loginToggle = $("#loginToggle");
    var $popupForm = $(".popup-form");

    $loginToggle.on("click", function () {
        $popupForm.toggle(1000);
    });
});