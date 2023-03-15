using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using static The_fifth_group_FinalProject.Models.Employee;

public class EmployeesController : Controller
{
    private readonly IConfiguration _config;

    public EmployeesController(IConfiguration config)
    {
        _config = config;
    }

    public IActionResult Index()
    {
        return View();
    }

    public ActionResult Login()
    {
        return View();
    }

    /// <summary>
    /// 執行登入
    /// </summary>
    /// <param name="inModel"></param>
    /// <returns></returns>
    public ActionResult DoLogin(DoLoginIn inModel)
    {
        DoLoginOut outModel = new DoLoginOut();

        // 檢查輸入資料
        if (string.IsNullOrEmpty(inModel.Account) || string.IsNullOrEmpty(inModel.Password))
        {
            outModel.ErrMsg = "請輸入資料";
        }
        else
        {
            SqlConnection conn = null;

            try
            {
                // 資料庫連線
                string connStr = _config.GetConnectionString("TheFifthGroupOfTopics");
                conn = new SqlConnection();
                conn.ConnectionString = connStr;
                conn.Open();

                // 將密碼轉為 SHA256 雜湊運算(不可逆)
                string salt = inModel.Account.Substring(0, 1).ToLower(); //使用帳號前一碼當作密碼鹽
                SHA256 sha256 = SHA256.Create();
                byte[] bytes = Encoding.UTF8.GetBytes(salt + inModel.Password); //將密碼鹽及原密碼組合
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder result = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    result.Append(hash[i].ToString("X2"));
                }
                string CheckPwd = result.ToString(); // 雜湊運算後密碼

                // 檢查帳號、密碼是否正確
                string sql = "select * from Employees where Account = @Account and Password = @Password";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;
                cmd.Connection = conn;

                // 使用參數化填值
                cmd.Parameters.AddWithValue("@Account", inModel.Account);
                cmd.Parameters.AddWithValue("@Password", CheckPwd); // 雜湊運算後密碼

                // 執行資料庫查詢動作
                SqlDataAdapter adpt = new SqlDataAdapter();
                adpt.SelectCommand = cmd;
                DataSet ds = new DataSet();
                adpt.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    // 有查詢到資料，表示帳號密碼正確

                    // 將登入帳號記錄在 Session 內
                    HttpContext.Session.SetString("Account", inModel.Account);
                    HttpContext.Session.SetString("Role", "Admin");
                    outModel.ResultMsg = "登入成功";
                    
                }
                else
                {
                    // 查無資料，帳號或密碼錯誤
                    outModel.ErrMsg = "帳號或密碼錯誤";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    //關閉資料庫連線
                    conn.Close();
                    conn.Dispose();
                }
            }
        }

        // 輸出json
        return Json(outModel);
    }
    public ActionResult Logout()
    {
        // Clear the session
        HttpContext.Session.Clear();

        // Redirect to the Login page
        return RedirectToAction("Login");
    }
}



