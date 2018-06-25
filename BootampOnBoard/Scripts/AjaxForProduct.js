//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "/Products/GetProducts",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Price + '</td>';
                html += '<td><button type="button" class="btn btn-warning" href="#" onclick="return getbyID(' + item.Id + ')">Edit</button></td>';
                html += '<td><button type="button" class="btn btn-danger" href="#" onclick="Delele(' + item.Id + ')">Delete</button></td>';
                html += '</tr>';
            });
            $('.tbody').html(html);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Add Data Function   
function Add() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}

    var productObj = {
        ProductId: $('#ProductId').val(),
        ProductName: $('#Name').val(),
        ProductPrice: $('#Price').val()
    };
    $.ajax({
        url: "/Products/Create",
        data: JSON.stringify(productObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Function for getting the Data Based upon Product ID  
function getbyID(ProductId) {
    var url = "Products/GetProduct?ProductId" + ProductId;
    $('#Name').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Products/GetProduct?ProductId=" + ProductId,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            var obj = JSON.parse(result);
            $('#ProductId').val(obj.ProductId);
            $('#Name').val(obj.ProductName);
            $('#Price').val(obj.ProductPrice);

            $('#myModal').modal('show');
            $('#btnUpdate').show();
            $('#btnAdd').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

//function for updating product's record  
function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var productObj = {
        ProductId: $('#ProductId').val(),
        ProductName: $('#Name').val(),
        ProductPrice: $('#Price').val()
    };
    $.ajax({
        url: "/Products/Update",
        data: JSON.stringify(productObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#ProductId').val("");
            $('#Name').val("");
            $('#Price').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting product's record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Product?");
    if (ans) {
        $.ajax({
            url: "/Products/Delete?ProductId=" + ID,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });
    }
}

//Function for clearing the textboxes  
function clearTextBox() {
    $('#ProductId').val("");
    $('#Name').val("");
    $('#Price').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Price').css('border-color', 'lightgrey');
}
//Valdidation using jquery  
function validate() {
    var isValid = true;

    if ($('#Name').val().trim() == "") {
        $('#Name').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Name').css('border-color', 'lightgrey');
    }
    if ($('#Price').val().trim() == "") {
        $('#Price').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Price').css('border-color', 'lightgrey');
    }
    return isValid;
}  