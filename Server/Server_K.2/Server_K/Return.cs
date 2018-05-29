using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server_K
{
   class Return
    {
       private string nume;
        

        public Return(string name)
        {
            this.nume = name;
        }


        public string GetNume()
        {
            return nume;
        }
     }
}
