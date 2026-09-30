using ClassLibrary1.Dto;
using LibraryWebAPI.Context;
using LibraryWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadersController : ControllerBase
    {
        private readonly MyDbContext _context;

        public ReadersController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<Reader>> GetReaders()
        {
            var readers = _context.Readers.ToList();
            return readers;
        }
        [HttpGet("{id}")]
        public ActionResult<Reader> GetSingleReader(int id)
        {
            return _context.Readers.SingleOrDefault(s => s.ReaderId == id);
        }
        [HttpPost]
        public ActionResult<Reader> Create(Reader reader)
        {
            if (reader != null)
            {
            _context.Readers.Add(reader);
            _context.SaveChanges();
            }
            return reader;
        }
        [HttpPut("{id}")]
        public ActionResult<Reader> UpdateReader(int id, Reader reader)
        {
            if (reader != null)
            {
                _context.Readers.Update(reader);
                _context.SaveChanges();
            }
            return reader;
        }
        [HttpDelete("{id}")]
        public ActionResult<Reader> DeactivateReader(int id)
        {
            var reader = _context.Readers.SingleOrDefault(s => s.ReaderId == id);
            reader.IsActive = false;
            _context.SaveChanges();
            return reader;

        }
    }
}
