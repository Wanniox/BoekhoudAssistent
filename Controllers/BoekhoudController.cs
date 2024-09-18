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

		public async Task<IActionResult> Index(string? table = null, string? GJAHR = null, string? BUKRS = null, string? BELNR = "0", string? BELNR2 = "999", bool details = false)
        {
			ViewData["table"] = table;
			int count = 0;
			List<string> BUKRSList = new List<string>();

            List<BKFP> BKFP = await GetBKFPAsync();
            List<BSEG> BSEG = await GetBSEGAsync();
            List<BKFP> correctBKFP = new List<BKFP>();
            List<BSEG> correctBSEG = new List<BSEG>();
            if (table != null) 
			{
                
                foreach (BKFP item in BKFP)
				{
                    if ((GJAHR == null || GJAHR == item.GJAHR.ToString()) && (BUKRS == null || BUKRS == item.BUKRS.ToString()) && (Int32.Parse(BELNR!) < item.BELNR) && item.BELNR > Int32.Parse(BELNR2!))
                    {
						count++;
						correctBKFP.Add(item);
					}
				}
				foreach (BSEG item in BSEG)
				{
                    if ((GJAHR == null || GJAHR == item.GJAHR.ToString()) && (BUKRS == null || BUKRS == item.BUKRS.ToString()) && (Int32.Parse(BELNR!) < item.BELNR) && item.BELNR > Int32.Parse(BELNR2!))
                    {
						count++;
                        correctBSEG.Add(item);
                    }
				}
                
                ViewData["count"] = count;
				
            } else
			{
				foreach (BSEG item in BSEG)
				{
					BUKRSList.Add(item.BUKRS.ToString());
				}
			}
			var ViewModel = new IndexViewModel
			{
				Table = table,
				GJAHR = GJAHR,
				BUKRS = BUKRS,
				BUKRSList = BUKRSList,
                BELNR = BELNR,
                BELNR2 = BELNR2,
                Details = details,
                BKFP = correctBKFP,
                BSEG = correctBSEG
            };

            return View(ViewModel);
		}
	}
}
