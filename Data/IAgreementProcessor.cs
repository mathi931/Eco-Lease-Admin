using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoLease_Admin.Data
{
    public interface IAgreementProcessor
    {
        Task<Agreement> GetFileName(int id);
        Task<Uri> InsertAgreement(Agreement agreement);
        Task<Uri> RemoveAgreement(int id);
    }
}
