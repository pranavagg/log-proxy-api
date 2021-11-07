using System.Collections.Generic;
using System.Linq;
using LogProxyApi.Entities;

namespace LogProxyApi.Helpers
{
    public static class ExtensionMethods
    {
        public static User WithoutPassword(this User user) {
            user.Password = null;
            return user;
        }
    }
}