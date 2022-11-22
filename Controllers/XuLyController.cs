using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI_DoAnHTTT_.Models;

namespace WebAPI_DoAnHTTT_.Controllers
{
    public class XuLyController : ApiController

    {
        [Route("api/QuanAn/DSQuanAn")]
        [HttpGet]
        public IHttpActionResult LayDSQA()
        {
            try
            {
                DataTable tb = Database.LayDsQuanAn();
                return Ok(tb);
            }
            catch
            {
                return NotFound();
            }
        }
        [Route("api/QuanAn/ThemQuanAnYeuThich")]
        [HttpGet]
        public IHttpActionResult ThemQuanYeuThich(string MSQA)
        {
            try
            {
                DataTable tb = Database.ThemQuanYeuThich(MSQA);
                return Ok(tb);
            }
            catch
            {
                return NotFound();
            }
        }
        [Route("api/QuanAn/DSQuanAnYeuThich")]
        [HttpGet]
        public IHttpActionResult DSQuanAnYT()
        {
            try
            {
                DataTable tb = Database.DSQuanYT();
                return Ok(tb);
            }
            catch { return NotFound(); }
        }
        [Route("api/QuanAn/XoaQuanYeuThich")]
        [HttpGet]
        public IHttpActionResult XoaQuanYeuThich(string MSQA)
        {
            try
            {
                DataTable tb = Database.XoaQuanYeuThich(MSQA);
                return Ok(tb);
            }
            catch
            {
                return NotFound();
            }
        }
        /*[Route("api/QuanAn/ThemQuanAnYeuThich")]
        [HttpPost]
        public IHttpActionResult ThemQuanAnYeuThich(QuanAnYT quanAn)
        {
            try
            {
                int tb = Database.ThemQuanAnYT(quanAn);
                return Ok(tb);
            }
            catch
            {
                return NotFound();
            }
        }*/
    }
}
