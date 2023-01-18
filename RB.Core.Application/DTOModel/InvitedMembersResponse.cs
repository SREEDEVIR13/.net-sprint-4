using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RB.Core.Application.DTOModel
{
public class InvitedMembersResponse
    {
        public string ImageSrc { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Status { get; set; }
        public int InvitationId { get; set; }
    }
}
