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
    public class CustomersController : ControllerBase
    {
        protected MySqlConnection? conn;
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = "SELECT * FROM Customer";
                    var result = conn.Query<Customer>(sqlcommand);
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
        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"SELECT * FROM Customer WHERE email = @Email";
                    var result = conn.QueryFirstOrDefault<Customer>(sqlcommand, param: new { Email = email });
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
        public IActionResult Post(Customer c)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"INSERT INTO Customer(email, password, name, tel, address, date_create) VALUES(@email, @password, @name, @tel, @address, @date_create)";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@email", c.email);
                    dynamic.Add("@password", c.password);
                    dynamic.Add("@name", c.name);
                    dynamic.Add("@tel", c.tel);
                    dynamic.Add("@address", c.address);
                    dynamic.Add("@date_create", c.date_create);
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
        public IActionResult Put(Customer c, int id)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"UPDATE Customer SET email=@email,password=@password, name=@name,tel=@tel,address=@address, date_create=@date_create WHERE id=@id";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@id", id);
                    dynamic.Add("@email", c.email);
                    dynamic.Add("@password", c.password);
                    dynamic.Add("@name", c.name);
                    dynamic.Add("@tel", c.tel);
                    dynamic.Add("@address", c.address);
                    dynamic.Add("@date_create", c.date_create);
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
                    var sqlcommand = $"DELETE FROM Customer WHERE id = @Id";
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
                    var sqlCommand = $"SELECT * FROM Customer WHERE name LIKE CONCAT('%',@search,'%') ORDER BY id LIMIT 10";
                    var sqlCommand1 = $"SELECT * FROM Customer WHERE name LIKE CONCAT('%',@search,'%')";
                    var total = conn.Query<Customer>(sqlCommand1, param: para).Count();
                    var result = conn.Query<Customer>(sqlCommand, param: para);
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
                        sqlCommand = $"SELECT * FROM Customer ORDER BY id LIMIT 20 OFFSET @PageNumber";
                        TotalRecord = conn.Query<Customer>("Select * from Customer").Count();
                    }
                    else
                    {
                        sqlCommand = $"SELECT * FROM Customer WHERE name LIKE CONCAT('%',@filter,'%') OR email LIKE CONCAT('%',@filter,'%') ORDER BY id LIMIT 20 OFFSET @PageNumber";
                        TotalRecord = conn.Query<Customer>("Select * from Customer WHERE name LIKE CONCAT('%',@filter,'%') OR email LIKE CONCAT('%',@filter,'%')", param: para).Count();
                    }
                    float tp = (float)TotalRecord / 20;
                    int TotalPage = (int)Math.Ceiling(tp);
                    var result = conn.Query<Customer>(sqlCommand, param: para);
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
