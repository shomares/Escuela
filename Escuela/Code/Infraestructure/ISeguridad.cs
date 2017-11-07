using Escuela.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Escuela.Code.Infraestructure
{
    public interface ISeguridad
    {
        bool isPasswordValid(secUser user, string password);
        string Save(secUser entity);
        void Update(secUser entity, string id);
        void Delete(secUser entity);
    }
}
