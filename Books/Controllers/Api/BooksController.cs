using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using Books.Models;

namespace Books.Controllers.Api
{
    public class BooksController : ApiController
    {
        private AppDbContext _context;
        public BooksController()
        {
            _context = new AppDbContext();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();
        }
    }
}
