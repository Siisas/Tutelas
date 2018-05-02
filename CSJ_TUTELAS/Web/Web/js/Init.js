/* Init DataTable */
var textoAyuda = '';
$(function () {

    if ($.fn.dataTable != undefined) {
        $.extend(true, $.fn.dataTable.defaults, {
            "bProcessing": true,
            "bdestroy": true,
            "bInfo": true,
            "start": 0,
            "order": [0, "asc"],
            "bDeferRender": true,
            "language": {
                "sProcessing": "Procesando...",
                "sLengthMenu": "Mostrar _MENU_ registros",
                "sZeroRecords": "No se encontraron resultados",
                "sEmptyTable": "Ningún dato disponible en esta tabla",
                "sInfo": "Mostrando del _START_ al _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando del 0 al 0 de 0 registros",
                "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                "sInfoPostFix": "",
                "sSearch": "Buscar:",
                "sUrl": "",
                "sInfoThousands": ",",
                "sLoadingRecords": "Cargando...",
                "oPaginate": {
                    "sFirst": "Primero",
                    "sLast": "último",
                    "sNext": "Siguiente",
                    "sPrevious": "Anterior"
                },
                "oAria": {
                    "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                    "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                }
            }
        });
    }

    if ($.datepicker != undefined) {
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '< Ant',
            nextText: 'Sig >',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'dd/mm/yy',
            firstDay: 1,
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };

        $.datepicker.setDefaults($.datepicker.regional['es']);
    }
});

$(document).ajaxStart(function () {
    if(typeof buscando != 'undefined'){
        if(!buscando)
            $.blockUI({ message: '<img src="../Content/Images/loadingblack.gif" class="load-peg" />' });
    }
    else{
        $.blockUI({ message: '<img src="../Content/Images/loadingblack.gif" class="load-peg" />' });
    }
});

$(document).ajaxStop(function () {
    if (typeof buscando != 'undefined') {
        if (!buscando)
            $.unblockUI();
    }
    else {
        $.unblockUI();
    }
        
});

$(document).ajaxError(function () {
    $.unblockUI();
});

function mostrarPanel(id) {
    if (id == '.dropdown-alerts') {
        $('.dropdown-user').hide();
    }
    else {
        $('.dropdown-alerts').hide();
    }

    $(id).slideToggle("slow");

    $(document).on("click", function () {
        $('.dropdown-user').hide();
        $('.dropdown-alerts').hide();
    });
}

function VotacionStar(cont, votar) {
    html = '';
    if (typeof cont == 'string') {
        cont = cont.replace(",", ".");
    }
    cont = parseFloat(cont);
    sw = true;
    for (i = 1; i <= 5.0; i++) {
        if (votar) {
            if (cont == 0) {
                html += '<i id="star' + i + '" class="fa fa-star-o" data="fa fa-star-o" aria-hidden="true" onmouseover="MouseOver(' + i + ', true);" onmouseout="MouseOver(' + i + ', false);" onclick="CalificarSentencia(' + i + ');"></i>';
            }
            else if (i <= cont) {
                html += '<i id="star' + i + '" class="fa fa-star" data="fa fa-star" aria-hidden="true" onmouseover="MouseOver(' + i + ', true);" onmouseout="MouseOver(' + i + ', false);" onclick="CalificarSentencia(' + i + ');"></i>';
            }
            else if ((cont > i || cont > i - 1) && sw) {
                html += '<i id="star' + i + '" class="fa fa-star-half-full" data="fa fa-star-half-full" aria-hidden="true" onmouseover="MouseOver(' + i + ', true);" onmouseout="MouseOver(' + i + ', false);" onclick="CalificarSentencia(' + i + ');"></i>';
                sw = false;
            }
            else {
                html += '<i id="star' + i + '"  class="fa fa-star-o" data="fa fa-star-o" aria-hidden="true" onmouseover="MouseOver(' + i + ', true);" onmouseout="MouseOver(' + i + ', false);" onclick="CalificarSentencia(' + i + ');"></i>';
            }
        }
        else {
            if (cont == 0) {
                html += '<i class="fa fa-star-o" aria-hidden="true"></i>';
            }
            else if (i <= cont) {
                html += '<i class="fa fa-star" aria-hidden="true"></i>';
            }
            else if ((cont > i || cont > i - 1) && sw) {
                html += '<i class="fa fa-star-half-full" aria-hidden="true"></i>';
                sw = false;
            }
            else {
                html += '<i class="fa fa-star-o" aria-hidden="true"></i>';
            }
        }
    }
    return html;
}

function MouseOver(cont, resaltar) {
    if (resaltar) {
        for (i = cont; i >= 1; i--) {
            $("#star" + i).attr("class", "fa fa-star ongoing");
        }
    }
    else {
        for (i = 1; i <= 5; i++) {
            $("#star" + i).attr("class", $("#star" + i).attr("data"));
        }
    }
}

function AbrirMensajeModal(tipo, texto, funcionAceptar, funcionCancelar) {
    $("#btnCancelarMensaje").hide();
    $("#divbtnAceptar").html('<a href="javascript:void(0);" id="btnAceptarMensaje" data-dismiss="modal" onclick="CerrarMensajeModal(); return false;" class="btn btn-default">Aceptar</a>');
    $("#divbtnCancelar").html('<a href="javascript:void(0);" id="btnCancelarMensaje" data-dismiss="modal" onclick="CerrarMensajeModal(); return false;" class="btn btn-default" style="display:none;">Cancelar</a>');
    $(".modal-header button").hide();

    var sw = false;
    if (tipo == 'info') {
        $("#HeaderMensaje").html("Informaci&oacute;n");
        $("#btnAceptarMensaje").attr("class", "btn btn-success");
        sw = true;
    }
    if (tipo == 'error') {
        $("#HeaderMensaje").html("Error");
        $("#btnAceptarMensaje").attr("class", "btn btn-danger");
        sw = true;
    }
    if (tipo == 'adver') {
        $("#HeaderMensaje").html("Advertencia");
        $("#btnAceptarMensaje").attr("class", "btn btn-warning");
        sw = true;
    }
    if (tipo == 'confir') {
        $("#HeaderMensaje").html("Confirmaci&oacute;n");
        $("#btnCancelarMensaje").show();
        sw = true;
    }

    if (!sw) {
        $("#HeaderMensaje").text(tipo);
        $("#btnAceptarMensaje").attr("class", "btn btn-success");
    }

    $("#BodyMensaje").html(texto);
    if (funcionAceptar != null && funcionAceptar != undefined && funcionAceptar != "") {
        id = 'btnAceptarMensaje' + Date.now();
        $("#divbtnAceptar").html('<a href="javascript:void(0);" id="' + id + '" data-dismiss="modal" class="btn btn-success">Aceptar</a>');
        $('#' + id).on("click", funcionAceptar);
    }

    if (funcionCancelar != null && funcionCancelar != undefined && funcionCancelar != "") {
        id = 'btnCancelarMensaje' + Date.now();
        $("#divbtnCancelar").html('<a href="javascript:void(0);" id="' + id + '" data-dismiss="modal" class="btn btn-warning">Cancelar</a>');
        $('#' + id).on("click", funcionCancelar);
    }

    $("#mostrarmodal").modal("show");
}

function CerrarMensajeModal() {
    $("#HeaderMensaje").html("");
    $("#btnAceptarMensaje").attr("class", "btn btn-default");
    $("#BodyMensaje").html("");
    $("#divbtnAceptar").html('<a href="javascript:void(0);" id="btnAceptarMensaje" data-dismiss="modal" onclick="CerrarMensajeModal(); return false;" class="btn btn-default">Aceptar</a>');
    $("#divbtnCancelar").html('<a href="javascript:void(0);" id="btnCancelarMensaje" data-dismiss="modal" onclick="CerrarMensajeModal(); return false;" class="btn btn-default" style="display:none;">Cancelar</a>');
    $("#mostrarmodal").modal("hide");
    $("#btnCancelarMensaje").hide();
}

function Redondear(numero, decimal) {

    if (typeof numero == 'string') {
        numero = numero.replace(",", ".");
    }

    numero = parseFloat(numero);
    numero = numero.toFixed(decimal);

    return ("" + numero).replace(".0", "");
}

function subString(texto, inicio, fin) {

    var continua = '';
    if (texto == "") {
        return "";
    }
    if (texto.length < fin) {
        fin = texto.length;
    }
    if (texto.length > fin) {
        continua = '...';
    }

    return texto.substring(inicio, fin) + continua;
}

// Solo permite ingresar numeros.
function soloNumeros(e) {
    var key = window.Event ? e.which : e.keyCode
    return (key >= 48 && key <= 57)
}

function temaresaltado(referencia, texto, tipo, flag) {
    var res = ''
    var stilo = '';
    if (tipo == "1") {
        stilo = '\"resaltarTema\"';
        texto = texto.replace(/<</g, "&lt;&lt;").replace(/>>/g, "&gt;&gt;").replace(/\"/g, "&quot;");
    }
    else {
        stilo = '\\\'resaltarTema\\\'';
        texto = texto.replace(/<</g, "&lt;&lt;").replace(/>>/g, "&gt;&gt;").replace(/\n/g, "<br/>").replace(/\r/g, "<br/>").replace(/\"/g, "&quot;");
    }

    if (flag != undefined && flag != null) {
        if (flag == '1') {
            texto = texto.replace(new RegExp(referencia, 'g'), "<span class=" + stilo + ">" + referencia + "</span>");
        }
        else {
            res = flag.replace(new RegExp('.*', 'g'), '').trim().split(" ");
            $.each(res, function (index, item) {
                texto = texto.replace(new RegExp(item, 'g'), "<span class=" + stilo + ">" + item + "</span>");
            });
        }
    }

    return texto;
}

$(document).ajaxComplete(
    function (event, xhr, settings) {
        if (xhr.responseText == "401") {
            AbrirMensajeModal("error", "Su sesión ha caducado!", function () {
                window.location.href = "/Account/Login";
            });
            $(".ui-dialog-content").dialog().dialog("close");
        }
});