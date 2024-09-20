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

            // Use IQueryable to build the queries with filters applied before executing them
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

            // Execute the queries and get the filtered results as lists
            List<BKFP> correctBKFP = await BKFPQuery.ToListAsync();
            List<BSEG> correctBSEG = await BSEGQuery.ToListAsync();

            // Count the total number of matching entries
            int count = correctBKFP.Count + correctBSEG.Count;

            // Generate unique lists of BUKRS, BELNR, and GJAHR from both BKFP and BSEG
            var BUKRSList = correctBKFP.Select(item => item.BUKRS.ToString())
                            .Union(correctBSEG.Select(item => item.BUKRS.ToString()))
                            .Distinct().OrderBy(bukrs => bukrs).ToList();

            var BELNRList = correctBKFP.Select(item => item.BELNR.ToString())
                            .Union(correctBSEG.Select(item => item.BELNR.ToString()))
                            .Distinct().OrderBy(belnr => belnr).ToList();

            var GJAHRList = correctBKFP.Select(item => item.GJAHR.ToString())
                            .Union(correctBSEG.Select(item => item.GJAHR.ToString()))
                            .Distinct().OrderBy(gjahr => gjahr).ToList();

            // Populate the ViewData for count and table
            ViewData["count"] = count;
            ViewData["table"] = table;

            // Prepare the ViewModel
            var ViewModel = new IndexViewModel
            {
                Table = table,
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
