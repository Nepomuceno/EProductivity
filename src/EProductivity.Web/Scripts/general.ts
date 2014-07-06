class App {
    constructor(public tooltip: boolean = false,
        public popover: boolean = false,
        public nanoScroller: boolean = false,
        public nestableLists: boolean = false,
        public hiddenElements: boolean = false,
        public bootstrapSwitch: boolean = false,
        public dateTime: boolean = false,
        public select2: boolean = false,
        public tags: boolean = false,
        public slider: boolean = false) {

    }
    toggleSideBar = () => {
        var b = $("#sidebar-collapse")[0];
        var w = $("#cl-wrapper");
        var s = $(".cl-sidebar");

        if (w.hasClass("sb-collapsed")) {
            $(".fa", b).addClass("fa-angle-left").removeClass("fa-angle-right");
            w.removeClass("sb-collapsed");
        } else {
            $(".fa", b).removeClass("fa-angle-left").addClass("fa-angle-right");
            w.addClass("sb-collapsed");
        }
    }
    updateHeight = () => {
        if (!$("#cl-wrapper").hasClass("fixed-menu")) {
            var button = $("#cl-wrapper .collapse-button").outerHeight();
            var navH = $("#head-nav").height();
            //var document = $(document).height();
            var cont = $("#pcont").height();
            var sidebar = ($(window).width() > 755 && $(window).width() < 963) ? 0 : $("#cl-wrapper .menu-space .content").height();
            var windowH = $(window).height();

            if (sidebar < windowH && cont < windowH) {
                if (($(window).width() > 755 && $(window).width() < 963)) {
                    var height = windowH;
                } else {
                    var height = windowH - button - navH;
                }
            } else if ((sidebar < cont && sidebar > windowH) || (sidebar < windowH && sidebar < cont)) {
                var height = cont + button + navH;
            } else if (sidebar > windowH && sidebar > cont) {
                var height = sidebar + button;
            }

            // var height = ($("#pcont").height() < $(window).height())?$(window).height():$(document).height();
            $("#cl-wrapper .menu-space").css("min-height", height);
        } else {
            $("#cl-wrapper .nscroller").nanoScroller({ preventPageScrolling: true });
        }
    }



init = () => {


        /*VERTICAL MENU*/
        $(".cl-vnavigation li ul").each(function () {
            $(this).parent().addClass("parent");
        });

        $(".cl-vnavigation li ul li.active").each(function () {
            $(this).parent().show().parent().addClass("open");
            //setTimeout(function(){updateHeight();},200);
        });

        $(".cl-vnavigation").delegate(".parent > a", "click", function (e) {
            $(".cl-vnavigation .parent.open > ul").not($(this).parent().find("ul")).slideUp(300, 'swing', function () {
                $(this).parent().removeClass("open");
            });

            var ul = $(this).parent().find("ul");
            ul.slideToggle(300, 'swing', function () {
                var p = $(this).parent();
                if (p.hasClass("open")) {
                    p.removeClass("open");
                } else {
                    p.addClass("open");
                }
                //var menuH = $("#cl-wrapper .menu-space .content").height();
                // var height = ($(document).height() < $(window).height())?$(window).height():menuH;
                //updateHeight();
                $("#cl-wrapper .nscroller").nanoScroller({ preventPageScrolling: true });
            });
            e.preventDefault();
        });

        /*Small devices toggle*/
        $(".cl-toggle").click(function (e) {
            var ul = $(".cl-vnavigation");
            ul.slideToggle(300, 'swing', function () {
            });
            e.preventDefault();
        });

        /*Collapse sidebar*/
        $("#sidebar-collapse").click(function () {
            this.toggleSideBar();
        });


        if ($("#cl-wrapper").hasClass("fixed-menu")) {
            var scroll = $("#cl-wrapper .menu-space");
            scroll.addClass("nano nscroller");

            function update_height() {
                var button = $("#cl-wrapper .collapse-button");
                var collapseH = button.outerHeight();
                var navH = $("#head-nav").height();
                var height = $(window).height() - ((button.is(":visible")) ? collapseH : 0) - navH;
                scroll.css("height", height);
                $("#cl-wrapper .nscroller").nanoScroller({ preventPageScrolling: true });
            }

            $(window).resize(function () {
                update_height();
            });

            update_height();
            $("#cl-wrapper .nscroller").nanoScroller({ preventPageScrolling: true });

        } else {
            $(window).resize(function () {
                //updateHeight();
            });
            //updateHeight();
        }


        /*SubMenu hover */
        var tool = $("<div id='sub-menu-nav' style='position:fixed;z-index:9999;'></div>");

        function showMenu(_this, e) {
            if (($("#cl-wrapper").hasClass("sb-collapsed") || ($(window).width() > 755 && $(window).width() < 963)) && $("ul", _this).length > 0) {
                $(_this).removeClass("ocult");
                var menu = $("ul", _this);
                if (!$(".dropdown-header", _this).length) {
                    var head = '<li class="dropdown-header">' + $(_this).children().html() + "</li>";
                    menu.prepend(head);
                }

                tool.appendTo("body");
                var top = ($(_this).offset().top + 8) - $(window).scrollTop();
                var left = $(_this).width();

                tool.css({
                    'top': top,
                    'left': left + 8
                });
                tool.html('<ul class="sub-menu">' + menu.html() + '</ul>');
                tool.show();

                menu.css('top', top);
            } else {
                tool.hide();
            }
        }

        $(".cl-vnavigation li").hover(function (e) {
            showMenu(this, e);
        }, e => {
                tool.removeClass("over");
                setTimeout(() => {
                    if (!tool.hasClass("over") && !($(".cl-vnavigation li:hover").length > 0)) {
                        tool.hide();
                    }
                }, 500);
            });

        tool.hover(function (e) {
            $(this).addClass("over");
        }, function () {
                $(this).removeClass("over");
                tool.fadeOut("fast");
            });


        $(document).click(function () {
            tool.hide();
        });
        $(document).on('touchstart click', function (e) {
            tool.fadeOut("fast");
        });

        tool.click(function (e) {
            e.stopPropagation();
        });

        $(".cl-vnavigation li").click(function (e) {
            if ((($("#cl-wrapper").hasClass("sb-collapsed") || ($(window).width() > 755 && $(window).width() < 963)) && $("ul", this).length > 0) && !($(window).width() < 755)) {
                showMenu(this, e);
                e.stopPropagation();
            }
        });

        $(".cl-vnavigation li").on('touchstart click', function () {
            //alert($(window).width());
        });

        $(window).resize(function () {
            //updateHeight();
        });

        var domh = $("#pcont").height();
        $(document).bind('DOMSubtreeModified', function () {
            var h = $("#pcont").height();
            if (domh != h) {
                //updateHeight();
            }
        });

        /*Return to top*/
        var offset = 220;
        var duration = 500;
        var button = $('<a href="#" class="back-to-top"><i class="fa fa-angle-up"></i></a>');
        button.appendTo("body");

        jQuery(window).scroll(function () {
            if (jQuery(this).scrollTop() > offset) {
                jQuery('.back-to-top').fadeIn(duration);
            } else {
                jQuery('.back-to-top').fadeOut(duration);
            }
        });

        jQuery('.back-to-top').click(function (event) {
            event.preventDefault();
            jQuery('html, body').animate({ scrollTop: 0 }, duration);
            return false;
        });

        /*Datepicker UI*/
        $(".ui-datepicker").datepicker();

        /*Tooltips*/
        if (this.tooltip) {
            $('.ttip, [data-toggle="tooltip"]').tooltip();
        }

        /*Popover*/
        if (this.popover) {
            $('[data-popover="popover"]').popover();
        }

        /*NanoScroller*/
        if (this.nanoScroller) {
            $(".nscroller").nanoScroller();
        }

        /*Nestable Lists*/
        if (this.nestableLists) {
            $('.dd').nestable();
        }

        /*Switch*/
        if (this.bootstrapSwitch) {
            $('.switch').bootstrapSwitch();
        }

        /*DateTime Picker*/
        if (this.dateTime) {
            $(".datetime").datetimepicker({ autoclose: true });
        }

        /*Select2*/
        if (this.select2) {
            $(".select2").select2({
                width: '100%'
            });
        }

        /*Tags*/
        if (this.tags) {
            $(".tags").select2({ tags: 0, width: '100%' });
        }

        /*Slider*/
        if (this.slider) {
            $('.bslider').slider();
        }

        /*Input & Radio Buttons*/
        if (jQuery().iCheck) {
            $('.icheck').iCheck({
                checkboxClass: 'icheckbox_square-blue checkbox',
                radioClass: 'iradio_square-blue'
            });
        }

        /*Bind plugins on hidden elements*/
        if (this.hiddenElements) {
            /*Dropdown shown event*/
            $('.dropdown').on('shown.bs.dropdown', () => {
                $(".nscroller").nanoScroller();
            });

            /*Tabs refresh hidden elements*/
            $('.nav-tabs').on('shown.bs.tab', e => {
                $(".nscroller").nanoScroller();
            });
        }
    }
}

$(() => {
    $("body").animate({ opacity: 1, 'margin-left': 0 }, 300);
    var app = new App();
    app.init();
});

$('span.field-validation-valid, span.field-validation-error').each(function () {
    $(this).addClass('help-inline');
});

$('form').submit(function () {
    if ($(this).valid()) {
        $(this).find('div.control-group').each(function () {
            if ($(this).find('span.field-validation-error').length == 0) {
                $(this).removeClass('error');
            }
        });
    }
    else {
        $(this).find('div.control-group').each(function () {
            if ($(this).find('span.field-validation-error').length > 0) {
                $(this).addClass('error');
            }
        });
    }
});