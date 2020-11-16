



$('#TextModal').on('show.bs.modal', function (e) {

    btn = $(e.relatedTarget);


    noteID = btn.data("note-id")//Bir elementi data attribute'u kullanılıyorsa data metodu kullanılır

    $("#TextModal_body").load("/Note/DescriptionAndTitle/" + noteID)


})