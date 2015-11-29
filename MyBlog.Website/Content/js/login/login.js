var COOKIE_NAME = 'sys__username';
$(function () {
    choose_bg();
    //changeCode();
    if ($.cookie(COOKIE_NAME)) {
        $("#j_username").val($.cookie(COOKIE_NAME));
        $("#j_password").focus();
        $("#j_remember").attr('checked', true);
    } else {
        $("#j_username").focus();
    }
    /*$("#captcha_img").click(function(){
        changeCode();
    });*/
    $("#login_form").submit(function () {
        var issubmit = true;
        var i_index = 0;
        $(this).find('.in').each(function (i) {
            if ($.trim($(this).val()).length == 0) {
                $(this).css('border', '1px #ff0000 solid');
                issubmit = false;
                if (i_index == 0)
                    i_index = i;
            }
        });
        if (!issubmit) {
            $(this).find('.in').eq(i_index).focus();
            return false;
        }
        var $remember = $("#j_remember");
        if ($remember.attr('checked')) {
            $.cookie(COOKIE_NAME, $("#j_username").val(), { path: '/', expires: 15 });
        } else {
            $.cookie(COOKIE_NAME, null, { path: '/' });  //删除cookie
        }
        $("#login_ok").attr("disabled", true).val('登陆中..');
        var password = HMAC_SHA256_MAC($("#j_username").val(), $("#j_password").val());
        $("#j_password").val(HMAC_SHA256_MAC($("#j_randomKey").val(), password));
        window.location.href = 'index.html'; /*注意：生产环境时请删除此行*/
        return false;
    });
});
function genTimestamp() {
    var time = new Date();
    return time.getTime();
}
function changeCode() {
    //$("#captcha_img").attr("src", "/captcha.jpeg?t="+genTimestamp());
}
function choose_bg() {
    var bg = Math.floor(Math.random() * 9 + 1);
    $('body').css('background-image', 'url(../Content/images/loginbg_0' + bg + '.jpg)');
}