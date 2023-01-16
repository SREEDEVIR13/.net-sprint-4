using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.DTOModel
{
public class RequestHandlerDTO
    {

        public int HostedRideId { get; set; }
        public List<string> JoineeId { get; set; }
        public int Status { get; set; }
    }
}
