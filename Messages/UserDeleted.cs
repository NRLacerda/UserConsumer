using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserConsumer.Messages
{
    public class UserDeleted
    {
        // isso deve ser ajustado para um guid, esta ocmo int para teste
        public int UserID { get; set; }
    }
}
