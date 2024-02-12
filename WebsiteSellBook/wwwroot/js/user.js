
var dataTable;
$(document).ready(() => {
    loadDataTable();
})

function loadDataTable() {
    dataTable = $("#tblUserList").DataTable({
        "ajax": {
            url: '/admin/user/getall'
        },
        "columns": [
            { data: 'name', "Width": "10%" },
            { data: 'email', "Width": "20%" },
            { data: 'phoneNumber', "Width": "20%" },
            { data: 'company.companyName', "Width": "15%" },
            { data: 'role', "Width": "10%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `<div class="text-center">
                                     <a onclick=LockUnlock('${data.id}') class="btn btn-danger">
                                        <i class="bi bi-lock-fill" style="height:30px; cursor:pointer"></i> Lock
                                       </a>
                                    
                                        <a class="btn btn-danger" href="/Admin/User/Permissions?userId=${data.id}">
                                        <i class="bi bi-pencil-square" style="height:30px; cursor:pointer"></i> Permission
                                       </a>
			                    </div>`
                    } else {
                        return `<div class="text-center">
                                      <a onclick=LockUnlock('${data.id}') class="btn btn-success">
                                        <i class="bi bi-unlock-fill" style="height:30px; cursor:pointer"></i> Unlock
                                       </a>
                                        <a class="btn btn-danger" href="/Admin/User/Permissions?userId=${data.id}">
                                        <i class="bi bi-pencil-square" style="height:30px; cursor:pointer"></i> Permission
                                       </a>
			                    </div>`
                    }

                }, "Width": "25%"
            }
        ],

    });
}


function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success == true) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}