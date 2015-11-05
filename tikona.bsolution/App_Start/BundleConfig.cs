using System.Web.Optimization;

namespace Tikona.Bsolution
{ 
    public class BundleConfig
    { 
        public static void RegisterBundle(BundleCollection bundle)
        {
            bundle.Add(new StyleBundle(virtualPath: "~/bundles/Styles")
            .Include(virtualPath: "~/Content/css/style.css"));

            bundle.Add(new ScriptBundle(virtualPath: "~/bundles/Scripts")
                .Include(virtualPath: "~/Content/js/jquery.min.js")
                .Include(virtualPath: "~/Content/js/jquery.bxslider.min.js"));    
        }
    }
}