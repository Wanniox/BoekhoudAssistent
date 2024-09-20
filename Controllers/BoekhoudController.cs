using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using BoekhoudAssistent.Models;
using System;
using BoekhoudAssistent.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace BoekhoudAssistent.Controllers
{
	public class BoekhoudController : Controller
	{
		private readonly BoekhoudAssistentContext _context;
		public BoekhoudController(BoekhoudAssistentContext context)
		{
			_context = context;
		}
		public IQueryable<BKFP> GetBKFPQueryable()
		{
			return _context.BKFP.AsQueryable();
		}

		public IQueryable<BSEG> GetBSEGQueryable()
		{
			return _context.BSEG.AsQueryable();
		}

		public async Task<IActionResult> Index(string? table = null, string? GJAHR = null, string? BUKRS = null, string BELNR = "0", string BELNR2 = "2147483647", bool details = false)
        {
            int belnrStart = Int32.Parse(BELNR);
            int belnrEnd = Int32.Parse(BELNR2);

            IQueryable<BKFP> BKFPQuery = GetBKFPQueryable()
                .Where(item =>
                    (GJAHR == null || GJAHR == "Niet gespecificeerd" || GJAHR == item.GJAHR.ToString()) &&
                    (BUKRS == null || BUKRS == "Niet gespecificeerd" || BUKRS == item.BUKRS.ToString()) &&
                    belnrStart <= item.BELNR && item.BELNR <= belnrEnd);

            IQueryable<BSEG> BSEGQuery = GetBSEGQueryable()
                .Where(item =>
                    (GJAHR == null || GJAHR == "Niet gespecificeerd" || GJAHR == item.GJAHR.ToString()) &&
                    (BUKRS == null || BUKRS == "Niet gespecificeerd" || BUKRS == item.BUKRS.ToString()) &&
                    belnrStart <= item.BELNR && item.BELNR <= belnrEnd);

            List<BKFP> correctBKFP = await BKFPQuery.ToListAsync();
            List<BSEG> correctBSEG = await BSEGQuery.ToListAsync();

            int count = correctBKFP.Count + correctBSEG.Count;

            var BUKRSList = correctBKFP.Select(item => item.BUKRS.ToString())
                            .Union(correctBSEG.Select(item => item.BUKRS.ToString()))
                            .Distinct().OrderBy(bukrs => bukrs).ToList();

            var BELNRList = correctBKFP.Select(item => item.BELNR.ToString())
                            .Union(correctBSEG.Select(item => item.BELNR.ToString()))
                            .Distinct().OrderBy(belnr => belnr).ToList();

            var GJAHRList = correctBKFP.Select(item => item.GJAHR.ToString())
                            .Union(correctBSEG.Select(item => item.GJAHR.ToString()))
                            .Distinct().OrderBy(gjahr => gjahr).ToList();

            ViewData["count"] = count;
            ViewData["table"] = table;

            var ViewModel = new IndexViewModel
            {
                GJAHRList = GJAHRList,
                BUKRSList = BUKRSList,
                BELNRList = BELNRList,
                Details = details,
                BKFP = correctBKFP,
                BSEG = correctBSEG
            };

            return View(ViewModel);
        }
	}
}
