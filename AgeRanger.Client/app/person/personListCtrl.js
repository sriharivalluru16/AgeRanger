
    angular
        .module("ageRangerApp")
        .controller("PersonListCtrl", ["personResource", "$scope", function PersonListCtrl(personResource,$scope) {
            $scope.noRecords = true;
            $scope.persons = personResource.query(function (data) {
                $scope.persons = data;
                if ( $scope.persons.length >  0) {
                    $scope.noRecords = false;
                }
            });
        }]);