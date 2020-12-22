﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CandidateManagement.Helper
{
    public class ConstantHelper
    {
        public struct Company
        {
            public const string Name = "Vin Group";
            public const string Location = "Hồ Chí Minh";
            public const string Size = "100-499";
            public static readonly string Logo = FormatHelper.FormatCompanyLogo("logo.png");
            public static readonly string[] Banners = { "<div><a href='https://www.vietnamworks.com/chuyen-vien-van-hanh-va-trien-khai-cac-he-thong-cong-nghe-thong-tin-4-1321498-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=MOBIFONEITC-VHTK&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width='250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600 (updated)_113956.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href='https://www.vietnamworks.com/luong-thuong-hap-dan-ky-thuat-vien-co-khi-2-1-1-1-1-1320956-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=CROWN&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width='250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113954.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/engineering-trainee-fast-tracking-18-month-program-with-attractive-salary-progression-2-1319691-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=SPINMASTER&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113867.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/technical-support-engineer-2-year-experience-in-network-system-3-1-1319162-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=TEKEXPERTS&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113854.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/nhan-vien-thiet-ke-897-1319983-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=MEKTEC&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113948.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/pho-giam-doc-nha-may-24-1-1-2-1319890-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=CASABLANCA&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113950.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/hot-job-giao-vien-cong-nghe-luong-thuong-va-dai-ngo-hap-dan-1320586-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=TEKY&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113952.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/production-manager-445-1316333-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=ECCO&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113816.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/truong-bo-phan-cong-nghe-thong-tin-9-1317292-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=VAP&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113824.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/nhan-vien-kinh-doanh-muc-luong-len-den-1000-1316195-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=TAKARABELMONT&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600 (updated)_113808.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/senior-admissions-consultant-2-1-1318380-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=EF&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113903.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/lead-devops-aws-python-1317978-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=JONCKERS&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113901.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/cua-hang-truong-chuoi-ban-le-saigonco-op-1317772-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=SAIGONCOOP&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113874.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/c-c-plus-plus-engineer-1318094-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=VANTIX&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113876.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/senior-information-systems-analyst-yeu-cau-thanh-thao-tieng-anh-1-1317057-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=UNIS&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113844.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/giam-doc-ban-hang-57-1-1317394-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=SFHOME&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113846.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/sales-supervisor-freight-forwarding-3-1-1-1317945-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=PANTOS&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113848.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/sales-logistics-so-luong-10-nguoi-urgent-1317914-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=CIMC&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600 (updated)_113850.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/hot-job-tuyen-gap-03-quan-ly-san-xuat-nam-1317532-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=HTMP&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113852.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/senior-ruby-on-rails-developer-salary-upto-4000-1314908-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=CLOUDSTORM&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113727.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/chuyen-vien-tu-van-ban-hang-luong-cung-tu-7-10-trieu-thang-di-lam-ngay-1315689-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=SEABIG&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113729.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/nam-nhan-vien-quan-ly-thiet-bi-nha-xuong-tieng-nhat-n3-tieng-anh-toeic-600-diem-1314122-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=SD-QLTB&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113692.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/fullstack-dev-nodejs-javascript-upto-3-000-1-1314911-jd//?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=SHIFTASIA&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113703.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/vinsmart-hn-ky-su-phan-mem-nhung-2-1-1315183-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=VINSMART&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113800.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/mechanical-group-leader-1316575-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=KOBELCO&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113806.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/nhan-vien-kinh-doanh-sales-muc-luong-hap-dan-theo-nang-luc-1-1317090-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=BVHONGNGOC&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113794.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/programme-officer-in-mangrove-rehabilitation-and-community-based-in-can-tho-1316033-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=GIZ&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113802.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/nhan-vien-thiet-ke-894-1316442-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=SUMIHANEL&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113804.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/brse-salary-up-to-2500-1-1314143-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=GMO-ZCOM&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113796.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/customer-service-no-experience-required-3-1-2-1315209-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=KDDIGROUP&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113798.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/wash-engineer-1-1313709-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=TARGET-WASH&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113743.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/lead-production-engineer-hardlines-2-1-1-1313719-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=TARGET-LEAD&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113745.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/sr-production-engineer-raw-materials-assurance-knit-1-1313120-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=TARGET-SR&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113747.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/marketing-assistant-to-ceo-thu-nhap-hap-dan-len-toi-1000-usd-1314487-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=DLS&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113749.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/marketing-manager-878-1-1-1314758-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=DOE&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113751.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/thiet-ke-do-hoa-2d-designer-ha-noi-1314761-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=HAMRUOU-2D&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600 (updated)_113753.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/marketing-seo-1-1314787-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=HAMRUOU-SEO&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113755.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/quan-ly-cua-hang-xe-may-dien-thu-nhap-20-den-trieu-thang-1313402-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=VINFAST&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113650.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/ky-su-thiet-ke-he-thong-cap-vien-thong-bat-dau-di-lam-tu-15-12-2020-1-1312838-jd//?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=ELEMENT&amp;utm_content=PRESTJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113612.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/general-manager-legal-vietnam-1-1315651-jd/?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=MERCEDESBENZ&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600_113723.png' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>",
                                                        "<div><a href = 'https://www.vietnamworks.com/ky-su-quan-ly-thiet-bi-dien-600-1100-binh-thuan-2-1314335-jd//?utm_source=WEBSITE&amp;utm_medium=RIGHT&amp;utm_campaign=VINHTAN1&amp;utm_content=STJBANNER' target='_blank' rel='noopener'><img width = '250' height='300' class='mb-3 border border-info' src='https://images.vietnamworks.com/logo/500x600 (updated)_113725.jpg' alt=' tuyển dụng - T&igrave;m việc mới nhất, lương thưởng hấp dẫn.' /></a></div>" };
            public static readonly string[] Images = { FormatHelper.FormatCompanyImage("company.jpg"),
                                                       FormatHelper.FormatCompanyImage("company_1.jpg"),
                                                       FormatHelper.FormatCompanyImage("company_2.jpg") };
            public static readonly string[] Videos = { "<iframe class='embed-responsive d-block responsive-size img-fluid' src='//www.youtube.com/embed/fCok4AtNDyM?autoplay=1&amp;autohide=1&amp;fs=1&amp;rel=0&amp;hd=1&amp;wmode=opaque&amp;enablejsapi=1'></iframe>" };
        }

        public struct Admin
        {
            public const string Avatar = "~/images/noimage.jpg";
        }        

        public struct Submit
        {
            public const double Delay = 2;
        }

        public struct Asset
        {
            public const string Loading = "~/asset/loading.gif";
        }
    }
}
