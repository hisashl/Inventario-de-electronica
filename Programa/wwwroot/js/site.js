const SELECTOR_DATA_REFRESH = '#menu-container';
$(document).ready(function() {
    $.get("/Menu?handler=partial", function(data) {
        $(SELECTOR_DATA_REFRESH).html(data);
     }, 'html');     
});
