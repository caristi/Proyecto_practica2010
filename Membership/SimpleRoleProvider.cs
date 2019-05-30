using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using Sigacun.Models;

namespace Sigacun.Membership
{
    public class SimpleRoleProvider: RoleProvider
    {
        UsuarioModel accountRepository = new UsuarioModel();

        public override string[] GetRolesForUser(string username)
        {
            int rolesCount = 0;
            IQueryable<Sigacun.Models.UsuarioModel.RoleViewModel> rolesNames;

            try
            {
                // get roles for this user from DB...
                rolesNames = accountRepository.GetRolesForUser(username);
                rolesCount = rolesNames.Count();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            string[] roles = new string[rolesCount];
            int counter = 0;
            foreach (var item in rolesNames)
            {
                roles[counter] = item.RoleName.ToString();
                counter++;
            }

            return roles;
        }

       public override string ApplicationName
       {
           get
           {
               throw new NotImplementedException();
           }
           set
           {
               throw new NotImplementedException();
           }
       }

       public override bool IsUserInRole(string username, string roleName)
       {
           throw new NotImplementedException();
       }

       public override void AddUsersToRoles(string[] usernames, string[] roleNames)
       {
            throw new NotImplementedException();
        }
       public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
       {
           throw new NotImplementedException();
       }
       public override void CreateRole(string roleName)
       {
           throw new NotImplementedException();
       }
       public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
       {
           throw new NotImplementedException();
       }
       public override bool RoleExists(string roleName)
       {
           throw new NotImplementedException();
       }

       public override string[] GetUsersInRole(string roleName)
       {
           throw new NotImplementedException();
       }
       public override string[] FindUsersInRole(string roleName, string usernameToMatch)
       {
           throw new NotImplementedException();
       }
       public override string[] GetAllRoles()
       {
           throw new NotImplementedException();
       }
    }
}
