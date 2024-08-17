const CreateProductForm = $("#CreateProductForm")[0];

$.get(url + "Product/GetCreateForm", (data) => {
    CreateProductForm.innerHTML = data;
})