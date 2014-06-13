using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
/// Database functions.
/// </summary>
public class ClsDB
{
    private static bool _DEBUG;
    private string _strConn;

    /// <summary>
    /// Use default connection string in web.config
    /// </summary>
    public ClsDB()
    {
        _strConn = ConfigurationManager.ConnectionStrings["connCM"].ConnectionString;
        _DEBUG = ClsUtil.StrToBool(ConfigurationSettings.AppSettings["DEBUG_DB"]);
    }

    /// <summary>
    /// Use customized connection string.
    /// </summary>
    /// <param name="connName"></param>
    public ClsDB(string connName)
    {
        _strConn = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
        _DEBUG = ClsUtil.StrToBool(ConfigurationSettings.AppSettings["DEBUG_DB"]);
    }

    public string strConn()
    {
        return _strConn;
    }

    public static bool DEBUG() {
        return _DEBUG;
    }

    /// <summary>
    /// Assumption: query is to get count.
    /// </summary>
    /// <param name="strQuery"></param>
    /// <returns></returns>
    public int getCount(string strQuery)
    {
        string s = ExecuteScalar(strQuery);
        return Convert.ToInt32(s);
    }

    /// <summary>
    /// Used for Update, Delete and Insert queries that do not return any value.
    /// </summary>
    /// <param name="strQuery"></param>
    public void ExecuteNonQuery(string strQuery) 
    {
        if (ClsUtil.DEBUG()) { 
            // output query string.
        }

        string strConn = this.strConn();
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);

        try
        {
            conn.Open();
            comm.ExecuteNonQuery(); 
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            conn.Close();
        }
    }

    /// <summary>
    /// Return a scalar value from the query, which is the first variable in result set.
    /// </summary>
    /// <param name="strQuery"></param>
    /// <returns></returns>
    public string ExecuteScalar(string strQuery)
    {
        string ret = "";
        string strConn = this.strConn();
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                ret = sdr[0].ToString();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (sdr != null) sdr.Close();
            conn.Close();
        }

        return ret;
    }

    public void ExecuteReader(string strQuery) { 
    
    }

    /// <summary>
    /// The field is of type varbinary, e.g., for password field.
    /// </summary>
    /// <param name="strQuery"></param>
    /// <returns></returns>
    public byte[] ExecuteVarbinary(string strQuery)
    {
        byte[] ret = null;
        string strConn = this.strConn();
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);
        SqlDataReader sdr = null;

        try
        {
            conn.Open();
            sdr = comm.ExecuteReader();
            if (sdr.Read())
            {
                ret = (byte[])sdr[0];
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            if (sdr != null) sdr.Close();
            conn.Close();
        }

        return ret;
    }

    /// <summary>
    /// Return a dataset from the query.
    /// </summary>
    /// <param name="strQuery"></param>
    /// <returns></returns>
    public DataSet ExecuteDataSet(string strQuery)
    {
        DataSet ds = new DataSet();
        string strConn = this.strConn();
        SqlConnection conn = new SqlConnection(strConn);
        SqlCommand comm = new SqlCommand(strQuery, conn);

        try
        {
            conn.Open();
            SqlDataAdapter sda = new SqlDataAdapter();
            sda.SelectCommand = comm;
            sda.Fill(ds);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        finally
        {
            conn.Close();
        }

        return ds;
    }

    public DataTable ExecuteDataTable(string strQuery)
    {
        DataSet ds = ExecuteDataSet(strQuery);
        if (ds.Tables.Count > 0) return ds.Tables[0];
        else return null;
    }

    /// <summary>
    /// Encode a variable string to be used in a query.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string sqlEncode(string s)
    {
        if (s == null || s == "") return "''";

        s = s.Replace("'", "''");
        s = "'" + s + "'";
        return s;
    }
}
