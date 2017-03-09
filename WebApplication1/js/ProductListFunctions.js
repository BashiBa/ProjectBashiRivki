
var url = document.location.protocol + "//" + document.location.host;

       $(document).ready(function() { 
         
           try {
               $.ajax({
                   type: "GET",
                   dataType: "json",
                   url: url + '/Manufacturer/GetManufactures',
                   success: function (data) {
                       $.each(data, function (i, item) {
                           var option = document.createElement('option');
                           option.value = item.ManufacturerModelID;
                           option.innerHTML = item.Name;
                           $("#manufactureFilter").append(option);

                       });

                   },
                   error: function (xhr, st, err) {
                       console.log("showNewEvent ajax failed");

                   }
               });
           }
           catch (e) {
               writeToLogWindow('', 'page.productlist.js', 'manufactures');
           }

       });



       function  serach()

       {
          
         
           var pricefrom = $("#priceFromFilter").val();//val?
           var priceTo = $("#priceToFilter").val();//val??
           var color=  $("#colorFilter option:selected" ).text();
           var manufacturer=  $("#manufactureFilter option:selected" ).text();
          // alert(manufacturer)
        
           try {
               $.ajax({
                   type: "GET",
                   dataType: "json",  
                   url: url + '/Product/Search?pricefrom='+pricefrom+'&priceTo='+priceTo+'&color='+color+'&manufacturer='+manufacturer,
                   success: function (data) {
                       $('#gridProduct').empty();
                       $.each(data, function (i, item) {
            


                           //var content = $('<div></div>').attr("class", "col-xs-12");
                           //content.append($('<div></div>').attr("class", "product-col list clearfix"));
                           //content.append($('<div></div>').attr("class", "image"));

                           var html =   '<div class="col-xs-12">'+
                            '<div class="product-col list clearfix">'+
                                '<div class="image">'+
                                 '   <img src='+item.URLImage+' alt="product" class="img-responsive" />'+
                             '   </div>'+
                              '  <div class="caption">'+
                         '           <h4><a href="productDetail">'+item.Name+'</a></h4>'+
                                   ' <!--@Html.ActionLink("Details", "productDetail", new { id = item.ProductModelID })-->'+
                                   ' <h4>'+item.Type+'</h4>'+
                                    '<h4>'+item.Color+'</h4>'+
                                   ' <div class="description">'+
                                    
                                      '  buy this now!'+
                                   ' </div>'+

                                    '<div class="price">'+

                                        '<span class="price-new">'+ item.Price+'</span>'+
                              
                                    '</div>'+
                                  '  <div class="cart-button button-group">'+
                                       ' <button type="button" class="btn btn-cart">'+
                                            //<i class="fa fa-shopping-cart hidden-sm hidden-xs"></i>
                                          '  Add to Cart'+
                                        '</button>'+
                                        '<button type="button" title="Wishlist" class="btn btn-wishlist">'+
                                            //<i class="fa fa-heart"></i>
                                        '</button>'+
                                       ' <button type="button" title="Compare" class="btn btn-compare">'+
                                            //<i class="fa fa-bar-chart-o"></i>
                                       ' </button>'+
                                 '   </div></div></div></div>'
                //          ' Page (Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber) of @Model.PageCount'+

                //'Html.PagedListPager(Model, page => Url.Action("ProductsList",new { page, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter }))'
             
                          
                           $('#gridProduct').append(html);

                      
                      });

                   },
                   error: function (xhr, st, err) {
                       console.log("showNewEvent ajax failed");

                   }
               });
           }
           catch (e) {
               writeToLogWindow('', 'page.productlist.js', 'manufactures');
           }

       }

       function serachGrid() {


           var pricefrom = $("#priceFromFilter").val();//val?
           var priceTo = $("#priceToFilter").val();//val??
           var color = $("#colorFilter option:selected").text();
           var manufacturer = $("#manufactureFilter option:selected").text();
           // alert(manufacturer)

           try {
               $.ajax({
                   type: "GET",
                   dataType: "json",
                   url: url + '/Product/Search?pricefrom=' + pricefrom + '&priceTo=' + priceTo + '&color=' + color + '&manufacturer=' + manufacturer,
                   success: function (data) {
                       $('#pgrid').empty();
                       $('#gridProduct').empty(); 
                       $.each(data, function (i, item) {



                           //var content = $('<div></div>').attr("class", "col-xs-12");
                           //content.append($('<div></div>').attr("class", "product-col list clearfix"));
                           //content.append($('<div></div>').attr("class", "image"));

                           var html = '<div class="col-md-4 col-sm-6">' +
                            '<div class="product-col">' +
                                '<div class="image">' +
                                 '   <img src=' + item.URLImage + ' alt="product" class="img-responsive" />' +
                             '   </div>' +
                              '  <div class="caption">' +
                         '           <h4><a href="productDetail">' + item.Name + '</a></h4>' +
                                   ' <!--@Html.ActionLink("Details", "productDetail", new { id = item.ProductModelID })-->' +
                                   ' <h4>' + item.Type + '</h4>' +
                                    '<h4>' + item.Color + '</h4>' +
                                   ' <div class="description">' +

                                      '  buy this now!' +
                                   ' </div>' +

                                    '<div class="price">' +

                                        '<span class="price-new">' + item.Price + '</span>' +

                                    '</div>' +
                                  '  <div class="cart-button button-group">' +
                                       ' <button type="button" class="btn btn-cart">' +
                                            //<i class="fa fa-shopping-cart hidden-sm hidden-xs"></i>
                                          '  Add to Cart' +
                                        '</button>' +
                                     
                                 '   </div></div></div></div>'
                           //          ' Page (Model.PageCount < Model.PageNumber ? 0 : Model.PageNumber) of @Model.PageCount'+

                           //'Html.PagedListPager(Model, page => Url.Action("ProductsList",new { page, sortOrder = ViewBag.CurrentSort, currentFilter = ViewBag.CurrentFilter }))'


                           $('#gridProduct').append(html);


                       });

                   },
                   error: function (xhr, st, err) {
                       console.log("showNewEvent ajax failed");

                   }
               });
           }
           catch (e) {
               writeToLogWindow('', 'page.productlist.js', 'manufactures');
           }

       }
