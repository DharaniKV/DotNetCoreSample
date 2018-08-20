using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DotNetCoreSample.Models;
using System.Data;
using System.Net;
using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetCoreSample.Controllers
{
    //[Route("api/[controller]")]
    public class TodoController : ControllerBase
    {

        [HttpGet]
        [Route("api/Todo")]
        public OkObjectResult Get()
        {
           // SampleDBContext jj = new SampleDBContext();
            Response<List<Employee>> employeeObj = new Response<List<Employee>>();
            List<Employee> employees = null;
            using (var test = new SampleDBContext())
            {
                employees = (from c in test.Employee select c).ToList();
                employeeObj.Data = employees;
            }

            if (employees.Count == 0)
            {
                employeeObj.Status = HttpStatusCode.NotFound;
                employeeObj.Message = "No records found";
            }
            else
            {
                employeeObj.Status = HttpStatusCode.OK;
                employeeObj.Message = "records displayed successfully";
            }
            return Ok(employeeObj);
        }

        [Route("api/Todo/{EmpId}")]
        public ObjectResult Get(int EmpId)
        {
            SampleDBContext sampleDBContext = new SampleDBContext();
            Response<List<Employee>> responseObj = new Response<List<Employee>>();
            List<Employee> employees = null;
            // var ids = employees.Select(x => x.EmpId);
            try
            {
                using (var test = new SampleDBContext())
                {
                    employees = (from c in test.Employee where c.EmpId.Equals(EmpId) select c).ToList();
                }

                if (employees.Count != 0)
                {
                    responseObj.Status = HttpStatusCode.OK;
                    responseObj.Message = "record displayed successfully";
                    return Ok(employees);

                }
                else
                {
                    //responseObj.Status = HttpStatusCode.NotFound;
                    //responseObj.Message = "No records found with the given Id";
                    //throw new NullReferenceException(HttpStatusCode.NotFound);
                    //var message = new HttpResponseMessage(HttpStatusCode.BadRequest)
                    //{
                    //    Content = new StringContent("We cannot use IDs greater than 100.")
                    //};
                    responseObj.Status = HttpStatusCode.NotFound;
                    responseObj.Message = "Employee ID not found";
                    return NotFound(responseObj);
                    //throw new  Exception(message.ToString());
                }

            }
            catch (Exception ex)
            {
                responseObj.Status = HttpStatusCode.BadRequest;
                responseObj.Message = ex.Message;
                return BadRequest(responseObj);
                //throw new NullReferenceException(HttpStatusCode.NotFound);
            }
        }

           [HttpPost]
           [Route("api/Todo")]
           public IActionResult Insert([FromBody]Employee employee)
           {
               SampleDBContext resposeObj = new SampleDBContext();
            Response<List<Employee>> employeeObj = new Response<List<Employee>>();
            List<Employee> Object1 = new List<Employee>();
               try
               {
                   using (var test = new SampleDBContext())
                   {
                       test.Employee.Add(employee);
                       test.SaveChanges();
                  
                    employeeObj.Status = HttpStatusCode.OK;
                    employeeObj.Message = "record displayed successfully";
                }
                   return Ok(employeeObj);

               }
               catch (Exception ex)
               {
                employeeObj.Status = HttpStatusCode.NotFound;
                employeeObj.Message = "you have given wrong input";
                return NotFound(employeeObj);
                
               }
           }
           
           [HttpPut]
           [Route("api/Todo/{EmpId}")]
           public IActionResult Put(int EmpId, [FromBody] Employee employees)
           {
               SampleDBContext responseObj = new SampleDBContext();
            Response<List<Employee>> employeeObj = new Response<List<Employee>>();
            // List<Employee> Object1 = new List<Employee>();
            try
               {

                   using (var test = new SampleDBContext())
                   {
                       var newRecord = test.Employee.Where(t => t.EmpId == EmpId).FirstOrDefault();
                      // var newRecord = (from c in test.Employee where c.Equals(EmpId) select c).ToList();
                       if (newRecord != null)
                       {
                           newRecord.Name = employees.Name;
                           newRecord.City = employees.City;
                           newRecord.DeptId = employees.DeptId;
                           //test.Employee.Update(employees);
                           test.SaveChanges();
                       }
                       return Ok(employeeObj);
                   }                  
               }
               catch(Exception ex)
               {
                //return NotFound();
                employeeObj.Status = HttpStatusCode.NotFound;
                employeeObj.Message = "you have given wrong input";
                return NotFound(employeeObj);
            }

           }

           [Route("api/Todo/{EmpId}")]
           [HttpDelete]
           public IActionResult Delete(int EmpId)
           {
               SampleDBContext responseObj = new SampleDBContext();
            Response<List<Employee>> employeeObj = new Response<List<Employee>>();
            try
               {

                   using (var test = new SampleDBContext())
                   {
                       Employee employee = new Employee();

                       var record = test.Employee.Where(t => t.EmpId == EmpId).FirstOrDefault();

                       test.Employee.Remove(record);
                       test.SaveChanges();
                       return Ok(responseObj);
                   }

               }
               catch (Exception ex)
               {
                //return NotFound();
              
                employeeObj.Status = HttpStatusCode.NotFound;
                employeeObj.Message = "you have given wrong input";
                return NotFound(employeeObj);
            }
           }


        [Route("api/Todo/{EmpId}")]
        [HttpPatch]
        public IActionResult Patch(int EmpId,/*string empname,*/  [FromBody] Employee employee)
        {
            Response<List<Employee>> employeeObj = new Response<List<Employee>>();
            SampleDBContext responseObj = new SampleDBContext();

            try
            {
                using (var test = new SampleDBContext())
                {
                    var updateRecord = test.Employee.FirstOrDefault(t => t.EmpId == EmpId);
                    updateRecord.Name = employee.Name;   // Testcase1: If user try to update city column, there will be mismatch error with the body input
                    test.SaveChanges();
                    
                    employeeObj.Status = HttpStatusCode.OK;
                    employeeObj.Message = "record updated successfully";
                    return Ok(employeeObj);
                }
               

            }
            catch
            {
                employeeObj.Status = HttpStatusCode.NotFound;
                employeeObj.Message = "you have given wrong input";
                return NotFound(employeeObj);
            }

        }


    }

}



