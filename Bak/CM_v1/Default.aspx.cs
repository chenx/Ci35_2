using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

public partial class Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (ClsAuth.IsUser())
        {
            btnLogin.Visible = false;
        }
        else {
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
        string strQuery = "SELECT ID, login, gid, email FROM [User] WHERE login=" + ClsUtil.sqlEncode(UserName) +
            " AND passwd=HASHBYTES('MD5', " + ClsUtil.sqlEncode(Password) + ") AND disabled = '0'";

        string strConn = ClsUtil.Instance().strConn(); 
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);

        if (ClsUtil.Instance().DEBUG()) { Response.Write("query: " + strQuery); }

        try
        {
            conn.Open();
            using (SqlDataReader sdr = comm.ExecuteReader()) {
                if (sdr.Read())
                {
                    Session["userid"] = sdr["ID"].ToString();
                    Session["username"] = sdr["login"].ToString();
                    Session["role"] = getRole(sdr["gid"].ToString());
                    Session["email"] = sdr["email"].ToString();
                    return true;
                }            
            }
        }
        catch (Exception ex)
        {
            if (ClsUtil.Instance().DEBUG())
            {
                Response.Write("Error: " + ex.Message);
            }
        }
        finally
        {
            conn.Close();
        }

        return false;
    }


    private string getRole(string roleID) {
        return ClsUtil.Instance().getScalar("SELECT [name] FROM UserGroup WHERE ID = '" + roleID + "'");
    }
}
