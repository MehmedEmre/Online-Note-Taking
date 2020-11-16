




var module = angular.module("app", [])

    module.controller('ShowProfile', function ($scope, $http) {

    $(document).ready(function () {

        $scope.isEmpty = false;

        $scope.edit = function (htmlID,person) {


            $scope.copyPerson = angular.copy(person);

            $("#" + htmlID).children().toggle("slow")
               
        }


        $scope.cancel = function (htmlID) {

            $scope.person = $scope.copyPerson;

            $("#" + htmlID).children().toggle("slow")


        }

        

        $scope.send = function (htmlID,PersonModel) {

           

            $("#" + htmlID).children().toggle("slow")
           

            $http.post("/Home/UpdateProfile", { model: PersonModel}).then(function (response) {


            }).catch(function (error) {


            })
        }

        //İmage Upload Effect And Validation



        $scope.UploadImageAvatar = function () {

            $("#inputID").click()

        }

        $scope.FileName = "Resim Seç"

        $scope.ValidationUploadImage = function (element) {

            $scope.format = false;
            $scope.minmax = false;
            $("#labelID").removeAttr("style")
            $scope.submitButton = false;

            $scope.$apply(function () {

                if (element.files[0].name.length > 5 && element.files[0].name.length <= 15) {

                    var Index = element.files[0].name.lastIndexOf(".")

                    var substring = element.files[0].name.toLowerCase().substring(Index, element.files[0].name.length)

                    if (substring == ".jpg" || substring == ".jpeg" || substring == ".png") {

                        $scope.FileName = element.files[0].name.toLowerCase()

                        $("#buttonID").click()

                    } else {
                        $scope.format = true
                        $("#labelID").css("border-color", "red");
                        $("#labelID").css("border-width", "1px");
                        $("#labelID").css("border-style", "solid");
                    }

                } else {
                    $("#labelID").css("border-color", "red");
                    $("#labelID").css("border-width", "1px");
                    $("#labelID").css("border-style", "solid");
                    $scope.minmax = true
                }

            })

        }

       



    })


 




});


 

