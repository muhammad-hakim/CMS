Enums.permitProcedureTypes={
    ar:["Import", "Export"],
    en:["Import", "Export"],
}

Enums.permitProcedureBeneficiary={
    ar:["Trader", "Custom Broker", "Shipping Agent"],
    en:["Trader", "Custom Broker", "Shipping Agent"],
}

let customMethods = {
    getTypes()
    {
        return Enums.permitProcedureTypes[language];
    },
    getBeneficiaries()
    {
        return Enums.permitProcedureBeneficiary[language];
    }
};

let preRender = function(j){
    
    j.body.duration = j.body.duration || {};
    j.body.steps = j.body.steps || {};
    j.body.requirements = j.body.requirements || {};
    j.body.attachments = j.body.attachments || {};


    j.body.requirements1 = j.body.requirements1 || {};
    j.body.requirements1.ar = j.body.requirements1.ar || {};
    j.body.requirements1.en = j.body.requirements1.en || {};

    j.body.requirements2 = j.body.requirements2 || {};
    j.body.requirements2.ar = j.body.requirements2.ar || {};
    j.body.requirements2.en = j.body.requirements2.en || {};


    j.body.requirements3 = j.body.requirements3 || {};
    j.body.requirements3.ar = j.body.requirements3.ar || {};
    j.body.requirements3.en = j.body.requirements3.en || {};



    j.body.requirements4 = j.body.requirements4 || {};
    j.body.requirements4.list1 = j.body.requirements4.list1 || {};
    j.body.requirements4.list2 = j.body.requirements4.list2 || {};
                       

    j.body.requirements5 = j.body.requirements5 || {};
    j.body.requirements5.list1 = j.body.requirements5.list1 || {};
    j.body.requirements5.list2 = j.body.requirements5.list2 || {};

    j.body.url = j.body.url || {};
    
};

let postRender = function(j){};

let preSubmit = function (data) {    
    if (data.body) {
        data.body.requirements1.attachmentKey = "REQ1";
        data.body.requirements2.attachmentKey = "REQ2";
        data.body.requirements3.attachmentKey = "REQ3";
        data.body.requirements4.attachmentKey = "REQ4";
        data.body.requirements5.attachmentKey = "REQ5";
    }
   
    if (typeof $("#defaultype") != 'undefined' || $("#defaultype").length > 0) {
        var type = $("#defaultype").val();
        if (type) {
            data.body.type = type;
        }  
    }     
};