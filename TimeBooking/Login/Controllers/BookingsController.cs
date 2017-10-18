using Login.Infrastructure;
using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Login.Controllers
{
    public class BookingsController : ApiController
    {
        //ApplicationDbContext db = new ApplicationDbContext();

        //public JsonResult Get()
        //{
        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        var events = db.Booking.ToList();
        //        return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        //    }
        //}


        // GET api/<controller>
        public IEnumerable<Bookings> Get()
        {

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var events = db.Booking.ToList();
                return events;
            }

        }

        [System.Web.Http.HttpPost]
        public JsonResult Post(Bookings e)
        {
            var status = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                if (e.ID > 0)
                {
                    var v = db.Booking.Where(a => a.ID == e.ID).FirstOrDefault();
                    if (v != null)
                    {
                        v.StartDate = e.StartDate;
                        v.EndDate = e.EndDate;
                        v.Description = e.Description;
                        v.UserReservations = e.UserReservations;
                    }
                }
                else
                {
                    db.Booking.Add(e);
                }
                db.SaveChanges();
                status = true;
            }
            return new JsonResult { Data = new { status = status } };
        }

        [System.Web.Http.HttpDelete]
        public JsonResult Delete(int id)
        {
            var status = false;
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                var v = db.Booking.Where(a => a.ID == id).FirstOrDefault();
                if (v != null)
                {
                    db.Booking.Remove(v);
                    db.SaveChanges();
                    status = true;
                }
            }
            return new JsonResult { Data = new { status = status } };
        }


        //    // GET api/<controller>/5
        //    public Bookings Get(int id)
        //    {
        //        return null;
        //    }

        //    [System.Web.Http.HttpPost]
        //    // POST api/<controller>
        //    public IHttpActionResult Post([FromBody]Bookings value)
        //    {
        //        if (!ModelState.IsValid)
        //            return BadRequest("Invalid Data");

        //        db.Booking.Add(value);

        //        db.SaveChanges();

        //            return Ok();
        //    }

        //    // PUT api/<controller>/5
        //    public void Put(int id, [FromBody]Bookings value)
        //    {

        //    }

        //[System.Web.Http.HttpDelete]
        //// DELETE api/<controller>/5
        //public IHttpActionResult Delete(int id)
        //{

        //    using (ApplicationDbContext db = new ApplicationDbContext())
        //    {
        //        var booking = db.Booking.Find(id);

        //        if (booking == null)
        //        {
        //            return NotFound();
        //        }

        //        db.Booking.Remove(booking);
        //        db.SaveChanges();
        //    }

        //    return Ok();
        //}

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
    }
}