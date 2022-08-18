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

    $('.selectpicker').selectpicker();

    $('[data-switch=true]').bootstrapSwitch();

    $.fn.modal.Constructor.prototype.enforceFocus = function () { };
}

function metronicScript2() {
    if (typeof KTUtil !== 'undefined') {
        KTUtil.init();
    }
    if (typeof KTApp !== 'undefined') {
        KTApp.init();
    }
    if (typeof KTLayoutAside !== 'undefined') {
        KTLayoutAside.init();
    }
}

function metronicScript3() {

    
    //KTMenu.init();
    
    /*
    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTDialer.init);
    } else {
        KTDialer.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTDrawer.init);
    } else {
        KTDrawer.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTImageInput.init);
    } else {
        KTImageInput.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTMenu.init);
    } else {
        KTMenu.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTPasswordMeter.init);
    } else {
        KTPasswordMeter.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTScroll.init);
    } else {
        KTScroll.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTScrolltop.init);
    } else {
        KTScrolltop.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTSticky.init);
    } else {
        KTSticky.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTSwapper.init);
    } else {
        KTSwapper.init();
    }

    if (document.readyState === 'loading') {
        document.addEventListener('DOMContentLoaded', KTToggle.init);
    } else {
        KTToggle.init();
    }

    KTUtil.onDOMContentLoaded(function () {
        KTApp.init();
    });

    KTUtil.onDOMContentLoaded(function () {
        KTLayoutAside.init();
    });

    KTUtil.onDOMContentLoaded(function () {
        KTLayoutExplore.init();
    });

    KTUtil.onDOMContentLoaded(function () {
        KTLayoutSearch.init();
    });

    KTUtil.onDOMContentLoaded(function () {
        KTLayoutToolbar.init();
    });
    */
}