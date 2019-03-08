using ApplicationCore.Entities;
using ApplicationCore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Interfaces
{
    public interface IMakeorCheck
    {
        int ActionById { get; set; }
       
        DateTime? ActionDate { get; set; }

    }
}
