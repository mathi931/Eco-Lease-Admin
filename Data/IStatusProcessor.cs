using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    public interface IStatusProcessor
    {
        Task<List<Status>> LoadStatuses();
    }
}
