using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Models
{
    public class UserSessionInfo
    {
        public string UserName { get; set; }
        public string SessionID { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime LastTimeAction { get; set; }
        public string LastActionName { get; set; }
    }
}