$(document).ready(function () {
    'use strict';

    $('.parent1 input[type="radio"]').attr('disabled', true);
    $("input:radio[name='DescionQM'][value='yes']").css("border", "3px solid red");
});