function moveObject(event) {
    if (!event) event = window.event;

    // normalize the delta
    if (event.wheelDelta) {
        delta = event.wheelDelta / 60;  // IE and Opera
    }
    else if (event.detail) {
        delta = -event.detail / 2;  // W3C
    }
    delta = delta / 2;
}