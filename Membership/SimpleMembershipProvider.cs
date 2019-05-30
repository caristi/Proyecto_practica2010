using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Collections.Specialized;
using System.Text;
using Sigacun.Models;


namespace Sigacun.Membership
{
    public class SimpleMembershipProvider : MembershipProvider
    {

        public override void Initialize(string name, NameValueCollection config)
        {
            // Verify that config isn't null
            if (config == null)
                throw new ArgumentNullException("config");

            // Assign the provider a default name if it doesn't have one
            if (String.IsNullOrEmpty(name))
                name = "SimpleMembershipProvider";

            // Add a default "description" attribute to config if the
            // attribute doesn't exist or is empty
            if (string.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Custom SQL Provider");
            }

            // Call the base class's Initialize method
            base.Initialize(name, config);
        }

        public string EncryptPassword(string password)
        {
            //we use codepage 1252 because that is what sql server uses
            byte[] pwdBytes = Encoding.GetEncoding(1252).GetBytes(password);
            byte[] hashBytes = System.Security.Cryptography.MD5.Create().ComputeHash(pwdBytes);
            return Encoding.GetEncoding(1252).GetString(hashBytes);
        }


        public override bool ValidateUser(string username,string password)
        {
            UsuarioModel accountRepository = new UsuarioModel();
            var user = accountRepository.ObtenerUser(username);

            if (string.IsNullOrEmpty(password)) return false;
            if (user == null) return false;

            //string hash = EncryptPassword(password);
            //var email = user.Email;
            var pass = user.usu_contrasena;

            if (user == null) return false;

            if (pass == password)
            {
                //User = user;
                return true;
            }

            return false;
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            UsuarioModel usuarioModel = new UsuarioModel();

            var user = usuarioModel.ObtenerUser(username);

            try {

                UpdateModel(user);
                user.usu_contrasena = newPassword;
                usuarioModel.guardar();

                return true;
            
            }
            catch {

                return false;
            
            }


        }

        private void UpdateModel(usuarios user)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotSupportedException(); }
            set { throw new NotSupportedException(); }
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }


        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotSupportedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotSupportedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotSupportedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotSupportedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotSupportedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotSupportedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }


        public override MembershipUserCollection GetAllUsers(int pageIndex,int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }



        public override bool ChangePasswordQuestionAndAnswer(string username,string password, string newPasswordQuestion,string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteUser(string username,
            bool deleteAllRelatedData)
        {
            throw new NotSupportedException();
        }


        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override MembershipUser GetUser(object providerUserKey,
            bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override string ResetPassword(string username,
            string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotSupportedException();
        }
    }
}
