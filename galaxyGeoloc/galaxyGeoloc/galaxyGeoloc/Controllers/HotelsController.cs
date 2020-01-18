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
    public class HotelsController : ApiController
    {
        private galaxyGeolocContext db = new galaxyGeolocContext();


        public IQueryable<hotel> GetRooms()
        {
            return db.Hotels.AsQueryable();
        }
        //public List<hotel> GetRooms()
        //{
        //    List<hotel> list = new List<hotel>() { new hotel() { hotelId=1,hotelName="Hyderabad",address="Address",city="Hyderabad",zipCode=530002} };
        //    return list;
        //}

        // GET: api/Rooms/5
        [ResponseType(typeof(hotel))]
        public IHttpActionResult GetRoom(int id)
        {
            hotel room = db.Hotels.Find(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, hotel room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.hotelId)
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
        [ResponseType(typeof(hotel))]
        public IHttpActionResult PostRoom(hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotels.Add(hotel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hotel.hotelId }, hotel);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(hotel))]
        public IHttpActionResult DeleteRoom(int id)
        {
            hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hotels.Remove(hotel);
            db.SaveChanges();

            return Ok(hotel);
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
            return db.Hotels.Count(o => o.hotelId == id) < 0;
        }
    }
}