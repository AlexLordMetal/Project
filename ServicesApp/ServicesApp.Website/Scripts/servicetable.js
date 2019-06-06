$(document).ready(function () {
    $("#search-button").click(function (e) {
        loadServicesTableWithParams()
    });
    $("#Search").keypress(function (e) {
        if (e.keyCode == "13") {
            loadServicesTableWithParams()
        }
    });
    $("select#SortAscending").change(function (e) {
        loadServicesTableWithParams()
    });
    $("select#Category").change(function (e) {
        loadServicesTableWithParams()
    });
    $("select#ItemsPerPage").change(function (e) {
        loadServicesTableWithParams()
    });
    loadServicesTableWithParams();
});

var loadServicesTableWithParams = function (page) {
    var options = {};

    options.search = $("input#Search").val();
    options.orderby = $("#SortAscending :selected").val();
    options.categoryid = $("#Category :selected").val();
    options.itemsperpage = $("#ItemsPerPage :selected").val();
    if (page == null) {
        options.page = 1;
    }
    else {
        options.page = page;
    }

    loadServicesTable($("div#action-url").data("url"), options);
}

var loadServicesTable = function (link, options) {
    var url = new URL(link);

    url.searchParams.set('Search', options.search)
    url.searchParams.set('CategoryId', options.categoryid)
    url.searchParams.set('SortAscending', options.orderby)
    url.searchParams.set('ItemsPerPage', options.itemsperpage)
    url.searchParams.set('Page', options.page)

    $('#table-container').load(url.href, function () {
        $(".pagination :not(.active) a").click(function (e) {
            loadServicesTableWithParams(e.target.innerText)
        });
    });
}