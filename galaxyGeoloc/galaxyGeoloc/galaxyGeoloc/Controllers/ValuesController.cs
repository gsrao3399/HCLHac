using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace galaxyGeoloc.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        private string getLocationByGeoLocation(string longitude, string latitude)
        {
            string locationName = string.Empty;

            try
            {
                if (string.IsNullOrEmpty(longitude) || string.IsNullOrEmpty(latitude))
                    return "";

                string url = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&sensor=false", latitude, longitude);


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
               // lblError.Text = ex.Message;
            }


            return locationName;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
