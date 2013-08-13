Imports System.Web.Optimization

'Note: For instructions on enabling IIS7 classic mode, 
'visit http://go.microsoft.com/fwlink/?LinkId=301868
Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        AreaRegistration.RegisterAllAreas()

        IdentityConfig.ConfigureIdentity()
        FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters)
        RouteConfig.RegisterRoutes(RouteTable.Routes)
        BundleConfig.RegisterBundles(BundleTable.Bundles)
    End Sub
End Class

