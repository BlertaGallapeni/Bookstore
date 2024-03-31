var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Client/GetAll"
        },
        "columns": [
            { "data": "firstName" },
            { "data": "lastName"},
           /* { "data": "birthday", render: DataTable.render.datetime('M/D/YYYY') },*/
            { "data": "email" },
            { "data": "phoneNumber" },
            { "data": "address" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                        <a href="/Client/Edit?id=${data}"
                        class="btn btn-primary mx-2 p-1 px-2"> <i class="bi bi-pencil-square"></i></a>
                        <a onClick=Delete('/Client/Delete/${data}')
                        class="btn btn-danger mx-2 p-1 px-2"> <i class="bi bi-trash-fill"></i></a>
					</div>
                        `
                },
                "width": "5%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}

