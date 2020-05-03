let mdEditor = {

    init:function()
    {
        $("textarea.markdown").each(function(i,e){                      

            let textArea = $(e);

            let content = eval("contentApp.$data."+textArea.data("model"));

            textArea.parent().append(`<div class="tuiEditor"></div>`);

            let tuiDiv = textArea.parent().find('.tuiEditor').get(0);            

            let tuiEditor = new tui.Editor({
                el: tuiDiv, 
                previewStyle: 'tab',
                height: '500px',
                initialEditType: 'markdown',
                initialValue: content,//j.body[language],
                events:{
                    change: function(){
                       
                        textArea.val(tuiEditor.getValue());

                        e.dispatchEvent(new Event('input')) 
                    }
                },
                exts: [
                  {
                    name: 'chart',
                    minWidth: 100,
                    maxWidth: 600,
                    minHeight: 100,
                    maxHeight: 300
                  },
                  'scrollSync',
                  'colorSyntax',
                  'uml',
                  'mark',
                  'table'
                ]
              });

              let fullScreenBtn = $('<button class="fas fa-expand-arrows-alt" type="button" style="color:black"></button>');

              fullScreenBtn.click(function(){

                fullScreenBtn.parents(".tuiEditor").toggleClass("fullscreen");
            
                tuiEditor.changePreviewStyle(tuiEditor.mdPreviewStyle == 'tab'? 'vertical': 'tab');
            
                fullScreenBtn.toggleClass('fa-expand-arrows-alt');
                fullScreenBtn.toggleClass('fa-compress-arrows-alt');
              });               
        
        
            tuiEditor.getUI().getToolbar().addButton({
                name: 'fullscreen',
                tooltip: 'fullscreen',
                $el: fullScreenBtn
            });
        });
    }

};

