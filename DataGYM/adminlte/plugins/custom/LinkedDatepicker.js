$(function () {
    $("#txtFrom").datepicker({
        numberOfMonths: 1,
        dateFormat: "dd-M-yy",
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() + 1);
            $("#txtTo").datepicker("option", "minDate", dt);
        }
    });
    $("#txtTo").datepicker({
        numberOfMonths: 1,
        dateFormat: "dd-M-yy",
        onSelect: function (selected) {
            var dt = new Date(selected);
            dt.setDate(dt.getDate() - 1);
            $("#txtFrom").datepicker("option", "maxDate", dt);
        }
    });
});