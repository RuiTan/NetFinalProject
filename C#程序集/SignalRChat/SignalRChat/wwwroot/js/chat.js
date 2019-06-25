"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

var user = getUser();

var selfMessage1 = "<li class=\"clearfix\">\n<div class=\"message-data align-right\">\n<span class=\"message-data-name\">";
var selfMessage2 = "</span> <i class=\"icon circle me\"></i>\n</div>\n<div class=\"message other-message float-right\">\n";
var selfMessage3 = "</div></li>";

var otherMessage1 = "<li>\n<div class=\"message-data\">\n<span class=\"message-data-name\"><i class=\"icon circle online\"></i>\n";
var otherMessage2 = "</span>\n</div>\n<div class=\"message my-message\">\n";
var otherMessage3 = "</div>\n</li>";

function getUser(){
    var username = window.prompt("欢迎...", "要先再次输入你的用户名才能进入聊天室噢~");
    return username;
}

// 启动时，访问record.json以获取消息记录，并加载到消息框中
window.onload = function(){
    $.ajax({
        type: "GET",
        url: "source/record.json",
        dataType: "json",
        success: function(data){
            console.log(data);
            for (var r = 0; r < data.length; r++){
                addLi(data[r].name, data[r].text);
            }
        }
    });
}

// 指定消息添加到消息列表
function addLi(u, message){
    var li;
    if (user == u) {
        li = selfMessage1 + u + selfMessage2 + message + selfMessage3;   
    }else{
        li = otherMessage1 + u + otherMessage2 + message + otherMessage3; 
    }
    document.getElementById("messagesList").innerHTML += li;
}

// 这是客户端对服务端对应函数的实现，即服务端已经实现了数据同步了，现在此数据可被用于客户端操作
connection.on("ReceiveMessage", function (user, message) {
    addLi(user, message);
});

// 启动连接
connection.start().then(function(){
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

// sendButton的监听事件
document.getElementById("sendButton").addEventListener("click", function (event) {
    var message = document.getElementById("messageInput").value;
    addToJson(user, message);
    // 声明在服务端SignalR中心类中
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

// 发送请求给后端将当期消息添加到json文件(消息记录)中
function addToJson(u, message){
    $.ajax({
        url: 'Message/Post',
        type: 'post',
        data: {
            user: u,
            message: message
        },
        success: function(data){
            console.log('success');
        }
    })
}
