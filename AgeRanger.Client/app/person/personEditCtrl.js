
angular
    .module("ageRangerApp")
    .controller("PersonEditCtrl",
    [
        "person", "$scope", "$state", function PersonEditCtrl(person, $scope, $state) {
            
            $scope.person = person;
            if ($scope.person && $scope.person.id && person.id !== 0) {
                $scope.title = "Edit Details of " + $scope.person.firstName  + " " + $scope.person.lastName;
            } else {
                $scope.title = "New Person";
                $scope.person.age = "";
            }

            $scope.submit = function () {
                if(person.id === 0)
                    $scope.person.$save( function(data) {
                        $scope.person = {};
                        $scope.personForm.$setPristine();
                        $scope.operationResult = "New person created successfully";
                    });
                else
                    $scope.person.$save(function (data) {
                        $scope.person = {};
                        $scope.operationResult = "Person information updated successfully";
                    });
            }


            $scope.cancel=function() {
                $state.go('personList');
            }
        }
    ]);