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
            ClsUtil u = ClsUtil.Instance();

            string strQuery;
            if (action == "1") { // delete
                strQuery = "DELETE FROM [User] WHERE ID = " + ID;
            } else if (action == "2") { // disable
                strQuery = "UPDATE [User] SET disabled = '1', last_update_user_id = " +
                    ClsUtil.sqlEncode(context.Session["userid"].ToString()) + ", last_update_datetime = " +
                    ClsUtil.sqlEncode(DateTime.Now.ToString()) + " WHERE ID = " + ID;
            }
            else if (action == "3") { // enable
                strQuery = "UPDATE [User] SET disabled = '0', last_update_user_id = " +
                    ClsUtil.sqlEncode(context.Session["userid"].ToString()) + ", last_update_datetime = " +
                    ClsUtil.sqlEncode(DateTime.Now.ToString()) + " WHERE ID = " + ID;
            }
            else {
                writeResponse(context, "0");
                return;
            }

            if (ClsUtil._DEBUG) writeResponse(context, strQuery);

            string strConn = u.strConn();
            SqlConnection conn = null;

            try
            {
                conn = new SqlConnection(strConn);
                SqlCommand comm = new SqlCommand(strQuery, conn);

                conn.Open();
                comm.ExecuteNonQuery();
                writeResponse(context, "1");
            }
            catch (Exception ex)
            {
                writeResponse(context, "0");
                throw new Exception(ex.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
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