'use strict';

const express = require('express');
const db = require('./db.js');
const Request = require('tedious').Request;

const app = express();

app.get('/monitoria/monitores', function(req, res) {
    db.getConnection((err, conn) => {
        if (err) throw err;

        var r = new Request("SELECT * FROM ApiMonitores", (err, rowCount, rows) => {
            conn.close();

            if (err) {
                console.log(err); 
                res.status(500).send("Erro de servidor");   
            }
            else {
                var rowsArray = [];
                rows.forEach(cols => {
                    var rowObj = {};
                    cols.forEach(col => {
                        if (col.isNull)
                            rowObj[col.metadata.colName] = null
                        else
                            rowObj[col.metadata.colName] = col.value;
                    });
                    rowsArray.push(rowObj);
                });
                res.send(rowsArray)
            }
        });

        conn.execSql(r);
    });
});

app.get('/monitoria/horarios', function(req, res) {
    db.getConnection((err, conn) => {
        if (err) throw err;

        var r = new Request("SELECT * FROM vw_HorariosMonitoria", (err, rowCount, rows) => {
            conn.close();

            if (err) {
                console.log(err); 
                res.status(500).send("Erro de servidor");   
            }
            else {
                var rowsArray = [];
                rows.forEach(cols => {
                    var rowObj = {};
                    cols.forEach(col => {
                        if (col.isNull)
                            rowObj[col.metadata.colName] = null
                        else
                            rowObj[col.metadata.colName] = col.value;
                    });
                    rowsArray.push(rowObj);
                });
                res.send(rowsArray)
            }
        });

        conn.execSql(r);
    });
});

app.listen(3000);