
var text = null;
var linkEvent = null;
var noteID = null;
var btn = null;



$('#MyModal').on('show.bs.modal', function (e) {

    //ShowNoteComments partial dönen bir actiondır.
    //load dönen partialı "MyModal_body" içine koyar.

    //Modal show olduğunda fonksiyona bir nesne döner. 
    //relatedTarget  o an tıklanan butonu döndürür. Yani hangi butona tıklandığı bilgisi gelir.


    btn = $(e.relatedTarget);//e.relatedTarget jquery elementi değildir. jquery elementi olarak kullanmak için $() içine alınır


    //Html'de ---> data-note-id="item.ID"

    noteID = btn.data("note-id")//Bir elementi data attribute'u kullanılıyorsa data metodu kullanılır

    $("#MyModal_body").load("/Comment/ShowNoteComments/" + noteID)


})


function undo() {


    $(commentElementID).html(text)

    linkEvent.find("svg").removeClass("fa fa-save")
    linkEvent.find("svg").addClass("fa fa-edit")
    linkEvent.data("edit-mode", false)
    $("#undo").remove()
    commentElementID.attr("contenteditable", false)
    commentElementID.removeClass("comment-focus")

    $(commentElementID).addClass("success-comment-focus")

    setTimeout(function () {

        $(commentElementID).removeClass("success-comment-focus");

    },1000)

}


function remove(id, parentID, commentElementID){

    $.ajax({

        method: "POST",
        url: "/Comment/Delete/"+id,
        
    }).done(function (response) {

        if (response.data) {

            $("#"+commentElementID).removeClass("comment-focus")
            $("#" + parentID).addClass("danger-comment-focus")
            

            setTimeout(function () {

                $("#" + parentID).hide("slow").setTimeout(function () {

                    $("#MyModal_body").load("/Comment/ShowNoteComments/" + noteID)
                })

            }, 1000)

        } else {

            alert(response.text)

        }

    }).fail(function () {

        alert("Sunucuyla Bağlantı Kurulamadı!")

    })

}

function edit(commentID, thisLink,id) {


    linkEvent = $(thisLink)
    var mode = linkEvent.data("edit-mode")
    var newLink = $('<a id ="undo" href="#" onclick="undo()"></a >').append('<i class="fa fa-undo" title="Geri Al"></i >')
    commentElementID = $("#" + commentID);


    if (!mode) {
        text =  $("#" + commentID).text()
        linkEvent.find("svg").removeClass("fa fa-edit")
        linkEvent.find("svg").addClass("fa fa-save")
        linkEvent.data("edit-mode",true)
        linkEvent.after(newLink)
        commentElementID.attr("contenteditable", true)
        commentElementID.addClass("comment-focus")
        commentElementID.focus();

    } else {
        linkEvent.find("svg").removeClass("fa fa-save")
        linkEvent.find("svg").addClass("fa fa-edit")
        linkEvent.data("edit-mode", false)
        $("#undo").remove()
        commentElementID.attr("contenteditable", false)
        commentElementID.removeClass("comment-focus")

        var changedText = $("#" + commentID).text();

        $.ajax({
            method: "POST",
            url: "/Comment/Edit/" + id,
            data: { text: changedText}
        }).done(function (response) {

            if (response.data == true) {

                commentElementID.removeClass("comment-focus")
                commentElementID.addClass("success-comment-focus")

                setTimeout(function () {
                  $("#MyModal_body").load("/Comment/ShowNoteComments/" + noteID)
                  commentElementID.removeClass("success-comment-focus")
                  
                }, 1000)


            } else {
                alert(response.text)
            }

        }).fail(function () {

            alert("Sunucuyla Bağlantı Kurulamadı!")
        })
    

    }

}

function sendComment(id) {

    var commentText = $("#" + id).text();

    $.ajax({

        method: "POST",
        url: "/Comment/writeComment/",
        data: { "Text": commentText, "noteID": noteID },

    }).done(function (response) {

        if (response.data) {

           $("#MyModal_body").load("/Comment/ShowNoteComments/" + noteID)
           
        } else {

            alert(response.text);
        }

    }).fail(function () {

        alert("Sunucuyla Bağlantı Kurulamadı!")

    })


}

   






