var dataTable;
$(document).ready(() => {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $('#tblProductList').DataTable({
        "ajax": {
            url: '/admin/product/getallproduct'
        },
        "columns": [
            { data: 'title', "Width": "25%" },
            { data: 'isbn', "Width": "15%" },
            { data: 'price', "Width": "10%" },
            { data: 'author', "Width": "20%" },
            { data: 'category.category_Name', "Width": "15%" },
            {
                data: 'product_Id',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/product/createandupdateproduct?id=${data}" class="btn btn-success"><i class="bi bi-pencil-square" style="height:30px; cursor:pointer"></i>  Edit</a>
						&nbsp;
						<a  onClick=Delete('/admin/product/deleteproduct/${data}') class="btn btn-primary"><i class="bi bi-trash" style="height:30px; cursor:pointer"></i> Delete</a>
                    </div>`
                }, "Width": "25%"
            }

        ],
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    Swal.fire({
                        title: data.title,
                        text: data.message,
                        icon: data.icon
                    });
                }
            })

        }
    });
}