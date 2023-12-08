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
						<a  onClick=Delete('/admin/company/deletecompany/${data}') class="btn btn-primary"><i class="bi bi-trash" style="height:30px; cursor:pointer"></i> Delete</a>
                    </div>`
                }, "Width": "25%"
            }
        ],

    });
}