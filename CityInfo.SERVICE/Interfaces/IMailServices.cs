using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityInfo.SERVICE.Interfaces
{
    public interface IMailServices 
    {
        public void Send(string subject, string msg);
    }
}
