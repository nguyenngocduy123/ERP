using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Erp.BackOffice.Areas.Administration.Models
{
    [Serializable]
    public class ExceptionActionModel
    {
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ActionName { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string ControllerName { get; set; }

        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string AreaName { get; set; }
    }
}