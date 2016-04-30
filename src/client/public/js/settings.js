'use strict';
/**
 * @author Dario Olivini
 * @copyright 2016 Dario Olivini. All rights reserved.
 * See LICENSE file in root directory for full license.
 */

(function(){

    // forEach method, could be shipped as part of an Object Literal/Module
    function forEach(array, callback, scope) {
        for (var i = 0; i < array.length; i++) {
            callback.call(scope, i, array[i]); // passes back stuff we need
        }
    };

    var links = document.querySelector('.settings .nav.nav-tabs.sections').getElementsByTagName('li');

    // Create function outside loop
    function showTab() {
        var self = this, others =[], panelId, panels, panel;
        forEach(links, function(k,a){
            if(a !== self){
                others.push(a);
            }
        });
        if(this.className.match(/active|disabled/gi)){
            //do nothing...
        }else{
            this.className += ' active';
            for (var i = 0; i < others.length; i++) {
                others[i].className = others[i].className.replace("active","");
            }
            panelId = this.getElementsByTagName('a')[0].getAttribute("href").split("#")[1];
            panels = document.querySelectorAll('.settings .tab-content > div.tab-pane');
            forEach(panels, function(k,a){
                a.className = a.className.replace("active","");
            });
            panel = document.getElementById(panelId).className += " active";
        }

    }

    for (var i = 0; i < links.length; i++) {
        // <li> onclick, runAlert function
        links[i].addEventListener("click", showTab, false);
    }


})();