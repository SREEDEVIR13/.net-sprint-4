using RB.Core.Application.DTOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.Interface
{
    public interface IRequestHandling
    {
        public UserResponseDTO RequestHandle(RequestHandlerDTO request);
    }
}
