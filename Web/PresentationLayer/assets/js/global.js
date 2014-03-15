var stationeries_list_url = "/api/api.aspx/get_all_stationaries";


function loadAllStationaries(url_for_id) {
    $.ajax(
        {
            type: "POST",
            url: window.stationeries_list_url,
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var jsonObj = $.parseJSON(msg.d);
                // alert("CHECK: " + jsonObj);
                var stationaryJSONArray = [];
                for (var x = 0; x < jsonObj.length; x++) {
                    //stationaryJSONArray.push({ id: jsonObj[x].Id, label: jsonObj[x].StationaryName });
                    stationaryJSONArray.push({ id: jsonObj[x].id, label: jsonObj[x].stationery_name });
                }
                $(".stationeries_autocomplete").autocomplete({
                    source: stationaryJSONArray,
                    select: function (event, ui) {
                        window.location = url_for_id + "?id=" + ui.item.id;
                        //alert("Stationary Id:" + ui.item.id);
                        //$('#stationaryId').text(ui.item.id);
                    }
                });

            }
        }
    );
    } //loadAllStationaries

    function loadAllStationariesAndPutIntoHiddenField(hidden_field_id) {
        $.ajax(
        {
            type: "POST",
            url: window.stationeries_list_url,
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            async: true,
            cache: false,
            success: function (msg) {
                var jsonObj = $.parseJSON(msg.d);
                // alert("CHECK: " + jsonObj);
                var stationaryJSONArray = [];
                for (var x = 0; x < jsonObj.length; x++) {
                    //stationaryJSONArray.push({ id: jsonObj[x].Id, label: jsonObj[x].StationaryName });
                    stationaryJSONArray.push({ id: jsonObj[x].id, label: jsonObj[x].stationery_name });
                }
                $(".stationeries_autocomplete").autocomplete({
                    source: stationaryJSONArray,
                    select: function (event, ui) {
                        $(hidden_field_id).val(ui.item.id);
                        //alert("Stationary Id:" + ui.item.id);
                        //$('#stationaryId').text(ui.item.id);
                    }
                });

            }
        }
    );
    } //loadAllStationaries