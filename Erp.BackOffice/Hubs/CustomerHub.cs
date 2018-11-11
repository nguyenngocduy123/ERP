using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Erp.Domain.Account.Entities;
using Erp.BackOffice.Staff.Models;
using Erp.Domain.Staff.Repositories;
using WebMatrix.WebData;

namespace Erp.BackOffice.Hubs
{
    public class CustomerHub : Hub
    {
      
        public void loadUrl(string url)
        {
            Clients.Client(url).loadUrl(url);
        }
      
     
        public override Task OnConnected()
        {
          
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {            

            return base.OnDisconnected(stopCalled);
        }
    }
  
}