using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class SessionModel : HttpSessionStateBase
    {
        public static Boolean IsLogged
        {
            get
            {
                if (HttpContext.Current.Session["IsLogged"] != null && HttpContext.Current.Session["IsLogged"].ToString() != "False")
                    return true;
                else
                    return false;
            }
            set
            {
                HttpContext.Current.Session["IsLogged"] = value;
            }
        }

        public static Boolean IsAdmin
        {
            get
            {
                if (HttpContext.Current.Session["IsAdmin"] != null && HttpContext.Current.Session["IsAdmin"].ToString() != "False")
                    return true;
                else
                    return false;
            }
            set
            {
                HttpContext.Current.Session["IsAdmin"] = value;
            }
        }




        public static SSOUserDetail SSOUserDetail
        {
            get
            {
                if (HttpContext.Current.Session["_SSOUserDetail"] != null)
                    return (SSOUserDetail)HttpContext.Current.Session["_SSOUserDetail"];
                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["_SSOUserDetail"] = value;
            }
        }


        //public static m_Batch BatchMaster
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["_BatchMaster"] != null)
        //            return (m_Batch)HttpContext.Current.Session["_BatchMaster"];
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["_BatchMaster"] = value;
        //    }
        //}


        public static long UserDetailId
        {
            get
            {
                if (HttpContext.Current.Session["_UserDetailId"] != null)
                {

                    return Convert.ToInt64(HttpContext.Current.Session["_UserDetailId"].ToString());
                }

                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["_UserDetailId"] = value;
            }
        }

        public static Guid UserGuid
        {
            get
            {
                if (HttpContext.Current.Session["_UserGuid"] != null)
                {

                    return new Guid(HttpContext.Current.Session["_UserGuid"].ToString());
                }

                else
                    return Guid.NewGuid();
            }
            set
            {
                HttpContext.Current.Session["_UserGuid"] = value;
            }
        }

        public static string DisplayName
        {
            get
            {
                if (HttpContext.Current.Session["_UserDisplayName"] != null)
                {

                    return HttpContext.Current.Session["_UserDisplayName"].ToString();
                }

                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["_UserDisplayName"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["_UserName"] != null)
                {

                    return HttpContext.Current.Session["_UserName"].ToString();
                }

                else
                    return null;
            }
            set
            {
                HttpContext.Current.Session["_UserName"] = value;
            }
        }

        public static int DepartmentId
        {
            get
            {
                if (HttpContext.Current.Session["_DepartmentId"] != null)
                {

                    return Convert.ToInt32(HttpContext.Current.Session["_DepartmentId"].ToString());
                }

                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["_DepartmentId"] = value;
            }
        }


        //public static m_UserDetails UserDetail
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session["_UserDetail"] != null)
        //            return (m_UserDetails)HttpContext.Current.Session["_UserDetail"];
        //        else
        //            return null;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["_UserDetail"] = value;
        //    }
        //}

        public static long ApplicantLoginId
        {
            get
            {
                if (HttpContext.Current.Session["_ApplicantLoginId"] != null)
                {

                    return Convert.ToInt64(HttpContext.Current.Session["_ApplicantLoginId"].ToString());
                }

                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["_ApplicantLoginId"] = value;
            }
        }
        public static long InternDetailId
        {
            get
            {
                if (HttpContext.Current.Session["_InternDetailId"] != null)
                {

                    return Convert.ToInt64(HttpContext.Current.Session["_InternDetailId"].ToString());
                }

                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["_InternDetailId"] = value;
            }
        }

        public static int BatchId
        {
            get
            {
                if (HttpContext.Current.Session["_BatchId"] != null)
                    return Convert.ToInt32(HttpContext.Current.Session["_BatchId"].ToString());
                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["_BatchId"] = value;
            }
        }

        public static string BatchCode
        {
            get
            {
                if (HttpContext.Current.Session["_BatchCode"] != null)
                    return HttpContext.Current.Session["_BatchCode"].ToString();
                else
                    return "";
            }
            set
            {
                HttpContext.Current.Session["_BatchCode"] = value;
            }
        }

        public static string BatchName
        {
            get
            {
                if (HttpContext.Current.Session["_BatchName"] != null)
                    return HttpContext.Current.Session["_BatchName"].ToString();
                else
                    return "";
            }
            set
            {
                HttpContext.Current.Session["_BatchName"] = value;
            }
        }


        public static Guid BatchGuid
        {
            get
            {
                if (HttpContext.Current.Session["_BatchGuid"] != null)
                {

                    return new Guid(HttpContext.Current.Session["_BatchGuid"].ToString());
                }

                else
                    return Guid.NewGuid();
            }
            set
            {
                HttpContext.Current.Session["_BatchGuid"] = value;
            }
        }

        public static int RoleId
        {
            get
            {
                if (HttpContext.Current.Session["_RoleId"] != null)
                    return Convert.ToInt32(HttpContext.Current.Session["_RoleId"].ToString());
                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["_RoleId"] = value;
            }
        }

        public static string RoleName
        {
            get
            {
                if (HttpContext.Current.Session["_RoleName"] != null)
                    return HttpContext.Current.Session["_RoleName"].ToString();
                else
                    return "";
            }
            set
            {
                HttpContext.Current.Session["_RoleName"] = value;
            }
        }


        public static string RoleCode
        {
            get
            {
                if (HttpContext.Current.Session["_RoleCode"] != null)
                    return HttpContext.Current.Session["_RoleCode"].ToString();
                else
                    return "";
            }
            set
            {
                HttpContext.Current.Session["_RoleCode"] = value;
            }
        }


      


    }
}