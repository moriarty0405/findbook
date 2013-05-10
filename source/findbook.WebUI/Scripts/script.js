// JavaScript Document
function turnoff(obj){
    document.getElementById(obj).style.display="none";
}

$(function () {
    //������ͼ����
    //Set Default State of each portfolio piece
    $(".paging").show();
    $(".paging a:first").addClass("active");

    //Get size of images, how many there are, then determin the size of the image reel.
    var imageWidth = $(".window").width();
    var imageSum = $(".image_reel img").size();
    var imageReelWidth = imageWidth * imageSum;

    //Adjust the image reel to its new size
    $(".image_reel").css({ 'width': imageReelWidth });

    //Paging + Slider Function
    rotate = function () {
        var triggerID = $active.attr("rel") - 1; //Get number of times to slide
        var image_reelPosition = triggerID * imageWidth; //Determines the distance the image reel needs to slide

        $(".paging a").removeClass('active'); //Remove all active class
        $active.addClass('active'); //Add active class (the $active is declared in the rotateSwitch function)

        //Slider Animation
        $(".image_reel").animate({
            left: -image_reelPosition
        }, 500);

    };

    //Rotation + Timing Event
    rotateSwitch = function () {
        play = setInterval(function () { //Set timer - this will repeat itself every 3 seconds
            $active = $('.paging a.active').next();
            if ($active.length === 0) { //If paging reaches the end...
                $active = $('.paging a:first'); //go back to first
            }
            rotate(); //Trigger the paging and slider function
        }, 7000); //Timer speed in milliseconds (3 seconds)
    };

    rotateSwitch(); //Run function on launch

    //On Hover
    $(".image_reel a").hover(function () {
        clearInterval(play); //Stop the rotation
    }, function () {
        rotateSwitch(); //Resume rotation
    });

    //On Click
    $(".paging a").click(function () {
        $active = $(this); //Activate the clicked paging
        //Reset Timer
        clearInterval(play); //Stop the rotation
        rotate(); //Trigger rotation immediately
        rotateSwitch(); // Resume rotation
        return false; //Prevent browser jump to link anchor
    });


    //��ҳ��ؽ����У�hover���û�����ʾ��ϸ��Ϣ
    $(".hover_personal_info").hide();

    $(".hovering").mouseover(function () {
        var hudId = $(this).attr('id');
        var len = hudId.length;
        var infoID = "#info" + hudId.substring(3, len);

        $(infoID).show();
    });

    $(".hovering").mouseout(function () {
        var hudId = $(this).attr('id');
        var len = hudId.length;
        var infoID = "#info" + hudId.substring(3, len);

        $(infoID).hide();
    });
    $(".hover_personal_info").mouseover(function () {
        var hudId = $(this).attr('id');
        var len = hudId.length;
        var infoID = "#info" + hudId.substring(4, len);
                
        $(infoID).show();
    });
    $(".hover_personal_info").mouseout(function () {
        var hudId = $(this).attr('id');
        var len = hudId.length;
        var infoID = "#info" + hudId.substring(4, len);
                
        $(infoID).hide();
    });

});