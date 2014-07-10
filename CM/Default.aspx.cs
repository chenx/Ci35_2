using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.Services;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ClsAuth.IsUser())
        {
            btnLogin.Visible = false;
        }
        else {
            ((TextBox)btnLogin.FindControl("UserName")).Width = 150;
            ((TextBox)btnLogin.FindControl("Password")).Width = 150; // Otherwise this is shorter in IE.
            //((TextBox)btnLogin.FindControl("UserName")).Focus();
            SetFocus(btnLogin.FindControl("UserName"));
        }
    }

    protected void btnLogin_Authenticate(object sender, AuthenticateEventArgs e)
    {
        if (this.doLogin(btnLogin.UserName.ToString().Trim(), btnLogin.Password.ToString().Trim())) {
            e.Authenticated = true;
            btnLogin.Visible = false;
            Label1.Text = "Successfully Logged In.";
            Label1.ForeColor = System.Drawing.Color.Green;
            Response.Redirect("Default.aspx");
        }
        else {
            e.Authenticated = false;
            btnLogin.FailureText = ""; // Hide default message display (inside the login box, looks not nice).

            Label1.Text = "<p>Your login attemp was not successful. Please try again.</p>";
            Label1.ForeColor = System.Drawing.Color.Red;
        }
    }

    /// <summary>
    /// Use data reader, read the first row. Don't check whether there are extra rows.
    /// </summary>
    /// <param name="UserName"></param>
    /// <param name="Password"></param>
    /// <returns></returns>
    private bool doLogin(string UserName, string Password)
    {
        bool ok = false;

        try
        {
            string strConn = new ClsDB().strConn();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string strQuery = "SELECT ID, login, gid, email FROM [User] WHERE login = @login AND passwd=HASHBYTES('MD5', @pwd) AND disabled = '0'";

                SqlCommand comm = new SqlCommand(strQuery, conn);
                comm.Parameters.Add("@login", SqlDbType.VarChar, 50).Value = UserName;
                comm.Parameters.Add("@pwd", SqlDbType.VarChar, 50).Value = Password;

                if (ClsDB.DEBUG()) { Response.Write("query: " + strQuery); }
                conn.Open();
                using (SqlDataReader sdr = comm.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        Session["userid"] = sdr["ID"].ToString();
                        Session["username"] = sdr["login"].ToString();
                        Session["role"] = getUserRole(sdr["gid"].ToString());
                        Session["email"] = sdr["email"].ToString();
                        ok = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ClsUtil.DEBUG()) Response.Write("Error: " + ex.Message);
        }

        return ok;
    }

    // This also works and is secure.
    private bool doLogin_bak(string UserName, string Password)
    {
        bool ok = false;

        try {
            string strConn = new ClsDB().strConn();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                string strQuery = "SELECT ID, login, gid, email FROM [User] WHERE login=" + ClsDB.sqlEncode(UserName) +
                    " AND passwd=HASHBYTES('MD5', " + ClsDB.sqlEncode(Password) + ") AND disabled = '0'";
                SqlCommand comm = new SqlCommand(strQuery, conn);
                if (ClsDB.DEBUG()) { Response.Write("query: " + strQuery); }

                conn.Open();
                using (SqlDataReader sdr = comm.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        Session["userid"] = ClsUtil.getStrVal(sdr["ID"]);
                        Session["username"] = ClsUtil.getStrVal(sdr["login"]);
                        Session["role"] = getUserRole( ClsUtil.getStrVal(sdr["gid"]) );
                        Session["email"] = ClsUtil.getStrVal(sdr["email"]);
                        ok = true;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            if (ClsUtil.DEBUG()) Response.Write("Error: " + ex.Message);
        }

        return ok;
    }
    
    private string getUserRole(string roleID)
    {
        return new ClsDB().ExecuteScalar("SELECT [name] FROM UserGroup WHERE ID = " + ClsDB.sqlEncode(roleID) );
    }
}
