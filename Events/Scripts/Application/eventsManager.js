var eventsManager =
    {
        initialize: function () {
        },

        initializeEventAdd: function () {
            $("#StartDate").datepicker({ minDate: 0 });

            $(".browseUsersClick").unbind('click');
            $(".browseUsersClick").click(function () {
                eventsManager.showOwnerSelectDialog();
            });
        },

        selectUser: function (obj) {
            var me = obj;
            var value = me.find(".userId").html();

            $("table.userList tr.selected").removeClass("selected");
            me.addClass("selected");

            $("#Owner").html(value);
            $("#OwnerId").val(value);
            $("#dialog").dialog("close");
        },
        selectAttendeeUser: function (obj) {
            var me = obj;
            var value = me.find(".userId").html();

            $("table.userList tr.selected").removeClass("selected");
            me.addClass("selected");

            $("#Attendee").html(value);
            $("#AttendeeId").val(value);
            $("#dialog").dialog("close");
        },
        showOwnerSelectDialog: function () {
            $("#dialog").dialog({
                resizable: false,
                modal: true,
                width: 630,
            });

            $(".userDetails").unbind('click');
            $(".userDetails").click(function () {
                eventsManager.selectUser($(this));
            });
        },
        showUserSelectDialog: function () {
            $("#dialog").dialog({
                resizable: false,
                modal: true,
                width: 630,
            });

            $(".userDetails").unbind('click');
            $(".userDetails").click(function () {
                eventsManager.selectAttendeeUser($(this));
            });
        },
        initializeEventRegister: function () {

            $(".browseUsersClick").unbind('click');
            $(".browseUsersClick").click(function () {
                eventsManager.showUserSelectDialog();
            });
        }
    };