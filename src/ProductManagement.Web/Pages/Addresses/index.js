$(function () {
    var l = abp.localization.getResource("ProductManagement");
	
	var addressService = window.productManagement.addresses.addresses;
	
	
    var createModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Addresses/CreateModal",
        scriptUrl: "/Pages/Addresses/createModal.js",
        modalClass: "addressCreate"
    });

	var editModal = new abp.ModalManager({
        viewUrl: abp.appPath + "Addresses/EditModal",
        scriptUrl: "/Pages/Addresses/editModal.js",
        modalClass: "addressEdit"
    });

	var getFilter = function() {
        return {
            filterText: $("#FilterText").val(),
            cIty: $("#CItyFilter").val(),
			state: $("#StateFilter").val(),
			postalCodeMin: $("#PostalCodeFilterMin").val(),
			postalCodeMax: $("#PostalCodeFilterMax").val(),
			country: $("#CountryFilter").val(),
			userId: $("#UserIdFilter").val(),
			streetAddress: $("#StreetAddressFilter").val()
        };
    };

    var dataTable = $("#AddressesTable").DataTable(abp.libs.datatables.normalizeConfiguration({
        processing: true,
        serverSide: true,
        paging: true,
        searching: false,
        scrollX: true,
        autoWidth: true,
        scrollCollapse: true,
        order: [[1, "asc"]],
        ajax: abp.libs.datatables.createAjax(addressService.getList, getFilter),
        columnDefs: [
            {
                rowAction: {
                    items:
                        [
                            {
                                text: l("Edit"),
                                visible: abp.auth.isGranted('ProductManagement.Addresses.Edit'),
                                action: function (data) {
                                    editModal.open({
                                     id: data.record.id
                                     });
                                }
                            },
                            {
                                text: l("Delete"),
                                visible: abp.auth.isGranted('ProductManagement.Addresses.Delete'),
                                confirmMessage: function () {
                                    return l("DeleteConfirmationMessage");
                                },
                                action: function (data) {
                                    addressService.delete(data.record.id)
                                        .then(function () {
                                            abp.notify.info(l("SuccessfullyDeleted"));
                                            dataTable.ajax.reload();
                                        });
                                }
                            }
                        ]
                }
            },
			{ data: "cIty" },
			{ data: "state" },
			{ data: "postalCode" },
            {
                data: "country",
                render: function (country) {
                    if (country === undefined ||
                        country === null) {
                        return "";
                    }

                    var localizationKey = "Enum:Country." + country;
                    var localized = l(localizationKey);

                    if (localized === localizationKey) {
                        abp.log.warn("No localization found for " + localizationKey);
                        return "";
                    }

                    return localized;
                }
            },
			{ data: "userId" },
			{ data: "streetAddress" }
        ]
    }));

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $("#NewAddressButton").click(function (e) {
        e.preventDefault();
        createModal.open();
    });

	$("#SearchForm").submit(function (e) {
        e.preventDefault();
        dataTable.ajax.reload();
    });

    $("#ExportToExcelButton").click(function (e) {
        e.preventDefault();

        addressService.getDownloadToken().then(
            function(result){
                    var input = getFilter();
                    var url =  abp.appPath + 'api/app/addresses/as-excel-file' + 
                        abp.utils.buildQueryString([
                            { name: 'downloadToken', value: result.token },
                            { name: 'filterText', value: input.filterText }, 
                            { name: 'cIty', value: input.cIty }, 
                            { name: 'state', value: input.state },
                            { name: 'postalCodeMin', value: input.postalCodeMin },
                            { name: 'postalCodeMax', value: input.postalCodeMax }, 
                            { name: 'country', value: input.country }, 
                            { name: 'userId', value: input.userId }, 
                            { name: 'streetAddress', value: input.streetAddress }
                            ]);
                            
                    var downloadWindow = window.open(url, '_blank');
                    downloadWindow.focus();
            }
        )
    });

    $('#AdvancedFilterSectionToggler').on('click', function (e) {
        $('#AdvancedFilterSection').toggle();
    });

    $('#AdvancedFilterSection').on('keypress', function (e) {
        if (e.which === 13) {
            dataTable.ajax.reload();
        }
    });

    $('#AdvancedFilterSection select').change(function() {
        dataTable.ajax.reload();
    });
    
    
});
