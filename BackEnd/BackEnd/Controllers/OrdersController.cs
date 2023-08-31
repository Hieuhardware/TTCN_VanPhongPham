using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MySqlConnector;
using BackEnd.Model;
namespace BackEnd.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        protected MySqlConnection? conn;
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = "SELECT * FROM Orders";
                    var result = conn.Query<Order>(sqlcommand);
                    return StatusCode(200, result);
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
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"SELECT * FROM Orders WHERE cus_id = @Id";
                    var result = conn.Query<Order>(sqlcommand, param: new { Id = id });
                    return StatusCode(200, result);
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
        [HttpGet("newOrder")]
        public IActionResult GetNewOrder()
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    string sqlComand = "SELECT MAX(id) AS max  FROM orders";
                    var result = conn.QueryFirstOrDefault<string>(sqlComand);
                    return StatusCode(200, result);
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
        [HttpPost]
        public IActionResult Post(Order o)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"INSERT INTO Orders(cus_id, order_date, status, notes, address, tel, name) VALUES(@cus_id, @order_date, @status, @notes, @address, @tel, @name)";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@cus_id", o.cus_id);
                    dynamic.Add("@order_date", o.order_date);
                    dynamic.Add("@status", o.status);
                    dynamic.Add("@notes", o.notes);
                    dynamic.Add("@address", o.address);
                    dynamic.Add("@tel", o.tel);
                    dynamic.Add("@name", o.name);
                    var result = conn.Execute(sqlcommand, param: dynamic);
                    return StatusCode(201, result);
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
        [HttpPut("{id}")]
        public IActionResult Put(Order o, int id)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"UPDATE Orders SET status=@status WHERE id=@id";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@id", id);
                    dynamic.Add("@status", o.status);
                    var result = conn.Execute(sqlcommand, param: dynamic);
                    return StatusCode(200, result);
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
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand1 = $"DELETE FROM Order_details WHERE order_id = @Id";
                    var result1 = conn.Execute(sqlcommand1, param: new { Id = id });
                    var sqlcommand = $"DELETE FROM Orders WHERE id = @Id";
                    var result = conn.Execute(sqlcommand, param: new { Id = id });
                    return StatusCode(200, result);
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
        [HttpGet("search/{s}")]
        public IActionResult Search(string s)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@search", s);
                    var sqlCommand = $"SELECT * FROM Order WHERE name LIKE CONCAT('%',@search,'%') OR status LIKE CONCAT('%',@search,'%') ORDER BY id LIMIT 10";
                    var sqlCommand1 = $"SELECT * FROM Order WHERE name LIKE CONCAT('%',@search,'%') OR status LIKE CONCAT('%',@search,'%')";
                    var total = conn.Query<Order>(sqlCommand1, param: para).Count();
                    var result = conn.Query<Order>(sqlCommand, param: para);
                    return StatusCode(200, new
                    {
                        total = total,
                        data = result
                    });
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
        [HttpGet("pagination")]
        public IActionResult Pagination(int PageNumber, string? filter)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    string sqlCommand = "";
                    int x = (PageNumber - 1) * 20;
                    int TotalRecord = 0;
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@filter", filter);
                    para.Add("@PageNumber", x);
                    if (string.IsNullOrEmpty(filter))
                    {
                        sqlCommand = $"SELECT * FROM Orders ORDER BY id LIMIT 20 OFFSET @PageNumber";
                        TotalRecord = conn.Query<Order>("Select * from Orders").Count();
                    }
                    else
                    {
                        sqlCommand = $"SELECT * FROM Orders WHERE name LIKE CONCAT('%',@filter,'%') OR status LIKE CONCAT('%',@filter,'%') ORDER BY id LIMIT 20 OFFSET @PageNumber";
                        TotalRecord = conn.Query<Order>("Select * from Orders WHERE name LIKE CONCAT('%',@filter,'%') OR status LIKE CONCAT('%',@filter,'%')", param: para).Count();
                    }
                    float tp = (float)TotalRecord / 20;
                    int TotalPage = (int)Math.Ceiling(tp);
                    var result = conn.Query<Order>(sqlCommand, param: para);
                    return Ok(new
                    {
                        totalPage = TotalPage,
                        totalRecord = TotalRecord,
                        data = result,
                    });
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
