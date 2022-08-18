window.VerModal = {
    showModal: id => {
        $(`#${id}`).modal('show');
    },
    hideModal: id => {
        $(`#${id}`).modal('hide');
    }
};

window.SweetAlert2 = {
    Cargando: Msg => {
        Swal.fire({
            title: `Espere por favor`,
            text: `${Msg}`,
            showConfirmButton: false,
            allowOutsideClick: false,
            timer: 100000,
            onOpen: function () {
                Swal.showLoading()
            }
        })
    },
    Cerrar: () => {
        Swal.close();
    }
};

function CargaSelect2() {
    $('.select2').select2({
        placeholder: "Seleccione una opción",
        allowClear: true
    }).on('select2:unselecting', function () {
        $(this).data('unselecting', true);
    }).on('select2:opening', function (e) {
        if ($(this).data('unselecting')) {
            $(this).removeData('unselecting');
            e.preventDefault();
        }
    }).on('change.select2', function (e) {
        var selectedVal = e.target.value;
        console.log("Probando " + selectedVal);
        //return selectedVal;
        DotNet.invokeMethodAsync('AutenticacionBlazor', 'UpdateModel', selectedVal);
        //BlazorApp - Your Application DLL Name
        //UpdateModel - Function Name [JSInvokable]
    });;
}

window.DataTable = {
    Borrar: id => {
        // Tabla normal
        $(`#${id}`).DataTable().destroy();
        $(`#${id}_wrapper`).remove();
    },
    Normal: id => {
        // Tabla normal
        $(`#${id}`).DataTable().destroy();
        $(`#${id}`).DataTable({
            "language": {
                "url": "/metronic/plugins/custom/datatables/Spanish.json"
            },
            responsive: true,
            dom: `<'row'<'col-sm-6 text-left'f><'col-sm-6 text-right'B>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,
            buttons: [
                'print',
                'excelHtml5',
                'pdfHtml5',
            ],
        });
    },
    Custom1: id => {
        // Tabla sin la ultima columna
        $(`#${id}`).DataTable().destroy();
        $(`#${id}`).DataTable({
            "language": {
                "url": "/metronic/plugins/custom/datatables/Spanish.json"
            },
            responsive: true,
            dom: `<'row'<'col-sm-6 text-left'f><'col-sm-6 text-right'B>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,
            buttons: [
                {
                    extend: 'print',
                    exportOptions: {
                        columns: ':not(:last-child)',
                    }
                },
                {
                    extend: 'excelHtml5',
                    exportOptions: {
                        columns: ':not(:last-child)',
                    }
                },
                {
                    extend: 'pdfHtml5',
                    exportOptions: {
                        columns: ':not(:last-child)',
                    }
                },
            ],
        });
    },
    ActividadesComerciales: (id, titulo) => {
        // Tabla de Actividades Comerciales
        $(`#${id}`).DataTable().destroy();
        $(`#${id} tfoot th`).each(function () {
            var title = $(this).text();
            $(this).html('<input type="text" placeholder="Filtrar por ' + title + '" />');
        });
        $(`#${id}`).DataTable({
            "language": {
                "url": "/metronic/plugins/custom/datatables/Spanish.json"
            },
            searching: false,
            responsive: true,
            dom: `<'row'<'col-sm-6 text-left'f><'col-sm-6 text-right'B>>
			<'row'<'col-sm-12'tr>>
			<'row'<'col-sm-12 col-md-5'i><'col-sm-12 col-md-7 dataTables_pager'lp>>`,
            columnDefs: [
                { orderable: false, targets: -1 },
                { responsivePriority: 1, targets: 0 },
                { responsivePriority: 2, targets: -1 },
                { responsivePriority: 3, targets: 1 }
            ],
            buttons: [
                {
                    extend: 'print',
                    title: titulo,
                    exportOptions: {
                        columns: ':not(:last-child)',
                    }
                },
                {
                    extend: 'excelHtml5',
                    title: titulo,
                    exportOptions: {
                        columns: ':not(:last-child)',
                    }
                },
                {
                    extend: 'pdfHtml5',
                    title: titulo,
                    exportOptions: {
                        columns: ':not(:last-child)',
                    }
                },
            ],
            initComplete: function () {
                // Apply the search
                this.api().columns().every(function () {
                    var that = this;

                    $('input', this.footer()).on('keyup change clear', function () {
                        if (that.search() !== this.value) {
                            that
                                .search(this.value)
                                .draw();
                        }
                    });
                });
            }
        });
    }
};

$(document).on('DOMSubtreeModified', function () {
    // xx
});

function LimpiarModal() {
    $("li").find('.rz-state-highlight').removeClass("rz-state-highlight");
}

function metronicScript() {
    ////////////////////////////////////////////////////
    // Layout Base Partials(mandatory for core layout)//
    ////////////////////////////////////////////////////

    // Init Desktop & Mobile Headers
    if (typeof KTLayoutHeader !== 'undefined') {
        KTLayoutHeader.init('kt_header', 'kt_header_mobile');
    }

    // Init Header Menu
    if (typeof KTLayoutHeaderMenu !== 'undefined') {
        KTLayoutHeaderMenu.init('kt_header_menu', 'kt_header_menu_wrapper');
    }

    // Init Header Topbar For Mobile Mode
    if (typeof KTLayoutHeaderTopbar !== 'undefined') {
        KTLayoutHeaderTopbar.init('kt_header_mobile_topbar_toggle');
    }

    // Init Brand Panel For Logo
    if (typeof KTLayoutBrand !== 'undefined') {
        KTLayoutBrand.init('kt_brand');
    }

    // Init Aside
    if (typeof KTLayoutAside !== 'undefined') {
        KTLayoutAside.init('kt_aside');
    }

    // Init Aside Menu Toggle
    if (typeof KTLayoutAsideToggle !== 'undefined') {
        KTLayoutAsideToggle.init('kt_aside_toggle');
    }

    // Init Aside Menu
    if (typeof KTLayoutAsideMenu !== 'undefined') {
        KTLayoutAsideMenu.init('kt_aside_menu');
    }

    // Init Subheader
    if (typeof KTLayoutSubheader !== 'undefined') {
        KTLayoutSubheader.init('kt_subheader');
    }

    // Init Content
    if (typeof KTLayoutContent !== 'undefined') {
        KTLayoutContent.init('kt_content');
    }

    // Init Footer
    if (typeof KTLayoutFooter !== 'undefined') {
        KTLayoutFooter.init('kt_footer');
    }


    //////////////////////////////////////////////
    // Layout Extended Partials(optional to use)//
    //////////////////////////////////////////////

    // Init Scrolltop
    if (typeof KTLayoutScrolltop !== 'undefined') {
        KTLayoutScrolltop.init('kt_scrolltop');
    }

    // Init Sticky Card
    if (typeof KTLayoutStickyCard !== 'undefined') {
        KTLayoutStickyCard.init('kt_page_sticky_card');
    }

    // Init Stretched Card
    if (typeof KTLayoutStretchedCard !== 'undefined') {
        KTLayoutStretchedCard.init('kt_page_stretched_card');
    }

    // Init Code Highlighter & Preview Blocks(used to demonstrate the theme features)
    if (typeof KTLayoutExamples !== 'undefined') {
        KTLayoutExamples.init();
    }

    // Init Demo Selection Panel
    if (typeof KTLayoutDemoPanel !== 'undefined') {
        KTLayoutDemoPanel.init('kt_demo_panel');
    }

    // Init Chat App(quick modal chat)
    if (typeof KTLayoutChat !== 'undefined') {
        KTLayoutChat.init('kt_chat_modal');
    }

    // Init Quick Actions Offcanvas Panel
    if (typeof KTLayoutQuickActions !== 'undefined') {
        KTLayoutQuickActions.init('kt_quick_actions');
    }

    // Init Quick Notifications Offcanvas Panel
    if (typeof KTLayoutQuickNotifications !== 'undefined') {
        KTLayoutQuickNotifications.init('kt_quick_notifications');
    }

    // Init Quick Offcanvas Panel
    if (typeof KTLayoutQuickPanel !== 'undefined') {
        KTLayoutQuickPanel.init('kt_quick_panel');
    }

    // Init Quick User Panel
    if (typeof KTLayoutQuickUser !== 'undefined') {
        KTLayoutQuickUser.init('kt_quick_user');
    }

    // Init Quick Search Panel
    if (typeof KTLayoutQuickSearch !== 'undefined') {
        KTLayoutQuickSearch.init('kt_quick_search');
    }

    // Init Quick Cart Panel
    if (typeof KTLayoutQuickCartPanel !== 'undefined') {
        KTLayoutQuickCartPanel.init('kt_quick_cart');
    }

    // Init Search For Quick Search Dropdown
    if (typeof KTLayoutSearch !== 'undefined') {
        KTLayoutSearch().init('kt_quick_search_dropdown');
    }

    // Init Search For Quick Search Offcanvas Panel
    if (typeof KTLayoutSearchOffcanvas !== 'undefined') {
        KTLayoutSearchOffcanvas().init('kt_quick_search_offcanvas');
    }

    $.fn.modal.Constructor.prototype.enforceFocus = function () { };
}

function confirmar(title, text, icon, scb) {
    return new Promise(resolve => {
        Swal.fire({
            title,
            text,
            icon,
            showCancelButton: scb,
            confirmButtonText: 'Yes'
        }).then((result) => {
            if (result.value) {
                resolve(true);
            } else {
                resolve(false);
            }
        })
    })
}