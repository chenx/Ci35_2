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
/// To delete/disable/enable a record.
/// </summary>
public class Registry : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
{
    public void ProcessRequest (HttpContext context) {
        if (! ClsAuth.IsUser() || ! ClsAuth.IsAdmin())
        {
            writeResponse(context, "0");
            return;
        }
        
        string ClientID = context.Request.Form["id"];
        string action = context.Request.Form["action"];

        if (ClientID != "") { 
            string strQuery;
            if (action == "1") { // delete
                strQuery = "DELETE Client WHERE ID = " + ClsDB.sqlEncode(ClientID);
            } 
            else if (action == "2") { // remove/hide
                strQuery = "UPDATE Client SET disabled = '1', last_update_user_id = " +
                    ClsDB.sqlEncode( context.Session["userid"].ToString() ) + ", last_update_datetime = " + 
                    ClsDB.sqlEncode( DateTime.Now.ToString() ) + " WHERE ID = " + ClientID;
            }
            else if (action == "3") { // show
                strQuery = "UPDATE Client SET disabled = '0', last_update_user_id = " +
                    ClsDB.sqlEncode(context.Session["userid"].ToString()) + ", last_update_datetime = " +
                    ClsDB.sqlEncode(DateTime.Now.ToString()) + " WHERE ID = " + ClientID;
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
                if (ClsUtil.DEBUG()) s = ex.Message;
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