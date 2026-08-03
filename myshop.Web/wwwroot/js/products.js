$(document).ready(function () {

    $("#mytable").DataTable({
        ajax: {
            url: "/Product/GetData",
            type: "GET",
            dataSrc: "data"
        },
        columns: [
            { data: "name" },
            { data: "description" },
            { data: "price" },
            { data: "categoryName" },
            {
                data: "id",
                render: function (id) {
                    return `
                        <a href="/Product/Edit/${id}" class="btn btn-outline-primary btn-sm me-2">
                            <i class="fa-solid fa-pen"></i> Edit
                        </a>

                        <a href="/Product/Delete/${id}" class="btn btn-outline-danger btn-sm">
                            <i class="fa-solid fa-trash"></i> Delete
                        </a>
                        
                    `;
                }
            }
        ],
        autoWidth: false,
        scrollX: true
    });

});