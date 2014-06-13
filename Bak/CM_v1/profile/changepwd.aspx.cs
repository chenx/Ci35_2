using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class profile : System.Web.UI.Page
{
    private string db_old_pwd_hash;
    private string old_pwd;
    private string new_pwd;
    private string new_pwd2;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["userid"] == null || Session["userid"] == "")
        {
            this.msg.Text = "Not a valid user.";
            return;
        }
        string ID = Session["userid"].ToString();

        if (this.IsPostBack)
        {
            try
            {
                this.msg.Text = "";
                this.retrievePostVal();
                this.retrieve(ID);
                //Response.Write(this.db_old_pwd_hash + "=?=" + this.old_pwd );

                if (this.db_old_pwd_hash != this.old_pwd) {
                    this.msg.Text = "<p><font color='red'>Invalid old password.</font></p>";
                }
                else if (this.new_pwd != this.new_pwd2)
                {
                    this.msg.Text = "<p><font color='red'>New passwords not match.</font></p>";
                }
                else
                {
                    string check = check_pwd(this.new_pwd);
                    if (check != "")
                    {
                        this.msg.Text = "<p><font color='red'>" + check + ".</font></p>";
                    }
                    else
                    {
                        this.update(ID);
                        this.msg.Text = "<p><font color='green'>Your password has been updated.</font> </p>";
                    }
                }
            }
            catch (Exception ex)
            {
                this.msg.Text = "<p><font color='red'>" + ex.Message + "</font></p>";
            }
        }
        else
        {
            this.msg.Text = "";
        }
    }

    // check if password is good enough
    private string check_pwd(string s) {
        if (s.Length < 6) {
            return "Password must be at least 6 characters in length.";
        }
        return "";
    }

 
    private void retrievePostVal()
    {
        if (this.IsPostBack) {
            this.old_pwd = this.MD5( ClsUtil.getPostVal(Request["txtOldPwd"]) );
            this.new_pwd = ClsUtil.getPostVal(Request["txtNewPwd"]);
            this.new_pwd2 = ClsUtil.getPostVal(Request["txtNewPwd2"]);
        }
    }

    private void retrieve(string ID) {
        string sql = "SELECT passwd FROM [User] WHERE ID = " + ClsUtil.sqlEncode(ID);
        //Response.Write(sql);
        byte[] b = ClsUtil.Instance().getDBVarbinary(sql);
        this.db_old_pwd_hash = BitConverter.ToString(b).Replace("-", string.Empty); //System.Text.Encoding.ASCII.GetString( b );
    }

    private string MD5(string s) {
        return FormsAuthentication.HashPasswordForStoringInConfigFile(s, "MD5");
    }

    void update(string ID)
    {
        if (ID == "") return;

        ClsUtil u = ClsUtil.Instance();
        string strQuery = "UPDATE [User] SET passwd = HASHBYTES('MD5', '" + this.new_pwd + "') WHERE ID = " + ID;
        if (ClsUtil._DEBUG) Response.Write(strQuery);

        string strConn = u.strConn();
        SqlConnection conn = null;

        try
        {
            conn = new SqlConnection(strConn);
            SqlCommand comm = new SqlCommand(strQuery, conn);

            conn.Open();
            comm.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (conn != null) conn.Close();
        }
    }

}
