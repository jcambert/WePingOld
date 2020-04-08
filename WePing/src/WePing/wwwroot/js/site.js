
var desktop = {
    OnCloseWidget: function (element) {
        console.log(element + " was closed");
    },
    CloseWidget: function (element) {
        console.log('closing widget');
        var $BOX_PANEL = $(element).closest('.x_panel');
        $BOX_PANEL.remove();
    },
    ToggleWidget: function (element) {
        var $BOX_PANEL = $(element).closest('.x_panel'),
            $ICON = $(element).find('i'),
            $BOX_CONTENT = $BOX_PANEL.find('.x_content');
        console.dir($BOX_CONTENT);
        // fix for some div with hardcoded fix class
        if ($BOX_PANEL.attr('style')) {
            $BOX_CONTENT.slideToggle(200, function () {
                $BOX_PANEL.removeAttr('style');
            });
        } else {
            $BOX_CONTENT.slideToggle(200);
            $BOX_PANEL.css('height', 'auto');
        }

        $ICON.toggleClass('fa-chevron-up fa-chevron-down');
        return $ICON.hasClass('fa-chevron-down');
    },
    WidgetToggled: function (element, isCollapsed) {
        console.log("Widget is collaped:", isCollapsed);
    },
    onDropDownClick: function(pet){
        console.log(pet + " was clicked");
    }

}


