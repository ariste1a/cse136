'use strict';

/* Controllers */

angular.module('app.schedule', []).
  controller('scheduleCTRL', ['$scope', 'Courses', '$q', function ($scope, Courses, $q) {
      $scope.loadedLists = $q.defer();
      $scope.list = [];
      $scope.chat = $.connection.classHub;
      $scope.chatMessages = [];
      
      $scope.chat.client.getMessage = function (msg) {
          $scope.$apply(function () {
              if ($scope.chatMessages.length == 5) {
                  $scope.chatMessages.splice(0, 1);
              }
              $scope.chatMessages.push(msg);
          });

      };

      $.connection.hub.start(function () {
          $scope.chat.server.login($scope.user);
          $("#buttonSubmit").click(function () {
              $scope.chat.server.postMessage($("#txtInput").val());
          });

      });

      Courses.getCurrentList().then(function (data) {
          angular.forEach(data.data, function (element) {
              $scope.list.push({
                  classid: element.schedule_id,
                  year: element.year,
                  quarter: element.quarter,
                  session: element.session,
                  title: element.course_title,
                  description: element.course_description,
                  tooltip : element.session + " " +  element.quarter + " , " + element.year
              });
          });
          $scope.loadedLists.resolve();
      });
 
      $scope.days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];

      var date = new Date();
      var d = date.getDate();
      var m = date.getMonth();
      var y = date.getFullYear();

      $scope.calendarOptions = {
          //allDaySlot : true,
          minTime: 7,
          maxTime: 21,
          header: {
              left: 'prev,next today',
              center: 'title',
              right: 'month,agendaWeek,agendaDay'
          },
          defaultView: 'agendaWeek',

      };

      $scope.selectedOptions = [];

      $scope.eventSources = [[ ]];
      $scope.loadedLists.promise.then(function () {
          angular.forEach($scope.list, function (element) {
              var day = Math.floor(Math.random() * 6) + 11;
              var hr = Math.floor(Math.random() * 13) + 7;
              var inc = Math.floor(Math.random() * 2) + 1;
              element.day = day;
              element.hr = hr;
              element.inc = inc;
              element.start = new Date(y, m, day, hr);
              if (hr + inc == 23)
                  hr = 0;
              element.end = new Date(y, m, day, (hr + inc) % 23);
              element.allDay = false;
          });
          $scope.$watch("list.length", function (newVal) {
              if(newVal>0)
              $scope.list.sort(function (e1, e2) {
                  return parseInt(e1.title.substring(3)) - parseInt(e2.title.substring(3));
              });
          });
      });

      $scope.removeStudentClass = function (obj) {
          $scope.selectedOptions.splice($scope.selectedOptions.indexOf(obj), 1);
          angular.forEach($scope.eventSources[0], function (element, index) {
              if (element.classid == obj.classid) {
                  $scope.eventSources[0].splice($scope.eventSources[0].indexOf(element), 1);
              } else {
                  //console.log(element.title + " IS DIFF FROM " + obj.title);
              }
              
          });

          angular.forEach($scope.eventSources[0], function (element, index) {   // this is so bad...
              if (element.classid == obj.classid) {
                  $scope.eventSources[0].splice($scope.eventSources[0].indexOf(element), 1);
              } else {
                  //console.log(element.title + " IS DIFF FROM " + obj.title);
              }
          });
          $scope.list.push(obj);
          $scope.chat.server.removeClass($scope.user, obj.classid);
          console.log($scope.eventSources[0]);
      //    $scope.chat.server.postMessage(obj.title + " has been removed by " + $scope.user);
      }
      $scope.$watch("selectedOptions.length", function (newVal, oldVal) {
          if (newVal > oldVal) {
              angular.forEach($scope.selectedOptions, function (element) {
                  if ($scope.eventSources[0].indexOf(element) < 0) {
                      console.log(element);
                      // $scope.chat.server.postMessage(element.title + " has been added by " + $scope.user);

                      $scope.chat.server.addClass($scope.user, element.classid);
                      $scope.eventSources[0].push(element);
                      console.log(element);
                      var element2 = new Object();
                      element2.title = element.title;
                      element2.classid = element.classid;
                      element2.description = element.description;
                      var day = Math.floor(Math.random() * 6) + 4;
                      var hr = Math.floor(Math.random() * 13) + 7;
                      var inc = Math.floor(Math.random() * 2) + 1;
                      element2.start = new Date(y, m, day, hr);
                      element2.end = new Date(y, m, day, (hr + inc) % 23);
                      element2.allDay = false;
                      $scope.eventSources[0].push(element2);
                      console.log(element2);
                      var element3 = new Object();
                      element3.title = element.title;
                      element3.description = element.description;
                      var eStart = Date(element.start);
                      var eEnd = Date(element.end);
                      element3.classid = element.classid;
                      element3.start = new Date(y, m, (element.day + 2), element.hr);
                      element3.end = new Date(y, m, (element.day + 2), element.hr + element.inc);
                      element3.allDay = false;
                      $scope.eventSources[0].push(element3);
                      console.log(element3);
                  }
              });
          }

      });



  }])
  .controller('MyCtrl2', [function () {

  }]);