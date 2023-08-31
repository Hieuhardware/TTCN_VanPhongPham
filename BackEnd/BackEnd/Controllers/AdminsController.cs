using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using BackEnd.Model;
namespace BackEnd.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        protected MySqlConnection? conn;
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"SELECT * FROM Admin WHERE username = @Username AND active = 1";
                    var result = conn.QueryFirstOrDefault<Admin>(sqlcommand, param: new { Username = username });
                    return StatusCode(200, result);
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Có lỗi xảy ra vui lòng kiểm tra lại",
                    error = ex.Message
                });
            }     
        }
        [HttpGet("datatotal")]
        public IActionResult Get(int month, int year)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var orderStringSql = "SELECT COUNT(*)  FROM orders WHERE MONTH(order_date) = @month AND YEAR(order_date) = @year";
                    var orderNumbers = conn.QueryFirstOrDefault<int>(orderStringSql, param: new { month = month, year = year });
                    var productStringSql = "SELECT COUNT(*)  FROM product";
                    var productNumber = conn.QueryFirstOrDefault<int>(productStringSql);
                    var customerStringSql = "SELECT COUNT(*)  FROM customer WHERE MONTH(date_create) = @month AND YEAR(date_create) = @year";
                    var customerNumber = conn.QueryFirstOrDefault<int>(customerStringSql, param: new { month = month, year = year });
                    var moneyStringSql = "SELECT SUM(quantity * price)  FROM orderdetailview WHERE MONTH(order_date) = @month AND YEAR(order_date) = @year AND status = 'Đã giao hàng'";
                    var moneyNumber = conn.QueryFirstOrDefault<int>(moneyStringSql, param: new { month = month, year = year });
                    return StatusCode(200);
                    /*Ok(

                    new
                    {
                        order = orderNumbers,
                        product = productNumber,
                        customer = customerNumber,
                        money = moneyNumber,
                    });
                    */
                }
            }
            catch (Exception ex)
            {

                return StatusCode(500, new
                {
                    message = "Có lỗi xảy ra vui lòng kiểm tra lại",
                    error = ex.Message
                });
            }          
        }
        [HttpGet("datachart/month")]
        public IActionResult GetDataByMonth(int day, int month, int year)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    List<int?> orderList = new List<int?>();
                    List<int?> moneyList = new List<int?>();
                    for (int i = 1; i <= day; i++)
                    {
                        string OrderString = "SELECT COUNT(*)  FROM orders WHERE DAY(order_date) = @day AND MONTH(order_date) = @month AND YEAR(order_date) = @year AND status = 'Đã giao hàng'";
                        int? ordernumber = conn.QueryFirstOrDefault<int?>(OrderString, param: new { day = i, month = month, year = year });
                        orderList.Add(ordernumber);
                        string moneyString = "SELECT SUM(price * quantity)  FROM orderdetailview WHERE DAY(order_date) = @day AND MONTH(order_date) = @month AND YEAR(order_date) = @year AND status = 'Đã giao hàng'";
                        int? moneyNumber = conn.QueryFirstOrDefault<int?>(moneyString, param: new { day = i, month = month, year = year });
                        moneyList.Add(moneyNumber);
                    }
                    return Ok(
                        new
                        {
                            order = orderList,
                            money = moneyList
                        }
                        );
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Có lỗi xảy ra vui lòng kiểm tra lại",
                    error = ex.Message
                });
            }   
        }
        [HttpGet("datachart/year")]
        public IActionResult GetDataByYear(int year)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    List<int?> orderList = new List<int?>();
                    List<int?> moneyList = new List<int?>();
                    for (int i = 1; i <= 12; i++)
                    {
                        string OrderString = "SELECT COUNT(*)  FROM orders WHERE  MONTH(order_date) = @month AND YEAR(order_date) = @year AND status = 'Đã giao hàng'";
                        int? ordernumber = conn.QueryFirstOrDefault<int?>(OrderString, param: new { month = i, year = year });
                        orderList.Add(ordernumber);
                        string moneyString = "SELECT SUM(price * quantity)  FROM orderdetailview WHERE  MONTH(order_date) = @month AND YEAR(order_date) = @year AND status = 'Đã giao hàng'";
                        int? moneyNumber = conn.QueryFirstOrDefault<int?>(moneyString, param: new { month = i, year = year });
                        moneyList.Add(moneyNumber);
                    }
                    return Ok(
                        new
                        {
                            order = orderList,
                            money = moneyList
                        }
                        );
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Có lỗi xảy ra vui lòng kiểm tra lại",
                    error = ex.Message
                });
            }
        }
    }
}
