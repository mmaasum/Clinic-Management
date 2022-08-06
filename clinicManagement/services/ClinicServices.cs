
/*
 * Author Name: Md Masum Jahangir
 * Created Date: 28-July-2022
 * Purpose: To generate a financial report to analyse profitability of practitioners on each month
 * Version: 0.0.1
 */

using clinicManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace clinicManagement.services
{
    public class ClinicServices : IClinicServices
    {
        List<Practitioners> practitioners;
        List<Appointments> appointments;
        /// <summary>
        /// For each practitioner, display cost and revenue per month within the given date range
        /// </summary>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Appointments> PractitionerSummery(DateTime beginDate, DateTime endDate)
        {
            DateTime dateTime = Convert.ToDateTime(beginDate);

            practitioners = new List<Practitioners>();
            appointments = new List<Appointments>();
            List<Appointments> summery;
            try
            {
                practitioners = LoadPractitioners();
                appointments = LoadAppointments();

                summery = (from a in appointments
                                              join p in practitioners on a.practitioner_id equals p.id
                                              where (Convert.ToDateTime(a.date) >= beginDate && Convert.ToDateTime(a.date) <= endDate)

                                              group a by new { p.id, p.name } into g
                                              orderby g.Key.id ascending
                                              select new Appointments
                                              {
                                                  Practitioners = new Practitioners
                                                  {
                                                      id = g.Key.id,
                                                      name = g.Key.name,
                                                  },
                                                  revenue = g.Sum(t3 => t3.revenue),
                                                  cost = g.Sum(t3 => t3.cost)
                                              }).ToList();
            }
            catch (Exception)
            {
                throw;
            }
            

            
            return summery;
        }

        /// <summary>
        /// Display all the appointment data list for current practitioner within the selected date rang
        /// </summary>
        /// <param name="practitionersId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<Appointments> PractitionerDetail(int practitionersId, string startDate, string endDate)
        {
            practitioners = new List<Practitioners>();
            appointments = new List<Appointments>();
            List<Appointments> detail;
            try
            {
                practitioners = LoadPractitioners();
                appointments = LoadAppointments();

                detail = (from a in appointments
                                             join p in practitioners on a.practitioner_id equals p.id
                                             where (Convert.ToDateTime(a.date) >= Convert.ToDateTime(startDate)
                                             && Convert.ToDateTime(a.date) <= Convert.ToDateTime(endDate))
                                             && p.id == practitionersId

                                             select new Appointments
                                             {
                                                 Practitioners = new Practitioners
                                                 {
                                                     id = p.id,
                                                     name = p.name
                                                 },
                                                 date = a.date,
                                                 client_name = a.client_name,
                                                 appointment_type = a.appointment_type,
                                                 duration = a.duration,
                                                 revenue = a.revenue,
                                                 cost = a.cost
                                             }).ToList();
            }
            catch (Exception)
            {

                throw;
            }
            

            return detail;
        }

        /// <summary>
        /// Load data from practitioners.json file 
        /// </summary>
        /// <returns></returns>
        private List<Practitioners> LoadPractitioners()
        {
            practitioners = new List<Practitioners>();
            try
            {
                string practitionersFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "data", "practitioners.json");
                using (StreamReader r = new StreamReader(practitionersFilePath))
                {
                    string json = r.ReadToEnd();
                    practitioners = System.Text.Json.JsonSerializer.Deserialize<List<Practitioners>>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return practitioners;
        }

        /// <summary>
        /// Load data from appointments.json file 
        /// </summary>
        /// <returns></returns>
        private List<Appointments> LoadAppointments()
        {
            appointments = new List<Appointments>();

            try
            {
                string appointmentsFilePath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "data", "appointments.json");
                using (StreamReader r = new StreamReader(appointmentsFilePath))
                {
                    string json = r.ReadToEnd();
                    appointments = System.Text.Json.JsonSerializer.Deserialize<List<Appointments>>(json);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return appointments;
        }


    }
}
