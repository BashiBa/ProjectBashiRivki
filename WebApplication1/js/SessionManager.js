var listCart = [];

function hellow(item)
{
    alert("hellow");
}

function addToSession(id, url, name, type, color, price, manufacturerID, manufacturerName) { //id, url, name, type, color, price, manufacturerID, manufacturerName
   
    //$.post('/Purchase/Cart',
    //       { key: "cart", id: id, name: name, type: type, color: color, price: price, manufacturerID: manufacturerID, manufacturerName: manufacturerName}, function (data) {
    //           alert("Success " + data.success);
    //       });
    $.post('/Purchase/AddToSessionCart',
           { key: "cart", id: id, name: name, type: type, color: color, price: price, manufacturerID: manufacturerID, manufacturerName: manufacturerName },
           function (data) {
               if (data == true)
               {
                   alert("Success adding to cart !");
               }
               else
               {
                   alert("Adding to cart failed");
               }
                              
                          });
    //listCart = $.session.get("rivki");
    //listCart.push({ id: id, URLImage: url, Name: name, Type: type, Color: color, Price: price, Manufacturer: manufacturer });
    //$.session.set("cart", listCart);
};

function removeSession() {

        $.post('/Purchase/ClearSession');
}

//function testsession() {
//    $.post('/Purchase/SetVariable',
//           { key: "TestKey", value: 'Test' }, function (data) {
//               alert("Success " + data.success);
//           });
//}







