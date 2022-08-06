using clinicManagement.Models;
using System;
using System.Collections.Generic;

namespace clinicManagement.services
{
    public interface IClinicServices
    {
        public List<Appointments> PractitionerSummery(DateTime beginDate, DateTime endDate);
        public List<Appointments> PractitionerDetail(int practitionersId, string startDate, string endDate);
    }
}
