var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#myTable').DataTable({
        "ajax": {
            "url":"/Admin/Product/GetAll"
        },
        "columns": [
            { "data": "title", "width": "15%" },
            { "data": "isbn", "width": "15%" },
            { "data": "price", "width": "15%" },
            { "data": "author", "width": "15%" },
            { "data": "category.name", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return ` 
                         <div>
                          <a  href="/Admin/Product/Upsert?id=${data}" class="btn btn-secondary text-decoration-none">
                            <i class="bi bi-pencil-square"></i></a>
                        <a href="/Admin/Product/Delete?id=${data}"  class="btn btn-danger text-decoration-none">
                            <i class="bi bi-trash-fill"></i></a>
                          </div>
                           `
                },
                "width": "15%"
            }
        ]
    });
}