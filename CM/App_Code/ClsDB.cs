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
    /// Use default connection string in web.config.
    /// </summary>
    public ClsDB()
    {
        _strConn = ConfigurationManager.ConnectionStrings["connCM"].ConnectionString;
        _DEBUG = ClsUtil.StrToBool(ConfigurationSettings.AppSettings["DEBUG_DB"]);
    }

    /// <summary>
    /// Use customized connection string.
    /// </summary>
    /// <param name="connName">Database connnection string.</param>
    public ClsDB(string connName)
    {
        _strConn = ConfigurationManager.ConnectionStrings[connName].ConnectionString;
        _DEBUG = ClsUtil.StrToBool(ConfigurationSettings.AppSettings["DEBUG_DB"]);
    }

    /// <summary>
    /// Return the query string.
    /// </summary>
    /// <returns>The query string.</returns>
    public string strConn()
    {
        return _strConn;
    }

    /// <summary>
    /// Return whether the DEBUG mode is on/off.
    /// </summary>
    /// <returns>The DEBUG mode value.</returns>
    public static bool DEBUG() {
        return _DEBUG;
    }

    /// <summary>
    /// Assumption: query is to get count.
    /// </summary>
    /// <param name="strQuery">A database query.</param>
    /// <returns>Number of rows in the result set.</returns>
    public int getCount(string strQuery)
    {
        string s = ExecuteScalar(strQuery);
        return Convert.ToInt32(s);
    }

    /// <summary>
    /// Used for Update, Delete and Insert queries that do not return any value.
    /// </summary>
    /// <param name="strQuery">A database query.</param>
    public void ExecuteNonQuery(string strQuery) 
    {
        try
        {
            string strConn = this.strConn();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand comm = new SqlCommand(strQuery, conn);
                conn.Open();
                comm.ExecuteNonQuery();
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Return a scalar value from the query, which is the first variable in result set.
    /// </summary>
    /// <param name="strQuery">A database query.</param>
    /// <returns>Query result string.</returns>
    public string ExecuteScalar(string strQuery)
    {
        string ret = "";

        try
        {
            string strConn = this.strConn();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand comm = new SqlCommand(strQuery, conn);
                conn.Open();
                using (SqlDataReader sdr = comm.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        ret = sdr[0].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return ret;
    }

    /// <summary>
    /// This method is not implemented.
    /// </summary>
    /// <param name="strQuery">A database query.</param>
    public void ExecuteReader(string strQuery) { 
    
    }

    /// <summary>
    /// The field is of type varbinary, e.g., for password field.
    /// </summary>
    /// <param name="strQuery">A database query.</param>
    /// <returns>Query result: a byte[] array.</returns>
    public byte[] ExecuteVarbinary(string strQuery)
    {
        byte[] ret = null;

        try
        {
            string strConn = this.strConn();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand comm = new SqlCommand(strQuery, conn);
                conn.Open();
                using (SqlDataReader sdr = comm.ExecuteReader())
                {
                    if (sdr.Read())
                    {
                        ret = (byte[])sdr[0];
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return ret;
    }

    /// <summary>
    /// Return a dataset from the query.
    /// </summary>
    /// <param name="strQuery">A database query.</param>
    /// <returns>Query result: a DataSet object.</returns>
    public DataSet ExecuteDataSet(string strQuery)
    {
        DataSet ds = new DataSet();

        try
        {
            string strConn = this.strConn();
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                SqlCommand comm = new SqlCommand(strQuery, conn);
                conn.Open();
                SqlDataAdapter sda = new SqlDataAdapter();
                sda.SelectCommand = comm;
                sda.Fill(ds);
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return ds;
    }

    /// <summary>
    /// Return a datatable from the query.
    /// </summary>
    /// <param name="strQuery">A database query.</param>
    /// <returns>Query result: a DataTable object.</returns>
    public DataTable ExecuteDataTable(string strQuery)
    {
        DataSet ds = ExecuteDataSet(strQuery);
        if (ds.Tables.Count > 0) return ds.Tables[0];
        else return null;
    }

    /// <summary>
    /// Encode a variable string to be used in a query.
    /// </summary>
    /// <param name="s">A parameter string.</param>
    /// <returns>Encoded parameter string, which is safe to be used in a database query.</returns>
    public static string sqlEncode(string s)
    {
        if (s == null || s == "") return "''";

        s = s.Replace("'", "''");
        s = "'" + s + "'";
        return s;
    }
}
