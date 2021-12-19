using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Exception1
{
    public class InsertedWrongAdressException:Exception
    {
        public InsertedWrongAdressException(string message) : base(message)
        {

        }
    }
}
