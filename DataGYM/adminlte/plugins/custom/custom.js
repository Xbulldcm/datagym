$(document).ready(function () {
    var events = [];
    $.ajax({
        type: "GET",
        url: "/Calendario/GetEvents",
        success: function (data) {
            $.each(data, function (i, v) {
                events.push({
                    requestId: v.IDEvento,
                    user: v.nombre_completo,
                    title: v.nombre_completo,
                    asunto: v.Asunto,
                    description: v.Descripcion,
                    start: moment(v.Inicio),
                    end: v.Fin != null ? moment(v.Fin) : null,
                    color: v.Color,
                    allDay: v.EsTodoDia
                });
            })

            GenerateCalender(events);
        },
        error: function (error) {
            alert('Error al cargar el calendario');
        }
    })

    function GenerateCalender(events) {
        $('#calender').fullCalendar('destroy');
        $('#calender').fullCalendar({
            buttonText: {
                today: "Hoy",
                month: "Mes",
                week: "Semana",
                day: "Día"
            },
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Jue', 'Vie', 'Sáb'],
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            contentHeight: 400,
            defaultDate: new Date(),
            timeFormat: 'h(:mm)a',
            header: {
                left: 'prev,next today',
                center: 'user',
                right: 'month,basicWeek,basicDay'
            },
            eventLimit: true,
            eventColor: '#378006',
            events: events,
            eventClick: function (calEvent, jsEvent, view) {
                $('#myModal #eventTitle1').text(calEvent.user);
                var $description = $('<div/>');
                $description.append($('<p/>').html('<b>Solicitante: </b>' + calEvent.user));
                $description.append($('<p/>').html('<b>Asunto: </b>' + calEvent.asunto));
                $description.append($('<p/>').html('<b>Inicio: </b>' + calEvent.start.format("DD-MMM-YYYY HH:mm a")));
                if (calEvent.end != null) {
                    $description.append($('<p/>').html('<b>Fin: </b>' + calEvent.end.format("DD-MMM-YYYY HH:mm a")));
                }
                $description.append($('<p/>').html('<b>Descripcion: </b>' + calEvent.description));
                $description.append($('<p/>').html('<center><button class="btn btn-default" type="button" style="height:100%;width:100%;background-color:#4c75af;color:white" onclick="location.href=' + '\'' + '/Calendario/Edit/' + calEvent.requestId +'\''+ '">Aprobar&nbsp;<i class="fas fa-edit"></i></button></center>'));
                $('#myModal #pDetails').empty().html($description);

                $('#myModal').modal();
            }
        })
    }
})

$(document).ready(function () {
    $('.selectpicker').selectpicker({
        liveSearch: true,
        showSubtext: true
    });
});

