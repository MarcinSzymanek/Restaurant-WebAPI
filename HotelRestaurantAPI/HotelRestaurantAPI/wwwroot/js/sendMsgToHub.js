"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/KitchenService").build();

console.log("sendMsgToHub function call");

connection.start().then(function () {
    console.log("Connected");
}).catch(function (err) {
    console.error(err.toString());
});

document.getElementById("reservationSubmit").addEventListener("click", function (event) {
    connection.invoke("KitchenUpdate").catch(function (err) {
        console.log("SIGNALR HUB ERROR");
        return console.error(err.toString());
    });
});