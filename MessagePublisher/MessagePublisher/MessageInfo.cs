using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessagePublisher
{
    public record MessageInfo
    (
        string ID,
        string Name,
        string Details
    );
}
