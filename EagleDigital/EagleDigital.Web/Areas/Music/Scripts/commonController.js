(function (window, base, $) {
    var entityMap = {
        "&": "&amp;",
        "<": "&lt;",
        ">": "&gt;",
        '"': '&quot;',
        "'": '&#39;',
        "/": '&#x2F;'
    };

    function escapeHtml(string) {
        return String(string).replace(/[&<>"'\/]/g, function (s) {
            return entityMap[s];
        });
    }
    $.commonController = function (element, options) {
        var defaults = {
            form_has_changed: false,
        };
        var plugin = this;
        plugin.settings = {};
        var $element = $(element),
            localElement = element;

 

        plugin.init = function () {
            plugin.settings = $.extend({}, defaults, options);
            var that = this;
            var $form = $("#form-search #frm-search-all");
            $form.delegate("input, select, textarea", "change", function () {
                plugin.settings.form_has_changed = true;
            });
            var validator = $form.data("validator");
            validator.settings.ignore = ':hidden, [readonly=readonly], .k-readonly';
            validator.settings.debug = true;
            var isSubmit = false;
            validator.settings.submitHandler = function (form) {
                if (isSubmit) return false;
                isSubmit = true;
                plugin.settings.form_has_changed = false;
                window.location.href = "/Search/Index?inputsearch=" + window.commonController.data.term;
                //$form.ajaxSubmit({
                //    url: $(this).attr('action'),
                //    //dataType: "json",
                //    cache: false,
                //    beforeSend: function () {
                //    },
                //    // contentType: "application/json; charset=utf-8",
                //    //contentType: "text/html; charset=utf-8",
                //    data: data,
                //    statusCode: {
                //        400: function (xhr, status, err) {
                //            var errors = $.parseJSON(err);
                //           // validator.showErrors(errors);
                //        }
                //    },
                //    error: function (xhr, status, error) {
                //        isSubmit = false;
                //    },
                //    success: function (result) {
                //        console.log(result)
                //        //var msg = result.Message;
                //        //if (result.Success) {
                //        //    window.location.href = "/Search/Index?inputsearch=asas";
                //        //} else {
                //        //    //console.log(result.Errors)
                //        //}
                //        isSubmit = false;
                //    }
                //});
                return false;
            };
        }
        plugin.init();
    };
    var totalCartItems;
    window.commonController = {
        ui: {
            formId: "#frm",
            createForm: "",
            btnPageMyDj: "#MyDj a.page-button"
        },
        activityType: {
            Listened: 1,
            Followed: 2,
            Rated: 3,
            Favorites: 4,
            Likes: 5,
            Added: 6,
            Dislike: 7,
            Report: 8
        },
        loadType: {
            Element: 1,
            PartyPlayList: 2,
            Activity: 3,
            MyDj: 4,
            All: 5
        },
        searchType: {
            Mixes: 1,
            Djs: 2,
            Events: 3,
            User: 4
        },
        sidebarRecentActivity: {
            currentPage: 0,
            viewHolder: '',
            viewMoreButton: '',
            urlAction: '',
            pageSize: 4,

            canLoadMore: true,

            loadMore: function () {
                if (commonController.sidebarRecentActivity.canLoadMore) {
                    commonController.sidebarRecentActivity.canLoadMore = false;
                    commonController.sidebarRecentActivity.currentPage++;
                    $.ajax({
                        url: '/Member/RecentActivity?size=' + commonController.sidebarRecentActivity.pageSize + '&page=' + commonController.sidebarRecentActivity.currentPage,
                        type: 'GET',
                        contentType: 'application/json',
                        dataType: 'html',
                        cache: false,
                        error: function (resp) {
                            commonController.sidebarRecentActivity.canLoadMore = true;
                        },
                        success: function (result) {
                            if (result != null && result != "") {
                                $("#sidebar-right-activity").show();
                                if (commonController.sidebarRecentActivity.currentPage <= 1) {
                                    $(commonController.sidebarRecentActivity.viewHolder).html(result);

                                    $(commonController.sidebarRecentActivity.viewHolder).customScrollbar({
                                        skin: "default-skin",
                                        hScroll: false,
                                        updateOnWindowResize: true
                                    });

                                    
                                } else {
                                    $(commonController.sidebarRecentActivity.viewHolder + ' .overview').append(result);
                                }
                                
                                $(commonController.sidebarRecentActivity.viewHolder).customScrollbar("resize", true);
                                
//                                $(commonController.sidebarRecentActivity.viewHolder + ' .overview').resize(function (e) {
//                                   
//                                    $(commonController.sidebarRecentActivity.viewHolder).customScrollbar("scrollToY", $(commonController.sidebarRecentActivity.viewHolder + ' .overview').height());
//                                    
//                                });
                                $(commonController.sidebarRecentActivity.viewMoreButton).html("view more");
                                commonController.sidebarRecentActivity.canLoadMore = true;
                                
                            } else {
                                if (commonController.sidebarRecentActivity.currentPage <= 1) {
                                    $("#sidebar-right-activity").html("");
                                }
                                
                                commonController.sidebarRecentActivity.canLoadMore = false;
                                $(commonController.sidebarRecentActivity.viewMoreButton).html("");
                            }
                        }
                    });
                }
            }
        },
        data: {
            term: ""
        },
        init: function (options) {
            var defaultOptions = {
            };
            var settings = $.extend(defaultOptions, options);
            if ($.cookie("IsAuthenticated") != "true") {
                $("#user-logo").html('<a href="/" id="logo"><img src="/Assets/images/logo.png" alt="logo"></a>');
                //$("#nav-user").html('' +
                //    '<li>' +
                //    '@Html.ActionLink("Sign Up", "SignUp", "Account", routeValues: null, htmlAttributes: new { id = "registerLink" })' +
                //    '</li>' +
                //    '<li>' +
                //    '@Html.ActionLink("Sign In", "Login", "Account", routeValues: null, htmlAttributes: new { id = "loginLink" })' +
                //    '</li>');

                $("#form-search").hide();

            } else {
                commonController.getUnreadMessageCount();
                commonController.getShoppingCartItemAvaiable();
                $("#form-search").show();
                commonController.onSearch(settings);
                commonController.onSearchKeyPress(settings);
            }
        },
        getUnreadMessageCount: function () {
            $.ajax({
                url: "/Message/GetUnreadCount",
                type: 'GET',
                contentType: 'application/json',
                dataType: 'json',
                cache: false,
                success: function (result) {
                    if (result != null && result.UnReadMessages != null) {
                        commonController.updateUnreadMessageCount(result.UnReadMessages);
                    }
                }
            });
        },
        getShoppingCartItemAvaiable: function () {
            $.ajax({
                url: "/Payment/GetShoppingCartItemCount",
                type: 'GET',
                contentType: 'application/json',
                dataType: 'json',
                cache: false,
                success: function (result) {
                    if (result != null && result.Items != null) {
                        commonController.updateShoppingCartItem(result.Items);
                        totalCartItems = result.Items;
                    }
                }
            });
        },
        updateUnreadMessageCount: function (count) {
            if (count > 0) {
                $("#ic-message-header").addClass("has-message");
                if (count > 99) {
                    $("#ic-message-header span").html("99+");
                } else {
                    $("#ic-message-header span").html(count);
                }
            } else {
                $("#ic-message-header").removeClass("has-message");
            }
        }, updateShoppingCartItem: function (count) {
            if (count > 0) {
                $("#icon-notify-shoping-cart").addClass("has-message");
                if (count > 99) {
                    $("#icon-notify-shoping-cart span").html("99+");
                } else {
                    $("#icon-notify-shoping-cart span").html(count);
                }
            } else {
                $("#icon-notify-shoping-cart").removeClass("has-message");
            }
        }, clickBuyItem: function (e) {
            $.ajax({
                url: "/Payment/CardAddMix/" + e,
                type: 'GET',
                contentType: 'application/json',
                dataType: 'json',
                cache: false,
                success: function(result) {
                    console.log(result.mix);
                    var mix = $.parseJSON(result.mix);
                    if (result.Success == "Successful.") {
                        commonController.getShoppingCartItemAvaiable();
                        pathArray = location.href.split('/');
                        protocol = pathArray[0];
                        host = pathArray[2];
                        url = protocol + '//' + host;
                        var credit = parseInt(mix.Credit);
                        if (credit > 1) {
                            credit = credit + " credits";
                        } else {
                            credit = credit + " credit";
                        }
                        var image = '/Assets/images/thumb.png';
                        if (mix.Image != null) {
                            image = mix.Image;
                        }
                        if (/(^|\s)((https?:\/\/)?[\w-]+(\.[\w-]+)+\.?(:\d+)?(\/\S*)?)/gi.test(image)) {
                            image = image;
                        } else {
                            image = url + image;
                        }
                        var content = '<div class="modal-content modal-content-message"> ' +
                            '<div class="modal-header">' +
                            '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>' +
                            '<h4 class="modal-title" id="myModalLabel">The item is added to shopping cart successfully</h4>' +
                            '</div><div class="modal-body">' +
                            '<div class="row">' +
                            '<div class="col-md-2"><image src="' + image + '" width="57" height="57"/></div>' +
                            '<div class="col-md-10" style="word-break: break-all; font-weight:bold;">' + escapeHtml(mix.Title) + '</div>' +
                            '<div class="col-md-10"> Duration: ' + mix.StrTimeFormat + '</div>' +
                            '<div class="col-md-10"> Price: ' + credit + '</div>' +
                            '</div>' +
                            '</div></div></div>';
                        $('#modal-container').empty();
                        $("#modal-container").append(content);
                        $('#modal-container').modal('toggle');
                    } else {
                        alert(result.Message);
                    }
                }
            });
        },
        onSaveActivity: function (options) {
            var defaultOptions = {
                ActivityID: null,
                ObjectID: null,
                ActivityTypeID: null,
                element: null,
                likeDj: '',
                unlikeDj: ''
            };
            var settings = $.extend(defaultOptions, options);
            var jsonData = JSON.stringify({
                // ActivityID: settings.ActivityID,
                objectID: settings.ObjectID,
                activityTypeID: settings.ActivityTypeID
            });
            $.ajax({
                url: "/Member/SaveActivity",
                data: jsonData,
                type: 'POST',
                contentType: 'application/json',
                dataType: 'json',
                cache: false,
                success: function (response) {
                    if (response.Success === true) {
                        if (settings.element != null && settings.element != undefined) {
                            var $ele = $(settings.element).find("i");
                            switch (settings.ActivityTypeID) {
                                case commonController.activityType.Listened: {
                                    $("#ListenFavorPartial").load('/Member/ListenAndFavorPartial');
                                    break;
                                    // $(settings.element).find("i[class=djmix-jp-like]").removeClass("djmix-jp-like").addClass("djmix-jp-liked");
                                }
                                case commonController.activityType.Followed: {

                                    $(djController.ui.btnFollow).removeClass("active");
                                    if (response.Result != null) {
                                        if (response.Result) {
                                            $(djController.ui.btnFollow).addClass("active");
                                            $(djController.ui.btnFollow).html("following");
                                        } else {
                                            $(djController.ui.btnFollow).html("follow");
                                        }
                                    }

                                    break;
                                }
                                case commonController.activityType.Rated: {
                                    //if ($ele.attr('class').trim() == 'djmix-jp-upvote') {
                                    //    $ele.attr('class', 'djmix-jp-upvoted');
                                    //} else {
                                    //    $ele.attr('class', 'djmix-jp-upvote');
                                    //}
                                    break;
                                }
                                case commonController.activityType.Favorites: {
                                    $("#ListenFavorPartial").load('/Member/ListenAndFavorPartial');
                                    //if ($ele.attr('class').trim() == 'djmix-jp-like') {
                                    //    $ele.attr('class', 'djmix-jp-liked');
                                    //} else {
                                    //    $ele.attr('class', 'djmix-jp-like');
                                    //}
                                    break;
                                }
                                case commonController.activityType.Likes: {

                                    if (response.Result != null) {
                                        $(".infomation-user span.text-blue").text(response.Total);

                                        if (response.Result) {
                                            $(djController.ui.btnLike).removeClass("like");
                                            $(djController.ui.btnLike).addClass("liked");
                                            $(djController.ui.btnLike).attr("title", settings.unlikeDJ);
                                        } else {
                                            $(djController.ui.btnLike).removeClass("liked");
                                            $(djController.ui.btnLike).addClass("like");
                                            $(djController.ui.btnLike).attr("title", settings.likeDj);
                                        }
                                    }
                                    break;
                                }
                                case commonController.activityType.Dislike: {
                                    //if ($ele.attr('class').trim() == 'djmix-jp-downvote') {
                                    //    $ele.attr('class', 'djmix-jp-downvoted');
                                    //} else {
                                    //    $ele.attr('class', 'djmix-jp-downvote');
                                    //}
                                    break;
                                }
                                case commonController.activityType.Added: {
                                    break;
                                }
                                case commonController.activityType.Report: {
                                    //if ($ele.attr('class').trim() == 'djmix-jp-flag') {
                                    //    $ele.attr('class', 'djmix-jp-flag-show');
                                    //} else {
                                    //    $ele.attr('class', 'djmix-jp-flag');
                                    //}
                                    break;
                                }
                                default:
                                    break;

                            }
                            $('#djmix-buttons-id').load("/Member/GetActivityStatus?mixId=" + settings.ObjectID);
                        }
                        commonController.onLoadRightSideBar({ type: commonController.loadType.All });

                    }
                    //else {
                    //    //alert(response.Message);
                    //}
                }
            });
        },
        onLoadRightSideBar: function (options) {
            var defaultOptions = {
                element: $("#sidebar-right-partylist"),
                size: 1,
                url: "/Member/",
                type: commonController.loadType.Element
                /*
                type = 0 -> load use element
                type = 1 -> load use $("#sidebar-right-partylist")
                type = 2 -> load use $("#sidebar-right-activity")
                type = 3 -> load use $("#sidebar-right-mydj")
                type = 4 -> load all
                */
            };
            var settings = $.extend(defaultOptions, options);
            if (settings.type == commonController.loadType.Element) {
                settings.element.load(settings.url + "?size=" + settings.Size);
            } else if (settings.type == commonController.loadType.PartyPlayList && typeof $("#sidebar-right-partylist") != "undefined") {
                $("#sidebar-right-partylist").load('/Member/SideBarPartyPlaylist?size=1');
            } else if (settings.type == commonController.loadType.MyDj && typeof $("#sidebar-right-mydj") != "undefined") {
                $("#sidebar-right-mydj").load('/Member/SideBarMyDJs?size=1');
            } else if (settings.type == commonController.loadType.All) {
                if (typeof $("#sidebar-right-partylist") != "undefined")
                    $("#sidebar-right-partylist").load('/Member/SideBarPartyPlaylist?size=1');

                if (typeof $("#sidebar-right-mydj") != "undefined")
                    $("#sidebar-right-mydj").load('/Member/SideBarMyDJs?size=1');
            }
            if (typeof $("#sidebar-right-activity") != "undefined")
                $("#sidebar-right-activity").load('/Member/SideBarRecentActivity?size=1');
        },
        onSearch: function (options) {
            var defaultOptions = {
            };
            var settings = $.extend(defaultOptions, options);
            var select2 = $("#form-search #input-search").select2({
                ajax: {
                    url: "/Search/GetSearchData",
                    dataType: 'json',
                    delay: 250,
                    data: function (params) {
                        commonController.data.term = params.term;
                        return {
                            q: params.term, // search term
                            pageIndex: params.page
                        };
                    },
                    processResults: function (data, params) {
                        //commonController.data.term = params.term;
                        params.page = params.page || 1;
                        return {
                            results: data.items,
                            //pagination: {
                            //    more: (params.page * 30) < data.total_count
                            //}
                        };
                    },
                    cache: true
                },
                placeholder: "Search",
                //selectOnBlur: false,
                //allowClear: false,
                escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
                minimumInputLength: 1,
                templateResult: commonController.formatRepo, // omitted for brevity, see the source of this page
                //templateSelection: commonController.formatRepoSelection // omitted for brevity, see the source of this page
            });

            select2.on("select2:selecting ", function (e) {
                //console.log("select2:select", e);
                return false;
            });
            // select2.on("change", function (e) { console.log(e); });
            //select2.on("select2:select ", function (e) {
            //    var theID = select2.select2('data');
            //    console.log("select2:select", theID);

            //    select2.select2("close");
            //    return false;
            //});
        },
        formatRepo: function (repo) {
            if (repo.loading == true && repo.disabled == true) {
                return repo.text;
            } else {
                if (repo.IsGroupOption != true && repo.SearchType == commonController.searchType.Mixes) {
                    var markup = '' +
                        '<div item-index="3" class="item mix_item">' +
                        '<div class="image" style="float:left;">' +
                        '<a  href="#">' +
                        '<img style="height: 40px; width: 40px;max-height: 40px; min-height: 40px; max-width: 40px; min-width: 40px" alt="image" src="' + repo.UrlImage + '">' +
                        '<div class="play"></div>' +
                        '</a>' +
                        '</div>' +
                        ' <div  style="margin-left:47px">' +
                        ' <h4 class="search-title"><a  title="' + repo.text + '" href="#">' + commonController.decodeHtmlEntity(repo.text) + '</a></h4>' +
                        ' <p class="search-duration" >' + repo.DjName + '</p>' +
                        '</div>' +
                        '</div>';
                    return $('<div></div>')
                        .appendTo(markup)
                        .mouseup(function (e) {
                            e.stopPropagation();
                        }).parent()
                        .click(function (e) {
                            e.preventDefault();
                            $("#form-search #input-search").select2("close");
                            window.location.href = "/User/PlayMix?id=" + repo.id;
                        });
                }
                if (repo.IsGroupOption != true && repo.SearchType == commonController.searchType.Djs) {
                    var markup = '' +
                        '<div item-index="3" class="item mix_item">' +
                        '<div class="image" style="float:left;">' +
                        '<a href="#">' +
                        '<img style="height: 40px; width: 40px;max-height: 40px; min-height: 40px; max-width: 40px; min-width: 40px" alt="image" src="' + repo.UrlImage + '">' +
                        '<div class="play"></div>' +
                        '</a>' +
                        '</div>' +
                        ' <div  style="margin-left:47px">' +
                        ' <h4 class="search-title"><a title="' + repo.text + '" href="#">' + commonController.decodeHtmlEntity(repo.text) + '</a></h4>' +
                        '</div>' +
                        '</div>';
                    return $('<div></div>')
                        .appendTo(markup)
                        .mouseup(function (e) {
                            e.stopPropagation();
                        }).parent()
                        .click(function (e) {
                            e.preventDefault();
                            $("#form-search #input-search").select2("close");
                            window.location.href = "/" + repo.DjName;
                        });
                }
                if (repo.IsGroupOption != true && repo.SearchType == commonController.searchType.Events) {
                    var markup = '' +
                        '<div item-index="3" class="item mix_item event">' +
                        '<div class="image" style="float:left;">' +
                        '<a href="#">' +
                        '<img style="height: 40px; width: 40px;max-height: 40px; min-height: 40px; max-width: 40px; min-width: 40px" alt="image" src="' + repo.UrlImage + '">' +
                        '<div class="play"></div>' +
                        '</a>' +
                        '</div>' +
                        ' <div  style="margin-left:60px">' +
                        ' <h4 class="search-title"><a title="' + commonController.decodeHtmlEntity(repo.text) + '" href="#">' + commonController.decodeHtmlEntity(repo.text) + '</a></h4>' +
                        ' <p class="search-duration" >Location: ' + commonController.decodeHtmlEntity(repo.Value) + '</p>' +
                        ' <p class="search-duration" >Time: ' + repo.ReleaseDate + '</p>' +
                        ' <p class="search-duration" >Dj Name: ' + repo.DjName + '</p>' +
                        '</div>' +
                        '</div>';
                    return $('<div></div>')
                        .appendTo(markup)
                        .mouseup(function (e) {
                            e.stopPropagation();
                        }).parent()
                        .click(function (e) {
                            e.preventDefault();
                            $("#form-search #input-search").select2("close");
                            window.location.href = "/" + repo.DjName + "?eventid=" + repo.id;
                        });
                } else if (repo.IsGroupOption == true) {
                    return '<h4>' + repo.text + '</h4>';
                } else {
                    var markup = '' +
                        '<div item-index="3" class="item mix_item">' +
                        '<div class="image" style="float:left;">' +
                        '<a href="#">' +
                        '<img style="height: 40px; width: 40px;max-height: 40px; min-height: 40px; max-width: 40px; min-width: 40px" alt="image" src="' + repo.UrlImage + '">' +
                        '<div class="play"></div>' +
                        '</a>' +
                        '</div>' +
                        ' <div  style="margin-left:47px">' +
                        ' <h4 class="search-title"><a title="' + repo.text + '" href="#">' + repo.text + '</a></h4>' +
                        '</div>' +
                        '</div>';
                    return $('<div></div>')
                        .appendTo(markup)
                        .mouseup(function (e) {
                            e.stopPropagation();
                        }).parent();

                }
            }
        },
        formatRepoSelection: function (repo) {
            return repo.full_name || repo.text;
        }, fixedEncodeURIComponent: function (str) {
            return encodeURIComponent(str).replace(/[!'()]/g, escape).replace(/\*/g, "%2A");
        },
        encodeEntities: function (e) {
            return String(e).replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/"/g, '&quot;');
        },
        // encode(decode) html text into html entity
        decodeHtmlEntity: function (str) {
            return str.replace(/&#(\d+);/g, function (match, dec) {
                return String.fromCharCode(dec);
            });
        },
        encodeHtmlEntity: function (str) {
            var buf = [];
            for (var i = str.length - 1; i >= 0; i--) {
                buf.unshift(['&#', str[i].charCodeAt(), ';'].join(''));
            }
            return buf.join('');
        },
        htmlDecode: function (value) {
            return $("<div/>").html(value).text();
        },
        validation: {
            show: function (name, msg) {
                var obj = this;
                var ui = window.commonController.ui;
                window.commonController.get.errorForField.apply(obj, [name]).show().html(msg[0]);
                var iEc = window.commonController.get.field.apply(obj, [name]);
                var cbo = iEc.filter('[data-role="combobox"],[data-role="dropdownlist"]');
                if (cbo.length == 1) {
                    cbo.on("change", function () {
                        cbo.attr('data-role') == 'combobox' ? cbo.parent().children('span').removeClass("input-validation-error") : cbo.prev().removeClass("input-validation-error");
                        $(window.commonController.ui.createForm).find('[data-valmsg-for="' + name + '"]').html('');
                    });
                    cbo.attr('data-role') == 'combobox' ? cbo.parent().children('span').addClass("input-validation") : cbo.prev().addClass("input-validation");
                } else
                    iEc.addClass("input-validation-error");
            },
            hide: function (name) {
                var obj = this;
                window.commonController.get.errorForField.apply(obj, [name]).html("");
                var iEc = window.commonController.get.field.apply(obj, [name]);
                var cbo = iEc.filter('[data-role="combobox"],[data-role="dropdownlist"]');
                if (cbo.length == 1)
                    cbo.attr('data-role') == 'combobox' ? cbo.parent().children('span').removeClass("input-validation-error") : cbo.prev().removeClass("input-validation-error");
                else
                    iEc.removeClass("input-validation-error");
            },
            clear: function () {
                var obj = this;
                $(window.commonController.ui.createForm).find("span.error, span.field-validation-valid").empty();
                $(window.commonController.ui.createForm).find("input, textarea, selectbox, .k-dropdown-wrap.input-validation-error").removeClass("input-validation-error");
            },
            all: function (errors) {
                var obj = this;
                $.each(errors, function (k, v) {
                    commonController.validation.show.apply(obj, [v.field, v.msgs]);
                });
            }
        },
        get: {
            field: function (name) {
                var obj = this;
                return $(window.commonController.ui.createForm).find("[name='" + name + "'], [name='" + name + "_input']").on("keypress", function () {
                    $(this).removeClass("input-validation-error");
                    $(window.commonController.ui.createForm).find('[data-valmsg-for="' + name + '"]').html('');
                });
            },
            errorForField: function (name) {
                var obj = this;
                return $(window.commonController.ui.createForm).find("span[data-valmsg-for='" + name + "']");
            }
        },
        onSearchKeyPress: function (options) {
            var defaultOptions = {
            };
            var settings = $.extend(defaultOptions, options);
            $('#form-search #frm-search-all .select2-selection__rendered').on('keydown', function (e) {
                if (e.keyCode === 13) {
                    window.commonController.data.term = $('.select2-search__field').val();
                    if (window.commonController.data.term != undefined && window.commonController.data.term.trim() != '') {
                        $("#form-search #input-search").select2("close");
                        window.location.href = "/Search/Index?inputsearch=" + commonController.fixedEncodeURIComponent(window.commonController.data.term);
                        return false;
                    }
                    //$('#form-search #frm-search-all').submit();
                }
            });

        },
        myDjRender: function() {
            var id = $(this).attr("rel");

            if (typeof id == "undefined") {
                id = 1;
            }
            var path = "/Member/GetMyDj/" + id;
            $("#MyDj").load(path);
        },
        GetSeoURL: function (c) {
            for (var a = "", d = "\x00", f = 0, f = 0; f < c.length; f++) {
                var e = c.charAt(f), b = c.charCodeAt(f), d = d.charCodeAt(0);
                48 <= b && 57 >= b || 65 <= b && 90 >= b || 97 <= b && 122 >= b ? a += e : 0 <= "\"'".indexOf(e) && 48 <= d && 57 >= d ? a += "in" : "'" == e && (65 <= b && 90 >= b || 97 <= b && 122 >= b) ? a += "-" : "." == e ? a += 48 <= d && 57 >= d ? "." : "-" : 0 <= "- /,+".indexOf(e) && "-" != a[a.length - 1] && (a += "-");
                d = e
            }
            return "-" == a[a.length - 1] ? a.substring(0, a.length - 1) : a.toLowerCase()
        }
    };
    $.fn.commonController = function (options) {
        return this.each(function () {
            if (undefined == $(this).data('commonController')) {
                var plugin = new $.commonController(this, options);
                $(this).data('commonController', plugin);
            }
        });
    };
})(window, window.base, jQuery);