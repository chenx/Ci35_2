using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for ClsUser
/// </summary>
public class ClsUser
{
    public ClsUser()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string writeUserTypeList(string id, string name, string value) {
        string s = "<option value=''>-- Selecte One --</option>";

        string selected;
        
        selected = (value == "1" ? " selected" : "");
        s += "<option value='1'" + selected + ">Admin</option>";

        selected = (value == "2" ? " selected" : "");
        s += "<option value='2'" + selected + ">User</option>";

        s = "<select id='" + id + "' name='" + name + "'>" + s + "</select>";

        return s;
    }

    public static string getUserType(string type_id) {
        return ClsUtil.Instance().getScalar("SELECT name FROM UserType WHERE ID = '" + type_id + "'");
    }


    public static string writeUserStatusList(string id, string name, string value)
    {
        string s = "<option value=''>-- Selecte One --</option>";

        string selected;

        selected = (value == "False" ? " selected" : "");
        s += "<option value='0'" + selected + ">False</option>";

        selected = (value == "True" ? " selected" : "");
        s += "<option value='1'" + selected + ">True</option>";

        s = "<select id='" + id + "' name='" + name + "'>" + s + "</select>";

        return s;
    }

    public static string getUserNameById(string ID) {
        string s = "SELECT login FROM [User] WHERE ID = " + ClsUtil.sqlEncode(ID);
        return ClsUtil.Instance().getScalar(s);
    }
}
