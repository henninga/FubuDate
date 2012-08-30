using System.Collections.Generic;
using System.Linq;
using FubuDate.Behaviors;
using FubuMVC.Core.Registration;

namespace FubuDate.Configuration.Conventions
{
    public class AuthenticationConvention : IConfigurationAction
    {
        public void Configure(BehaviorGraph graph)
        {
            graph
                .Actions()
                .Where(c => !c.HandlerType.Namespace.Contains("Login") && !c.HandlerType.Namespace.Contains("Home"))
                .Each(c => c.WrapWith<AuthenticationRequiredBehavior>());
        }
    }
}