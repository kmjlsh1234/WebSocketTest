const ws = require('ws');
const express = require('express');

const app = express();

const httpServer = app.listen(7777,() =>{
    console.log('listening!');
});

const webSocketServer = new ws.Server({server : httpServer});

webSocketServer.on('connection',(ws, request) =>{
    const ip = request.socket.remoteAddress;
    console.log(`client ip : ${ip}`);

    //message받았을 때 이벤트 처리
    ws.on('message',(msg) =>{
        console.log(`message : ${msg}`);
        ws.send(msg.toString());
    });

    //에러 처리
    ws.on('error',(error)=>{
        console.log(`error : ${error}`);
    });

    ws.on('close', () =>{
        console.log('close')
    });

    
});

