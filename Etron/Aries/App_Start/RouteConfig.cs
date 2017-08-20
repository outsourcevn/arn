using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Aries
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
            "gioi thieu",
            "gioi-thieu",
            new { controller = "Home", action = "About" }
          );
            routes.MapRoute(
           "giai phap",
           "giai-phap",
           new { controller = "Home", action = "Solution" }
         );
            routes.MapRoute(
            "lien he",
            "lien-he",
            new { controller = "Home", action = "Contact" }
          );
            routes.MapRoute(
                "chi tiet",
                "{catname}/{name}-{id}",
                new { controller = "News", action = "Details", catname = UrlParameter.Optional, name = UrlParameter.Optional, id = UrlParameter.Optional }
            );
            routes.MapRoute(
                "danh muc ",
                "{catname}-{cat_id}/{pg}",
                new { controller = "News", action = "List", catname = UrlParameter.Optional, cat_id = UrlParameter.Optional, pg = UrlParameter.Optional }
            );
            routes.MapRoute(
             "AdminAddProduct",
             "admin/product/add",
             new { controller = "Products", action = "Add" }
           );
            routes.MapRoute(
              "AdminEditProduct",
              "admin/product/edit/{id}",
              new { controller = "Products", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            "AdminDeleteProduct",
            "admin/product/delete/{id}",
            new { controller = "Products", action = "Delete", id = UrlParameter.Optional }
          );

            //AdminRestoreOffice
            routes.MapRoute(
           "AdminRestoreProduct",
           "admin/product/restore/{id}",
           new { controller = "Products", action = "Restore", id = UrlParameter.Optional }
         );

            routes.MapRoute(
            "AdminListProduct",
            "admin/product/list",
            new { controller = "Products", action = "List" }
          );
            
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
