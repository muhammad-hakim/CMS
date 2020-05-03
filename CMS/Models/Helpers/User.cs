using System.Collections.Generic;
using System.Security.Principal;

namespace CMS
{
    public class User
    {

        static WindowsIdentity identity
        {
            get
            {
                return AppHttpContext.Current.User.Identity.IsAuthenticated ? ((WindowsIdentity)AppHttpContext.Current.User.Identity) : null;
            }
        }
        /*
        public static string username
        {
            get
            {
                return identity.Name;
            }
        }
        */
        public static string username;
        public static string _username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public static string email;
        public static string _email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        private static List<string> memberships;
		public static List<string> _memberships
		{
			get 
			{ 
				return memberships; 
			}
			set
			{
				memberships = value;
			}
		}
		/*
        public static List<string> memberships
        {
            get
            {
                List<string> _groups = new List<string>();

                _groups.Add("Anonymous");

                if (!AppHttpContext.Current.User.Identity.IsAuthenticated) return _groups;

                _groups.Add("Everyone");

                foreach (var group in identity.Groups)
                {
                    string ADGroup = group.Translate(typeof(NTAccount)) + "";
                    _groups.Add(ADGroup.Contains("\\") ? ADGroup.ToLower().Split("\\")[ADGroup.ToLower().Split("\\").Count() - 1] : ADGroup.ToLower());
                }

                //_groups.Add("cms_admin");

                return _groups;
            }
        }
		*/
        public static bool isMember(List<string> groups)
        {

            if (groups!= null)
            {
                if (memberships == null && groups.Contains("Everyone"))
                {

                    return true;
                }
                else if (memberships == null)
                {
                    return false;
                }
                if (memberships.FindIndex(m => m.ToLower() == "cms_test_admin") > -1) return true;

                if (groups == null || groups.Count == 0) return false;

                foreach (var gp in groups)
                {
                    if (memberships.FindIndex(m => m.ToLower() == gp.ToLower()) > -1) return true;
                }

                return false;
            }
            return false;
        }

        public static void logout()
        {
            if (AppHttpContext.Current.User.Identity.IsAuthenticated)
                AppHttpContext.Current.Session.Clear();
        }
    }
}
