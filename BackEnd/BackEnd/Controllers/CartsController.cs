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
    public class CartsController : ControllerBase
    {
        protected MySqlConnection? conn;
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = "SELECT * FROM CartView";
                    var result = conn.Query<Cart>(sqlcommand);
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
                    var sqlcommand = $"SELECT * FROM CartView WHERE cus_id = @Id";
                    var result = conn.Query<Cart>(sqlcommand, param: new { Id = id });
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
        public IActionResult Post(Cart c)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"INSERT INTO Cart(cus_id, prod_id, quantity) VALUES(@cus_id, @prod_id, @quantity)";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@cus_id", c.cus_id);
                    dynamic.Add("@prod_id", c.prod_id);
                    dynamic.Add("@quantity", c.quantity);
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
        [HttpPut]
        public IActionResult Put(Cart c)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"UPDATE Cart SET quantity=@quantity WHERE cus_id=@cus_id AND prod_id=@prod_id";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@cus_id", c.cus_id);
                    dynamic.Add("@prod_id", c.prod_id);
                    dynamic.Add("@quantity", c.quantity);
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
        [HttpDelete("{cid}/{pid}")]
        public IActionResult Delete(string cid, string pid)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"DELETE FROM Cart WHERE cus_id = @cus_id AND prod_id = @prod_id";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@cus_id", cid);
                    dynamicParameters.Add("@prod_id", pid);
                    var result = conn.Execute(sqlcommand, param: dynamicParameters);
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
        [HttpDelete("{cid}")]
        public IActionResult Delete(string cid)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"DELETE FROM Cart WHERE cus_id = @cus_id";
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@cus_id", cid);
                    var result = conn.Execute(sqlcommand, param: dynamicParameters);
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
    }
}
