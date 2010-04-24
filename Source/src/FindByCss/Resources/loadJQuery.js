if (typeof window.jQuery != 'undefined') {
} else {
    var s = window.document.createElement('script');
    s.type = "text/javascript";
    s.setAttribute('src', 'http://ajax.googleapis.com/ajax/libs/jquery/1.2.6/jquery.min.js');

    window.document.getElementsByTagName('head')[0].appendChild(s);

//    var loadHandler = function() { window.jQuery.noConflict(); };

//    if (s.addEventListener) { // Mozilla / WebKit
//        s.addEventListener("load", loadHandler, false);
//    } else if ("onreadystatechange" in s) { // IE
//        s.onreadystatechange = function() {
//        if (this.readyState == 'complete' || this.readyState == 'loaded') { loadHandler(); }
//        };
//    } else {
//        throw "unable to set load callback to put jQuery into .noConflict() mode.";
//    }
}
