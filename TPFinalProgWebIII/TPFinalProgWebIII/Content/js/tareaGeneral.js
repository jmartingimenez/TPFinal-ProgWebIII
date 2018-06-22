

function checkear()
{

    $('.filtro').change(function() {

   //     alert("Evento");

        if ($('#completas').is(":checked") && $('#nocompletas').is(":checked")) {

            $('.completada').closest('tr').show();
            $('.nocompletada').closest('tr').show();


        } else if ($('#completas').is(":checked") && $('#nocompletas').not(":checked")) {

            $('.completada').closest('tr').show();
            $('.nocompletada').closest('tr').hide();

        } else if ($('#completas').not(":checked") && $('#nocompletas').is(":checked")) {

            $('.completada').closest('tr').hide();
            $('.nocompletada').closest('tr').show();
        }
        else if ($('#completas').not(":checked") && $('#nocompletas').not(":checked")) {

            $('.nocompletada').closest('tr').hide();
            $('.completada').closest('tr').hide();

        }
    });
}


