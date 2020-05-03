let starRating = {
    canRate : true
};

starRating.init = function(el, initialValue, onSelect)
{
    starRating.onSelect = onSelect;
    starRating.el = $(el);
    starRating.stars = starRating.el.find("li.star");

    starRating.stars.on('mouseover', function () {

        if(!starRating.canRate) return;

        var value = parseInt($(this).data('value'), 10); 

        // Now highlight all the stars that's not after the current hovered star
        starRating.stars.each(function (e) {
            if (e < value) $(this).addClass('hover');
            else $(this).removeClass('hover');
        });
    });

    starRating.stars.on('mouseout', function () 
    {        
        if(!starRating.canRate) return;

        starRating.stars.each(function (e) {
            $(this).removeClass('hover');
        });
    });

    starRating.stars.on('click', function () {

        if (!starRating.canRate) return;

        var value = parseInt($(this).data('value'), 10);

        starRating.set(value);

        starRating.canRate = false;
        
        if(starRating.onSelect) starRating.onSelect(value);        
    });
    
    starRating.set(initialValue);
    

}

starRating.set = function(value){

    var stars = starRating.el.find("li.star");
    
    for (i = 0; i < stars.length; i++) 
    {
        $(stars[i]).removeClass('selected');

        if (i < value) $(stars[i]).addClass('selected');
    }
}