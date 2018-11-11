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
using Erp.Domain.Repositories;
using WebMatrix.WebData;
using System.Web.Mvc;
namespace Erp.BackOffice.Hubs
{
    public class ErpHub : Hub
    {
        //khai báo hubs để sử dụng cho hàm public static (dùng để gọi trong controller)
        private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<Erp.BackOffice.Hubs.ErpHub>();
     
        #region loadUrl 
        public void loadUrl(string url, int userId)
        {
            var listStaffCanView = StaffClients.Where(x => x.UserLoggedId == userId).Select(x => x.ConnectionID).ToList();
            Clients.Clients(listStaffCanView).loadUrl(url);
        }
        #endregion

        #region SendAlert
        public void SendAlert(int receiverUserId, int senderUserId, string title)
        {
            UserRepository userRepository = new UserRepository(new Domain.ErpDbContext());
            var senderUser = userRepository.GetUserById(senderUserId);
            var ProfileImage = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(senderUser.ProfileImage, "Staff", "user");
            var listStaffCanView = StaffClients.Where(x => ("," + receiverUserId + ",").Contains("," + x.UserLoggedId + ",")).Select(x => x.ConnectionID).ToList();
            //chuẩn bị nội dung của alert hiển thị kèm thông báo
            string message = "<span class=\"blue\"><b>"
                  + senderUser.FullName + "</b></span>" + title + " ...</span><span class=\"msg-time\"><i class=\"ace-icon fa fa-clock-o\"></i> "
                  + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + "</span></span></a>";
            //gửi alert kèm thông báo cho người dùng thấy.
            Clients.Clients(listStaffCanView).alert(message, ProfileImage);
        }
        #endregion

        #region Tạo mới notifications sử dụng trực tiếp trong view
        public void Send(int id, string message)
        {
            InternalNotificationsRepository notificationsRepository = new InternalNotificationsRepository(new Domain.Staff.ErpStaffDbContext());
            var q = notificationsRepository.GetInternalNotificationsById(id);

            var listStaffCanView = StaffClients.Where(x => ("," + q.PlaceOfReceipt + ",").Contains("," + x.UserLoggedId + ",")).Select(x => x.ConnectionID).ToList();

            Clients.Clients(listStaffCanView).addNotification(id, message);
            //Clients.All.addNotification(id, message);
        }
        #endregion

        #region Tạo mới notifications sử dụng trực tiếp trong controller
        public static void CreateNotifications(int? Id, string title,string CreateUserName, string ProfileImage,string ModuleName,DateTime? CreateDate,int? IdTask,int? SendUserId)
        {
           ProfileImage = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(ProfileImage, "Staff", "user");
            var listStaffCanView = StaffClients.Where(x => ("," + SendUserId + ",").Contains("," + x.UserLoggedId + ",")).Select(x => x.ConnectionID).ToList();
            var content = Contentmessage(Id, IdTask, ProfileImage, title, CreateUserName, CreateDate, "Detail", ModuleName);
            //chuẩn bị nội dung của alert hiển thị kèm thông báo
            string message = "<span class=\"blue\"><b>"
                  + CreateUserName + "</b></span> <a href=\"/" + ModuleName + "/" + "Detail" + "/"
                  + Id + "\">" +" " +title + " ...</a></span><span class=\"msg-time\"><i class=\"ace-icon fa fa-clock-o\"></i> "
                  + CreateDate.Value.ToString("HH:mm dd/MM/yyyy") + "</span></span></a>";
            //gửi thông báo đến notifications của người nhận
            hubContext.Clients.Clients(listStaffCanView).addNotification(IdTask, content);
            //gửi alert kèm thông báo cho người dùng thấy.
            hubContext.Clients.Clients(listStaffCanView).alert(message, ProfileImage);
        }
        #endregion

        #region SendAlert sử dụng trực tiếp trong controller
        public static void SendAlerts(int receiverUserId, int senderUserId, string title)
        {
            UserRepository userRepository = new UserRepository(new Domain.ErpDbContext());
            var senderUser = userRepository.GetUserById(senderUserId);
            var ProfileImage = Erp.BackOffice.Helpers.Common.KiemTraTonTaiHinhAnh(senderUser.ProfileImage, "Staff", "user");
            var listStaffCanView = StaffClients.Where(x => ("," + receiverUserId + ",").Contains("," + x.UserLoggedId + ",")).Select(x => x.ConnectionID).ToList();
            //chuẩn bị nội dung của alert hiển thị kèm thông báo
            string message = "<span class=\"blue\"><b>"
                  + senderUser.FullName + "</b></span>" + title + " ...</span><span class=\"msg-time\"><i class=\"ace-icon fa fa-clock-o\"></i> "
                  + DateTime.Now.ToString("HH:mm dd/MM/yyyy") + "</span></span></a>";
            //gửi alert kèm thông báo cho người dùng thấy.
            hubContext.Clients.Clients(listStaffCanView).alert(message, ProfileImage);
        }
        #endregion

        #region Contentmessage nội dung tin nhắn gửi di
        public static string Contentmessage(int? Id, int? notificationId, string ProfileImage, string Titles, string CreateUserName, DateTime? CreateDate, string ActionName, string ModuelName)
        {
            string Contentmessage = "<li id=\"notifications_item_" + notificationId + "\" style=\"background-color:#ECF2F7 !important\" class=\"checkallseen_color\">"

                   +"<div class=\"notifications-action pull-right\">"
                   +"<a onclick=\"CheckDisable(@item.Id)\" class=\"red pull-right\" data-rel=\"tooltip\" title=\"\" data-placement=\"left\" data-original-title=\"Ẩn thông báo\">"
                   +"<i class=\"ace-icon fa fa-times\"></i></a>"
                   +"<a onclick=\"CheckSeen(@item.Id)\" class=\"blue pull-right\" data-rel=\"tooltip\" title=\"\" data-placement=\"left\" data-original-title=\"Đánh dấu đã đọc thông báo\">"
                   +"<i class=\"ace-icon fa fa-check-circle\"></i></a></div>"

                   +"<div style=\"display:initial\">"
                   +"<img src=\""+ ProfileImage + "\"class=\"msg-photo\">"
                   +"<div class=\"msg-body\">"

                   +"<a href=\"/" + ModuelName + "/" + ActionName + "/"+ Id + "\">"
                   +"<span class=\"msg-title\"><span class=\"blue\"><b>"
                   + CreateUserName + "</b></span>" +" "+ Titles + " ...</span></a>"

                   +"<span class=\"msg-time\"><i class=\"ace-icon fa fa-clock-o\"></i> "
                   + CreateDate.Value.ToString("HH:mm dd/MM/yyyy") + "</span></div></div></li>";
            return Contentmessage;
        }
        #endregion

        public static List<StaffClient> StaffClients = new List<StaffClient>();

        public override Task OnConnected()
        {
            //lấy list user đang đăng nhập
            var userLoggedId = Helpers.Common.CurrentUser.Id;
            //lấy nhân viên dựa vào user.
            //var GetStaffByUserId = Helpers.Common.GetStaffByUserId(userLoggedId);
            //var staffId = GetStaffByUserId == null ? 0 : GetStaffByUserId.Id;
            if (StaffClients.Where(x => x.UserLoggedId == userLoggedId).FirstOrDefault() == null)
            {
                StaffClient user = new StaffClient() { Name = Context.User.Identity.Name,ConnectionID=Context.ConnectionId, UserLoggedId = userLoggedId};
                StaffClients.Add(user);
                Clients.Others.userConnected(user.UserLoggedId);
            }
            return base.OnConnected();
        }
        public override Task OnReconnected()
        {

            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            //var a = GlobalHost.Configuration.ConnectionTimeout;
            //var b = GlobalHost.Configuration.DisconnectTimeout;
            //var c = GlobalHost.Configuration.KeepAlive;

            if (stopCalled)
            {
                if (StaffClients.Any(x => x.ConnectionID == Context.ConnectionId))
                {
                    StaffClient user = StaffClients.First(x => x.ConnectionID == Context.ConnectionId);

                    //xoa khoi danh sach cac user dang dang nhap
                    StaffClients.RemoveAll(x => x.ConnectionID == Context.ConnectionId);

                    user.ConnectionID = null;
                    int count = 5;
                    bool isDisconnected = true;
                    while (count > 0)
                    {
                        //user = Users.First(x => x.ConnectionID == Context.ConnectionId);
                        Clients.All.addNewMessageToPage(user.UserLoggedId.ToString(), "ConnectionID: " + user.ConnectionID + "; Context.ConnectionId: " + Context.ConnectionId);
                        if (string.IsNullOrEmpty(user.ConnectionID))
                        {
                            Thread.Sleep(1000);
                            count--;
                        }
                        else
                        {
                            isDisconnected = false;
                            break;
                        }
                    }

                    if (isDisconnected)
                    {
                        Clients.Others.userLeft(user.UserLoggedId);
                        StaffClients.Remove(user);
                    }
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        #region ẩn nút chụp hình ở server khi client đang sử dụng
        public static void ShowOrHiddenCamera(bool show,string status)
        {
            Erp.BackOffice.Helpers.Common.SetSetting("status_camera_server", status);
            var listStaffCanView = StaffClients.Select(x => x.ConnectionID).ToList();
          
            //gửi alert kèm thông báo cho người dùng thấy.
            hubContext.Clients.Clients(listStaffCanView).CloseCamera(show);
        }
        #endregion
    }

    public class StaffClient
    {
        public string Name;
        public string ConnectionID;
        //  public int StaffId;
        public int UserLoggedId;
    }
}