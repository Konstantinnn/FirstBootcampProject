//Load Data in Table when documents is ready  
$(document).ready(function () {
    loadData();
});

//Load Data function  
function loadData() {
    $.ajax({
        url: "/Customers/GetCustomers",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            var html = '';
            $.each(result, function (key, item) {
                html += '<tr>';
                html += '<td>' + item.Id + '</td>';
                html += '<td>' + item.Name + '</td>';
                html += '<td>' + item.Address + '</td>';
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

    var customerObj = {
        CustomerId: $('#CustomerId').val(),
        CustomerName: $('#Name').val(),
        CustomerAddress: $('#Address').val()
    };
    $.ajax({
        url: "/Customers/Create",
        data: JSON.stringify(customerObj),
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

//Function for getting the Data Based upon Customer ID  
function getbyID(CustomerId) {
    $('#Name').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
    $.ajax({
        url: "/Customers/GetCustomer?CustomerId=" + CustomerId,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            alert(result);
            $('#CustomerId').val(result.CustomerId);
            $('#Name').val(result.CustomerName);
            $('#Address').val(result.CustomerAddress);

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

//function for updating customer's record  
function Update() {
    //var res = validate();
    //if (res == false) {
    //    return false;
    //}
    var customerObj = {
        CustomerId: $('#CustomerId').val(),
        CustomerName: $('#Name').val(),
        CustomerAddress: $('#Address').val()
    };
    $.ajax({
        url: "/Customers/Update",
        data: JSON.stringify(customerObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            loadData();
            $('#myModal').modal('hide');
            $('#CustomerId').val("");
            $('#Name').val("");
            $('#Address').val("");
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//function for deleting employee's record  
function Delele(ID) {
    var ans = confirm("Are you sure you want to delete this Customer?");
    if (ans) {
        $.ajax({
            url: "/Customers/Delete?CustomerId=" + ID,
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
    $('#CustomerId').val("");
    $('#Name').val("");
    $('#Address').val("");
    $('#btnUpdate').hide();
    $('#btnAdd').show();
    $('#Name').css('border-color', 'lightgrey');
    $('#Address').css('border-color', 'lightgrey');
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
    if ($('#Address').val().trim() == "") {
        $('#Address').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#Address').css('border-color', 'lightgrey');
    }
    return isValid;
}  
