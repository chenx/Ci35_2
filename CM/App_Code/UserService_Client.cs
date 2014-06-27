using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Used for webservice of prividing a client's information.
/// </summary>
public class UserService_Client
{
    /// <summary>
    /// Client Table fields.
    /// </summary>
    public string Case_Id;
    public string Client_Type;
    public string First_Name;
    public string Last_Name;
    public string Attorney;
    public string Paralegal;
    public string Date_Of_Injury;
    public string Statute_Of_Limitation;
    public string Phone_Number;
    public string Address;
    //public string Date_Of_Birth;
    //public string Social_Security_Number;
    public string Case_Type;
    public string At_Fault_Party;
    public string Settlement_Type;
    public string Settlement_Amount;
    public string Disposition;
    public string Case_Notes;
    public string Date_For_Perspective_Client;

    public UserService_Client()
    {
    
    }
}
