//$(document).ready(function () {

var CodeMorseView = function () {
    this.init();
    this.bindEvents();
}

CodeMorseView.prototype = {
    info: null,
    codeTimeout: null,
    pauseTimeout: null,
    interval: 250,
    dotMaxCount: 2,
    dashMinCount: 3,
    pauseCodeCount: 0,
    pauseCharCount: 0,
    pauseWordCount: 8,
    pauseEndCount: 16,

    spendingCode: 0,
    spendingPause: 0,

    internalMessage: '',

    dotValue: '1',
    dashValue: '111',
    codeSpaceValue: '0',
    charSpaceValue: '000',
    wordSpaceValue: '000000000',
    endValue:'00000000000000000000000',

    init: function () {
        this.info = $("#info").data();
    },

    cleanPanels: function () {

    },
    cleanPanels: function (e) {
        this.internalMessage = "";
        $("#resultBits").html("");
        $("#resultMorse").html("");
        $("#resultHumano").html("");
    },
    showMessage: function (msg) {
        this.internalMessage = this.internalMessage.concat(msg);
        $("#resultBits").text(this.internalMessage);

    },
    bindEvents: function () {
        $('#btnStart').on('click', $.proxy(this.onStart, this));
        $('#btnSignal').on('mousedown', $.proxy(this.onPress, this));
        $('#btnSignal').on('mouseup', $.proxy(this.onLeave, this));
        $('#btnSend').on('mouseup', $.proxy(this.sendMessage, this));
        $('#btnEnd').on('click', $.proxy(this.onEnd, this));

    },
    onEnd: function () {
        this.showMessage(this.endValue);
        this.done();
    },
    sendMessage: function () {
        var url = this.info.urlTranslate;

        $.post(url, { bits: this.internalMessage })
            .done(function (result) {
                if (result.IsOK) {
                    $("#resultMorse").html(result.ResultMorse);
                    $("#resultHumano").html(result.ResultHumano);
                }
            })
            .fail(function (result) {

            });
    },
    done: function (e) {
        clearInterval(this.pauseTimeout);
        $("#btnSignal").hide().removeClass("pauseCode").removeClass("pauseChar").removeClass("pauseWord");
        $("#btnStart").show();
        $("#btnSend").show();
        $("#btnEnd").hide();

    },
    onStart: function (e) {
        $(e.currentTarget).hide();
        $('#btnSignal').show();
        $("#btnSend").hide();
        $("#btnStart").hide();
        $("#btnEnd").show();
        this.cleanPanels();
    },
    onPress: function (e) {
        var self = this;
        var btn = $(e.currentTarget);
        clearInterval(this.pauseTimeout);
        btn.removeClass("pauseCode").removeClass("pauseChar").removeClass("pauseWord");


        this.spendingCode = 0;

        if (this.internalMessage.length > 0) {

            if (this.spendingPause <= this.dotMaxCount)
                this.showMessage(this.codeSpaceValue);
            else if (this.spendingPause >= this.dashMinCount && this.spendingPause <= this.pauseWordCount)
                this.showMessage(this.charSpaceValue);
            else if (this.spendingPause > this.pauseWordCount)
                this.showMessage(this.wordSpaceValue);


        }




        this.codeTimeout = setInterval(function () {
            self.spendingCode++;

            if (self.spendingCode >= self.dashMinCount) {
                btn.addClass("dash");
                console.log('dash');
                debugger;
            }
            else {
                btn.removeClass("dash");
                console.log('remove dash');
            }
        }, this.interval);
    },

    onLeave: function (e) {

        var self = this;
        var btn = $(e.currentTarget);

        btn.removeClass("dash");
        console.log('remove dash');
        //btn.removeClass('active').removeClass('dot').removeClass('dash').addClass('pauseCode');
        if (this.spendingCode >= this.dashMinCount)
            this.showMessage(this.dashValue);
        else
            this.showMessage(this.dotValue);

        this.spendingPause = 0;
        this.pauseTimeout = setInterval(function () {
            self.spendingPause++;

            if (self.spendingPause <= self.dotMaxCount)
                btn.addClass("pauseCode");
            else if (self.spendingPause >= self.dashMinCount && self.spendingPause <= self.pauseWordCount)
                btn.removeClass("pauseCode").addClass("pauseChar");
            else if (self.spendingPause > self.pauseWordCount && self.spendingPause < self.pauseEndCount)
                btn.removeClass("pauseChar").addClass("pauseWord");
            else if (self.spendingPause >= self.pauseEndCount)
                self.done();

        }, this.interval);




        clearInterval(this.codeTimeout);
    }

};

    var viewModel = new CodeMorseView();



//});
