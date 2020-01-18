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
    public class Hotels1Controller : ApiController
    {
        //private galaxyGeolocContext db = new galaxyGeolocContext();

        private IRepository<hotel, galaxyGeolocContext> db;

        public Hotels1Controller(IRepository<hotel, galaxyGeolocContext> _db)
        {
            db = _db;
        }
        // GET: api/Rooms
        public IQueryable<hotel> GetRooms()
        {
            return db.GetAll();//.Rooms;
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(hotel))]
        public IHttpActionResult GetRoom(int id)
        {
            hotel room = db.FindBy(o => o.hotelId == id).FirstOrDefault();
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

            db.Edit(room);
            // db.Entry(room).State = EntityState.Modified;

            try
            {
                db.Save();
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

            db.Create(hotel);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = hotel.hotelId }, hotel);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(hotel))]
        public IHttpActionResult DeleteRoom(int id)
        {
            hotel hotel = db.FindBy(o => o.hotelId == id).FirstOrDefault();
            if (hotel == null)
            {
                return NotFound();
            }

            db.Delete(hotel);
            db.Save();

            return Ok(hotel);
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        private bool RoomExists(int id)
        {
            hotel room = db.FindBy(o => o.hotelId == id).FirstOrDefault();
            return room != null;
        }
    }
}