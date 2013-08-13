angular.module('services', []).factory('Courses', function($http) {
 
    return {
        getAllLists: function () {
            return $http.get("/Schedule/getScheduleList").then(function (result) {
                return result;
            });
        },
        getCurrentList: function () {

            return $http.get("/Schedule/getScheduleList?yearFilter=" + new Date().getFullYear()).then(function (result) {
                return result;
            });
        }
    }
});