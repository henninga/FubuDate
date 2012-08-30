using FubuDate.Endpoints.Login;
using FubuMVC.Core;
using FubuMVC.Core.Behaviors;
using FubuMVC.Core.Runtime;
using FubuMVC.Core.Security;
using FubuMVC.Core.Urls;

namespace FubuDate.Behaviors
{
    public class AuthenticationRequiredBehavior : BasicBehavior
    {
        private readonly ISecurityContext _securityContext;
        private readonly IUrlRegistry _urlRegistry;
        private readonly IOutputWriter _outputWriter;

        public AuthenticationRequiredBehavior(ISecurityContext securityContext, IUrlRegistry urlRegistry, IOutputWriter outputWriter)
            : base(PartialBehavior.Ignored)
        {
            _securityContext = securityContext;
            _urlRegistry = urlRegistry;
            _outputWriter = outputWriter;
        }

        protected override DoNext performInvoke()
        {
            if (_securityContext.IsAuthenticated())
            {
                return DoNext.Continue;
            }

            var url = _urlRegistry.UrlFor(new LoginIndexRequest());
            _outputWriter.RedirectToUrl(url);
            return DoNext.Stop;
        }
    }
}   