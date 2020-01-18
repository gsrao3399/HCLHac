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
    public class Rooms1Controller : ApiController
    {
        //private galaxyGeolocContext db = new galaxyGeolocContext();

        private IRepository<Room, galaxyGeolocContext> db;

        public Rooms1Controller(IRepository<Room, galaxyGeolocContext> _db)
        {
            db = _db;
        }
        // GET: api/Rooms
        public IQueryable<Room> GetRooms()
        {
            return db.GetAll();//.Rooms;
        }

        // GET: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult GetRoom(int id)
        {
            Room room = db.FindBy(o => o.Id == id).FirstOrDefault();
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        // PUT: api/Rooms/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRoom(int id, Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != room.Id)
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
        [ResponseType(typeof(Room))]
        public IHttpActionResult PostRoom(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Create(room);
            db.Save();

            return CreatedAtRoute("DefaultApi", new { id = room.Id }, room);
        }

        // DELETE: api/Rooms/5
        [ResponseType(typeof(Room))]
        public IHttpActionResult DeleteRoom(int id)
        {
            Room room = db.FindBy(o => o.Id == id).FirstOrDefault();
            if (room == null)
            {
                return NotFound();
            }

            db.Delete(room);
            db.Save();

            return Ok(room);
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
            Room room = db.FindBy(o => o.Id == id).FirstOrDefault();
            return room != null;
        }
    }
}