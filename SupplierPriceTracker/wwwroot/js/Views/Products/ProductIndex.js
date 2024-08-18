const CreateProductForm = $("#CreateProductForm")[0];
var SubmitProductInput;
$.get(url + "Product/GetCreateForm", (data) => {
    SetupForm(data);
})

function SetupForm(data) {
    CreateProductForm.innerHTML = data;
    SubmitProductInput = $("#SubmitProductInput")[0];
    SubmitProductInput.addEventListener("click", () => { SubmitProduct() });
}

function SubmitProduct() {
    let category = $("#CategoryInput")[0].value;
    let name = $("#NameInput")[0].value;
    let measureUnit = $("#MeasureUnitInput")[0].value;

    $.post(url + "product", { name: name, ProductCategoryId: category, MeasureUnitId: measureUnit })
        .done(function (data) {
            location.reload();
        })
        .fail(function (data) {
            SetupForm(data.responseText);
        });
}

function SearchProduct(value) {}