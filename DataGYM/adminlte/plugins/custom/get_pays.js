$("body").on("click", "#btn_buscar", function () {

    //Reference the variables to the TextBoxes.
    var ejercicio_temp_EXERCISE_TYPE_ID = $("#ejercicio_temp_EXERCISE_TYPE_ID");;



    //Get the Exercise Type name that belongs to the selected ID
    var eT = $.ajax({
        type: "POST",
        url: "/Pay/GetPays",
        dataType: 'json',
        cache: false,
        async: false,
        data: { 'id': ejercicio_temp_EXERCISE_TYPE_ID.val() }
    }).responseText.replace("\"", "").replace("\"", "");

    var tBody = $("#tb_exercise > TBODY")[0];

    //Add Row.
    var row = tBody.insertRow(-1);

    //Add cells.

    var cell = $(row.insertCell(-1));
    cell.html(ejercicio_temp_EXERCISE_TYPE_ID.val());
    cell.attr("style", "display:none;")

    cell = $(row.insertCell(-1));
    cell.html(eT);

    cell = $(row.insertCell(-1));
    cell.html(ejercicio_temp_EXERCISE_DAY.val());


    cell = $(row.insertCell(-1));
    cell.html(ejercicio_temp_EXERCISE_NAME.val());


    cell = $(row.insertCell(-1));
    cell.html(ejercicio_temp_EXERCISE_REP_COUNT.val());

    cell = $(row.insertCell(-1));
    cell.html(ejercicio_temp_EXERCISE_REP_ROUND.val());

    cell = $(row.insertCell(-1));
    cell.html(ejercicio_temp_EXECISE_NOTE.val());


    cell = $(row.insertCell(-1));
    var btnRemove = $("<input />");
    btnRemove.attr("type", "button");
    btnRemove.attr("onclick", "Remove(this);");
    btnRemove.attr("style", "height:100%;width:100%;");
    btnRemove.attr("class", "btn btn-danger");
    btnRemove.val("Eliminar");
    cell.append(btnRemove);

    //Clear the TextBoxes and Selects.
    $("#ejercicio_temp_EXERCISE_TYPE_ID").val('').trigger('change')
    $("#ejercicio_temp_EXERCISE_DAY").val('').trigger('change')
    ejercicio_temp_EXERCISE_NAME.val("");
    ejercicio_temp_EXERCISE_REP_COUNT.val("");
    ejercicio_temp_EXERCISE_REP_ROUND.val("");
    ejercicio_temp_EXECISE_NOTE.val("");

});

function Remove(button) {
    //Determine the reference of the Row using the Button.
    var row = $(button).closest("TR");

    if (confirm("Seguro que desea eliminarlo?")) {
        //Get the reference of the Table.
        var table = $("#tb_exercise")[0];

        //Delete the Table row using it's Index.
        table.deleteRow(row[0].rowIndex);
    }
};

$("body").on("click", "#btn_guardar", function () {
    var routine = {};
    routine.Name_Routine = $("#Name_Routine").val();
    routine.NOTE_Routine = $("#NOTE_Routine").val();
    routine.USER_ID = $("#USER_ID").val();
    routine.IS_ACTIVE = $("#IS_ACTIVE").val();


    //Loop through the Table rows and build a JSON array.
    var exercises = new Array();
    $("#tb_exercise TBODY TR").each(function () {
        var row = $(this);
        var exercise = {};
        exercise.EXERCISE_TYPE_ID = row.find("TD").eq(0).html();
        exercise.EXERCISE_DAY = row.find("TD").eq(2).html();
        exercise.EXERCISE_NAME = row.find("TD").eq(3).html();
        exercise.EXERCISE_REP_COUNT = row.find("TD").eq(4).html();
        exercise.EXERCISE_REP_ROUND = row.find("TD").eq(5).html();
        exercise.EXECISE_NOTE = row.find("TD").eq(6).html();
        exercises.push(exercise);
    });

    //Send the JSON with data to Controller using AJAX.

    var dat = JSON.stringify(exercises);

    $.ajax({
        type: "POST",
        url: "/Routine/InsertRoutine",
        data: JSON.stringify(routine),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (r) {
            var d1 = { routine_id: r, lista_ejericios: exercises };
            var d2 = JSON.stringify(d1);
            $.ajax({
                type: "POST",
                url: "/Exercise/InsertExercises",
                data: d2,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r2) {
                    alert("La rutina se inserto correctamente con un total de " + r2 + " ejercicios");
                }
            });
        }
    });
});