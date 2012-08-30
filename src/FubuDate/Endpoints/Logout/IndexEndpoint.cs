using FubuDate.Endpoints.Login;
using FubuMVC.Core.Continuations;
using FubuMVC.Core.Security;

namespace FubuDate.Endpoints.Logout
{
    public class IndexEndpoint
    {
        readonly IAuthenticationContext _authenticationContext;

        public IndexEndpoint(IAuthenticationContext authenticationContext)
        {
            _authenticationContext = authenticationContext;
        }

        public FubuContinuation Get(LogoutIndexRequest request)
        {
            _authenticationContext.SignOut();
            return FubuContinuation.RedirectTo<LoginIndexRequest>();
        }
    }

    public class LogoutIndexRequest
    {
    }
}