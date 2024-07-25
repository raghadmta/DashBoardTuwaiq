function AanewItem() {
    $('#NewProduct').modal('show');
}
function AanewItemDetial() {
    $('#NewProductDetails').modal('show');
}
function AanewItemDamagedproducts() {
    $('#NewDamagedproducts').modal('show');
}



var gid;
var deleteUrl;
var returnUrl;
function ShowUpdateProductDetailsModal(productId) {
     $.ajax({
        url: '/Home/GetProductDetailsfor',
        type: 'GET',
        data: { id: productId },
        success: function (product) {
            if (product) {
                $('#UpdateProductDetails input[name="Name"]').val(product.name);
                $('#UpdateProductDetails input[name="description"]').val(product.description);
                $('#UpdateProductDetails input[name="Price"]').val(product.price);
                $('#UpdateProductDetails input[name="Qty"]').val(product.qty);
                $('#UpdateProductDetails input[name="Color"]').val(product.color);
                $('#UpdateProductDetails input[name="Id"]').val(product.id);
                $('#UpdateProductDetails select[name="Products_Id"]').val(product.products_Id).attr('disabled', true);
                $('#UpdateProductDetails').modal('show');
            } else {
                alert("Product not found.");
            }
        },
        error: function () {
            alert("An error occurred while fetching product details.");
        }
    });
} function ShowUpdateDamagedproductsModal(productId) {
    console.log(productId);
    $.ajax({
        url: '/Home/GetDamagedproducts',
        type: 'GET',
        data: { id: productId },
        success: function (product) {
            if (product) {
                $('#UpdateDamagedproducts input[name="Qty"]').val(product.qty);
                $('#UpdateDamagedproducts select[name="ProductId"]').val(product.productId).attr('disabled', true);
                $('#UpdateDamagedproducts input[name="Id"]').val(product.id);
                $('#UpdateDamagedproducts').modal('show');
            } else {
                alert("Product not found.");
            }
        },
        error: function () {
            alert("An error occurred while fetching product details.");
        }
    });
}

 

// Show the modal for updating a product
function ShowUpdateProductModal(productId) {
$.ajax({
    url: '/Home/GetProductDetails',  
    type: 'GET',
    data: { id: productId },
    success: function (product) {
        if (product) {
            $('#UpdateProduct input[name="Name"]').val(product.name);
            $('#UpdateProduct input[name="description"]').val(product.description);
            $('#UpdateProduct input[name="Id"]').val(productId);

             $('#UpdateProduct').modal('show');
        } else {
            alert("Product not found.");
        }
    },
    error: function () {
        alert("An error occurred while fetching product details.");
    }
});
}





// Show the modal for deleting product


function ShowDelProductModal(id) {
    gid = id;
    deleteUrl = '/Home/Delete';
    returnUrl = '/Home/AddNewItem';
    $('#confirmProduct').modal('show');
}

function ShowDelProductDetailsModal(id) {
    gid = id;
    deleteUrl = '/Home/DeleteProductDetails';
    returnUrl = '/Home/ProductsDetails';

    $('#confirmProductDetails').modal('show');
}
function ShowDelDamagedproducts(id) {
    gid = id;
    deleteUrl = '/Home/DeleteDamagedproducts';
    returnUrl = '/Home/Damagedproducts';

    $('#confirmDamagedproducts').modal('show');
}
function ConfirmDelete() {
    $.ajax({
        url: deleteUrl,
        type: 'POST',
        data: { record_no: gid },
        success: function (result) {
            window.location.href = returnUrl;
        },

    });
}



