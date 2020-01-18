using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using galaxyGeoloc.Models;
using galaxyGeoloc.Repository;

namespace galaxyGeoloc.Controllers
{
    public class BookingsController : ApiController
    {
        private galaxyGeolocContext db = new galaxyGeolocContext();


        public IQueryable<Booking> GetBookings()
        {
            return db.Bookings.AsQueryable();
        }
        //public List<Room> GetRooms()
        //{
        //    List<Room> list = new List<Room>() { new Room() { hotelId=1,hotelName="Hyderabad",address="Address",city="Hyderabad",zipCode=530002} };
        //    return list;
        //}

        // GET: api/Rooms/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult GetBookings(int id)
        {
            Booking room = db.Bookings.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Booking room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Id)
            {
                return BadRequest();
            }

            //db.Edit(room);
            db.Entry(room).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rooms
        [ResponseType(typeof(Booking))]
        public IHttpActionResult PostRoom(Booking Room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Bookings.Add(Room);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Room.Id }, Room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Booking))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Booking Room = db.Bookings.Find(id);
            if (Room == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(Room);
            db.SaveChanges();

            return Ok(Room);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool RoomExists(int id)
        {
            return db.Bookings.Count(o => o.Id == id) < 0;
        }
    }
}