
    //Insert default item "Select" in dropdownlist on load
    $(document).ready(function () {
            var items = "<option value='0'>Изберете квартал</option>";
    $("#Neighbourhood").html(items);
});

//Bind City dropdownlist
        $("#city").change(function () {
            var cityName = $("#city").val();

    var url = "/RealEstates/GetNeighbourhoodsList";

            $.getJSON(url, {CityName: cityName }, function (data) {
                var item = "";
    $("#Neighbourhood").empty();
                $.each(data, function (i, Neighbourhood) {

        item += '<option>' + Neighbourhood.text + '</option>'
    });
    $("#Neighbourhood").html(item);
});
});
