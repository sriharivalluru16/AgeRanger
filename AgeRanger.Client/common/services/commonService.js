angular
    .module("ageRangerApp")
    .factory("personResource",
    [
        "$resource", function ($resource) {
            return $resource("http://localhost:53564/api/person/:id");
        }
    ]);