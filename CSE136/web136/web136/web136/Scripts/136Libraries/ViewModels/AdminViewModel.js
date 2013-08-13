// AdminViewModel depends on the Models/AdminModel to process requests (Load, Update)
define(['Models/AdminModel'], function (adminModel) {
    function AdminViewModel() {
        this.Load = function () {
            var adminModelObj = new adminModel();

            // Because the Load() is a async call (asynchronous), we'll need to use
            // the callback approach to handle the data after data is loaded.
            adminModelObj.Load(function (adminModel) {
                // using jquery to tie the viewModel with the html/view element
                // You could also use knockout.js to bind view & view-model similar to the View/Home/Index page
                $('#first').val(adminModel.FirstName);
                $('#last').val(adminModel.LastName);
            });
        };

        this.Update = function () {
            var adminModelObj = new adminModel();

            // convert the viewModel to same structure as PLAdmin model (presentation layer model)
            var adminData = {
                Id: '1',
                FirstName: $('#first').val(),
                LastName: $('#last').val()
            };

            adminModelObj.Update(adminData, function (message) {
                $('#divMessage').html(message);
            });

        };
    }
    return AdminViewModel;
}
);