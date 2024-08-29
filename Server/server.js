const WebSocket = require('ws')
const wss = new WebSocket.Server({ port : 7777 },() =>{
    console.log('Server Start!')
})

wss.on('connection',function connection(ws){
    ws.on('message', (data) =>{
        console.log('data : %o \n',data)
        ws.send(data);
    })
})

wss.on('listening',()=>{
    console.log('listening on 7777');
})