var dataTable;
$(document).ready(() => {
    let url = window.location.search;
    if (url.includes("pending")) {
        loadDataTable("pending");
    } else if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    } else if (url.includes("completed")) {
        loadDataTable("completed");
    } else if (url.includes("approved")) {
        loadDataTable("approved");
    } else {
        loadDataTable("all");
    }

})

function loadDataTable(status) {
    dataTable = $('#tblOrderList').DataTable({
        "ajax": {
            url: '/admin/order/getall?status=' + status
        },
        "columns": [
            { data: 'id', "Width": "15%" },
            { data: 'name', "Width": "15%" },
            { data: 'phoneNumber', "Width": "10%" },
            { data: 'applicationUser.email', "Width": "20%" },
            { data: 'orderStatus', "Width": "20%" },
            { data: 'orderTotal', "Width": "10%" },
            {
                data: 'product_Id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/product/detail?id=${data}" class="btn btn-success"><i class="bi bi-pencil-square" style="height:30px; cursor:pointer"></i>  Detail</a>
                    </div>`
                }, "Width": "15%"
            }

        ],
    });
}
