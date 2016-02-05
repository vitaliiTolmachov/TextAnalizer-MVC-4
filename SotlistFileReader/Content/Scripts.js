//document.onload = function() {};
window.addEventListener("load", function () {

    // обработка события отправки запроса
    document.forms[0].addEventListener("submit", function (e) {
        //var isValid = true;
        var wrongSourseMessage = "Для выбранного типа загрузки не задан источник";

        var pasteRadio = document.getElementById("paste");
        var textArea = document.getElementById("textarea");
        var downloadRadio = document.getElementById("download");
        var downloadBtn = document.getElementById("browseButton");

        // если условие сработает значение в форме будет считаться не правильным.
        if (pasteRadio.checked && textArea.value == "") {
            e.preventDefault();
            alert(wrongSourseMessage);
        }
        if (downloadRadio.checked && downloadBtn.files.length === 0) {
            e.preventDefault();
            alert(wrongSourseMessage);
        }

        // В случае если форма заполнена не правильно - отображаем сообщение об ошибке 
        // и предотвращаем отправку запроса с помощью вызова preventDefault()

    });

})