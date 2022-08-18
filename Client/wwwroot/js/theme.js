$(document).ready(function () {
    if (typeof KTDialer !== 'undefined') {
        KTDialer.init();
    }

    if (typeof KTDrawer !== 'undefined') {
        KTDrawer.init();
    }

    if (typeof KTImageInput !== 'undefined') {
        KTImageInput.init();
    }

    if (typeof KTMenu !== 'undefined') {
        KTMenu.init(); KTMenu.init();
    }

    if (typeof KTPasswordMeter !== 'undefined') {
        KTPasswordMeter.init();
    }
    
    if (typeof KTScroll !== 'undefined') {
        KTScroll.init();
    }
    
    if (typeof KTScrolltop !== 'undefined') {
        KTScrolltop.init();
    }
    
    if (typeof KTSticky !== 'undefined') {
        KTSticky.init();
    }
    
    if (typeof KTSwapper !== 'undefined') {
        KTSwapper.init();
    }
    
    if (typeof KTToggle !== 'undefined') {
        KTToggle.init();
    }
    
    if (typeof KTApp !== 'undefined') {
        KTApp.init();
    }
    
    if (typeof KTLayoutAside !== 'undefined') {
        KTLayoutAside.init();
    }
    
    if (typeof KTLayoutExplore !== 'undefined') {
        KTLayoutExplore.init();
    }
    
    if (typeof KTLayoutSearch !== 'undefined') {
        KTLayoutSearch.init();
    }
    
    if (typeof KTLayoutToolbar !== 'undefined') {
        KTLayoutToolbar.init();
    }

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl)
    })
});