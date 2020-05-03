let mdViewer = {

    init:function()
    {
        $("p.markdown").each(function(i,e){                      

            let paragraph = $(e);

            let content = paragraph.text();

            paragraph.parent().append(`<div class="tuiEditor"></div>`);

            let tuiDiv = paragraph.parent().find('.tuiEditor').get(0);            

            let tuiEditor = new tui.Editor({
                viewer:true,
                el: tuiDiv, 
                initialValue: content,              
                exts: [
                  {
                    name: 'chart',
                    //minWidth: 100,
                    maxWidth: 600,
                    //minHeight: 100,
                    maxHeight: 300
                  },
                  'uml',
                  'mark',
                  'table'
                ]
              });
        });
    }

};

