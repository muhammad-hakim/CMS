using CMS.Services;
using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CMS.Models.Helpers
{
    public class LoginView
    {
        [Display(Name = "User name")]
        [Required(ErrorMessage = "Please enter the User Name!")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter the Password!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        //[Display(Name = "Remember me?")]
        //public bool RememberMe { get; set; }
    }
    public class Loginresult
    {
        public ClaimsIdentity claimsid;
        public string message;
    }
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class Group
    {
        public int ID { get; set; }
        //[Display(Name = "Group Name")]
        public string Name { get; set; }
        public int PID { get; set; }
    }
    public static class MyAuthentication
    {
        public const String ApplicationCookie = "MyProjectAuthenticationType";
    }
    public class validate
    {
        List<Group> ADGroups = new List<Group>();

        public Loginresult ValidateCredentials(string userName, string password)
        {
            //userName = "mimran";
            //password = "Tr!ckM8@12345";
            Loginresult loginresult = new Loginresult();
            loginresult.claimsid = null;
            loginresult.message = string.Empty;
            try
            {
                using (var adContext = new PrincipalContext(ContextType.Domain, "ad01.devfasah.sa:389", "user1", "P@ssw0rd"))

                //	using (var adContext = new PrincipalContext(ContextType.Domain, "devfasah.sa", "system_test_7", "P@ssw0rd7"))
                {

                    bool isAuthenticated = false;
                    UserPrincipal userPrincipal = null;

                    try
                    {
                        bool isvalid = adContext.ValidateCredentials(userName, password);
                        if (!isvalid)
                        {
                            loginresult.claimsid = null;
                            loginresult.message = "Invalid Credentials";
                            return loginresult;
                        }
                        userPrincipal = UserPrincipal.FindByIdentity(adContext, IdentityType.SamAccountName, userName);
                        if (userPrincipal != null)
                        {
                            //in dev test let this be true.
                            isAuthenticated = true;
                        }
                        //foreach (GroupPrincipal group in userPrincipal.GetGroups())
                        //{
                        //	Console.Out.WriteLine(group);
                        //}
                    }
                    catch (Exception ex)
                    {
                        string e1 = ex.Message;
                        isAuthenticated = false;
                        userPrincipal = null;
                        loginresult.claimsid = null;
                        loginresult.message = "An error occurred. Please contact Administrator :Error" + ex.Message;
                    }
                    if (!isAuthenticated || userPrincipal == null)
                    {
                        loginresult.claimsid = null;
                        loginresult.message = "Invalid Credentials";
                    }

                    if (userPrincipal.IsAccountLockedOut())
                    {
                        // here can be a security related discussion weather it is worth 
                        // revealing this information
                        loginresult.claimsid = null;
                        loginresult.message = "Account is locked";
                    }

                    if (userPrincipal.Enabled.HasValue && userPrincipal.Enabled.Value == false)
                    {
                        // here can be a security related discussion weather it is worth 
                        // revealing this information
                        return loginresult;
                    }


                    var ISMappUser = GetallADGroups(userPrincipal);
                    if (ISMappUser.Count == 0 || ISMappUser == null)
                    {
                        // not mapp user in HRM system roles
                        loginresult.claimsid = null;
                        loginresult.message = "No Groups associated with this Account";
                        return loginresult;
                    }

                    var identity = CreateIdentity(userPrincipal);


                    loginresult.claimsid = identity;
                    User.username = userName;
                    loginresult.message = "Success";
                    return loginresult;
                }
            }
            catch (Exception ex)
            {
                loginresult.claimsid = null;
                string err = ex.Message;
                loginresult.message = "An error occurred. Please contact Administrator :Error" + ex.Message;
                return loginresult;
            }
        }
        //if you want to get all Groups of Specific User 
        public List<string> GetallADGroups(UserPrincipal mUserPrincipal)
        {
            var RoleValue = new List<string>();

            var dirEntry = (System.DirectoryServices.DirectoryEntry)mUserPrincipal.GetUnderlyingObject();
            foreach (string groupDN in dirEntry.Properties["memberOf"])
            {
                var parts = groupDN.Replace("CN=", "").Split(',');
                //Add item in AD Group List
                ADGroups.Add(new Group { Name = parts[0] });
                RoleValue.Add(parts[0]);
            }
            return RoleValue;
        }

        private ClaimsIdentity CreateIdentity(UserPrincipal userPrincipal)
        {
            var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            //identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "Active Directory"));
            identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.Name));

            //identity.AddClaim(new Claim(ClaimTypes.Name, userPrincipal.SamAccountName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userPrincipal.SamAccountName));
            if (!String.IsNullOrEmpty(userPrincipal.EmailAddress))
            {
                identity.AddClaim(new Claim(ClaimTypes.Email, userPrincipal.EmailAddress));
            }

            // add your own claims if you need to add more information stored on the cookie

            ////this claims list ofr system User roles
            var claims = new List<Claim>();




            List<string> memberships = new List<string>();


            //Anonymous
            ADGroups.Add(new Group { Name = "Everyone" });
            ADGroups.Add(new Group { Name = "Anonymous" });
            foreach (Group item in ADGroups)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Name.ToString()));
                memberships.Add(item.Name.ToString());
            }

            if (claims.Count > 0)
                identity.AddClaims(claims);

            CMS.User._memberships = memberships;

            return identity;

        }



        //////////////////////////// new libraries

        public static ILdapConnection _conn;
        //public void authenticateUser2()
        //{
        //    var ADHost = "ad01.devfasah.sa";
        //    var saslRequest = new SaslDigestMd5Request("user1", "P@ssw0rd", "devfasah.sa", ADHost);
        //    using (var conn = new LdapConnection())
        //    {
        //        try
        //        {
        //            conn.Connect(ADHost, 389);
        //            conn.StartTls();
        //            conn.Bind(saslRequest);
        //            Console.WriteLine($"[{conn.AuthenticationMethod}] {conn.AuthenticationDn}");
        //        }
        //        finally
        //        {
        //            if (conn.Tls)
        //            {
        //                conn.StopTls();
        //            }
        //        }
        //    }
        //}


        public Loginresult ValidateUserWithDefaultObject(string username, string password)
        {
            Loginresult loginresult = new Loginresult();
            string userDn = $"{username}@{"devfasah.sa"}";

            using (var connection = new LdapConnection { SecureSocketLayer = false })
            {
                connection.Connect("ad01.devfasah.sa", 389);
                connection.Bind(userDn, password);
                if (connection.Bound)
                {
                    //we need to creat default identity user


                    //var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                    //                new Claim(ClaimTypes.NameIdentifier, username),
                    //                new Claim(ClaimTypes.Name, userDn)
                    //                // other required and custom claims
                    //           }, "DefaulAuthenticationUser"));


                    var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);


                    List<string> memberships = new List<string>();
                    var claims = new List<Claim>();

                    //Anonymous
                    ADGroups.Add(new Group { Name = "Everyone" });
                    ADGroups.Add(new Group { Name = "Anonymous" });
                    foreach (Group item in ADGroups)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item.Name.ToString()));
                        memberships.Add(item.Name.ToString());
                    }

                    if (claims.Count > 0)
                        identity.AddClaims(claims);

                    CMS.User._memberships = memberships;

                    loginresult.claimsid = identity;

                    return loginresult;



                }
            }


            return loginresult;

        }

        public async Task<Loginresult> ValidateUserUsinLdapApi(string username, string password)
        {

            Loginresult loginresult = new Loginresult();


            AuthorizationLdap _LdapApiAuthenticationService = new AuthorizationLdap();
            string json = await _LdapApiAuthenticationService.getUserRoles(username, password);

            if (!string.IsNullOrEmpty(json))
            {


                string Roles = json.Replace("[", "").Replace("]", "");  // now you have an array of 3 strings
                var RolesArray = Roles.Split(',');  // now you have the same as in the first line

                foreach (string role in RolesArray)
                {

                    ADGroups.Add(new Group { Name = role.Replace("\r\n", string.Empty).Replace("\"", string.Empty).Trim() });
                }
                var identity = new ClaimsIdentity(MyAuthentication.ApplicationCookie, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);


                List<string> memberships = new List<string>();
                var claims = new List<Claim>();

                //Anonymous
                ADGroups.Add(new Group { Name = "Everyone" });
                ADGroups.Add(new Group { Name = "Anonymous" });
                foreach (Group item in ADGroups)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item.Name.ToString()));
                    memberships.Add(item.Name.ToString());
                }

                if (claims.Count > 0)
                    identity.AddClaims(claims);

                CMS.User._memberships = memberships;
                CMS.User.username = username;
                loginresult.claimsid = identity;

                return loginresult;
            }
            else
            {
                return loginresult;
            }









        }

        public void UnValidateUser()
        {

            Loginresult loginresult = new Loginresult();


            AuthorizationLdap _LdapApiAuthenticationService = new AuthorizationLdap();


            var identity = new ClaimsIdentity();


            List<string> memberships = new List<string>();
            var claims = new List<Claim>();

            //Anonymous
            ADGroups = new List<Group>();

            ADGroups.Add(new Group { Name = "Everyone" });
            ADGroups.Add(new Group { Name = "Anonymous" });
            foreach (Group item in ADGroups)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Name.ToString()));
                memberships.Add(item.Name.ToString());
            }

            if (claims.Count > 0)
                identity.AddClaims(claims);

            CMS.User._memberships = null;
            CMS.User.username = "";
        }

        public Loginresult ValidateUserWithDefaultObjectForSerach(string username, string password)
        {
            Loginresult loginresult = new Loginresult();
            string userDn = $"{"user1"}@{"devfasah.sa"}";

            using (var connection = new LdapConnection { SecureSocketLayer = false })
            {
                connection.Connect("ad01.devfasah.sa", 389);
                connection.Bind(userDn, password);
                if (connection.Bound)
                {

                    string searchFilter = string.Empty; //"(cn=" + username + ")";

                    var queue = connection.Search(string.Empty,
LdapConnection.ScopeSub, searchFilter, null, false, (LdapSearchQueue)
null, (LdapSearchConstraints)null);
                    LdapMessage message;
                    while ((message = queue.GetResponse()) != null)
                    {
                        if (message is LdapSearchResult)
                        {
                            //             LdapEntry entry = (LdapSearchResult)message.Entry;

                            //             // Get the attribute set of the entry
                            //             LdapAttributeSet attributeSet = entry.GetAttributeSet();
                            //             System.Collections.IEnumerator ienum =
                            //   attributeSet.GetEnumerator();

                            //             // Parse through the attribute set to get the attributes and
                            //             ///   the corresponding values
                            //             while (ienum.MoveNext())
                            //             {
                            //                 LdapAttribute attribute = (LdapAttribute)ienum.Current;
                            //                 string attributeName = attribute.Name;
                            //                 string attributeVal = attribute.StringValue;
                            //                 Console.WriteLine(attributeName + "value:" +
                            //attributeVal);
                            //             }
                        }
                    }

                    //Procced 

                    //While all the required entries are parsed, disconnect   
                    connection.Disconnect();


                    return loginresult;



                }
            }


            return loginresult;

        }
        public Loginresult ValidateUser(string username, string password)
        {
            Loginresult loginresult = new Loginresult();
            string userDn = $"{username}@{"devfasah.sa"}";

            using (var connection = new LdapConnection { SecureSocketLayer = false })
            {
                connection.Connect("ad01.devfasah.sa", 389);
                connection.Bind(userDn, password);
                if (connection.Bound)
                {


                    var groups = new HashSet<string>();
                    var searchBase = string.Empty;
                    var filter = "(objectCategory=person)(objectClass=user)";
                    var atr = new string[1];
                    atr[0] = "memberOf";
                    var search = connection.Search(searchBase, LdapConnection.ScopeSub, filter, atr, false);
                    while (search.HasMore())
                    {
                        var nextEntry = search.Next();
                        groups.Add(nextEntry.Dn);
                        var childGroups = GetChildren(string.Empty, nextEntry.Dn);
                        foreach (var child in childGroups)
                        {
                            groups.Add(child);
                        }
                    }
                    return loginresult;


                    // string searchBase = "ou=users,o=Company";
                    ///  string searchFilter = "objectClass=inetOrgPerson";
                    //string[] requiredAttributes = { "cn", "sn", "uid" };
                    //ILdapSearchResults lsc = connection.Search(string.Empty,
                    //                    LdapConnection.ScopeSub,
                    //                    string.Empty,
                    //                    null,
                    //                    false);
                    //while (lsc.HasMore())
                    //{

                    //    LdapEntry nextEntry = null;
                    //    try
                    //    {
                    //        nextEntry = lsc.Next();
                    //    }
                    //    catch (LdapException e)
                    //    {
                    //        Console.WriteLine("Error : " + e.LdapErrorMessage);
                    //        continue;
                    //    }


                    //}
                }
            }


            return loginresult;
        }

        public List<string> GetallADGroupsForLdap(List<string> userGroups)
        {
            var RoleValue = new List<string>();

            //var dirEntry = (System.DirectoryServices.DirectoryEntry)mUserPrincipal.GetUnderlyingObject();
            foreach (string groupDN in userGroups)//dirEntry.Properties["memberOf"])
            {
                var parts = groupDN.Replace("CN=", "").Split(',');
                //Add item in AD Group List
                ADGroups.Add(new Group { Name = parts[0] });
                RoleValue.Add(parts[0]);
            }
            return RoleValue;
        }
        static ILdapConnection GetConnection()
        {
            LdapConnection ldapConn = _conn as LdapConnection;
            if (ldapConn == null)
            {
                // Creating an LdapConnection instance
                ldapConn = new LdapConnection() { SecureSocketLayer = true };
                //Connect function will create a socket connection to the server - Port 389 for insecure and 3269 for secure
                ldapConn.Connect("ad01.devfasah.sa", 389);
                //Bind function with null user dn and password value will perform anonymous bind to LDAP server
                ldapConn.Bind(@"devfasah\user1", "P@ssw0rd");
            }
            return ldapConn;
        }
        HashSet<string> SearchForGroup(string groupName)
        {
            var ldapConn = GetConnection();
            var groups = new HashSet<string>();
            var searchBase = string.Empty;
            var filter = $"(&(objectClass=group)(cn={groupName}))";
            var search = ldapConn.Search(searchBase, LdapConnection.ScopeSub, filter, null, false);
            while (search.HasMore())
            {
                var nextEntry = search.Next();
                groups.Add(nextEntry.Dn);
                var childGroups = GetChildren(string.Empty, nextEntry.Dn);
                foreach (var child in childGroups)
                {
                    groups.Add(child);
                }
            }
            return groups;
        }

        object getUserGroupsByYserNameGroup(string userName, ILdapConnection ldapConn)
        {
            ///var ldapConn = GetConnection();
            var users = new HashSet<string>();
            //My domain have 4 DC's
            var searchResults = ldapConn.Search(
                string.Empty,//You can use String.Empty for all domain search. This is example about users
                LdapConnection.ScopeSub,//Use SUB
                string.Empty,// Example of filtering with *. You can use String.Empty to query without filtering
                null, // no specified attributes
                false // return attr and value
                );

            while (searchResults.HasMore())
            {
                var nextEntry = searchResults.Next();
                nextEntry.GetAttributeSet();
                var attr = nextEntry.GetAttribute("mail");

                if (attr == null)
                {
                    users.Add(nextEntry.GetAttribute("distinguishedName").StringValue);
                }
                else
                {
                    users.Add(nextEntry.GetAttribute("mail").StringValue);
                }

            }

            return users;
        }

        static HashSet<string> GetChildren(string searchBase, string groupDn, string objectClass = "group")
        {
            var ldapConn = GetConnection();
            var listNames = new HashSet<string>();
            var filter = "(&amp;(objectCategory=person)(objectClass=user))";
            var atr = new string[1];
            atr[0] = "memberOf";
            var search = ldapConn.Search(searchBase, LdapConnection.ScopeSub, filter, null, false);
            while (search.HasMore())
            {
                var nextEntry = search.Next();
                listNames.Add(nextEntry.Dn);
                var children = GetChildren(string.Empty, nextEntry.Dn);
                foreach (var child in children)
                {
                    listNames.Add(child);
                }
            }
            return listNames;
        }
        void SearchForUser(string company, HashSet<string> groups = null)
        {
            var ldapConn = GetConnection();
            var users = new HashSet<string>();
            string groupFilter = (groups?.Count ?? 0) > 0 ?
                $"(|{string.Join("", groups.Select(x => $"(memberOf={x})").ToList())})" :
                string.Empty;
            var searchBase = string.Empty;
            string filter = $"(&(objectClass=user)(objectCategory=person)(company={company}){groupFilter})";
            var search = ldapConn.Search(searchBase, LdapConnection.ScopeSub, filter, null, false);
            while (search.HasMore())
            {
                var nextEntry = search.Next();
                nextEntry.GetAttributeSet();
                users.Add(nextEntry.Dn);
            }
        }



    }




    public class LdapAuthenticationService
    {

    }







    /*
    public class RevokeAuthenticationEvents : CookieAuthenticationEvents
    {
        private readonly IMemoryCache _cache;
        private readonly ILogger _logger;

        public RevokeAuthenticationEvents(
          IMemoryCache cache,
          ILogger<RevokeAuthenticationEvents> logger)
        {
            _cache = cache;
            _logger = logger;
        }

        public override Task ValidatePrincipal(
          CookieValidatePrincipalContext context)
        {
            var userId = context.Principal.Claims
              .First(c => c.Type == ClaimTypes.Name);

            if (_cache.Get<bool>("revoke-" + userId.Value))
            {
                context.RejectPrincipal();

                _cache.Remove("revoke-" + userId.Value);
                _logger.LogDebug("Access has been revoked for: "
                  + userId.Value + ".");
            }

            return Task.CompletedTask;
        }
    }*/
}
