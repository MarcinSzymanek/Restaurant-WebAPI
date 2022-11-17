"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/KitchenService").build();

connection.start().then(function () {
    console.log("Connected");
}).catch(function (err) {
    console.error(err.toString());
});

connection.on("kitchenUpdate", function () {
    console.log("KITCHEN UPDATE CALLED MOTADAGAFSF");
    window.location.reload();
});



