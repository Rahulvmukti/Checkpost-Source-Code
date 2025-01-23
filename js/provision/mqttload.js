var mqtt;
var gMyId = 0;
var connect_status = "Connect";
var mycamerasettings;
var streamname;
var uuid;
var camname;
var useremail;
function setstreamname(str_stream, deviceid,email) {
    uuid = deviceid;
    streamname = str_stream;
    useremail = email;
}

function onConnectBtnClick() {
    var str = connect_status;
    if (str == 'Disconnect') {
        console.log("Disconnecting MQTT Client .....");
        mqtt.disconnect();
    } else {
        console.log("Connecting MQTT Client .....");
        MQTTconnect();
    }
}
//update publish url _(also update publish enable)
function onSetstreamname(updatestreampuburl) {
    var i;
    var encodedMsg;
    var encodedMsgLen;
    var offset;
    var msgHdr = {
        srcId: 0,
        dstId: 0,
        msgType: 0,
        msgLen: 0
    };
    var appSettings = mycamerasettings;
    appSettings.streamCfg.publishUrl = updatestreampuburl;
    appSettings.streamCfg.enabled = 1;
    var msgStr = VUtil_encodeMsg(appSettings);
    console.log("XML Encoded Msg");
    console.log(msgStr);
    var buffer = new ArrayBuffer(msgStr.length + 16);
    var encodedMsg = new Uint8Array(buffer);

    msgHdr.srcId = gMyId;
    msgHdr.dstId = 0;
    msgHdr.msgType = MSG_TYPE_SET_CONFIG;
    msgHdr.msgLen = msgStr.length;
    offset = VUtil_encodeMsgHeader(encodedMsg, msgHdr);

    for (i = 0; i < msgStr.length; i++) {
        encodedMsg[offset++] = msgStr.charCodeAt(i);
    }

    var message = new Paho.MQTT.Message(encodedMsg);
    message.destinationName = dbName + '/' + tbName + '/' + 'tx/' + topic;
    message.qos = ourQos;
    message.retained = false;

    console.log("Sending SET_CONFIG message : " + message.destinationName);
    mqtt.send(message);
}
function SetEmail(email) {
    var i;
    var encodedMsg;
    var encodedMsgLen;
    var offset;
    var msgHdr = {
        srcId: 0,
        dstId: 0,
        msgType: 0,
        msgLen: 0
    };
    var appSettings = mycamerasettings;
    appSettings.grUuid = email;
    appSettings.emailCfg.to = email;
    var msgStr = VUtil_encodeMsg(appSettings);
    console.log("XML Encoded Msg");
    console.log(msgStr);
    var buffer = new ArrayBuffer(msgStr.length + 16);
    var encodedMsg = new Uint8Array(buffer);

    msgHdr.srcId = gMyId;
    msgHdr.dstId = 0;
    msgHdr.msgType = MSG_TYPE_SET_CONFIG;
    msgHdr.msgLen = msgStr.length;
    offset = VUtil_encodeMsgHeader(encodedMsg, msgHdr);

    for (i = 0; i < msgStr.length; i++) {
        encodedMsg[offset++] = msgStr.charCodeAt(i);
    }

    var message = new Paho.MQTT.Message(encodedMsg);
    message.destinationName = dbName + '/' + tbName + '/' + 'tx/' + topic;
    message.qos = ourQos;
    message.retained = false;

    console.log("Sending SET_CONFIG message : " + message.destinationName);
    mqtt.send(message);
}


function onSetBtnClick() {

    /* Encode Message */
    var i;
    var encodedMsg;
    var encodedMsgLen;
    var offset;
    var msgHdr = {srcId: 0,dstId: 0,msgType: 0,msgLen: 0};
    var appSettings = mycamerasettings;

    //edit cameraname 
    var camtxt = document.getElementById('camnameplayer');
    camtxt.setAttribute('disabled', 'disabled');
    appSettings.osdCfg.cont_1 = camtxt.value;
    /* Group UUID */
    appSettings.grUuid = useremail;
    appSettings.emailCfg.to = useremail;
    //
    appSettings.displayCfg.hue = document.getElementById('Slider1').value;
    appSettings.displayCfg.brightness = document.getElementById('Slider2').value;
    appSettings.displayCfg.contrast = document.getElementById('Slider3').value;
    appSettings.displayCfg.saturation = document.getElementById('Slider4').value;

    appSettings.nwInfo.networktype = document.getElementById('wifiinfo').value;
    appSettings.recordCh011.enable = document.getElementById('record_on_off').checked ? 1 : 0;
    //
    appSettings.displayCfg.ircutmode = document.getElementById('ircut').selectedIndex + 1;
    appSettings.imageCfg.flip = document.getElementById('flip_on_off').checked ? 1 : 0;
    appSettings.imageCfg.mirror = document.getElementById('mirror_on_off').checked ? 1 : 0;
    appSettings.imageCfg.wdr = document.getElementById('wdr').selectedIndex;

    appSettings.streamCfg.enabled = document.getElementById('publish_on_off').checked ? 1 : 0;
    appSettings.streamCfg.enableAudio = document.getElementById('audio_on_off').checked ? 1 : 0;
    appSettings.mdCfg.MdbEnable = document.getElementById('motion_on_off').checked ? 1 : 0;
    appSettings.mdCfg.md_email_switch = document.getElementById('email_on_off').checked ? 1 : 0;
    if ($("#quality_level option:selected").text() == "Very High") {
        appSettings.videoCh012.bps = 768;
        appSettings.videoCh012.fps = 25;
        appSettings.videoCh012.gop = 100,
        appSettings.videoCh012.brmode = 2;
        appSettings.videoCh012.width = 640;
        appSettings.videoCh012.height = 360;
        appSettings.videoCh012.bmainstream = 1;
        appSettings.videoCh012.bfield = 0;
        appSettings.videoCh012.piclevel = 1;
        appSettings.videoCh012.fixqplevel = 1;

    } else if ($("#quality_level option:selected").text() == "High") {
        appSettings.videoCh012.bps = 512;
        appSettings.videoCh012.fps = 20;
        appSettings.videoCh012.gop = 100,
        appSettings.videoCh012.brmode = 2;
        appSettings.videoCh012.width = 640;
        appSettings.videoCh012.height = 360;
        appSettings.videoCh012.bmainstream = 1;
        appSettings.videoCh012.bfield = 0;
        appSettings.videoCh012.piclevel = 2;
        appSettings.videoCh012.fixqplevel = 2;
    } else if ($("#quality_level option:selected").text() == "Low") {
        appSettings.videoCh012.bps = 128;
        appSettings.videoCh012.fps = 10;
        appSettings.videoCh012.gop = 100,
        appSettings.videoCh012.brmode = 2;
        appSettings.videoCh012.width = 640;
        appSettings.videoCh012.height = 360;
        appSettings.videoCh012.bmainstream = 1;
        appSettings.videoCh012.bfield = 0;
        appSettings.videoCh012.piclevel = 4;
        appSettings.videoCh012.fixqplevel = 4;
    } else if ($("#quality_level option:selected").text() == "Very Low") {
        appSettings.videoCh012.bps = 32;
        appSettings.videoCh012.fps = 3;
        appSettings.videoCh012.gop = 100,
        appSettings.videoCh012.brmode = 2;
        appSettings.videoCh012.width = 640;
        appSettings.videoCh012.height = 360;
        appSettings.videoCh012.bmainstream = 1;
        appSettings.videoCh012.bfield = 0;
        appSettings.videoCh012.piclevel = 5;
        appSettings.videoCh012.fixqplevel = 5;
    } else {
        appSettings.videoCh012.bps = 256;
        appSettings.videoCh012.fps = 15;
        appSettings.videoCh012.gop = 100,
        appSettings.videoCh012.brmode = 2;
        appSettings.videoCh012.width = 640;
        appSettings.videoCh012.height = 360;
        appSettings.videoCh012.bmainstream = 1;
        appSettings.videoCh012.bfield = 0;
        appSettings.videoCh012.piclevel = 3;
        appSettings.videoCh012.fixqplevel = 3;
    }

    appSettings.timeCfg.timeZone = $('#timeZone :selected').val();
    var msgStr = VUtil_encodeMsg(appSettings);
    console.log("XML Encoded Msg");
    console.log(msgStr);
    var buffer = new ArrayBuffer(msgStr.length + 16);
    var encodedMsg = new Uint8Array(buffer);

    msgHdr.srcId = gMyId;
    msgHdr.dstId = 0;
    msgHdr.msgType = MSG_TYPE_SET_CONFIG;
    msgHdr.msgLen = msgStr.length;
    offset = VUtil_encodeMsgHeader(encodedMsg, msgHdr);

    for (i = 0; i < msgStr.length; i++) {
        encodedMsg[offset++] = msgStr.charCodeAt(i);
    }

    var message = new Paho.MQTT.Message(encodedMsg);
    message.destinationName = dbName + '/' + tbName + '/' + 'tx/' + topic;
    message.qos = ourQos;
    message.retained = false;

    console.log("Sending SET_CONFIG message : " + message.destinationName);
    mqtt.send(message);
    $("#modal").modal('hide');
}


function onGetBtnClick() {
    /* Encode Message */
    var i;
    var encodedMsg;
    var encodedMsgLen;
    var strName = uuid; //document.getElementById('uuid').value;
    var request = '<uuid name=' + strName + ' >'
    var requestLen = request.length + 1;
    var offset;
    var msgHdr = {
        srcId: 0,
        dstId: 0,
        msgType: 0,
        msgLen: 0
    };

    console.log("Stream Name : " + strName);
    //$('#uuid').val(strName);

    var buffer = new ArrayBuffer(requestLen + 16);
    var encodedMsg = new Uint8Array(buffer);

    msgHdr.srcId = gMyId;
    msgHdr.dstId = 0;
    msgHdr.msgType = MSG_TYPE_GET_CONFIG;
    msgHdr.msgLen = requestLen;
    offset = VUtil_encodeMsgHeader(encodedMsg, msgHdr);

    for (i = 0; i < request.length; i++) {
        encodedMsg[offset++] = request.charCodeAt(i);
    }
    encodedMsg[offset++] = 0;


    var message = new Paho.MQTT.Message(encodedMsg);
    message.destinationName = dbName + '/' + tbName + '/' + 'tx/' + topic;
    message.qos = ourQos;
    message.retained = false;

    console.log("Sending GET_CONFIG message : " + message.destinationName);

    mqtt.send(message);
}

// rebbot function
function onRebootBtnClick() {
    /* Encode Message */
    var res = confirm("Are you sure you want to Reboot this Camera???");
    if (res) {
        var i;
        var encodedMsg;
        var encodedMsgLen;
        var strName = uuid; // document.getElementById('uuid').value;
        var request = '<uuid name=' + strName + ' >'
        var requestLen = request.length + 1;
        var offset;
        var msgHdr = {
            srcId: 0,
            dstId: 0,
            msgType: 0,
            msgLen: 0
        };
        console.log("Stream Name : " + strName);
        //$('#uuid').val(strName);
        var buffer = new ArrayBuffer(requestLen + 16);
        var encodedMsg = new Uint8Array(buffer);
        msgHdr.srcId = gMyId;
        msgHdr.dstId = 0;
        msgHdr.msgType = MSG_TYPE_REBOOT;
        msgHdr.msgLen = requestLen;
        offset = VUtil_encodeMsgHeader(encodedMsg, msgHdr);
        for (i = 0; i < request.length; i++) {
            encodedMsg[offset++] = request.charCodeAt(i);
        }
        encodedMsg[offset++] = 0;
        var message = new Paho.MQTT.Message(encodedMsg);
        message.destinationName = dbName + '/' + tbName + '/' + 'tx/' + topic;
        message.qos = ourQos;
        message.retained = false;
        console.log("Sending GET_CONFIG message : " + message.destinationName);
        mqtt.send(message);
    }
}

// Factory Default Function
function onDfltBtnClick() {
    /* Encode Message */
    var fres = confirm("Are you sure you want to Factory Reset this Camera???");
    if (fres) {
        var i;
        var encodedMsg;
        var encodedMsgLen;
        var strName = uuid; // document.getElementById('uuid').value;
        var request = '<uuid name=' + strName + ' >'
        var requestLen = request.length + 1;
        var offset;
        var msgHdr = {
            srcId: 0,
            dstId: 0,
            msgType: 0,
            msgLen: 0
        };
        console.log("Stream Name : " + strName);
        $('#uuid').val(strName);
        var buffer = new ArrayBuffer(requestLen + 16);
        var encodedMsg = new Uint8Array(buffer);
        msgHdr.srcId = gMyId;
        msgHdr.dstId = 0;
        msgHdr.msgType = MSG_TYPE_FACTORY_DFLT;
        msgHdr.msgLen = requestLen;
        offset = VUtil_encodeMsgHeader(encodedMsg, msgHdr);
        for (i = 0; i < request.length; i++) {
            encodedMsg[offset++] = request.charCodeAt(i);
        }
        encodedMsg[offset++] = 0;
        var message = new Paho.MQTT.Message(encodedMsg);
        message.destinationName = dbName + '/' + tbName + '/' + 'tx/' + topic;
        message.qos = ourQos;
        message.retained = false;
        console.log("Sending GET_CONFIG message : " + message.destinationName);
        mqtt.send(message);
    }
}

function onGetStatsBtnClick() {
    /* Encode Message */
    var encodedMsg;
    var msgHdr = {
        srcId: 0,
        dstId: 0,
        msgType: 0,
        msgLen: 0
    };


    var buffer = new ArrayBuffer(16);
    var encodedMsg = new Uint8Array(buffer);

    msgHdr.srcId = gMyId;
    msgHdr.dstId = 0;
    msgHdr.msgType = MSG_TYPE_GET_STATUS;
    msgHdr.msgLen = 0;
    VUtil_encodeMsgHeader(encodedMsg, msgHdr);
    console.log(dbName + '/' + tbName + '/' + 'tx/' + topic);
    var message = new Paho.MQTT.Message(encodedMsg);
    message.destinationName = dbName + '/' + tbName + '/' + 'tx/' + topic;
    message.qos = ourQos;
    message.retained = false;

    mqtt.send(message);

    setTimeout(onGetStatsBtnClick, updateTime);
}

function MQTTconnect() {
    //@TODO : Pravin, add logic to get topic & server address from input box
    gMyId = VUtil_getStreamId(topic);

    mqtt = new Paho.MQTT.Client(
        host,
        port,
        "web_" + parseInt(Math.random() * 100,
            10));
    var options = {
        timeout: 3,
        useSSL: useTLS,
        cleanSession: cleansession,
        onSuccess: onConnect,
        onFailure: function (message) {
            connect_status = "Connection failed: " + message.errorMessage + "Retrying";
            setTimeout(MQTTconnect, reconnectTimeout);
        }
    };

    mqtt.onConnectionLost = onConnectionLost;
    mqtt.onMessageArrived = onMessageArrived;

    if (username != null) {
        options.userName = username;
        options.password = password;
    }
    console.log("Host=" + host + ", port=" + port + " TLS = " + useTLS + " username=" + username + " password=" + password);
    mqtt.connect(options);
}

function onConnect() {
    //$('#status').val('Connected to ' + host + ':' + port);

    /* Connection succeeded; subscribe to our topic */
    mqtt.subscribe(dbName + "/" + tbName + "/" + "rx/" + topic, {
        qos: ourQos
    });
    mqtt.subscribe(dbName + "/" + tbName + "/" + "alarm/" + uuid, {
        qos: ourQos
    });

    connect_status = 'Disconnect';

    setTimeout(onGetStatsBtnClick, 1000);
    onGetBtnClick();
}

function onConnectionLost(response) {
    //setTimeout(MQTTconnect, reconnectTimeout);

    connect_status = 'Connect';
    // document.getElementById('statusTxt1').innerHTML = '';
    // document.getElementById('statusTxt2').innerHTML = '';
};

function processKeepAliveMsg(payload, offset, len) { }

function processGetConfigMsg(payload, offset, len) {

    console.log('Received response for : GET_CONFIG');

    var i;
    var x;
    var arraySize;
    var appSettings = VUtil_decodeGetConfigMsg(payload, offset, len);

    
    
  document.getElementById('publish_on_off').checked = (0 == appSettings.streamCfg.enabled) ? false : true;
    document.getElementById('audio_on_off').checked = (0 == appSettings.streamCfg.enableAudio) ? false : true;
    document.getElementById('motion_on_off').checked = (0 == appSettings.mdCfg.MdbEnable) ? false : true;
    if((0 == appSettings.mdCfg.MdbEnable) ? false : true)
        $("#div_emailmotion").show();
    else
        $("#div_emailmotion").hide();

    document.getElementById('email_on_off').checked = (0 == appSettings.mdCfg.md_email_switch) ? false : true;
    // document.getElementById('publish_on_off').checked = (0 == appSettings.streamCfg.enabled) ? false : true;

    camname = document.getElementById('camnameplayer');
    camname.value = appSettings.osdCfg.cont_1;
    var wifi = document.getElementById('wifiinfo');
    wifi.innerHTML = appSettings.nwInfo.networktype;

    document.getElementById('imghue').innerHTML = appSettings.displayCfg.hue;
    document.getElementById('Slider1').value = appSettings.displayCfg.hue;
    document.getElementById('imgbright').innerHTML = appSettings.displayCfg.brightness;
    document.getElementById('Slider2').value = appSettings.displayCfg.brightness;
    document.getElementById('imgcontrast').innerHTML = appSettings.displayCfg.contrast;
    document.getElementById('Slider3').value = appSettings.displayCfg.contrast;
    document.getElementById('imgsaturation').innerHTML = appSettings.displayCfg.saturation;
    document.getElementById('Slider4').value = appSettings.displayCfg.saturation;
    document.getElementById('ircut').selectedIndex = appSettings.displayCfg.ircutmode - 1;
    console.log("IrCut : " + appSettings.displayCfg.ircutmode);

    document.getElementById('record_on_off').checked = (1 == appSettings.recordCh011.enable) ? true : false;
    document.getElementById('flip_on_off').checked = (1 == appSettings.imageCfg.flip) ? true : false;
    document.getElementById('mirror_on_off').checked =(1 == appSettings.imageCfg.mirror) ? true : false;
    document.getElementById('wdr').selectedIndex = appSettings.imageCfg.wdr;


    var index;
    //    var options = document.getElementById("tz").options;
    //    for (i = 0; i < options.length; i++) {
    //        if (0 == (options[i].text.indexOf(appSettings.timeCfg.timeZone))) {
    //            document.getElementById('timeZone').selectedIndex = i;
    //            break;
    //        }
    //    }
    $("#timeZone").val(appSettings.timeCfg.timeZone);

    if (appSettings.videoCh012.bps == "768") {
        document.getElementById('quality_level').selectedIndex = 0;
    } else if (appSettings.videoCh012.bps == "512") {
        document.getElementById('quality_level').selectedIndex = 1;
    } else if (appSettings.videoCh012.bps == "128") {
        document.getElementById('quality_level').selectedIndex = 3;
    } else if (appSettings.videoCh012.bps == "32") {
        document.getElementById('quality_level').selectedIndex = 4;
    } else {
        document.getElementById('quality_level').selectedIndex = 2;
    }

    mycamerasettings = appSettings;
    
    //  var app_pubstreamname = appSettings.streamCfg.publishUrl.substring(appSettings.streamCfg.publishUrl.lastIndexOf('/') + 1, appSettings.streamCfg.publishUrl.length);
    if (appSettings.streamCfg.publishUrl != ("rtmp://" + cameraurl)) {
        onSetstreamname("rtmp://" + cameraurl);
    }
    
    if (appSettings.grUuid != useremail || appSettings.emailCfg.to != useremail)
        SetEmail(useremail);


}

function processSetConfigMsg(payload, offset, len) {

}

function processAudioMsg(payload, offset, len) { }

function processFileMsg(payload, offset, len) { }

function processDisconnectMsg(payload, offset, len) { }

function processStatusMsg(payload, offset, len) {
    var clientStats = VUtil_decodeStatusMsg(payload, offset, len);
    var str = '';

    for (var i = 0; i < (clientStats.length - 1); i++) {
        if (1 == clientStats[i].isUp) {
            str += '<font color="green">' + clientStats[i].name + ' </font>'
        } else {
            str += '<font color="red">' + clientStats[i].name + ' </font>'
        }
    }
    //document.getElementById('statusTxt2').innerHTML = str;
}

function processAlarmMsg(topic) {
    console.log("Got Alarm From : " + topic);
    window.alert("Intrusion Alarm From : " + topic);
}

function onMessageArrived(message) {
    var topic = message.destinationName;
    var payload = message.payloadBytes;

    var msgHdr = VUtil_decodeMsgHeader(payload)
    if (msgHdr.dstId != gMyId) {
        return;
    }

    var str = 'Topic : ' + topic + '= srcId : ' + msgHdr.srcId + ', dstId : ' + msgHdr.dstId + ', msgType : ' + VUtil_getMsgString(msgHdr.msgType) + ', msgLen : ' + msgHdr.msgLen;
    //   document.getElementById('statusTxt1').innerHTML = str;

    switch (msgHdr.msgType) {
        case 0:
            processKeepAliveMsg(payload, 16, msgHdr.msgLen);
            break;

        case 1:
            processGetConfigMsg(payload, 16, msgHdr.msgLen);
            break;

        case 2:
            processSetConfigMsg(payload, 16, msgHdr.msgLen);
            break;

        case 3:
            processAudioMsg(payload, 16, msgHdr.msgLen);
            break;

        case 4:
            processFileMsg(payload, 16, msgHdr.msgLen);
            break;

        case 5:
            processDisconnectMsg(payload, 16, msgHdr.msgLen);
            break;

        case 6:
            processStatusMsg(payload, 16, msgHdr.msgLen);
            break;

        default:
            break;
    }

};

$(document).ready(function () {

    topic = "webPc-" + parseInt(Math.random() * 10) + parseInt(Math.random() * 10) + parseInt(Math.random() * 10) + parseInt(Math.random() * 10);

    $("#camnameplayer").text(camname);

    setTimeout(onConnectBtnClick, 1000);

});
$(function () {
    $("#motion_on_off").change(function () {
        if (this.checked) {
            //Do stuff
           
            $("#div_emailmotion").show();
        }
        else {
           
            $("#div_emailmotion").hide();
        }
    });
});