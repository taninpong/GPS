using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GPS
{
    public interface IGetGPS
    {
        void GetGPS();
        bool CheckStatus();
    }
}
