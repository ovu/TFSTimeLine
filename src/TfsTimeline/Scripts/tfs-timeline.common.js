var tfsTimeline = tfsTimeline || {};

tfsTimeline.parseJsonDate = function (jsonDate) {
    var offset = new Date().getTimezoneOffset() * 60000;
    var parts = /\/Date\((-?\d+)([+-]\d{2})?(\d{2})?.*/.exec(jsonDate);

    if (parts[2] == undefined) parts[2] = 0;
    if (parts[3] == undefined) parts[3] = 0;
    if (parts[2] == 0 && parts[3] == 0) offset = 0;

    return new Date(+parts[1] + offset + parts[2] * 3600000 + parts[3] * 60000);
};

tfsTimeline.formatDuration = function (duration) {
    var hours = Math.floor(duration / 3600);
    var minutes = Math.floor((duration - (hours * 3600)) / 60);
    var seconds = duration - (minutes * 60) - (hours * 3600);
    
    var result = (hours > 0) 
                    ? hours + ":" + tfsTimeline.padLeadingZero(minutes) + ":" + tfsTimeline.padLeadingZero(seconds)
                    : minutes + ":" + tfsTimeline.padLeadingZero(seconds);

    return result;
};

tfsTimeline.formatDurationShortTime = function secondsToTime(secs) {
    var hours = Math.floor(secs / (60 * 60));

    var divisor_for_minutes = secs % (60 * 60);
    var minutes = Math.floor(divisor_for_minutes / 60);

    var divisor_for_seconds = divisor_for_minutes % 60;
    var seconds = Math.ceil(divisor_for_seconds);

    var result = (hours > 0)
                        ? hours + ":" + minutes + ":" + seconds
                        : minutes + ":" + tfsTimeline.padLeadingZero(seconds);
    return result;
};

tfsTimeline.formatShortTime = function (time) {
    return time.getHours() + ":" + tfsTimeline.padLeadingZero(time.getMinutes());
};

tfsTimeline.formatShortDate = function(time) {
    var monthNames = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
    var currentDate = time.getDate();
    var currentMonth = time.getMonth();
    
    return currentDate + "-" + monthNames[currentMonth];
};

tfsTimeline.formatShortDateWithYear = function (time) {
    var monthNames = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
    var currentDate = time.getDate();
    var currentMonth = time.getMonth();
    var currentYear = time.getFullYear();

    return currentDate + "-" + monthNames[currentMonth] + " " + currentYear;
};

tfsTimeline.padLeadingZero = function (number) {
    return ("0" + number).slice(-2);
};

tfsTimeline.setTimeout = function (command, milliseconds) {
    setTimeout(command, milliseconds);
};