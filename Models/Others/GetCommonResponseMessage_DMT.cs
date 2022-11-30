using System;
using System.Collections.Generic;
using System.Linq;

namespace NewZapures_V2.Models.Others
{
    public class GetCommonResponseMessage_DMT
    {

        public static List<DMT_ResponseCode> responseCodeCollectionDMT(string responseCode = "200", string apiErrorCode = null)
        {
            Dictionary<string, List<DMT_ResponseCode>> errorCodeCollection = new Dictionary<string, List<DMT_ResponseCode>>();

            #region DMT 400 Collection
            List<DMT_ResponseCode> codeCollection400 = new List<DMT_ResponseCode>();
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "0", errorMessage = "Invalid client", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1039", errorMessage = "Invalid Beneficiary Id", status = "failure", systemimpacted = "Corporate" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1070", errorMessage = "Invalid Amount", status = "failure", systemimpacted = "Corporate" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1073", errorMessage = "Invalid first name", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1074", errorMessage = "Invalid last name", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1075", errorMessage = "Invalid address 1", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1076", errorMessage = "Invalid address 2", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1077", errorMessage = "Invalid state", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1078", errorMessage = "Invalid city", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1079", errorMessage = "Invalid country", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1080", errorMessage = "Invalid pin code", status = "failure" });
            codeCollection400.Add(new DMT_ResponseCode { responseCode = "1208", errorMessage = "Transaction mode and Bank IFSC are conflicting", status = "failure", systemimpacted = "Corporate/Retail" });
            #endregion


            #region DMT 401 Collection
            List<DMT_ResponseCode> codeCollection401 = new List<DMT_ResponseCode>();

            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG113", errorMessage = "Authorization Header is not present" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG118", errorMessage = "Partner Jwt Data is not present for partner id" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG119", errorMessage = "Partner details is not present for partner id" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG117", errorMessage = "Partner Id is not active" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG105", errorMessage = "You are not authorized to access this API" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG107", errorMessage = "Partner Sub Id is missing in JWT token" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG108", errorMessage = "Partner Sub Id is not ACTIVE for partner id" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG109", errorMessage = "Partner Sub Id does not exist for" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG110", errorMessage = "Invalid Issuer of JWT" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG111", errorMessage = "Cannot decode JWT Token" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG112", errorMessage = "Cannot verify signature of jwt token" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG123", errorMessage = "Token expiration should be a future date" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG124", errorMessage = "JWT passed is not valid" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG121", errorMessage = "Timestamp is either empty or invalid" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG116", errorMessage = "Issuer (iss) is not present in JWT Token" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "PASSG115", errorMessage = "requestReferenceId is not present in JWT Token" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "2001", errorMessage = "jwt is missing or invalid", status = "failure" });
            codeCollection401.Add(new DMT_ResponseCode { responseCode = "2004", errorMessage = "Invalid user token", status = "failure", systemimpacted = "Retail" });

            #endregion

            #region DMT Registration Status Code 200
            List<DMT_ResponseCode> codeCollection200 = new List<DMT_ResponseCode>();

            codeCollection200.Add(new DMT_ResponseCode { responseCode = "200", errorMessage = "Ok Success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "0", errorMessage = "Success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1023", errorMessage = "Downstream System Fails", systemimpacted = "Corporate", status = "unknown" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1024", errorMessage = "First name is present , last name and address not present", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1025", errorMessage = "First name is present , last name is present and address not present", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1026", errorMessage = "First name is present , last name is not present and address present", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1027", errorMessage = "First name is not present, last name present and address present", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1028", errorMessage = "First name is not present, last name not present and address present.", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1029", errorMessage = "First name is not present, last name present and address not present.", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1030", errorMessage = "Invalid mbId", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1031", errorMessage = "Mobile number not exists", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1032", errorMessage = "User Does Not Exists", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1033", errorMessage = "Invalid agentId", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1034", errorMessage = "No mapping of ifsc/bank name found", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1038", errorMessage = "Beneficiary already exists", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1042", errorMessage = "First name is not present, last name is not present and address not present.", status = "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1043", errorMessage = "Transaction Request Id Already Exists.", status = "duplicate", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1044", errorMessage = "Invalid otp.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1045", errorMessage = "Invalid State.",status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1046", errorMessage = "No benefiary exists.",status= "success" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1049", errorMessage = "Invalid Authorization.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1050", errorMessage = "Invalid Mobile.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1051", errorMessage = "Account Blocked.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1052", errorMessage = "OTP limit reached.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1054", errorMessage = "reached otp limit.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1055", errorMessage = "User does not prefer getting OTP on this number.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1056", errorMessage = "Beneficiary limit breached.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1057", errorMessage = "Account details incorrect.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1058", errorMessage = "Beneficiary daily limit breached.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1059", errorMessage = "No profile exists.", status = "failure" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1061", errorMessage = "Monthly limit not acceptable as per limit.", status = "failure",systemimpacted="Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1062", errorMessage = "User monthly limit breached with this amount.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1064", errorMessage = "Request declined by TS.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1065", errorMessage = "Product service unavailable.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1068", errorMessage = "Invalid ownership.", status = "failure", systemimpacted = "Retail" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1081", errorMessage = "beneficiary details empty.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1082", errorMessage = "account number empty.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1083", errorMessage = "beneficiary ifsc empty.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1084", errorMessage = "beneficiary name null or empty.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1085", errorMessage = "beneficiary nick name null or empty.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1086", errorMessage = "Login Failed.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1087", errorMessage = "Bad Request.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1088", errorMessage = "No valid user exist.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1089", errorMessage = "Invalid email.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1090", errorMessage = "Device id is blank.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1091", errorMessage = "Mobile number is already pending for verification.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1092", errorMessage = "Client does not have permission to fetch detail.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1093", errorMessage = "Blocked due to security reason.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1094", errorMessage = "Email already registered.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1095", errorMessage = "Mobile already registered.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1096", errorMessage = "Otp limit reached.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1097", errorMessage = "Device identifier mismatch", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1098", errorMessage = "Invalid mobile number", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1100", errorMessage = "Device is not verified.", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1103", errorMessage = "First leg of transactions failed or pending.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1104", errorMessage = "Second leg of transactions failed or pending.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1105", errorMessage = "First leg of transactions failed or pending.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1106", errorMessage = "Second leg of transactions failed or pending.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1107", errorMessage = "Last leg of transaction pending / Imps pending.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1108", errorMessage = "Last leg of transaction failed / Imps failed.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1207", errorMessage = "Beneficiary monthly limit breached with this amount.", status = "failure", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "1304", errorMessage = "Last leg of transaction pending NEFT.", status = "pending", systemimpacted = "Corporate" });
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "2003", errorMessage = "Service DB is down", status = "failure"});
            codeCollection200.Add(new DMT_ResponseCode { responseCode = "8003", errorMessage = "TS unavailable", status = "pending", systemimpacted = "Corporate" });
            #endregion

            #region DMT Registration Status Code 500
            List<DMT_ResponseCode> codeCollection500 = new List<DMT_ResponseCode>();

            codeCollection500.Add(new DMT_ResponseCode { responseCode = "2002", errorMessage = "Unknown error at our end", status = "unknown"});
            codeCollection500.Add(new DMT_ResponseCode { responseCode = "PASSG120", errorMessage = "Something went wrong, please try again after some time." });


            List<DMT_ResponseCode> codeCollection504 = new List<DMT_ResponseCode>();
            codeCollection504.Add(new DMT_ResponseCode { responseCode = "FAILURE_ORIGIN_CONNECTIVITY", errorMessage = "Could not connect to origin" });
            codeCollection504.Add(new DMT_ResponseCode { responseCode = "FAILURE_ORIGIN_READ_TIMEOUT", errorMessage = "The request to the origin timed out" });

            List<DMT_ResponseCode> codeCollection503 = new List<DMT_ResponseCode>();
            codeCollection503.Add(new DMT_ResponseCode { responseCode = "FAILURE_LOCAL_THROTTLED_ORIGIN_SERVER_MAXCONN", errorMessage = "Request throttled due to max connection limit reached" });

            List<DMT_ResponseCode> codeCollection502 = new List<DMT_ResponseCode>();
            codeCollection502.Add(new DMT_ResponseCode { responseCode = "FAILURE_ORIGIN", errorMessage = "The origin returned a failure" });

            #endregion


            errorCodeCollection.Add("401", codeCollection401);
            errorCodeCollection.Add("200", codeCollection200);
            errorCodeCollection.Add("500", codeCollection500);
            errorCodeCollection.Add("504", codeCollection504);
            errorCodeCollection.Add("503", codeCollection503);
            errorCodeCollection.Add("502", codeCollection502);

            if (apiErrorCode != null)
                return errorCodeCollection[responseCode].Where(wh => wh.responseCode == apiErrorCode).ToList();
            else
                return errorCodeCollection[responseCode];
        }

        public class DMT_ResponseCode
        {
            public string responseCode { get; set; }
            public string status { get; set; }
            public string systemimpacted { get; set; }
            public string errorMessage { get; set; }
        }

        public class DMTResponse
        {
            public string status { get; set; }
            public int response_code { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string customerMobile { get; set; }
            public int limitLeft { get; set; }
            public string txn_id { get; set; }

        }
    }
}
