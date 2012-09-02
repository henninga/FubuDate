using System.Collections.Generic;
using System.Linq;
using FubuMVC.Core.Security;
using FubuMVC.Core.Web.Security;
using Raven.Client;


namespace FubuDate.Endpoints.Users
{
    public class IndexEndpoint
    {
        readonly IDocumentSession _session;
        readonly ISecurityContext _securityContext;

        public IndexEndpoint(IDocumentSession session)
        {
            _session = session;
            
        }

        public UsersViewModel Get(UsersRequest request)
        {
            var users = _session.Query<Domain.User>().Select(x => new UserViewModel()
            {
                Id = x.Id,
                Username = x.Username
            }).ToList();
            return new UsersViewModel{Users = users};
        }
    }

    public class UsersRequest
    {
        public Domain.User CurrentUser { get; set; }
    }

    public class UsersViewModel 
    {
        public IEnumerable<UserViewModel> Users { get; set; }
    }

    public class UserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; }
    }
}