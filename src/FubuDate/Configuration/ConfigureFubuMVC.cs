using FubuDate.Binders;
using FubuDate.Configuration.Conventions;
using FubuDate.Endpoints.Home;
using FubuDate.Localization;
using FubuLocalization.Basic;
using FubuMVC.Core;
using FubuMVC.Spark;
using FubuMVC.Validation;
using FubuValidation;

namespace FubuDate.Configuration
{
    public class ConfigureFubuMVC : FubuRegistry
    {
        public ConfigureFubuMVC()
        {
            // This line turns on the basic diagnostics and request tracing
            IncludeDiagnostics(true);

            // All public methods from concrete classes ending in "Controller"
            // in this assembly are assumed to be action methods
            Actions.IncludeClassesSuffixedWithController();

            // Policies
            Routes.HomeIs<HomeIndexRequest>()
                //.HomeIs<IndexEndpoint>(x => x.Get(new HomeIndexRequest()))
                .IgnoreControllerNamesEntirely()
                .IgnoreMethodSuffix("Html")
                .RootAtAssemblyNamespace();

            this.UseSpark();
            this.ApplyEndpointConventions();
            Models.BindPropertiesWith<CurrentUserBinder>();
            //Import<AssetsConfiguration>();
            ApplyConvention<RavenSessionConvention>();
            ApplyConvention<AuthenticationConvention>();

            this.Validation(validation =>
            {
                validation
                    .Actions
                    .Include(y => y.HasInput && y.InputType().Name.Contains("Input"));
                validation
                    .Failures
                    .If(t => t.InputType() != null && t.InputType().Name.Contains("Input"))
                    .RedirectBy<HandlerModelDescriptor>();

            });

            Views
                .TryToAttachWithDefaultConventions()
                .RegisterActionLessViews(x => x.ViewModelType == typeof(Notification));

            Assets
                .Alias("jquery-ui").Is("libs/jquery-ui-1.8.20.custom.min.js")
                .Alias("jquery").Is("libs/jquery-1.7.2.js")
                .Alias("jquery-validate").Is("libs/jquery.validate.1.9.0.js")
                .Alias("unobtrusive-ajax").Is("libs/jquery.unobtrusive-ajax.min.js")
                .Alias("unobtrusive-validate").Is("libs/jquery.validate.unobtrusive.js")
                .Alias("plugins").Is("plugins.js")
                .Alias("custom-scripts").Is("script.js")
                .Asset("jquery").Preceeds("jquery-ui")
                .Asset("jquery").Preceeds("jquery-validate")
                .Asset("jquery-ui").Requires("jquery")
                .Asset("jquery-validate").Requires("jquery")
                .Asset("unobtrusive-validate").Requires("jquery-validate")
                .Asset("plugins").Requires("jquery")
                .Asset("custom-scripts").Requires("jquery")
                .OrderedSet("jquery-set").Is("jquery,jquery-ui")
                .OrderedSet("jquery-form-set").Is("jquery-set,jquery-validate,unobtrusive-ajax,unobtrusive-validate");

            Assets.YSOD_on_missing_assets(true);
        }
    }
}
