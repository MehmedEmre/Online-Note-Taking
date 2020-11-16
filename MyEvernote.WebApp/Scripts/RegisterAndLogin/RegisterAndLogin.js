



$(document).ready(function () {


    var list = document.querySelectorAll(".validation-summary-errors ul li")

    var loginButton = document.getElementById("login")

        loginButton.addEventListener("click", function () {
            

                list.forEach(function (item, index) {

                    item.setAttribute("class", "alert alert-primary")
                    item.setAttribute("role", "alert")
                    item.setAttribute("style", "list-style-type:none")

                })

        })
   


})