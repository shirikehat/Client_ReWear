using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ReWear.Models
{
    
    public class BasicData
    {
        public BasicData() { }

        
        public List<Status> Statuses { get; set; }
        public List<PrType> PrTypes { get; set; }
    }
}
