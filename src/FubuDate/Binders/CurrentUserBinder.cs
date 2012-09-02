using System.Reflection;
using FubuCore.Binding;
using FubuDate.Domain;

namespace FubuDate.Binders
{
    public class CurrentUserBinder : IPropertyBinder
    {
        public bool Matches(PropertyInfo property)
        {
            return property.PropertyType == typeof (User) && property.Name == "CurrentUser";
        }

        public void Bind(PropertyInfo property, IBindingContext context)
        {
            property.SetValue(context.Object, new User(), null);
            
        }
    }
}