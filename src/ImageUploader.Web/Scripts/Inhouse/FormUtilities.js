// ReSharper disable ThisInGlobalContext
this.rhysc = this.rhysc || {};
// ReSharper restore ThisInGlobalContext
(function (ko, rhysc) {
    "use strict";
    ko.validation.init({
        registerExtenders: true,
        messagesOnModified: true,
        insertMessages: true,
        parseInputAttributes: true,
        messageTemplate: null
    });

    var addInputToForm,
        addDataValuesToForms,
        synchPost,
        appendAntiForgeryToken;

    addInputToForm = function (name, value, form) {
        $('<input />', {
            type: 'hidden',
            name: name,
            value: value
        }).appendTo(form);
    };

    addDataValuesToForms = function (data, form, prefix) {
        prefix = prefix || '';
        $.each(data, function (name, value) {
            if ($.isArray(value)) {
                $.each(value, function (index, innerValue) {
                    var fullPrefix = prefix + name + '[' + index + '].';
                    addDataValuesToForms(innerValue, form, fullPrefix);
                });
            } else if ($.isPlainObject(value)) {
                var fullPrefix = prefix + name + '.';
                addDataValuesToForms(value, form, fullPrefix);
            } else {
                addInputToForm(prefix + name, value, form);
            }
        });
    };

    synchPost = function (action, method, input) {
        var form;
        form = $('<form />', {
            action: action,
            method: method,
            style: 'display: none;'
        });

        if (input !== undefined) {
            addDataValuesToForms(input, form);
        }
        form.appendTo('body').submit();
    };

    appendAntiForgeryToken = function (formIdentifier, model) {
        var antiForgKey = '__RequestVerificationToken',
            antiForgValue;
        antiForgValue = $(formIdentifier).find('[name="' + antiForgKey + '"]').val();
        model[antiForgKey] = antiForgValue;
    };

    rhysc.viewModelSubmit = function (formSubmitEvent, vm) {
        formSubmitEvent.preventDefault();
        if (!vm.canSubmit()) {
            vm.errors.showAllMessages();
            return;
        }
        $(formSubmitEvent.target).find('button[type=submit], input[type=submit]').attr('disabled', 'disabled');
        var jsonToPost = vm.json();
        appendAntiForgeryToken(formSubmitEvent.target, jsonToPost);
        synchPost('', 'POST', jsonToPost);
    };

    // ReSharper disable ThisInGlobalContext
}(ko, this.rhysc));
// ReSharper restore ThisInGlobalContext