



function likedNote(e,nID) {

    var element = $(e)
    var noteID = nID
    var img = element.find("img")
    var likedStatus = element.data("liked-mod");
    var span = element.find("span");

    $.ajax({

        method: "POST",
        url: "/Note/SendLiked",
        data: { nID: noteID, state: likedStatus}


    }).done(function (response) {


        if (response.status) {

            element.data("liked-mod", !likedStatus);

            if (!likedStatus) {//Like
                img.attr("src", "/SVG/heart fill.svg")
            }
            else {//Unlike
                img.attr("src", "/SVG/heart empty.svg")
            }

            span.html(response.likedCount)   

        } else {

            alert(response.text)
        }


    }).fail(function () {

        alert("Sunucuya Ulaşılamadı!")
    })

}