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
    public class ProductsController : ControllerBase
    {
        protected MySqlConnection? conn;
        [HttpGet]
        public IActionResult Get(string id)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    if (string.IsNullOrEmpty(id))
                    {
                        var sqlcommand = "SELECT * FROM Product";
                        var result = conn.Query<Product>(sqlcommand);
                        return StatusCode(200, result);
                    }
                    else
                    {
                        var sqlcommand = "SELECT * FROM Product WHERE cat_id = @Id";
                        var result = conn.Query<Product>(sqlcommand, param: new { Id = id });
                        return StatusCode(200, result);
                    }
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
                    var sqlcommand = $"SELECT * FROM Product WHERE id = @Id";
                    var result = conn.QueryFirstOrDefault<Product>(sqlcommand, param: new { Id = id });
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
        public IActionResult Post(Product p)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"INSERT INTO Product(cat_id, name, origin, price, image, description, amount, unit) VALUES(@cat_id, @name, @origin, @price, @image, @description, @amount, @unit)";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@cat_id", p.cat_id);
                    dynamic.Add("@name", p.name);
                    dynamic.Add("@origin", p.origin);
                    dynamic.Add("@price", p.price);
                    dynamic.Add("@image", p.image);
                    dynamic.Add("@description", p.description);
                    dynamic.Add("@amount", p.amount);
                    dynamic.Add("@unit", p.unit);
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
        public IActionResult Put(Product p, int id)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"UPDATE Product SET cat_id=@cat_id,name=@name, origin=@origin,price=@price,image=@image,description=@description,amount=@amount,unit=@unit WHERE id=@id";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@id", id);
                    dynamic.Add("@cat_id", p.cat_id);
                    dynamic.Add("@name", p.name);
                    dynamic.Add("@origin", p.origin);
                    dynamic.Add("@price", p.price);
                    dynamic.Add("@image", p.image);
                    dynamic.Add("@description", p.description);
                    dynamic.Add("@amount", p.amount);
                    dynamic.Add("@unit", p.unit);
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
                    var sqlcommand = $"DELETE FROM Product WHERE id = @Id";
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
                    var sqlCommand = $"SELECT * FROM Product WHERE name LIKE CONCAT('%',@search,'%') ORDER BY id LIMIT 10";
                    var sqlCommand1 = $"SELECT * FROM Product WHERE name LIKE CONCAT('%',@search,'%')";
                    var total = conn.Query<Product>(sqlCommand1, param: para).Count();
                    var result = conn.Query<Product>(sqlCommand, param: para);
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
        public IActionResult Pagination(string? id, int PageNumber, string? filter)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    string sqlCommand = "";
                    int x = (PageNumber - 1) * 18;
                    int TotalRecord = 0;
                    DynamicParameters para = new DynamicParameters();
                    para.Add("@id", id);
                    para.Add("@filter", filter);
                    para.Add("@PageNumber", x);
                    if (string.IsNullOrEmpty(filter))
                    {
                        if (string.IsNullOrEmpty(id))
                        {
                            sqlCommand = $"SELECT * FROM Product ORDER BY id LIMIT 18 OFFSET @PageNumber";
                            TotalRecord = conn.Query<Product>("Select * from Product").Count();
                        }
                        else
                        {
                            sqlCommand = $"SELECT * FROM Product WHERE cat_id =@id ORDER BY id LIMIT 18 OFFSET @PageNumber";
                            TotalRecord = conn.Query<Product>("Select * from Product WHERE cat_id = @id", param: para).Count();
                        }

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(id))
                        {
                            sqlCommand = $"SELECT * FROM Product WHERE name LIKE CONCAT('%',@filter,'%') ORDER BY id LIMIT 18 OFFSET @PageNumber";
                            TotalRecord = conn.Query<Product>("Select * from Product WHERE name LIKE CONCAT('%',@filter,'%')", param: para).Count();
                        }
                        else
                        {
                            sqlCommand = $"SELECT * FROM Product WHERE cat_id =@id AND name LIKE CONCAT('%',@filter,'%') ORDER BY id LIMIT 18 OFFSET @PageNumber";
                            TotalRecord = conn.Query<Product>("Select * from Product WHERE cat_id =@id AND name LIKE CONCAT('%',@filter,'%')", param: para).Count();
                        }
                    }
                    float tp = (float)TotalRecord / 18;
                    int TotalPage = (int)Math.Ceiling(tp);
                    var result = conn.Query<Product>(sqlCommand, param: para);
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
