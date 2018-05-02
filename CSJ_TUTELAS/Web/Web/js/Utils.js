/*Cargar DataTable*/
var opcrear = false;
var opconsultar = false;
var crear = false;
var consultar = false;
var editar = false;
var eliminar = false;


function CargarDataTable(idtable, url, columns, indextarget, arrayrender, flagrow) {

    var dtable = $('#' + idtable).DataTable({
        "bServerSide": true,
        "sAjaxSource": url,
        "dom": 'lfrtip',
        "bInfo": true,
        "scrollX": true,
        "aoColumns": columns,
        "columnDefs": [
            {
                "render": function (data, type, row) {
                    if (flagrow == undefined) {

                        var html = '';

                        var regex = new RegExp("\'", "g");
                        var name = eval("row." + arrayrender[1]).replace(regex, "");

                        if (editar) {
                            html += '<span onclick ="AbrirModal(2,\'' + eval("row." + arrayrender[0]) + '\');">' +
                                    '   <a href="#">' +
                                    '       <i title="Editar" class="fa fa-2x fa-edit"></i>' +
                                    '   </a>' +
                                    '</span>';
                        }
                        if (eliminar) {
                            html += '<span onclick="del_Row(\'' + eval("row." + arrayrender[0]) + '\',\'' + name + '\');">' +
                                    '   <a href="#">' +
                                    '     <i title="Eliminar" class="fa fa-2x fa-trash"></i>' +
                                    '   </a>' +
                                    '</span>';
                        }
                        return html;
                    }
                    else {
                        var html = '';

                        if (editar) {
                            html += '<span onclick ="AbrirModal(2, this);">' +
                                    '   <a href="#">' +
                                    '       <i title="Editar" class="fa fa-2x fa-edit"></i>' +
                                    '   </a>' +
                                    '</span>';
                        }
                        if (eliminar) {
                            html += '<span onclick="del_Row(this);">' +
                                    '   <a href="#">' +
                                    '     <i title="Eliminar" class="fa fa-2x fa-trash"></i>' +
                                    '   </a>' +
                                    '</span>';
                        }
                        return html;
                    }
                },
                "targets": indextarget
            }
        ],
        "fnDrawCallback": function (oSettings) {
            $("#" + idtable + "_next").attr("title", "Ir a la siguiente página");
            $("#" + idtable + "_previous").attr("title", "Ir a la página anterior");
        }
    });

    if (!editar && !eliminar) {
        if (dtable != null) {

            var Acciones = dtable.column(indextarget);
            Acciones.visible(false);
        }
    }

    return dtable;
}

function Permisos() {
    var Permisos = $('#Permisos').val();
    $.each(Permisos.split(','), function (i, item) {
        if (item == "1") {
            crear = true;
            $(".accionestabla").show();
        }
        else if (item == "2") {
            consultar = true;
            $(".userscontent").show();
        }
        else if (item == "3") {
            editar = true;
        }
        else if (item == "4") {
            eliminar = true;
        }
    });
}


function CargarRegistro(url, data, funcion) {
    $.ajax({
        url: url,
        type: "POST",
        data: data,
        success: function (data) {
            for (var i in data) {
                if (data[i] != null) {
                    var value = data[i];
                    if (value.toString().indexOf("/Date(") >= 0) {
                        $('#' + i).val(ShowDate(value));
                    }
                    else {
                        $('#' + i).val(data[i].toString().trim());
                    }
                }
            }

            funcion(data);
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("some error");
        }
    });
}

function ShowDate(data) {
    var date = new Date(parseInt(data.replace("/Date(", "").replace(")/", ""), 10));
    debugger;
    var dd = date.getDate();
    var month = date.getMonth() + 1;
    var fecha = (dd.toString().length > 1 ? dd : "0" + dd) + "/" + (month.toString().length > 1 ? month : "0" + month) + "/" + date.getFullYear();
    if (fecha == "01/01/1") {
        return "";
    }
    else {
        return fecha;
    }
}

/*
    Valida los campos de un formulario
    si todos los campos pasan la validacion devuelve "true"
    en caso de alguna validacion falle devuelve "false"

    Recibe un arreglo de objetos con las siguientes propiedades:

    elemento: el campo que se va a validar,
    required: si el elemento es requerido,
    maxLeng: longitud maxima del campo,
    minLeng: longitud minima del campo,
    regExp:
        expresion: expresion regular,
        mensaje: el mensaje de error si el campo no pertenece a la expresion,

        o un arreglo con varias expresiones y su respectivo mensaje
    
    requiredValue: valida si el elemento no es igual al valor indicado,
    output: elemento(s) donde se muestra el mensaje de error

*/
/*
[ { elemento: HTML, 
    required: boolean, 
    maxLeng: number, 
    minLeng: number,
    regExp: { expresion: RegExp, mensaje: string } o [{ expresion: RegExp, mensaje: string },...], 
    requiredValue: any
    output: element } ]
*/
function agregarValidacion(campos) {

    this.campos = campos;

    this.validar = function () {
        let result = true;

        this.campos.forEach(item => {

            let valid = true;
            let notEmpty = true;
            let message = '';
            let currentValue = item.elemento.val();

            if (item.hasOwnProperty('required')) {
                if (currentValue == '') {
                    message = 'Este campo es obligatorio';
                    valid = false;
                };
            }
            else {

                if (currentValue == '') {
                    notEmpty = false;
                }
            };

            if (item.hasOwnProperty('requiredValue') && valid && notEmpty) {
                if (currentValue == item.requiredValue) {
                    message = 'Este campo es obligatorio';
                    valid = false;
                };
            };
            if (item.hasOwnProperty('regExp')) {

                if (Array.isArray(item.regExp)) {

                    item.regExp.forEach(regexp => {

                        if (!regexp.expresion.test(currentValue) && valid && notEmpty) {
                            message = regexp.mensaje;
                            valid = false;
                        };
                    })
                }
                else {

                    if (!item.regExp.expresion.test(currentValue) && valid && notEmpty) {
                        message = item.regExp.mensaje;
                        valid = false;
                    };
                }
            };
            if (item.hasOwnProperty('maxLeng') && valid && notEmpty) {
                if (currentValue.length > item.maxLeng) {
                    message = 'Se supero el maximo de caracteres';
                    valid = false;
                };
            };
            if (item.hasOwnProperty('minLeng') && valid && notEmpty) {
                if (currentValue.length < item.minLeng) {
                    message = 'El campo debe tener al menos ' + item.minLeng + ' caracteres';
                    valid = false;
                };
            };
            if (item.hasOwnProperty('output'))

                if (item.output.length > 1) {
                    item.output.each((i, element) => {
                        $(element).html(message);
                    });
                }
                else
                    item.output.html(message);

            if (!valid)
                result = false;
        });

        return result;
    };

    this.limpiarMensajes = function () {
        this.campos.forEach(item => {
            item.output.html("");
        });
    };

    return this;
};

