using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Metrica.Controllers
{
    public class HospitalController : Controller
    {
        [ActionName("doctor-shedule")]
        public ViewResult DoctorSchedule() => View();

        [ActionName("all-appointments")]
        public ViewResult appointments() => View();


        [ActionName("all-doctors")]
        public ViewResult Doctors() => View();

        [ActionName("add-doctor")]
        public ViewResult AddDoctor() => View();

        [ActionName("edit-doctor")]
        public ViewResult EditDoctor() => View();

        [ActionName("doctor-profile")]
        public ViewResult DoctorProfile() => View();


        [ActionName("all-patients")]
        public ViewResult Patients() => View();

        [ActionName("add-patient")]
        public ViewResult AddPatient() => View();

        [ActionName("edit-patient")]
        public ViewResult EditPatient() => View();

        [ActionName("patient-profile")]
        public ViewResult PatientProfile() => View();


        [ActionName("all-payments")]
        public ViewResult Payments() => View();

        [ActionName("payment-invoice")]
        public ViewResult PaymentInvoice() => View();

        [ActionName("cashless-payments")]
        public ViewResult CashlessPayments() => View();

        
        [ActionName("all-staff")]
        public ViewResult Staffs() => View();

        [ActionName("add-member")]
        public ViewResult AddMember() => View();

        [ActionName("edit-member")]
        public ViewResult EditMember() => View();

        [ActionName("member-profile")]
        public ViewResult MemberProfile() => View();

        [ActionName("salary")]
        public ViewResult Salary() => View();


        [ActionName("all-rooms")]
        public ViewResult AllRooms() => View();

        [ActionName("insurance-company")]
        public ViewResult InsuranceCompany() => View();
        
        public ViewResult attendance() => View();
        public ViewResult leaves() => View();
        public ViewResult chat() => View();
        public ViewResult departments() => View();
        public ViewResult events() => View();
        public ViewResult expenses() => View();
        public ViewResult holidays() => View();
    }
}
