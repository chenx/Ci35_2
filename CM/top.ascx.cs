﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.SessionState;

public partial class top : System.Web.UI.UserControl
{
    private string _page_title;
    public string page_title { get { return _page_title; } set { _page_title = value; } }
    
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public string writeMenu()
    {
        string s = "";

        string current = ((page_title == "Home - Welcome") ? " class='current'" : "");
        s += "<li" + current + "><a href='Default3.aspx'>Home</a>";

        current = ((page_title == "About Us") ? " class='current'" : "");
        s += "<li" + current + "><a href='About3.aspx'>About Us</a>";

        //if (isset($_SESSION['role']) && $_SESSION['role'] == "admin") {
        if (Session["role"] != null && Session["role"].ToString() == "admin")
        {
            if (page_title == "Site Admin")
            {
                s += "<li class='current'><a href='#'>Site Admin</a>";
            }
            else
            {
                current = "";
                if (page_title.StartsWith("Site Admin")) current = "class='current'";
                s += "<li $current><a href='adminhome.php'>Site Admin</a>";
            }
            s += "<ul>";
            s += "<li><a href='admin_users.php'>Manage Users</a></li>";
            //$s .= "<li><a href='admin_images.php'>Manage Images</a></li>";
            s += "<li><a href='#' onclick='javascript: open_file(\"admin_images.php\");'>Manage Images</a></li>";
            s += "<li><a href='admin_create_schema.php'>Create Schema For Tables</a></li>";
            s += "<li><a href='admin_dump_table.php'>Dump Contents Of Tables</a></li>";
            s += "<li><a href='admin_backup_db.php'>Backup Database</a></li>";
            s += "</ul>";
            s += "</li>";
        }

        if (Session["username"] != null && Session["username"].ToString() != "")
        {
            if (page_title == "Member Home")
            {
                s += "<li class='current'><a href='#'>Member Home</a></li>";
            }
            else
            {
                s += "<li><a href='home.php'>Member Home</a></li>";
            }

            if (page_title == "My Profile")
            {
                s += "<li class='current'><a href='#'>My Profile</a></li>";
            }
            else
            {
                s += "<li><a href='profile.php'>My Profile</a></li>";
            }

            string t = "<a href='logout.php'>Log out</a>";
            s += "<li>" + t + "</li>";
        }

        s = "<ul id='nav'>" + s + "</ul>";
        return s;
    }
}
