using Erp.BackOffice.Areas.Administration.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Erp.BackOffice.Helpers
{
    public class ExceptionActionHelper
    {
        public static ExceptionActionsModel ReadExceptionActions()
        {
            ExceptionActionsModel exceptionActionsModel = null;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExceptionActionsModel));
            using (TextReader textReader = new StreamReader(HttpContext.Current.Server.MapPath("~/App_Data/ActionExcepted.xml")))
            {
                exceptionActionsModel = (ExceptionActionsModel)xmlSerializer.Deserialize(textReader);
            }
            return exceptionActionsModel;
        }

        public static void WriteExceptionActions(ExceptionActionsModel excptionActions)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(ExceptionActionsModel));
            using (TextWriter textWriter = new StreamWriter(HttpContext.Current.Server.MapPath("~/App_Data/ActionExcepted.xml")))
            {
                xmlSerializer.Serialize(textWriter, excptionActions);
            }
        }
    }
}