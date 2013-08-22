define([], function () {
    function AdminModel() {

        this.FirstName = "First";
        this.LastName = "Last";

        // "this" object in Javascript is not the same as C# "this" keyword.
        // In JavaScript, "this" object is the object that is current executing the method
        // so "this" object changes as program calls different methods.
        // The best way to retain the "this" pointer is to assign to another variable.
        // Common name to use it "that".
        var that = this; 

        this.Load = function (callback) {
            $.ajax({
                method: 'GET',
                url: "/Admin/GetAdminInfo?AdminId=1",
                data: "",
                dataType: "json",
                success: function (result) {
                    // when ajax call recevies data, it'll call the function "callBack" which is passed in this as a parameter.
                    // See AdminViewModel.load and see how it's being used
                    that.FirstName = result.FirstName;
                    that.LastName = result.LastName;

                    callback(that); // "that" is currently pointing to the AdminModel object
                },
                error: function () {
                    // if the call fails, it will return FirstName="First" and LastName="Last"
                    alert('Error while loading course list.  Is your service layer running?');
                    callBack(that);
                }
            });
        };

        this.Update = function (adminData, callback) {
            $.ajax({
                method: 'POST',
                url: "/Admin/UpdateAdminInfo",
                data: adminData, // note, adminData must be the same as PLAdmin for this to work correctly
                success: function (message) {
                    callback(message);
                },
                error: function () {
                    callback('Error while updating admin info');
                }
            });

        };
    }

    return AdminModel;
});