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
		public async Task<List<BKFP>> GetBKFPAsync()
		{
			return await _context.BKFP.ToListAsync();
		}

		public async Task<List<BSEG>> GetBSEGAsync()
		{
			return await _context.BSEG.ToListAsync();
		}

		public async Task<IActionResult> Index(string? table = null, string? GJAHR = null, string? BUKRS = null, string? BELNR = "0", string? BELNR2 = "2147483647", bool details = false)
        {
			ViewData["table"] = table;
			int count = 0;
			List<string> BUKRSList = new List<string>();
            List<string> BELNRList = new List<string>();
            List<string> GJAHRList = new List<string>();

            List<BKFP> BKFP = await GetBKFPAsync();
            List<BSEG> BSEG = await GetBSEGAsync();
            List<BKFP> correctBKFP = new List<BKFP>();
            List<BSEG> correctBSEG = new List<BSEG>();

            foreach (BKFP item in BKFP)
			{
                if ((GJAHR == null || GJAHR == "Niet gespecificeerd" || GJAHR == item.GJAHR.ToString()) && (BUKRS == null || BUKRS == "Niet gespecificeerd" || BUKRS == item.BUKRS.ToString()) && (Int32.Parse(BELNR!) <= item.BELNR) && item.BELNR <= Int32.Parse(BELNR2!))
                {
					count++;
                    if (table != null)
                    {
                        correctBKFP.Add(item);
                    }
				}
			}
			foreach (BSEG item in BSEG)
			{
                if ((GJAHR == null || GJAHR == "Niet gespecificeerd" || GJAHR == item.GJAHR.ToString()) && (BUKRS == null || BUKRS == "Niet gespecificeerd" || BUKRS == item.BUKRS.ToString()) && (Int32.Parse(BELNR!) <= item.BELNR) && item.BELNR <= Int32.Parse(BELNR2!))
                {
					count++;
                    if (table != null)
                    {
                        correctBSEG.Add(item);
                    }
                }
			}
                
            ViewData["count"] = count;
				
            foreach (BSEG item in BSEG)
			{
				if (!BUKRSList.Contains(item.BUKRS.ToString()))
                {
                    BUKRSList.Add(item.BUKRS.ToString());
                }
                if (!BELNRList.Contains(item.BELNR.ToString()))
                {
                    BELNRList.Add(item.BELNR.ToString());
                }
                if (!GJAHRList.Contains(item.GJAHR.ToString()))
                {
                    GJAHRList.Add(item.GJAHR.ToString());
                }
			}
			foreach (BKFP item in BKFP)
			{
                if (!BUKRSList.Contains(item.BUKRS.ToString()))
                {
                    BUKRSList.Add(item.BUKRS.ToString());
                }
                if (!BELNRList.Contains(item.BELNR.ToString()))
                {
                    BELNRList.Add(item.BELNR.ToString());
                }
                if (!GJAHRList.Contains(item.GJAHR.ToString()))
                {
                    GJAHRList.Add(item.GJAHR.ToString());
                }
            }
			BUKRSList.Sort();
            BELNRList.Sort();
			GJAHRList.Sort();
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
