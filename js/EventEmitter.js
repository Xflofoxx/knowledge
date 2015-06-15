/**
 * Created by Dario on 12/06/2015.
 */
var EventEmitter = function EventEmitter(){
    this.LOG_TAG = "EventEmitter:";
    this.listeners = {};
};

EventEmitter.prototype.emit = function emit(event, data) {
    var l;
    var listeners = this.listeners[event];
    if (listeners) {
        // preserve on listener for next emit
        for (l = 0; l < listeners.on.length; l++) {
            listeners.on[l](data);
        }
        //empty the once listeners
        if (listeners.one) {
            while (l = listeners.one.pop()) {
                l(data);
            }
        }
    }
};
EventEmitter.prototype.on = function on(event, handler) {
    this.listeners[event] = this.listeners[event] || {on: [], one: []};
    this.listeners[event].on.push(handler);
};
EventEmitter.prototype.one = function one(event, handler) {
    this.listeners[event] = this.listeners[event] || {on: [], one: []};
    this.listeners[event].one.push(handler);
};
EventEmitter.prototype.removeAll = function removeAll(event) {
    if (event) {
        //remove all listeners for the event
        delete this.listeners[event];
    } else {
        //empty all
        this.listeners = {};
    }
};

