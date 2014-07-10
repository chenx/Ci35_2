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
    private string _Case_Id;
    private string _Client_Type;
    private string _First_Name;
    private string _Last_Name;
    private string _Attorney;
    private string _Paralegal;
    private string _Date_Of_Injury;
    private string _Statute_Of_Limitation;
    private string _Phone_Number;
    private string _Address;
    //private string Date_Of_Birth;
    //private string Social_Security_Number;
    private string _Case_Type;
    private string _At_Fault_Party;
    private string _Settlement_Type;
    private string _Settlement_Amount;
    private string _Disposition;
    private string _Case_Notes;
    private string _Date_For_Perspective_Client;


    
    public UserService_Client()
    {
    
    }

    public string Case_Id 
    { 
        get { return _Case_Id; } 
        set { _Case_Id = value; } 
    }

    public string Client_Type 
    {
        get { return _Client_Type; }
        set { _Client_Type = value; } 
    }

    public string First_Name
    {
        get { return _First_Name; }
        set { _First_Name = value; } 
    }

    public string Last_Name
    {
        get { return _Last_Name; }
        set { _Last_Name = value; } 
    }

    public string Attorney
    {
        get { return _Attorney; }
        set { _Attorney = value; } 
    }

    public string Paralegal
    {
        get { return _Paralegal; }
        set { _Paralegal = value; } 
    }

    public string Date_Of_Injury
    {
        get { return _Date_Of_Injury; }
        set { _Date_Of_Injury = value; } 
    }

    public string Statute_Of_Limitation
    {
        get { return _Statute_Of_Limitation; }
        set { _Statute_Of_Limitation = value; } 
    }

    public string Phone_Number
    {
        get { return _Phone_Number; }
        set { _Phone_Number = value; } 
    }

    public string Address
    {
        get { return _Address; }
        set { _Address = value; } 
    }

    //public string Date_Of_Birth;

    //public string Social_Security_Number;

    public string Case_Type
    {
        get { return _Case_Type; }
        set { _Case_Type = value; } 
    }

    public string At_Fault_Party
    {
        get { return _At_Fault_Party; }
        set { _At_Fault_Party = value; } 
    }

    public string Settlement_Type
    {
        get { return _Settlement_Type; }
        set { _Settlement_Type = value; } 
    }

    public string Settlement_Amount
    {
        get { return _Settlement_Amount; }
        set { _Settlement_Amount = value; } 
    }

    public string Disposition
    {
        get { return _Disposition; }
        set { _Disposition = value; } 
    }

    public string Case_Notes
    {
        get { return _Case_Notes; }
        set { _Case_Notes = value; } 
    }

    public string Date_For_Perspective_Client
    {
        get { return _Date_For_Perspective_Client; }
        set { _Date_For_Perspective_Client = value; } 
    }
}
