var dataTable;
$(document).ready(() => {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#tblCompanyList").DataTable({
        "ajax": {
            url: '/admin/company/getall'
        },
        "columns": [
            { data: 'companyName', "Width": "10%" },
            { data: 'stressAddress', "Width": "20%" },
            { data: 'city', "Width": "10%" },
            { data: 'state', "Width": "10%" },
            { data: 'postalCode', "Width": "10%" },
            { data: 'phoneNumber', "Width": "15%" },
            {
                data: 'cpmpanyId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                        <a href="/admin/company/createandupdatecompany?id=${data}" class="btn btn-success"><i class="bi bi-pencil-square" style="height:30px; cursor:pointer"></i>  Edit</a>
						    &nbsp;
						    <a  onClick=Delete('/admin/company/deletecompany/${data}') class="btn btn-primary" > <i class="bi bi-trash" style="height:30px; cursor:pointer"> </i> Delete</a>
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