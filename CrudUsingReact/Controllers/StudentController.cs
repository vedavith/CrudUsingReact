using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CrudUsingReact.Models;
using Newtonsoft.Json;

namespace CrudUsingReact.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {
        Response setResponse;
        CrudDemoEntities _student = new CrudDemoEntities();
        

        [Route("all")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllStudents()
        {
            try
            {
                var students = _student.studentmasters.ToList();
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, students);
            }
            catch (Exception e)
            {
                setResponse = new Response
                {
                    Status = "Failed",
                    Message = e.Message
                };
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, setResponse);
            } 
        }

        [Route("create/{id?}")]
        [HttpPost]
        public async Task<HttpResponseMessage> CreateStudent([FromBody]Student student, int? id = 0)
        {

            studentmaster studentrepo = new studentmaster();
            studentrepo.Name = student.Name;
            studentrepo.Class = student.Class;
            studentrepo.RollNo = student.RollNo;
            studentrepo.Address = student.Address;

            if (id == 0)
            {
                try
                {
                    _student.studentmasters.Add(studentrepo);
                    _student.SaveChanges();
                    setResponse = new Response {
                        Status = "Success",
                        Message = "Student Inserted"
                    };
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, setResponse);
                }
                catch(Exception e)
                {
                    setResponse = new Response
                    {
                        Status = "Failed",
                        Message = e.Message
                    };
                    return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest,setResponse);
                }
            }
            else
            {
                var studentId = Convert.ToInt32(id);
                try
                {
                    var updatedStudent = _student.studentmasters.Where(sm => sm.Id == studentId).FirstOrDefault();
                    updatedStudent.Id = studentId;
                    updatedStudent.Name = studentrepo.Name;
                    updatedStudent.Class = studentrepo.Class;
                    updatedStudent.RollNo = studentrepo.RollNo;
                    updatedStudent.Address = studentrepo.Address;

                    _student.SaveChanges();
                    setResponse = new Response
                    {
                        Status = "Success",
                        Message = "Student Updated"
                    };
                    return Request.CreateResponse(System.Net.HttpStatusCode.OK, setResponse);

                }
                catch (Exception e)
                {

                    setResponse = new Response
                    {
                        Status = "Failed",
                        Message = e.Message
                    };
                    return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, setResponse);
                }   
            }
        }

        [Route("delete/{id}")]
        [HttpDelete]
        public async Task<HttpResponseMessage> DeleteStudentOnId(int id)
        {
            try
            {
                var deleteStudentObject = _student.studentmasters.Where(sm => sm.Id == id).FirstOrDefault();
                _student.studentmasters.Remove(deleteStudentObject);
                _student.SaveChanges();

                setResponse = new Response
                {
                    Status = "Success",
                    Message = "Student Deleted"
                };
                return Request.CreateResponse(System.Net.HttpStatusCode.Accepted, setResponse);
            }
            catch (Exception e)
            {
                setResponse = new Response
                {
                    Status = "Failed",
                    Message = e.Message
                };
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, setResponse);
            }
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetStudentOnId(int id)
        {
            try
            {
                var StudentOnId = _student.studentmasters.Where(sm => sm.Id == id).FirstOrDefault();
                return Request.CreateResponse(System.Net.HttpStatusCode.OK, StudentOnId);
            }
            catch (Exception e)
            {
                setResponse = new Response {
                    Status = "Failed",
                    Message = e.Message
                };
                return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, setResponse);
            }
        }
    }
}