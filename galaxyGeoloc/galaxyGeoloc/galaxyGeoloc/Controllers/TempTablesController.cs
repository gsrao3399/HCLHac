using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Xml.Linq;
using galaxyGeoloc.Models;

namespace galaxyGeoloc.Controllers
{
    public class TempTablesController : ApiController
    {
        private galaxyGeolocContext db = new galaxyGeolocContext();

        //// GET: api/TempTables
        public async Task<IQueryable<TempTable>> GetTempTables(int m, int n)
        {
            var address = "paris, france";
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(address));

            using (var client = new HttpClient())
            {
                var request = await client.GetAsync(requestUri);
                var content = await request.Content.ReadAsStringAsync();
                var xmlDocument = XDocument.Parse(content);
                // return xmlDocument;
            }

            return db.TempTables;
        }




        ////// GET: api/TempTables
        //public async Task<IQueryable<TempTable>> GetTempTables(int m, int n)
        //{
        //    var results;
        //    var placeList = new List<Place>();
        //    string apiKey = "your api key";

        //    if ((string.IsNullOrEmpty(txtLat.Text) || string.IsNullOrEmpty(txtLon.Text))) //if we dont have a lon/lat .. search txt
        //    {
        //        results = await Places.Api.TextSearch(txtQuery.Text, apiKey);
        //    }
        //    else
        //    {
        //        results = await Places.Api.SearchPlaces(Convert.ToDouble(txtLat.Text), Convert.ToDouble(txtLon.Text), txtQuery.Text, apiKey);
        //    }

        //    //add the results to placeList
        //    foreach (var place in results.Places)
        //    {
        //        placeList.Add(place);
        //    }

        //    //if there are more than one 'page' of results...
        //    while (results.Next != null)
        //    {
        //        //get the next lot of results
        //        results = await Places.Api.GetNext(results.Next, apiKey);

        //        foreach (var place in results.Places)
        //        {
        //            placeList.Add(place);
        //        }
        //    }

        //    foreach (var place in placeList)
        //    {
        //        var placeDetails = await Places.Api.GetDetails(place.PlaceId, apiKey);

        //        //do stuff with your place and placeDetails                
        //        string name = place.Name;
        //        string address = placeDetails.Address;
        //        //......
        //    }

        //}


        // GET: api/TempTables/5
        [ResponseType(typeof(TempTable))]
        public IHttpActionResult GetTempTable(int id)
        {
            TempTable tempTable = db.TempTables.Find(id);
            if (tempTable == null)
            {
                return NotFound();
            }

            return Ok(tempTable);
        }

        // GET: api/TempTables/5
        [ResponseType(typeof(TempTable))]
        public IHttpActionResult GetTempTable()
        {
            string locationName = string.Empty;

            try
            {
                string lo = "17.41141° N";
                string la = "78.48478° E";
                if (string.IsNullOrEmpty(lo) || string.IsNullOrEmpty(la))
                    return NotFound();

                string url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", la, lo);


                WebRequest request = WebRequest.Create(url);

                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        DataSet dsResult = new DataSet();
                        dsResult.ReadXml(reader);
                        try
                        {
                            foreach (DataRow row in dsResult.Tables["result"].Rows)
                            {
                                string fullAddress = row["formatted_address"].ToString();
                            }
                        }
                        catch (Exception)
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return Ok(locationName);
        }

        // PUT: api/TempTables/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTempTable(int id, TempTable tempTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tempTable.Id)
            {
                return BadRequest();
            }

            db.Entry(tempTable).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempTableExists(id))
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

        // POST: api/TempTables
        [ResponseType(typeof(TempTable))]
        public IHttpActionResult PostTempTable(TempTable tempTable)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TempTables.Add(tempTable);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tempTable.Id }, tempTable);
        }

        // DELETE: api/TempTables/5
        [ResponseType(typeof(TempTable))]
        public IHttpActionResult DeleteTempTable(int id)
        {
            TempTable tempTable = db.TempTables.Find(id);
            if (tempTable == null)
            {
                return NotFound();
            }

            db.TempTables.Remove(tempTable);
            db.SaveChanges();

            return Ok(tempTable);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TempTableExists(int id)
        {
            return db.TempTables.Count(e => e.Id == id) > 0;
        }
    }
}