using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// Web service for user authentication and providing client information.
/// </summary>
[WebService(Namespace = "http://framework.cm.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class UserService : System.Web.Services.WebService {

    public UserService () {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(Description = @"
Authenticate a user.<br/>
<br/>
<b>Parameters:</b><br/>
string UserName : User's account name<br/>
string Password : User's password<br/>
<br/>
<b>Output:</b><br/>
bool : True if the user is identified; False otherwise.<br/>
Note:
1) Ideally this query should be encrypted.
2) Instead of providing username and password for each web service call as implemented here,
alternatively a token string with timeout constraint can be returned from this UserAuth() call, 
such that the user can use the token string for other web service queries and avoid providing 
username and password each time. 
For this alternative implementation, a database table can be setup with 3 essential columns: 
user_id, token_string, last_access_time. This is like managing the session yourself.
<br/>
<br/>
    ")]
    public bool UserAuth(string UserName, string Password) {
        bool ok = false;

        try
        {
            string strConn = new ClsDB().strConn();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string strQuery = "SELECT COUNT(ID) FROM [User] WHERE login = @login AND passwd=HASHBYTES('MD5', @pwd) AND disabled = '0'";
                SqlCommand comm = new SqlCommand(strQuery, conn);

                comm.Parameters.Add("@login", SqlDbType.VarChar, 50).Value = UserName;
                comm.Parameters.Add("@pwd", SqlDbType.VarChar, 50).Value = Password;

                conn.Open();
                using (SqlDataReader sdr = comm.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        int ct = sdr.GetInt32(0);
                        ok = (ct == 1);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }

        return ok;
    }


    [WebMethod(Description = @"
Returns a list of case_id of clients.<br/>
<br/>
<b>Parameters:</b><br/>
string UserName : User's account name<br/>
string Password : User's password<br/>
<br/>
<b>Output:</b><br/>
List<string> : A list of strings, each represents the case_id of a client.<br/>
<br/>
")]
    public List<string> GetClientCaseIdList(string UserName, string Password)
    {
        List<string> list = new List<string>();

        try
        {
            if (!UserAuth(UserName, Password)) {
                throw new Exception("Unauthorized user");
            }

            string query = "SELECT Case_Id FROM Client WHERE disabled = '0'";
            DataTable dt = new ClsDB().ExecuteDataTable(query);

            for (int i = 0; i < dt.Rows.Count; ++i) {
                list.Add(dt.Rows[i][0].ToString());
            }
        }
        catch (Exception ex) {
            list.Clear();
            list.Add(ex.Message);
        }

        return list;
    }


    [WebMethod(Description = @"
Returns information of all clients.<br/>
<br/>
<b>Parameters:</b><br/>
string UserName : User's account name<br/>
string Password : User's password<br/>
<br/>
<b>Output:</b></br>
List<UserService_Client> : A list of UserService_Client objects, each represents a client's information.<br/>
<br/>
")]
    public List<UserService_Client> GetClientList(string UserName, string Password)
    {
        if (!UserAuth(UserName, Password)) return null;

        List<UserService_Client> list = new List<UserService_Client>();

        try
        {
            string query = "SELECT * FROM Client WHERE disabled = '0'";
            DataTable dt = new ClsDB().ExecuteDataTable(query);

            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                UserService_Client c = new UserService_Client();
                c.Case_Id = ClsUtil.getStrVal(dt.Rows[i]["Case_Id"]);
                c.Client_Type = ClsClient.getClientType( ClsUtil.getStrVal((dt.Rows[i]["Client_Type"])) );
                c.First_Name = ClsUtil.getStrVal(dt.Rows[i]["First_Name"]);
                c.Last_Name = ClsUtil.getStrVal(dt.Rows[i]["Last_Name"]);
                list.Add(c);
            }
        }
        catch (Exception ex)
        {
            list.Clear();
        }

        return list;
    }


    [WebMethod(Description = @"
Returns information of a client given the client's CaseId.<br/>
<br/>
<b>Parameters:</b><br/>
string UserName : User's account name<br/>
string Password : User's password<br/>
string CaseId : Client's case_id<br/>
<br/>
<b>Output:</b><br/>
UserService_Client : an object that represents a client's information.<br/>
<br/>
")]
    public UserService_Client GetClient(string UserName, string Password, string CaseId)
    {
        if (!UserAuth(UserName, Password)) return null;

        try
        {
            string query = "SELECT * FROM Client WHERE disabled = '0' AND Case_Id = " + ClsDB.sqlEncode(CaseId);
            DataTable dt = new ClsDB().ExecuteDataTable(query);

            if (dt.Rows.Count == 1)
            {
                DataRow r = dt.Rows[0];
                UserService_Client c = new UserService_Client();
                c.Case_Id = ClsUtil.getStrVal(r["Case_Id"]);
                c.Client_Type = ClsClient.getClientType(ClsUtil.getStrVal((r["Client_Type"])));
                c.First_Name = ClsUtil.getStrVal(r["First_Name"]);
                c.Last_Name = ClsUtil.getStrVal(r["Last_Name"]);
                return c;
            }
        }
        catch (Exception ex)
        {
        }

        return null;
    }
}

