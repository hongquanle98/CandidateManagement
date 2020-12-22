using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Helper
{
    public static class FormatHelper
    {
        public static string FormatDate(DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
        public static string FormatDateTime(DateTime date)
        {
            return date.ToString("dd/MM/yyyy, HH:mm:ss");
        }
        public static string FormatGender(string gender)
        {
            return gender.Equals("M") ? "Male" : "Female";
        }
        public static string FormatCurrency(decimal currency)
        {
            return string.Format(new CultureInfo("vi-VN", false), "{0:c0}", currency);
        }
        public static string FormatAdminAvatar(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return string.Format("~/images/{0}", "noimage.jpg");
            return string.Format("~/admin/avatar/{0}", imageName);
        }
        public static string FormatCandidateAvatar(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return string.Format("~/images/{0}", "noimage.jpg");
            return string.Format("~/candidate/avatar/{0}", imageName);
        }
        public static string FormatCompanyLogo(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return string.Format("~/images/{0}", "noimage.jpg");
            return string.Format("~/company/logo/{0}", imageName);
        }
        public static string FormatCompanyImage(string imageName)
        {
            if (string.IsNullOrEmpty(imageName))
                return string.Format("~/images/{0}", "noimage.jpg");
            return string.Format("~/company/image/{0}", imageName);
        }
    }
}
