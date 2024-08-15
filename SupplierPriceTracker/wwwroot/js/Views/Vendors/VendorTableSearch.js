
const vendorNameSearchBar = $("#VendorNameSearchBar")[0];
const isVendorDeleted = $("#isVendorDeleted")[0];
const vendorTableBody = $('#vendorTableBody')[0];

vendorNameSearchBar.addEventListener("keyup", () => {
    SearchVendors(vendorNameSearchBar.value, isVendorDeleted.checked);
});
isVendorDeleted.addEventListener("change", () => {
    SearchVendors(vendorNameSearchBar.value, isVendorDeleted.checked);
});

function SearchVendors(vendorName, isDeleted) {
    $.get(
        "https://localhost:7021/Vendor/SearchVendor" + `?name=${vendorName}` + `&isDeleted=${isDeleted}`,
        (data) => {
            DisplaySearchResults(data);
        }
    )
}
function DisplaySearchResults(result) {
        vendorTableBody.innerHTML = "";
    result.forEach((element) => {
        vendorTableBody.innerHTML += `
            <tr>
				<td>
					${element.id}
				</td>
				<td>
					${element.name}
				</td>
			</tr>
        `;
    });
}