var api_base = '/api/'

String.prototype.replaceAll = function(find, replacement) {
	var re = new RegExp(find, 'g');
	str = String(this).replace(re, replacement);
	return str;
}

Date.prototype.pattern=function(fmt) {        
    var o = {        
    "M+" : this.getMonth()+1, //月份        
    "d+" : this.getDate(), //日        
    "h+" : this.getHours()%12 == 0 ? 12 : this.getHours()%12, //小时        
    "H+" : this.getHours(), //小时        
    "m+" : this.getMinutes(), //分        
    "s+" : this.getSeconds(), //秒        
    "q+" : Math.floor((this.getMonth()+3)/3), //季度        
    "S" : this.getMilliseconds() //毫秒        
    };        
    var week = {        
    "0" : "\u65e5",        
    "1" : "\u4e00",        
    "2" : "\u4e8c",        
    "3" : "\u4e09",        
    "4" : "\u56db",        
    "5" : "\u4e94",        
    "6" : "\u516d"       
    };        
    if(/(y+)/.test(fmt)){        
        fmt=fmt.replace(RegExp.$1, (this.getFullYear()+"").substr(4 - RegExp.$1.length));        
    }        
    if(/(E+)/.test(fmt)){        
        fmt=fmt.replace(RegExp.$1, ((RegExp.$1.length>1) ? (RegExp.$1.length>2 ? "\u661f\u671f" : "\u5468") : "")+week[this.getDay()+""]);        
    }        
    for(var k in o){        
        if(new RegExp("("+ k +")").test(fmt)){        
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length==1) ? (o[k]) : (("00"+ o[k]).substr((""+ o[k]).length)));        
        }        
    }        
    return fmt;        
}      

function invoke(method, param, callback) {
	var url = api_base + method;
	$.post(url, param, function(result){
        if (!callback) return;
		callback(result);
	}, 'json')
}

function show_alert(type, msg) {
	var alert = $('<div class="alert alert-' + type + ' ' + 'error-msg" style="left: 543px; display: block;"><button type="button" class="close" data-dismiss="alert">×</button><strong>' + msg + '</strong></div>');
	alert.appendTo('body');
	setTimeout(function() {
		alert.fadeOut();
	}, 2000);
}

function parse_template(selector, data) {
    var tpl = $(selector).html();
    for (var key in data) {
        tpl = tpl.replaceAll('<%= ' + key + ' %>', data[key]);
    }
    return tpl;
}

$(document).ready(function(){
    $('.async-form').submit(function(){
        var form = $(this);
        var param = form.serializeArray();
        var method = form.data('method');
        var respType = form.data('response');
        invoke(method, param, function(data) {
            if (respType == 'success') {
                if (data && data['success'] == 1) {
                    show_alert('success', '操作成功');
                } else {
                    show_alert('error', '失败: ' + data['reason']);
                }
            }
            form[0].reset();
        });
        return false;
    });
});