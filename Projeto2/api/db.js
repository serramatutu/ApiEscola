'use strict';

const Connection = require('tedious').Connection;
const fs = require('fs');

function getConnection(callback) {
    fs.readFile('dbconfig.json', (err, data) => {
        var config = JSON.parse(data);
        var conn = new Connection(config);

        conn.on('connect', (err) => {
            callback(err, conn);
        })
    });
}

module.exports = {
    getConnection: getConnection
};
