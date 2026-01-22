using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using memolib;

namespace webapi_app.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MemoController : ControllerBase
    {
        [HttpGet("list")]
        public IActionResult GetList()
        {
            using MemoDac dac = new();
            IMemoDac itf = dac.CreateExecution<IMemoDac>();
            List<Memo> memos = itf.GetMemos();
            return Ok(memos);
        }

        [HttpPost("new")]
        public IActionResult NewMemo(Memo newMemo)
        {
            MemoBiz biz = new();
            IMemoBiz itf = biz.CreateExecution<IMemoBiz>();
            int newId = itf.Insert(newMemo);
            return Ok(new { newId });
        }

        [HttpPost("save")]
        public IActionResult NewMemos(Memo[] memos)
        {
            MemoBiz biz = new();
            IMemoBiz itf = biz.CreateExecution<IMemoBiz>();
            var newIds = itf.InsertMany(memos);
            return Ok(new { newIds });
        }
    }
}
