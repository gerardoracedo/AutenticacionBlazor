function CerrarModal(id) {
    $("#" + id).modal('hide');
}

function AbrirCerrarModal(id) {
    $("#" + id).modal('toggle');
}

function LimparModal(id, tiempo) {
    if (!tiempo) {
        $("#" + id).html('');
    } else {
        setTimeout(function () { $("#" + id).html(''); }, tiempo + '000');
    }
}

function CargaModal() {
    return new Promise(resolve => {
        setTimeout(function () { resolve(true); }, '2000');
    });
}

function RecargarSelect() {
    setTimeout(function () {
        $('.selectpicker').selectpicker();
        $('.selectpicker').selectpicker('refresh');
    }, 1000);
}

function RecargarSelect2() {
    setTimeout(function () {
        $('.form-select').select2();
    }, 1000);
}

function LimpiarSelect(id, valor) {
    $("#" + id).val(valor);
    $("#" + id).selectpicker("refresh");
    //$("#" + id).select2();
}

function DatePicker_YM() {
    return new Promise(resolve => {
    $(".datepicker_ym").datepicker({
        todayHighlight: true,
        orientation: "bottom left",
        templates: {
            leftArrow: '<i class="la la-angle-left"></i>',
            rightArrow: '<i class="la la-angle-right"></i>'
        },
        format: "yyyy-mm",
        startView: "months",
        minViewMode: "months",
        autoclose: true
    }).on('change', function (ev) {
        var _fecha = $(this).val();
        resolve(_fecha);
    });
    })
}

function DatePicker_YMD() {
    return new Promise(resolve => {
        $(".datepicker_ym").datepicker({
            todayHighlight: true,
            orientation: "bottom left",
            templates: {
                leftArrow: '<i class="la la-angle-left"></i>',
                rightArrow: '<i class="la la-angle-right"></i>'
            },
            format: "dd/mm/yyyy",
            autoclose: true
        }).on('change', function (ev) {
            var _fecha = $(this).val();
            resolve(_fecha);
        });
    })
}

function SwalNormal(title, text, icon, cancel) {
    return new Promise(resolve => {
        $(".ctooltip").tooltip("hide");
        Swal.close();
        Swal.fire({
            title,
            text,
            icon,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            showCancelButton: cancel,
            confirmButtonText: "Aceptar"
        }).then((result) => {
            if (result.value) {
                resolve(true);
            } else {
                resolve(false);
            }
        })
    })
}

function SwalConfirmLoading(title, text, icon, cancel) {
    return new Promise(resolve => {
        $(".ctooltip").tooltip("hide");
        Swal.close();
        Swal.fire({
            title,
            text,
            icon,
            confirmButtonColor: '#3085d6',
            cancelButtonColor: '#d33',
            showCancelButton: cancel,
            confirmButtonText: "Aceptar"
        }).then((result) => {
            if (result.value) {
                SwalL();
                resolve(true);
            } else {
                resolve(false);
            }
        })
    })
}
function SwalLoading(ti, te) {
    $(".ctooltip").tooltip("hide");
    Swal.close();
    if (!ti) { ti = "Cargando..." }
    if (!te) { te = "Por favor espere." }
    Swal.fire({
        title: ti,
        text: te,
        allowOutsideClick: false,
        didOpen: function () {
            Swal.showLoading()
        }
    });
}
function SwalLoading2(ti, te) {
    $(".ctooltip").tooltip("hide");
    Swal.close();
    if (!ti) { ti = "Cargando..." }
    if (!te) { te = "Por favor espere." }
    Swal.fire({
        title: ti,
        text: te,
        imageUrl: "images/LOGO-YB.png",
        imageWidth: 250,
        imageHeight: 189,
        allowOutsideClick: false,
        didOpen: function () {
            Swal.showLoading()
        }
    });
}
function SwalCerrar(time) {
    RecargarSelect();
    setTimeout(function () { Swal.close(); }, time+'000');
}
function SwalLoadingPage() {
    Swal.close();
    Swal.fire({
        title: "Cargando...",
        text: "",
        imageUrl: "images/LOGO-YB.png",
        imageWidth: 250,
        imageHeight: 189,
        allowOutsideClick: false,
        didOpen: function () {
            Swal.showLoading()
        }
    });
}

function QuitarDataTable(tabla) {
    $("#" + tabla).dataTable().fnDestroy();
    $(".ctooltip").tooltip("hide");
}
function CargarDataTable(tabla) {
    $(document).ready(function () {
        if ($.fn.DataTable.isDataTable('#' + tabla)) {
            $("#" + tabla).dataTable().fnDestroy();
        }
        $("#" + tabla).DataTable({
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json",
            },
            "ordering": false,
            "dom":
                "<'row'" +
                "<'col-sm-6 d-flex align-items-center justify-conten-start'l>" +
                "<'col-sm-6 d-flex align-items-center justify-content-end'f>" +
                ">" +

                "<'table-responsive'tr>" +

                "<'row'" +
                "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
                "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
                ">"
        });
    });
    
}

function IniToolTip() {
    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl, {
            container: 'body',
            trigger: 'hover'
        });
    });
}
function ListaDirecciones() {
    KTDrawer.init();
    IniToolTip();
}
function ListaTelefonos() {
    KTDrawer.init();
    IniToolTip();
}
function ListaMail() {
    KTDrawer.init();
    IniToolTip();
}
function ListaPersonaJuridica() {
    KTDrawer.init();
    IniToolTip();
}

function ListaAspNetRoles() {
    KTDrawer.init();
    IniToolTip();
}

function ListaAspNetUsers() {
    KTDrawer.init();
    IniToolTip();
}

function ListaOficinas() {
    KTDrawer.init();
    IniToolTip();
}

function ListaPersonasFisicas() {
    KTDrawer.init();
    IniToolTip();
}

function ListaActividadesPersona() {
    KTDrawer.init();
    IniToolTip();
}

function ListaActividades() {
    KTDrawer.init();
    IniToolTip();
}

function checkbox() {

}