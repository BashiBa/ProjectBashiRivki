var listCart = [];

function hellow(item) {

    alert("hellow");

}

function addToSession(productModel) { //id, url, name, type, color, price, manufacturerID, manufacturerName

    //var id = $(elem).data('assigned-id');

    var id = $(productModel).data('assigned-id');

    $.post('/Purchase/AddToSessionCart',

    { key: "cart", id: id },

    function (data) {

        alert(data.success);

        $.post('/Purchase/SumItemsInSession',

        //{ key: "cart", id: id },

        function (data) {

            $("#cart-total").text(data.count + ' items');

        });

    });

    //$.post('/Purchase/AddToSessionCart',

    // { key: "cart", id: id, name: name, type: type, color: color, price: price, manufacturerID: manufacturerID, manufacturerName: manufacturerName },

    // function (data) {

    // alert("Success " + data.success);

    // });

};

function goToCart() {

    //location.reload(true);

    window.location.reload(true);

    window.location.href = "Purchase/AddToCart";

}



function removeFromSession(productModel) {

    //$.post('/Purchase/ClearFromSession');

    var id = $(productModel).data('assigned-id');

    $.post('/Purchase/ClearFromSession',

    { key: "cart", id: id },

    function (data) {

        alert(data.success);

    });

}



function GetSumPriceInSession() {

    //$.post('/Purchase/ClearFromSession');

    //var id = $(productModel).data('assigned-id');

    $.post('/Purchase/TotalPriceInSession',

    //{ key: "cart", id: id },

    function (data) {

        alert(data.success);

        ;

    });

}

//function AssignButtonClicked(elem) {

// var id = $(elem).data('assigned-id');

//}





//function testsession() {

// $.post('/Purchase/SetVariable',

// { key: "TestKey", value: 'Test' }, function (data) {

// alert("Success " + data.success);

// });

//}

