using Microsoft.AspNet.SignalR;
using SingalRJs.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SingalRJs
{
    public class akash : Hub
    {
        SqlConnection con;
        DataSet ds;
        DataTable dt;
        SignalREntities _signalRDB;

        

        public void Connect(string userName, string Name, string pass)
        {
            _signalRDB = new SignalREntities();
            LoginTable reg = new LoginTable();
            var id = Context.ConnectionId;
            //reg.Id = Context.ConnectionId;
            reg.UserName = userName;
            reg.Name = Name;
            reg.Password = pass;
            _signalRDB.LoginTables.Add(reg);
            _signalRDB.SaveChanges();

        }
    }
}