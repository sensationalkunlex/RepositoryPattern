using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Interfaces
{
    public interface IDateTrackable
    {
        DateTime? ModifiedDate { get; set; }

        DateTime CreatedDate { get; set; }

    }
}
