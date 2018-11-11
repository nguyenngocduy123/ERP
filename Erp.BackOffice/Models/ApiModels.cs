using Erp.Domain.Staff.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Models
{
    public class FingerPrintInsertListModel
    {
        public int FPMachineId { get; set; }
        public List<FingerPrint> ListFingerPrint { get; set; }
    }

    public class CheckInOutInsertListModel
    {
        public int FPMachineId { get; set; }
        public List<CheckInOut> ListCheckInOut { get; set; }
    }
}