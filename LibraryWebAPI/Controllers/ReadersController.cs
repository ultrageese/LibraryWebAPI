using ClassLibrary1.Dto;
using LibraryWebAPI.Context;
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
        public ActionResult<IEnumerable<ReaderDto>> GetReaders()
        {
            var readers = _context.Readers.ToList();
            var retReaders = new List<ReaderDto>();
            foreach (var reader in readers)
            {
                retReaders.Add((ReaderDto)reader);
            }
            return retReaders;
        }
        [HttpGet("{id}")]
        public ActionResult<ReaderDto> GetSingleReader(int id)
        {
            return (ReaderDto)_context.Readers.SingleOrDefault(s => s.ReaderId == id);
        }
        [HttpPost]
        public ActionResult<ReaderDto> Create(ReaderDto reader)
        {
            return reader;
        }
    }
}
