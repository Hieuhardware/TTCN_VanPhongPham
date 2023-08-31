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
    public class OrderDetailsController : ControllerBase
    {
        protected MySqlConnection? conn;
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = "SELECT * FROM OrderDetailView";
                    var result = conn.Query<OrderDetail>(sqlcommand);
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
                    var sqlcommand = $"SELECT * FROM OrderDetailView WHERE order_id = @Id";
                    var result = conn.Query<OrderDetail>(sqlcommand, param: new { Id = id });
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
        public IActionResult Post(IEnumerable<OrderDetail> od)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var result = 0;
                    foreach (var o in od.ToList())
                    {
                        var sqlcommand = $"INSERT INTO Order_Details(prod_id, order_id, quantity, price ) VALUES(@prod_id, @order_id, @quantity, @price)";
                        DynamicParameters dynamic = new DynamicParameters();
                        dynamic.Add("@prod_id", o.prod_id);
                        dynamic.Add("@order_id", o.order_id);
                        dynamic.Add("@quantity", o.quantity);
                        dynamic.Add("@price", o.price);
                        result = result + conn.Execute(sqlcommand, param: dynamic);
                    }

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
        [HttpDelete]
        public IActionResult Delete(OrderDetail o)
        {
            try
            {
                using (conn = new MySqlConnection(SqlConnectionString.ConnectionString))
                {
                    var sqlcommand = $"DELETE FROM OrderDetail WHERE prod_id = @prod_id AND order_id = @order_id";
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@prod_id", o.prod_id);
                    dynamic.Add("@order_id", o.order_id);
                    var result = conn.Query<Product>(sqlcommand, param: dynamic);
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
