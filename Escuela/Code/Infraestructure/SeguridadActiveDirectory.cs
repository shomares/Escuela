using Escuela.Code.Infraestructure;
using Escuela.Types;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Web;

namespace Escuela.Code
{
    public class SeguridadActiveDirectory : ISeguridad
    {
        public bool isPasswordValid(secUser user, string password)
        {
            bool salida = false;
            using (PrincipalContext ps = new PrincipalContext(GetContext()))
            {
                salida = ps.ValidateCredentials(user.cveUser, password);

            }
            return salida;
        }



        public string Save(secUser entity)
        {
            var salida = String.Empty;
            using (PrincipalContext ps = new PrincipalContext(GetContext()))
            {
                UserPrincipal foundUser = UserPrincipal.FindByIdentity(ps, entity.cveUser);
                if (foundUser == null)
                {
                    using (var up = new UserPrincipal(ps, entity.cveUser, entity.password, true))
                    {
                        up.Save();
                        return entity.cveUser;
                    }
                }
            }
            return null;
        }


        public void Update(secUser entity, string id)
        {
            using (PrincipalContext ps = new PrincipalContext(GetContext()))
            {
                UserPrincipal foundUser = UserPrincipal.FindByIdentity(ps, id);
                if (foundUser != null)
                {
                    foundUser.SetPassword(entity.password);
                    foundUser.Save();
                }
                else
                {
                    using (var up = new UserPrincipal(ps, entity.cveUser, entity.password, true))
                    {
                        up.Save();
                    }

                }

            }
        }


        public void Delete(secUser entity)
        {
            using (PrincipalContext ps = new PrincipalContext(GetContext()))
            {
                UserPrincipal foundUser = UserPrincipal.FindByIdentity(ps, entity.cveUser);
                if (foundUser != null)
                {
                    foundUser.Delete();
                }
            }
        }

        private static ContextType GetContext()
        {
            return ContextType.Machine;
        }

    }
}