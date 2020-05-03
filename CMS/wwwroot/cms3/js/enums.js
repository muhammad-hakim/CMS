let primeNumber = [2, 3, 5, 7, 11];

let Enums = {

    attachments: {
        en: ["PDF", "DOCX"],
        ar: ["PDF", "DOCX"]
    },
    attachmentsRelation: {
        en: [{ value: "REQ1", name: "REQ1" }, { value: "REQ2", name: "REQ2" }, { value: "REQ3", name: "REQ3" }],
        ar: [{ value: "REQ1", name: "متطلبات الجهة الاولى" }, { value: "REQ2", name: "متطلبات الجهة الثانية" }, { value: "REQ3", name: "متطلبات الجهة الثالثة" }],
    },
    roles: {
        en: ["Trader", "Broker", "Express Mail"],
        ar: ["Trader", "Broker", "Express Mail"]
    },
    ports: {
        en: ["Sea", "Air", "Land", "Dry"],
        ar: ["Sea", "Air", "Land", "Dry"]
    },
    channels: {
        en: ["Web", "Mobile", "Webservice", "API", "H2H"],
        ar: ["Web", "Mobile", "Webservice", "API", "H2H"]
    },
    dataTypes: {
        en: ["Integer", "Float", "Decimal", "String", "File"],
        ar: ["Integer", "Float", "Decimal", "String", "File"]
    },
    dependencies: {
        en: ["Database", "Internal Service", "External Service", "Storage"],
        ar: ["Database", "Internal Service", "External Service", "Storage"]
    }

};

let enumToArray = function (e) {
    let list = [];

    if (!e) return list;

    for (i = 0; i < primeNumber.length; i++) {
        if (Number.isSafeInteger(e / primeNumber[i])) list.push(primeNumber[i]);
    }

    return list;
};


let arrayToEnum = function (array) {
    let number = 1;

    for (i = 0; i < array.length; i++) {
        number = Number.parseInt(array[i]) * number;
    }

    return number;
};

let strArrayToEnum = function (strArray) {
    let list = strArray.split(',');

    return arrayToEnum(list);
};