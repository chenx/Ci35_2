<%@ WebHandler Language="C#" Class="Registry" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// To delete certificate record.
/// </summary>
public class Registry : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest (HttpContext context) {
        if (!ClsAuth.IsAdmin())
        {
            writeResponse(context, "0");
            return;
        }
        
        string ID = context.Request.Form["id"];
        string action = context.Request.Form["action"];

        if (ID != "") { 
            string strQuery;
            if (action == "1") { // delete
                strQuery = "DELETE FROM [User] WHERE ID = " + ClsDB.sqlEncode(ID);
            } else if (action == "2") { // disable
                strQuery = "UPDATE [User] SET disabled = '1', last_update_user_id = " +
                    ClsDB.sqlEncode(context.Session["userid"].ToString()) + ", last_update_datetime = " +
                    ClsDB.sqlEncode(DateTime.Now.ToString()) + " WHERE ID = " + ClsDB.sqlEncode(ID);
            }
            else if (action == "3") { // enable
                strQuery = "UPDATE [User] SET disabled = '0', last_update_user_id = " +
                    ClsDB.sqlEncode(context.Session["userid"].ToString()) + ", last_update_datetime = " +
                    ClsDB.sqlEncode(DateTime.Now.ToString()) + " WHERE ID = " + ClsDB.sqlEncode(ID);
            }
            else {
                writeResponse(context, "0");
                return;
            }

            
            try
            {
                new ClsDB().ExecuteNonQuery(strQuery);
                writeResponse(context, "1");
            }
            catch (Exception ex) {
                string s = "0";
                if (ClsDB.DEBUG()) s = ex.Message;
                writeResponse(context, s);
            }
        }
    }

    private void writeResponse(HttpContext context, string v) {
        context.Response.ContentType = "text/plain";
        context.Response.Write(v);
    }
         
    public bool IsReusable {
        get {
            return false;
        }
    }

}