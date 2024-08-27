using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Job
{
    public record CreateJobRequest (
        string JobTitle,
        string JobDescription,
        int departmentId
    );
}
