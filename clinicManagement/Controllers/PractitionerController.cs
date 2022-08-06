using clinicManagement.Models;
using clinicManagement.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace clinicManagement.Controllers
{
    public class PractitionerController : Controller
    {
        private readonly IClinicServices _clinicServices;
        public PractitionerController(IClinicServices clinicServices)
        {
            _clinicServices = clinicServices;
        }

        /// <summary>
        /// For each practitioner, display cost and revenue per month within the given date range
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public IActionResult Index(DateTime beginDate, DateTime endDate)
        {
            List<Appointments> summery;
            try
            {
                if (beginDate.Year == 1)
                    TempData["Message"] = "Please provide valid start date";
                if (endDate.Year == 1)
                    TempData["Message"] = "Please provide valid end date";
                if (beginDate.Date == null)
                    TempData["Message"] = "Please provide start date";
                if (endDate == null)
                    TempData["Message"] = "Please provide end date";
                if (beginDate > endDate)
                    TempData["Message"] = "Please provide valid date range";

                summery = _clinicServices.PractitionerSummery(beginDate, endDate);

                if(summery == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                /*
                 * Email setup configuration in this section for error handling
                 */

                ModelState.AddModelError("", "Please write first name.");
                return BadRequest(ex);
            }
            


            ViewBag.summery = summery;
            return View();
        }

        /// <summary>
        /// Display all the appointment data list for current practitioner within the selected date rang
        /// </summary>
        /// <param name="practitionersId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Appointments")]
        public IActionResult Appointments(int practitionersId, string startDate, string endDate)
        {
            List<Appointments> detail;
            try
            {
                if(practitionersId ! >0)
                    TempData["Message"] = "Invalid practitioners";
                if (string.IsNullOrEmpty(startDate))
                    TempData["Message"] = "Please provide valid start date";
                if (string.IsNullOrEmpty(endDate))
                    TempData["Message"] = "Please provide valid end date";

                detail = _clinicServices.PractitionerDetail(practitionersId, startDate, endDate);
                if (detail == null)
                    return NotFound();
            }
            catch (Exception ex)
            {
                /*
                 * Email setup configuration in this section for error handling
                 */
                throw ex;
            }

            ViewBag.detail = detail;
            return View();
        }
    }
}
