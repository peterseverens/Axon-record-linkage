using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace axon_console
{
    public class dataCheckResults
    {
        public UInt64 Ns { get; set; }
        public UInt64 Nr { get; set; }
        public UInt16 Vc0 { get; set; }
        public UInt16 Vc { get; set; }
        public UInt16 Vc1 { get; set; }
        public UInt64 rpos { get; set; }
        public string result { get; set; }    
    }
}

namespace axon_console
{
    public class dataReadResults
    {
        public UInt64 Ns { get; set; }
        public UInt64 Nr { get; set; }
        
        public string result { get; set; }
    }
}