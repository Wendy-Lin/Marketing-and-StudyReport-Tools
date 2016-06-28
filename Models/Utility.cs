using System;
using System.IO;
using System.Data;
using System.Configuration;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.Mail;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Drawing;


/// <summary>
/// Summary description for Utility
/// </summary>
namespace Discoverx.Models
{
public static class Utility
{
    private static readonly Random Random = new Random();
    public enum EndianType
    {
        Unknown,
        Little,
        Middle,
        Big
    }

    public enum MessageType
    {
        Success,
        Warning,
        Error
    }

    public const string FileDownLoadPath=@"D:\Uploads\MaxCloud\";

    //************String Extended Validation Methods********************//


    public static bool IsValidFQDN(this string str, int maxLength, bool allowEmpty)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            return (str.Trim().Length <= maxLength && Regex.IsMatch(str.Trim(), "(?=^.{1,254}$)(^(?:(?!\\d+\\.)[a-zA-Z0-9_\\-]{1,63}\\.?)+(?:[a-zA-Z]{1,})$)")) ? true : false;

        }
    }

    public static bool IsValidIPv4Address(this string str, bool allowEmpty)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
            return Regex.IsMatch(str.Trim(), @"^([1-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])(\.([0-9]|[1-9][0-9]|1[0-9][0-9]|2[0-4][0-9]|25[0-5])){3}$");

    }

    public static bool IsValidIPv6Address(this string str, bool allowEmpty)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
            return Regex.IsMatch(str.Trim(), "/^\\s*((([0-9A-Fa-f]{1,4}:){7}([0-9A-Fa-f]{1,4}|:))|(([0-9A-Fa-f]{1,4}:){6}(:[0-9A-Fa-f]{1,4}|((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)(\\.(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){5}(((:[0-9A-Fa-f]{1,4}){1,2})|:((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)(\\.(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)){3})|:))|(([0-9A-Fa-f]{1,4}:){4}(((:[0-9A-Fa-f]{1,4}){1,3})|((:[0-9A-Fa-f]{1,4})?:((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)(\\.(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){3}(((:[0-9A-Fa-f]{1,4}){1,4})|((:[0-9A-Fa-f]{1,4}){0,2}:((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)(\\.(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){2}(((:[0-9A-Fa-f]{1,4}){1,5})|((:[0-9A-Fa-f]{1,4}){0,3}:((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)(\\.(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)){3}))|:))|(([0-9A-Fa-f]{1,4}:){1}(((:[0-9A-Fa-f]{1,4}){1,6})|((:[0-9A-Fa-f]{1,4}){0,4}:((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)(\\.(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)){3}))|:))|(:(((:[0-9A-Fa-f]{1,4}){1,7})|((:[0-9A-Fa-f]{1,4}){0,5}:((25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)(\\.(25[0-5]|2[0-4]\\d|1\\d\\d|[1-9]?\\d)){3}))|:)))(%.+)?\\s*$/");

    }
   

    public static bool IsValidName(this string str, int maxLength, bool allowEmpty)
    {

        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            if (str.Trim().Length <= maxLength)
                return Regex.IsMatch(str, "^[a-zA-Z0-9]");
            else
                return false;

        }
    }

    public static bool IsValidID(this string str, int minValue, int maxValue, bool allowEmpty)
    {

        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            if (Regex.IsMatch(str, "\\d+"))
            {
                int id = ParseInt(str.Trim());
                return (id <= maxValue && id >= minValue && id >= 0) ? true : false;
            }
            else
                return false;

        }
    }

    public static bool IsValidNumber(this string str, int minValue, int maxValue, bool allowEmpty)
    {

        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            if (Regex.IsMatch(str, "\\d+"))
            {
                int num = ParseInt(str.Trim());
                return (num <= maxValue && num >= minValue) ? true : false;
            }
            else
                return false;

        }
    }

    //***Matches times seperated by : will match a 24 hour time, or a 12 hour time with AM or PM specified. Allows 0-59 minutes, and 0-59 seconds. Seconds are not required.**//

    public static bool IsValidTime(this string str, bool allowEmpty)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
            return Regex.IsMatch(str, "^((([0]?[1-9]|1[0-2])(:)[0-5][0-9]((:)[0-5][0-9])?( )?(AM|am|aM|Am|PM|pm|pM|Pm))|(([0]?[0-9]|1[0-9]|2[0-3])(:)[0-5][0-9]((:)[0-5][0-9])?))$");

    }

    public static bool IsValidDate(this string str, bool allowEmpty, EndianType endianType)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            DateTime dateResult;
            DateTimeStyles styles=DateTimeStyles.None;

            if (endianType == EndianType.Unknown)
                return DateTime.TryParse(str, out dateResult);
            else if (endianType == EndianType.Little)  //**dd/mm/yyyy
                return DateTime.TryParse(str, CultureInfo.CreateSpecificCulture("fr-FR"), styles, out dateResult);
            else if (endianType == EndianType.Middle)  //**mm/dd/yyyy
                return DateTime.TryParse(str, CultureInfo.CreateSpecificCulture("en-US"), styles, out dateResult);
            else if (endianType == EndianType.Big) //** yyyy/mm/dd
                return DateTime.TryParse(str, CultureInfo.CreateSpecificCulture("ja-JP"), styles, out dateResult);
           
            else
                return false;
        }

    }

    public static bool IsValidText(this string str, int maxLength, bool allowEmpty)
    {

        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            return (str.Length <= maxLength) ? true : false;

        }
    }


    public static bool IsValidNumericString(this string str, int minLength, int maxLength, bool allowEmpty)
    {
       

        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            return (str.Trim().Length <= maxLength && str.Trim().Length >= minLength && Regex.IsMatch(str, "^\\d+$")) ? true : false;

        }
    
    }
    
    
    
    public static bool IsValidE164Number(this string str, int maxLength, bool allowEmpty)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            return (str.Trim().Length <= maxLength && Regex.IsMatch(str, "^\\+?\\d+$") ? true : false);

        }
       
    }

    public static bool IsValidPassword(this string str, int minLength, int maxLength, bool allowEmpty)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else
        {
            return (str.Trim().Length <= maxLength && str.Trim().Length >= minLength) ? true : false;

        }
    }



    public static bool IsValidSIPURI(this string str, int maxLength, bool allowEmpty)
    {
        string MatchSIPURIPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                                    + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                                    + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				                        [0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                                    + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,10})$";

        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else if (str.Trim().Length > maxLength)
        {
            return false;
        }
        else
        {
            return Regex.IsMatch(str, MatchSIPURIPattern);
        }
    }

    public static bool IsValidSamAccount(this string str, int maxLength, bool allowEmpty)
    {
        string MatchSIPURIPattern = @"^[\w]+\\[\w]+$";

        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else if (str.Trim().Length > maxLength)
        {
            return false;
        }
        else
        {
            return Regex.IsMatch(str, MatchSIPURIPattern);
        }
    }

    public static bool IsValidDomain(this string str, int maxLength, bool allowEmpty)
    {
        if (str.Trim().Length == 0)
        {
            return allowEmpty ? true : false;
        }
        else if (str.Trim().Length > maxLength)
        {
            return false;
        }
        else 
        {
            return  (Regex.IsMatch(str, "^[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}$"));
            
        }
    
    }

   

    //***********End of Validation Methods******************************//


    //************Extend Methods********************//


    public static string ParseFQDN(this string str)
    {

         return str.Trim();
       
    }

    public static string ParseIPv4Address(this string str)
    {

        return IPAddress.Parse(str).ToString();
       
    }

    public static string ParseIPv6Address(this string str)
    {
        return IPAddress.Parse(str).ToString();
       
    }


    public static string ParseName(this string str)
    {
         return str.Trim();
    }

    public static string ParseID(this string str)
    {
        return str.TrimStart('0');
       
    }

    public static string ParseNumber(this string str)
    {
        return str.TrimStart('0');
       
    }

    
    public static string ParseTime(this string str)
    {
        return str.Trim();
       
    }

    public static string ParseDate(this string str, int endianType)
    {
        DateTime date = ParseDate(str);
        if (endianType == 1)  //**dd/mm/yyyy
            return date.ToString("d", CultureInfo.CreateSpecificCulture("fr-FR"));
        else if (endianType == 2)  //**mm/dd/yyyy
            return date.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));
        else if (endianType == 3) //** yyyy/mm/dd
            return date.ToString("d", CultureInfo.CreateSpecificCulture("ja-JP"));
        else if (endianType == 0)
            return date.ToString();
        else
            return str.Trim();
    }

    public static DateTime? ParseDateOrDefault(this string inputDate)
    {
        return (inputDate == null || inputDate == string.Empty) ? null : (DateTime?)(inputDate.ParseDate());
    }



    public static string ParseNumericString(this string str)
    {

        return str.Trim();
       
    }



    public static string ParseE164Number(this string str)
    {
       return str.Trim();
       
    }

    public static string ParsePassword(this string str)
    {
        return str;
       
    }

    public static string ParseSIPURI(this string str)
    {
        return str.Trim();

    }

    public static string ReplaceGreekSymbol(this string str)
    {
        return str.Replace("β", "beta").Replace("α", "alpha").Replace("δ", "delta").Replace("-Δ", "delta").Replace("Δ", "delta").Replace("γ", "gamma").Replace("Γ", "gamma");

    }

    public static string RemoveSpecialCharacters(this string str)
    {
        if (str != null && str != string.Empty)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
        else
            return string.Empty;
    }

    public static string ParseString(this object p)
    {
        if (p == null || p == DBNull.Value)
            return string.Empty;
        else
            return p.ToString().Trim();
    }

    public static short ParseShort(this object p)
    {
        short result;

        if (p == null || p == DBNull.Value)
            result = 0;
        else
        {
            try
            {
                result = Convert.ToInt16(p.ToString());
            }
            catch
            {
                result = 0;
            }
        }
        return result;
    }

    public static int ParseInt(this object p)
    {
        int result;

        if (p == null || p == DBNull.Value)
            result = 0;
        else
        {
            try
            {
                result = Convert.ToInt32(p.ToString());
            }
            catch
            {
                result = 0;
            }
        }
        return result;
    }

    public static long ParseLong(this object p)
    {
        long result;

        if (p == null || p == DBNull.Value)
            result = 0;
        else
        {
            try
            {
                result = Convert.ToInt64(p.ToString());
            }
            catch
            {
                result = 0;
            }
        }
        return result;
    }

    public static Double ParseDouble(this object p)
    {
        Double result;

        if (p == null || p == DBNull.Value)
            result = 0;
        else
        {
            try
            {
                result = Convert.ToDouble(p.ToString());
            }
            catch
            {
                result = 0;
            }
        }
        return result;
    }

    public static DateTime ParseDate(this object p)
    {
        if (p == null || p == DBNull.Value)
            return DateTime.MinValue;
        else
        {
            try
            {
                return Convert.ToDateTime(p);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }

    public static bool ParseBoolean(this object p)
    {
        try
        {
            return Convert.ToBoolean(p);
        }
        catch
        {
            return false;
        }
    }

    public static string ParsePhoneNumber(this string number)
    {
        if (number != null)
        {
            
            number = number.Replace(" ", "");
            number = number.Replace("-", "");
            number = number.Replace("(", "");
            number = number.Replace(")", "");
            number = number.Replace(".", "");

            try
            {
                if (number.Length == 10)
                    return String.Format("{0:(###) ###-####}", double.Parse(number));
                else if (number.Length == 11)
                    return String.Format("{0: +# (###) ###-####}", double.Parse(number));
            }
            catch
            {
            }
            finally
            {

            }
        }
        return number;
    }

    public static long GetAreaCode(this object phoneNumber)
    {
        long pNumber = phoneNumber.ParseLong() / 10000000;
        return (phoneNumber.ParseLong()) / 10000000;
    }

    public static long GetPrefix(this object phoneNumber)
    {
        return ((phoneNumber.ParseLong()) / 10000) % 1000;
    }

    public static string GetOrthologTarget(this string target)
    {
        target=target.ParseString();
        if (target != string.Empty && (target.IndexOf("m") == 0 || target.IndexOf("r") == 0 || target.IndexOf("s") == 0 || target.IndexOf("c") == 0))
            return target.Left(1);
        else
            return string.Empty;
    }


    public static string ToQueryString(this object request, string separator = ",")
    {
        if (request == null)
            throw new ArgumentNullException("request");

        // Get all properties on the object
        var properties = request.GetType().GetProperties()
            .Where(x => x.CanRead)
            .Where(x => x.GetValue(request, null) != null)
            .ToDictionary(x => x.Name, x => x.GetValue(request, null));

        // Get names for all IEnumerable properties (excl. string)
        var propertyNames = properties
            .Where(x => !(x.Value is string) && x.Value is IEnumerable)
            .Select(x => x.Key)
            .ToList();

        // Concat all IEnumerable properties into a comma separated string
        foreach (var key in propertyNames)
        {
            var valueType = properties[key].GetType();
            var valueElemType = valueType.IsGenericType
                                    ? valueType.GetGenericArguments()[0]
                                    : valueType.GetElementType();
            if (valueElemType.IsPrimitive || valueElemType == typeof(string))
            {
                var enumerable = properties[key] as IEnumerable;
                properties[key] = string.Join(separator, enumerable.Cast<object>());
            }
        }

        // Concat all key/value pairs into a string separated by ampersand
        return string.Join("&", properties
            .Select(x => string.Concat(
                Uri.EscapeDataString(x.Key), "=",
                Uri.EscapeDataString(x.Value.ToString()))));
    }


    public static String GetErrorMessage(this ModelStateDictionary modelState)
    {
        ////return String.Join(Environment.NewLine, modelState.Values.SelectMany(v => v.Errors)
        //                                                   .Select(v => v.ErrorMessage));
        return String.Join("<br />", modelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage));

    }

    public static byte[] GetFileData(this string filePath)
    {

        if (!File.Exists(filePath))
            throw new FileNotFoundException("The file does not exist.",
                filePath);
        return File.ReadAllBytes(filePath);
    }

    public static string ReplaceFirstOccurrance(this string original, string oldValue, string newValue)
    {
        if (String.IsNullOrEmpty(original))
            return String.Empty;
        if (String.IsNullOrEmpty(oldValue))
            return original;
        if (String.IsNullOrEmpty(newValue))
            newValue = String.Empty;
        int loc = original.IndexOf(oldValue);
        if (loc != -1)
        {
            return original.Remove(loc, oldValue.Length).Insert(loc, newValue);
        }
        else
            return original;
    }

    public static string ReplaceLastOccurrence(this string Source, string Find, string Replace)
    {
        string result = Source;
        int Place;
        if (!(String.IsNullOrEmpty(Source) || String.IsNullOrEmpty(Find) || String.IsNullOrEmpty(Replace)))
        {
            Place = Source.LastIndexOf(Find);
            if (Place != -1)
            {
                result = Source.Remove(Place, Find.Length).Insert(Place, Replace);
            }
        }

        return result;
    }

    public static string Left(this string str, int length)
    {
        string result;
        if (str.Length >= length)
            result = str.Substring(0, length);
        else
            result = str;

        return result;
    }

    public static string Right(this string param, int length)
    {
        //start at the index based on the lenght of the sting minus
        //the specified lenght and assign it a variable
        string result = param.Substring(param.Length - length, length);
        //return the result of the operation
        return result;
    }


    public static string Mid(this string str, int startIndex, int length)
    {
        //start at the specified index in the string ang get N number of
        //characters depending on the lenght and assign it to a variable
        string result = str.Substring(startIndex, length);
        //return the result of the operation
        return result;
    }

    public static string Replace(this string strText, string strFind, string strReplace)
    {

        int iPos=strText.IndexOf(strFind);
        String strReturn="";
        while(iPos!=-1)
        {
          strReturn+=strText.Substring(0,iPos) + strReplace;
          strText=strText.Substring(iPos+strFind.Length);
          iPos=strText.IndexOf(strFind);
        }
        if(strText.Length>0)
          strReturn+=strText;
        return strReturn;
    }

    public static int getNumberOfOccurencies(this String inputString, String checkString)
    {
        if (checkString.Length > inputString.Length || checkString.Equals("")) { return 0; }
        int lengthDifference = inputString.Length - checkString.Length;
        int occurencies = 0;
        for (int i = 0; i < lengthDifference; i++)
        {
            if (inputString.Substring(i, checkString.Length).Equals(checkString)) { occurencies++; i += checkString.Length - 1; }
        }
        return occurencies;
    }

    public static double StandardDeviation(this IEnumerable<double> values)
    {
        double avg = values.Average();
        return Math.Sqrt(values.Sum(v => Math.Pow(v - avg, 2)) / (values.Count()-1));
    }

    public static double GetPercentageEfficacy(double avg, double MinValue, double MaxValue)
    {
        return Math.Round((double)(((avg - MinValue) / (MaxValue - MinValue)) * 100 ), 1);
    }

    public static XElement ToXElement(this XmlNode node) 
    { 
        XElement element = null; 
        if (null != node) { 
            element = XElement.Parse(node.OuterXml); 
        } 
        return element; 
    } 


    public static string RemoveHTMLTag(this string str)
    {
        return Regex.Replace(str, "<(.|\n)*?>", ""); 
    }

    public static byte[] ToByteArray(this string str)
    {
        return System.Text.Encoding.GetEncoding(1251).GetBytes(str);
    }

    public static DateTime GetBeginOfDate(this DateTime date)
    {

        return Utility.ParseDate((date.ToShortDateString()) + " 12:00:00 AM");
    }

    public static DateTime GetEndOfDate(this DateTime date)
    {

        return Utility.ParseDate((date.ToShortDateString()) + " 11:59:59 PM"); 
    }

    public static DateTime GetEndOfDate(this String date)
    {

        return Utility.ParseDate(date + " 11:59:59 PM");
    }

    public static int NthIndexOf(this string target, string value, int n)
    {
        Match m = Regex.Match(target, "((" + Regex.Escape(value) + ").*?){" + n + "}");

        if (m.Success)
            return m.Groups[2].Captures[n - 1].Index;
        else
            return -1;
    }

    public static int GetQuarter(this DateTime dt)
    {
        return (dt.Month - 1) / 3 + 1;
    }

    public static string GetTwoDigitMonth(this DateTime dt)
    {
        return dt.Month < 10 ? String.Format("0{0}", dt.Month) : dt.Month.ToString();
    }

    public static int GetTwoDigitYear(this DateTime dt)
    {
        return (dt.Year % 100);
    }
    
    private static string HttpContent(string url)
    {
        System.Net.WebRequest objRequest = System.Net.HttpWebRequest.Create(url);
        StreamReader sr = new StreamReader(objRequest.GetResponse().GetResponseStream());
        string result = sr.ReadToEnd();
        sr.Close();
        return result;
    }

   
    
    public static void WriteBinaryFile(string fileName, byte[] binary)
    {
        try
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write(binary);
                }
            }
        }
        catch { throw; }
    }

    public static void WriteTextFile(string fileName, string contents)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(fileName, false))
            {
                sw.Write(contents);
            }
        }
        catch
        { }
    }

  
    
    
   


    
    public static bool IsValidSqlDate(this DateTime date)
    {
        return ((date >= (DateTime)SqlDateTime.MinValue) && (date <= (DateTime)SqlDateTime.MaxValue));
    }


    public static bool IsImageExistRemotely(string uriToImage, string mimeType = "")
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);
        request.Method = "HEAD";

        try
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK) //&& response.ContentType == mimeType
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch
        {
            return false;
        }
    }
   
    public static System.Drawing.Image GetRemoteImage(string URL) 
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);

        request.Timeout = 20000;
        request.ReadWriteTimeout = 20000;

        HttpWebResponse response= (HttpWebResponse)request.GetResponse();
        System.Drawing.Image img = System.Drawing.Image.FromStream(response.GetResponseStream());
        return img;

    }

    public static double GetAverage(double value1, double value2)
    {
        return ((value1.ParseDouble() + value2.ParseDouble()) / 2);
    }
   
    public static string ByteArrayToStr(byte[] dBytes)
    {

        return System.Text.Encoding.GetEncoding(1251).GetString(dBytes);

    }

    public static int Pixel2MTU(int pixels)
    {
        int mtus = pixels * 9525;
        return mtus;
    }

    /// <summary>
    /// Function to save byte array to a file
    /// </summary>
    /// <param name="_FileName">File name to save byte array</param>
    /// <param name="_ByteArray">Byte array to save to external file</param>
    /// <returns>Return true if byte array save successfully, if not return false</returns>
    public static bool ByteArrayToFile(string _FileName, byte[] _ByteArray)
    {
        try
        {
            // Open file for reading
            FileStream _FileStream = new FileStream(_FileName, FileMode.Create, FileAccess.Write);

            // Writes a block of bytes to this stream using data from a byte array.
            _FileStream.Write(_ByteArray, 0, _ByteArray.Length);

            // close file stream
            _FileStream.Close();

            return true;
        }
        catch (Exception _Exception)
        {
            // Error
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
        }

        // error occured, return false
        return false;
    }

    public static String ConvertToBase(this int num, int nbase)
    {
        String chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        // check if we can convert to another base
        if (nbase < 2 || nbase > chars.Length)
            return "";

        int r;
        String newNumber = "";

        // in r we have the offset of the char that was converted to the new base
        while (num >= nbase)
        {
            r = num % nbase;
            newNumber = chars[r] + newNumber;
            num = num / nbase;
        }
        // the last number to convert
        newNumber = chars[num] + newNumber;

        return newNumber;
    }

    public static string CreateRandomPassword(int passwordLength)
    {
        string allowedChars = "abcdefghijkmnpqrstuvwxyzABCDEFGHJKLMNPQRSTUVWXYZ123456789@$#_-";
        char[] chars = new char[passwordLength];
        Random rd = new Random();

        for (int i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
        }

        return new string(chars);
    }

    /////////////////////////////////////////////////////////////////////////////
    //
    //		AltiCrypt ()
    //
    //		Parameters:
    //			pw			[input] String to be encrypted. 
    //							(only first 8 characters are significant)
    //
    //			salt		[input] String to be used as 'salt'.
    //							(only first 2 characters are significant)
    //
    //			encrypted	[output] Encrypted string of 12 characters.
    //							Assumes that the string is preallocated.
    //							The first 2 characters denote the salt used.
    //
    //		Returns:
    //			The encrypted string.
    //
    //////////////////////////////////////////////////////////////////////////////

    public static string altiCrypt(this string pw, string encode)
    {
        char[] salt = new char[3];
        int i, j, c;
        int temp;
        char[] block = new char[66];
        char[] iobuf = new char[16];
        char[] localsalt = new char[3];
        char[] C = new char[28];
        char[] D = new char[28];
        char[] E = new char[48];
        char[] L = new char[64];
        //char *R = &L[32];
        char[] R = new char[32];
        char[] tempL = new char[32];
        char[] f = new char[32];
        char[] preS = new char[48];
        char[,] KS = new char[16, 48];
        int[] PC1_C = {
	        57,49,41,33,25,17, 9,
	         1,58,50,42,34,26,18,
	        10, 2,59,51,43,35,27,
	        19,11, 3,60,52,44,36,
        };

        int[] PC1_D = {
	        63,55,47,39,31,23,15,
	         7,62,54,46,38,30,22,
	        14, 6,61,53,45,37,29,
	        21,13, 5,28,20,12, 4,
        };
        int[] PC2_C = {
	        14,17,11,24, 1, 5,
	         3,28,15, 6,21,10,
	        23,19,12, 4,26, 8,
	        16, 7,27,20,13, 2,
        };

        int[] PC2_D = {
	        41,52,31,37,47,55,
	        30,40,51,45,33,48,
	        44,49,39,56,34,53,
	        46,42,50,36,29,32,
        };

        int[] shifts = { 1, 1, 2, 2, 2, 2, 2, 2, 1, 2, 2, 2, 2, 2, 2, 1, };

        int[] e2 = {
	        32, 1, 2, 3, 4, 5,
	         4, 5, 6, 7, 8, 9,
	         8, 9,10,11,12,13,
	        12,13,14,15,16,17,
	        16,17,18,19,20,21,
	        20,21,22,23,24,25,
	        24,25,26,27,28,29,
	        28,29,30,31,32, 1,
        };
        int[] IP = {
	        58,50,42,34,26,18,10, 2,
	        60,52,44,36,28,20,12, 4,
	        62,54,46,38,30,22,14, 6,
	        64,56,48,40,32,24,16, 8,
	        57,49,41,33,25,17, 9, 1,
	        59,51,43,35,27,19,11, 3,
	        61,53,45,37,29,21,13, 5,
	        63,55,47,39,31,23,15, 7,
        };
        int[] FP = {
	        40, 8,48,16,56,24,64,32,
	        39, 7,47,15,55,23,63,31,
	        38, 6,46,14,54,22,62,30,
	        37, 5,45,13,53,21,61,29,
	        36, 4,44,12,52,20,60,28,
	        35, 3,43,11,51,19,59,27,
	        34, 2,42,10,50,18,58,26,
	        33, 1,41, 9,49,17,57,25,
        };
        int[,] S = new int[8, 64]{
	        {14, 4,13, 1, 2,15,11, 8, 3,10, 6,12, 5, 9, 0, 7,
	         0,15, 7, 4,14, 2,13, 1,10, 6,12,11, 9, 5, 3, 8,
	         4, 1,14, 8,13, 6, 2,11,15,12, 9, 7, 3,10, 5, 0,
	        15,12, 8, 2, 4, 9, 1, 7, 5,11, 3,14,10, 0, 6,13},

	        {15, 1, 8,14, 6,11, 3, 4, 9, 7, 2,13,12, 0, 5,10,
	         3,13, 4, 7,15, 2, 8,14,12, 0, 1,10, 6, 9,11, 5,
	         0,14, 7,11,10, 4,13, 1, 5, 8,12, 6, 9, 3, 2,15,
	        13, 8,10, 1, 3,15, 4, 2,11, 6, 7,12, 0, 5,14, 9},

	        {10, 0, 9,14, 6, 3,15, 5, 1,13,12, 7,11, 4, 2, 8,
	        13, 7, 0, 9, 3, 4, 6,10, 2, 8, 5,14,12,11,15, 1,
	        13, 6, 4, 9, 8,15, 3, 0,11, 1, 2,12, 5,10,14, 7,
	         1,10,13, 0, 6, 9, 8, 7, 4,15,14, 3,11, 5, 2,12},

	         {7,13,14, 3, 0, 6, 9,10, 1, 2, 8, 5,11,12, 4,15,
	        13, 8,11, 5, 6,15, 0, 3, 4, 7, 2,12, 1,10,14, 9,
	        10, 6, 9, 0,12,11, 7,13,15, 1, 3,14, 5, 2, 8, 4,
	         3,15, 0, 6,10, 1,13, 8, 9, 4, 5,11,12, 7, 2,14},

	         {2,12, 4, 1, 7,10,11, 6, 8, 5, 3,15,13, 0,14, 9,
	        14,11, 2,12, 4, 7,13, 1, 5, 0,15,10, 3, 9, 8, 6,
	         4, 2, 1,11,10,13, 7, 8,15, 9,12, 5, 6, 3, 0,14,
	        11, 8,12, 7, 1,14, 2,13, 6,15, 0, 9,10, 4, 5, 3},

	        {12, 1,10,15, 9, 2, 6, 8, 0,13, 3, 4,14, 7, 5,11,
	        10,15, 4, 2, 7,12, 9, 5, 6, 1,13,14, 0,11, 3, 8,
	         9,14,15, 5, 2, 8,12, 3, 7, 0, 4,10, 1,13,11, 6,
	         4, 3, 2,12, 9, 5,15,10,11,14, 1, 7, 6, 0, 8,13},

	         {4,11, 2,14,15, 0, 8,13, 3,12, 9, 7, 5,10, 6, 1,
	        13, 0,11, 7, 4, 9, 1,10,14, 3, 5,12, 2,15, 8, 6,
	         1, 4,11,13,12, 3, 7,14,10,15, 6, 8, 0, 5, 9, 2,
	         6,11,13, 8, 1, 4,10, 7, 9, 5, 0,15,14, 2, 3,12},

	        {13, 2, 8, 4, 6,15,11, 1,10, 9, 3,14, 5, 0,12, 7,
	         1,15,13, 8,10, 3, 7, 4,12, 5, 6,11, 0,14, 9, 2,
	         7,11, 4, 1, 9,12,14, 2, 0, 6,10,13,15, 3, 5, 8,
	         2, 1,14, 7, 4,10, 8,13,15,12, 9, 0, 3, 5, 6,11}
        };
        int[] P = {
	        16, 7,20,21,
	        29,12,28,17,
	         1,15,23,26,
	         5,18,31,10,
	         2, 8,24,14,
	        32,27, 3, 9,
	        19,13,30, 6,
	        22,11, 4,25,
        };
        if (encode == null)
        {
            salt[0] = (char)(pw.Length + (int)'A');		// using the length of the pw as salt[0]
            salt[1] = (char)((new Random().Next() % 26) + (int)'A');	// a random character..
            salt[2] = (char)0;		// make sure it is null-terminated..
        }
        else
        {
            salt[0] = encode.ToCharArray()[0];
            salt[1] = encode.ToCharArray()[1];
            salt[2] = (char)0;
        }
        if (salt == null)
        {
            salt = new char[3];
        }
        if (salt[0] == 0)
        {
            salt[0] = (char)((new Random().Next() % 48) + (int)'A');
            salt[1] = (char)((new Random().Next() % 48) + (int)'A');
            salt[2] = (char)0;
        }
        c = pw.ToCharArray()[0];
        int cursor = 0;
        for (i = 0; i < 66; i++)
            block[i] = (char)0;
        for (i = 0; c != 0 && i < 64 && cursor < pw.Length; )
        {
            for (j = 0; j < 7; j++, i++)
                block[i] = (char)((c >> (6 - j)) & 01);
            i++;
            ++cursor;
            if (cursor < pw.Length)
                c = pw.ToCharArray()[cursor];
        }

        //	AltiLocalSetkey(block, C, D, E, KS);
        {
            char[] key = block;
            int i1, j1, k;
            int t;

            /*
             * First, generate C and D by permuting
             * the key.  The low order bit of each
             * 8-bit char is not used, so C and D are only 28
             * bits apiece.
             */
            for (i1 = 0; i1 < 28; i1++)
            {
                C[i1] = key[PC1_C[i1] - 1];
                D[i1] = key[PC1_D[i1] - 1];
            }
            /*
             * To generate Ki, rotate C and D according
             * to schedule and pick up a permutation
             * using PC2.
             */
            for (i1 = 0; i1 < 16; i1++)
            {
                /*
                 * rotate.
                 */
                for (k = 0; k < shifts[i1]; k++)
                {
                    t = C[0];
                    for (j1 = 0; j1 < 28 - 1; j1++)
                        C[j1] = C[j1 + 1];
                    C[27] = (char)t;
                    t = D[0];
                    for (j1 = 0; j1 < 28 - 1; j1++)
                        D[j1] = D[j1 + 1];
                    D[27] = (char)t;
                }
                /*
                 * get Ki. Note C and D are concatenated.
                 */
                for (j1 = 0; j1 < 24; j1++)
                {
                    KS[i1, j1] = C[PC2_C[j1] - 1];
                    KS[i1, j1 + 24] = D[PC2_D[j1] - 28 - 1];
                }
            }

            for (i1 = 0; i1 < 48; i1++)
                E[i1] = (char)e2[i1];
        }

        for (i = 0; i < 66; i++)
            block[i] = (char)0;

        for (i = 0; i < 2; i++)
        {
            c = salt[i];
            iobuf[i] = (char)c;
            if (c > 'Z')
                c -= 6;
            if (c > '9')
                c -= 7;
            c -= '.';
            for (j = 0; j < 6; j++)
            {
                if (((c >> j) & 01) != 0)
                {
                    temp = E[6 * i + j];
                    E[6 * i + j] = E[6 * i + j + 24];
                    E[6 * i + j + 24] = (char)temp;
                }
            }
        }

        for (int iter = 0; iter < 25; iter++)
        {
            // AltiLocalEncrypt(block, 0, f, E, L, R, tempL, preS, KS);
            int i2, ii;
            int t, j2, k;

            /*
             * First, permute the bits in the input
             */
            for (j2 = 0; j2 < 64; j2++)
                L[j2] = block[IP[j2] - 1];
            /*
             * Perform an encryption operation 16 times.
             */
            for (ii = 0; ii < 16; ii++)
            {
                i2 = ii;
                /*
                 * Save the R array,
                 * which will be the new L.
                 */
                for (j2 = 0; j2 < 32; j2++)
                    tempL[j2] = L[j2 + 32];
                /*
                 * Expand R to 48 bits using the E selector;
                 * exclusive-or with the current key bits.
                 */
                for (j2 = 0; j2 < 48; j2++)
                    preS[j2] = (char)(L[E[j2] - 1 + 32] ^ KS[i2, j2]);
                /*
                 * The pre-select bits are now considered
                 * in 8 groups of 6 bits each.
                 * The 8 selection functions map these
                 * 6-bit quantities into 4-bit quantities
                 * and the results permuted
                 * to make an f(R, K).
                 * The indexing into the selection functions
                 * is peculiar; it could be simplified by
                 * rewriting the tables.
                 */
                for (j2 = 0; j2 < 8; j2++)
                {
                    t = 6 * j2;
                    k = S[j2, (preS[t + 0] << 5) +
                        (preS[t + 1] << 3) +
                        (preS[t + 2] << 2) +
                        (preS[t + 3] << 1) +
                        (preS[t + 4] << 0) +
                        (preS[t + 5] << 4)];
                    t = 4 * j2;
                    f[t + 0] = (char)((k >> 3) & 01);
                    f[t + 1] = (char)((k >> 2) & 01);
                    f[t + 2] = (char)((k >> 1) & 01);
                    f[t + 3] = (char)((k >> 0) & 01);
                }
                /*
                 * The new R is L ^ f(R, K).
                 * The f here has to be permuted first, though.
                 */
                for (j2 = 0; j2 < 32; j2++)
                    L[j2 + 32] = (char)(L[j2] ^ f[P[j2] - 1]);
                /*
                 * Finally, the new L (the original R)
                 * is copied back.
                 */
                for (j2 = 0; j2 < 32; j2++)
                    L[j2] = tempL[j2];
            }
            /*
             * The output L and R are reversed.
             */
            for (j2 = 0; j2 < 32; j2++)
            {
                t = L[j2];
                L[j2] = L[j2 + 32];
                L[j2 + 32] = (char)t;
            }
            /*
             * The final output
             * gets the inverse permutation of the very original.
             */
            for (j2 = 0; j2 < 64; j2++)
                block[j2] = L[FP[j2] - 1];
        }

        for (i = 0; i < 10; i++)
        {
            c = 0;
            for (j = 0; j < 6; j++)
            {
                c <<= 1;
                c |= block[6 * i + j];
            }
            c += '.';
            if (c > '9')
                c += 7;
            if (c > 'Z')
                c += 6;
            iobuf[i + 2] = (char)c;
        }
        iobuf[i + 2] = (char)0;
        if (iobuf[1] == 0)
            iobuf[1] = iobuf[0];
        return new string(iobuf);
    }


    public static Boolean IsEmpty(this DataSet ds)
    {
        if (ds == null)
            return true;
        else if (ds.Tables.Count == 0)
            return true;
        else
        {
            if (ds.Tables[0].Rows.Count == 0)
                return true;
            else
                return false;
        }
    }

    public static Boolean IsEmpty(this DataTable dt)
    {
        if (dt == null)
            return true;
        else
        {
            if (dt.Rows.Count == 0)
                return true;
            else
                return false;
        }
    }

    public static bool isPropertiesChanged(this object source, object target)
    {
        bool bChanged = false;
        var theType = target.GetType();
        foreach (var prop in source.GetType().GetProperties())
        {
            var propGetter = prop.GetGetMethod();
            if (theType.GetProperty(prop.Name) == null)
                continue;
            
            var t_propGetter = theType.GetProperty(prop.Name).GetGetMethod();
            if (t_propGetter == null)
                continue;
            
            var valueToSet = propGetter.Invoke(source, null);
            var t_valueToSet = t_propGetter.Invoke(target, null);

            if (valueToSet != null && t_valueToSet == null)
            {
                bChanged = true;
            }
            else if (valueToSet != null && t_valueToSet != null && !valueToSet.Equals(t_valueToSet))
            {
                bChanged = true;
            }
        }

        return bChanged;
    }

    public static void copyPropertiesTo(this object source, object target)
    {
        var theType = target.GetType();
        foreach (var prop in source.GetType().GetProperties())
        {
            var propGetter = prop.GetGetMethod();
            if (theType.GetProperty(prop.Name) == null)
                continue;
            var propSetter = theType.GetProperty(prop.Name).GetSetMethod();
            if (propSetter == null)
                continue;
            var valueToSet = propGetter.Invoke(source, null);
            if (propSetter != null)
            {
                propSetter.Invoke(target, new[] { valueToSet });
            }
        }
    }

    
     public static void copyPropertiesTo2(this object source, object target)
     {
         var theType = target.GetType();
          foreach (var prop in source.GetType().GetProperties())
          {
                  var propGetter = prop.GetGetMethod();
                  if (theType.GetProperty(prop.Name) == null)
                      continue;
                  var propSetter = theType.GetProperty(prop.Name).GetSetMethod();
                  if (propSetter == null)
                      continue;
                 var valueToSet = propGetter.Invoke(source, null);
                  if (propSetter != null && valueToSet != null)
                  {
                      propSetter.Invoke(target, new[] { valueToSet });
                  }
          }
      }
  



    public static void clearObject(this object obj)
    {
        var OType = obj.GetType();
        foreach (var prop in OType.GetProperties())
        {
            var propGetter = prop.GetGetMethod();
            if (!prop.PropertyType.ToString().StartsWith("MaxCloud.Models") && !prop.PropertyType.ToString().StartsWith("System.Collections"))
                continue;
            var propSetter = prop.GetSetMethod();
            object valueToSet = null;
            propSetter.Invoke(obj, new[] { valueToSet });
        }
    }


    public static object addCleanObj(this object dbSet, object i)
    {
        Type t = i.GetType();
        Assembly a = Assembly.GetAssembly(t);
        object iCopy = a.CreateInstance(t.FullName);
        i.copyPropertiesTo(iCopy);
        iCopy.clearObject();
        MethodInfo method = dbSet.GetType().GetMethod("Add");
        method.Invoke(dbSet, new [] { iCopy });
        return iCopy;
    }

    public static void AddRange<TEntity>(this DbSet<TEntity> dbSet, IList<TEntity> entities) where TEntity : class
    {
        foreach (TEntity e in entities)
        {
            dbSet.Add(e);
        }
    }

    public static int daysPast(this object oldDate)
    {
        if (oldDate != null)
        {
            DateTime oldDate1 = (DateTime)oldDate;

            long tickDiff = DateTime.Now.Ticks - oldDate1.Ticks;
            tickDiff = tickDiff / 10000000; //have seconds now
            int _age = (int)(tickDiff / 86400);
            return _age;//should be days
        }
        return 0;
    }

    public static int minsPast(this object oldDate)
    {
        if (oldDate != null)
        {
            DateTime oldDate1 = (DateTime)oldDate;

            long tickDiff = DateTime.Now.Ticks - oldDate1.Ticks;
            tickDiff = tickDiff / 10000000; //have seconds now
            int _age = (int)(tickDiff / 60);
            return _age;//should be mins
        }
        return 0;
    }

    public static String GetLicenseFormat(String licensekey, out int count)
    {
        String keyFormat;
        count = -1;
        try
        {
            if (Left(licensekey, 5).ToUpper() == "6AM06")
            {
                keyFormat = Utility.Left(licensekey, 5);
                count = 1;
            }
            else if (Left(licensekey, 1) == "6" || Left(licensekey, 1) == "4")
            {
                keyFormat = Utility.Left(licensekey, 4);
                count = 1;
            }
            else if (Left(licensekey, 1) == "9" && (Left(licensekey, 2) != "9D" && Left(licensekey, 2) != "9E" && Left(licensekey, 2) != "9F"))
            {
                keyFormat = Utility.Left(licensekey, 4);
                count = 1;
            }
            else
            {
                if (Left(licensekey, 1) == "R" || Left(licensekey, 1) == "S" || Left(licensekey, 1) == "N" || Left(licensekey, 1) == "9")
                {
                    count = getFeatureCount(licensekey, 2, 2);
                    keyFormat = Utility.Left(licensekey, 2) + "nn";

                }
                else if (Left(licensekey, 1) == "M")
                {
                    count = getFeatureCount(licensekey, 2, 3);
                    keyFormat = Utility.Left(licensekey, 2) + "nnn";
                }
                else
                {
                    if (Left(licensekey, 1) == "1")
                    {
                        count = getFeatureCount(licensekey, 1, 3);
                    }

                    else
                    {
                        count = getFeatureCount(licensekey, 2, 2);
                    }

                    keyFormat = Utility.Left(licensekey, 1) + "nnn";
                }


            }
        }
        catch (Exception _Exception)
        {
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            keyFormat = "";
        }

        return keyFormat;
    }

    public static int getFeatureCount(string licensekey, int startIndex, int length)
    {
        int num = 0;
        try{
            num= Convert.ToInt32(Utility.Mid(licensekey, startIndex, length), 16);
        }
        catch (Exception _Exception)
        {
            Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            num = 0;
        }
        return num;
    }

    public static Nullable<double> RoundToSignificantDigits(this Nullable<double> d, int digits)
    {
        if (d == 0)
            return 0;
        else if (d == null)
            return d;

        else
        {
            double scale = Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(d.ParseDouble()))) + 1);
            return scale * Math.Round(d.ParseDouble() / scale, digits);
        }
    }

    public static Nullable<double> NullableMathRound(this Nullable<double> d, int digits)
    {
        if (d == null)
            return d;
        else
        {
            return Math.Round(d.ParseDouble(), digits);
        }
    }

    public static void CopyProperties(object source, object target)
    {
        var sourceType = source.GetType();
        var targetType = target.GetType();
        var propMap = GetMatchingProperties(sourceType, targetType);

        for (var i = 0; i < propMap.Count; i++)
        {
            var prop = propMap[i];
            var sourceValue = prop.SourceProperty.GetValue(source,
                                null);
            prop.TargetProperty.SetValue(target, sourceValue, null);
        }
    }

    public static IList<PropertyMap> GetMatchingProperties(Type sourceType, Type targetType)
    {
        var sourceProperties = sourceType.GetProperties();
        var targetProperties = targetType.GetProperties();

        var properties = (from s in sourceProperties
                          from t in targetProperties
                          where s.Name == t.Name &&
                                s.CanRead &&
                                t.CanWrite &&
                                s.PropertyType.IsPublic &&
                                t.PropertyType.IsPublic &&
                                s.PropertyType == t.PropertyType &&
                                (
                                  (s.PropertyType.IsValueType &&
                                   t.PropertyType.IsValueType
                                  ) ||
                                  (s.PropertyType == typeof(string) &&
                                   t.PropertyType == typeof(string)
                                  )
                                )
                          select new PropertyMap
                          {
                              SourceProperty = s,
                              TargetProperty = t
                          }).ToList();
        return properties;
    }

    public class PropertyMap
    {
        public PropertyInfo SourceProperty { get; set; }
        public PropertyInfo TargetProperty { get; set; }
    }

    private static Dictionary<string, PropertyMap[]> _maps =
    new Dictionary<string, PropertyMap[]>();

    public static void AddPropertyMap<T, TU>()
    {
        var props = GetMatchingProperties(typeof(T), typeof(TU));
        var className = GetClassName(typeof(T), typeof(TU));

        _maps.Add(className, props.ToArray());
    }

    //public static void CopyProperties(object source,
    //    object target)
    //{

    //    var className = GetClassName(source.GetType(),
    //                                 target.GetType());
    //    var propMap = _maps[className];

    //    for (var i = 0; i < propMap.Length; i++)
    //    {
    //        var prop = propMap[i];
    //        var sourceValue = prop.SourceProperty.GetValue(source,
    //                          null);
    //        prop.TargetProperty.SetValue(target, sourceValue, null);
    //    }
    //}

    public static string GetClassName(Type sourceType,
        Type targetType)
    {
        var className = "Copy_";
        className += sourceType.FullName.Replace(".", "_");
        className += "_";
        className += targetType.FullName.Replace(".", "_");

        return className;
    }

    // Define default min and max password lengths.
    private static int DEFAULT_MIN_PASSWORD_LENGTH  = 8;
    private static int DEFAULT_MAX_PASSWORD_LENGTH  = 15;

    // Define supported password characters divided into groups.
    // You can add (or remove) characters to (from) these groups.
    private static string PASSWORD_CHARS_LCASE  = "abcdefgijkmnopqrstwxyz";
    private static string PASSWORD_CHARS_UCASE  = "ABCDEFGHJKLMNPQRSTWXYZ";
    private static string PASSWORD_CHARS_NUMERIC= "23456789";
    private static string PASSWORD_CHARS_SPECIAL= "*$-+?_&=!%{}/";

    public static string GeneratePassword(int length, bool strongPassword)
    {
        if (length >= DEFAULT_MIN_PASSWORD_LENGTH && length <= DEFAULT_MAX_PASSWORD_LENGTH)
        {
            return GeneratePassword(length, length, strongPassword);
        }
        return "";
    }

    public static string GeneratePassword(int minLength,
                                  int maxLength, bool strongPassword)
    {
        // Make sure that input parameters are valid.
        if (minLength <= 0 || maxLength <= 0 || minLength > maxLength)
            return null;

        // Create a local array containing supported password characters
        // grouped by types. You can remove character groups from this
        // array, but doing so will weaken the password strength.
        char[][]  charGroups; 
        if (strongPassword)
        {
            charGroups = new char[][] 
            {
                PASSWORD_CHARS_LCASE.ToCharArray(),
                PASSWORD_CHARS_UCASE.ToCharArray(),
                PASSWORD_CHARS_NUMERIC.ToCharArray(),
                PASSWORD_CHARS_SPECIAL.ToCharArray()
            };
        }
        else
        {
            charGroups = new char[][] 
            {
                PASSWORD_CHARS_LCASE.ToCharArray(),
                PASSWORD_CHARS_UCASE.ToCharArray(),
                PASSWORD_CHARS_NUMERIC.ToCharArray()
            };
        }

        // Use this array to track the number of unused characters in each
        // character group.
        int[] charsLeftInGroup = new int[charGroups.Length];

        // Initially, all characters in each group are not used.
        for (int i=0; i<charsLeftInGroup.Length; i++)
            charsLeftInGroup[i] = charGroups[i].Length;
        
        // Use this array to track (iterate through) unused character groups.
        int[] leftGroupsOrder = new int[charGroups.Length];

        // Initially, all character groups are not used.
        for (int i=0; i<leftGroupsOrder.Length; i++)
            leftGroupsOrder[i] = i;

        // Because we cannot use the default randomizer, which is based on the
        // current time (it will produce the same "random" number within a
        // second), we will use a random number generator to seed the
        // randomizer.
        
        // Use a 4-byte array to fill it with random bytes and convert it then
        // to an integer value.
        byte[] randomBytes = new byte[4];

        // Generate 4 random bytes.
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        rng.GetBytes(randomBytes);

        // Convert 4 bytes into a 32-bit integer value.
        int seed = (randomBytes[0] & 0x7f) << 24 |
                    randomBytes[1]         << 16 |
                    randomBytes[2]         <<  8 |
                    randomBytes[3];

        // Now, this is real randomization.
        Random  random  = new Random(seed);

        // This array will hold password characters.
        char[] password = null;

        // Allocate appropriate memory for the password.
        if (minLength < maxLength)
            password = new char[random.Next(minLength, maxLength+1)];
        else
            password = new char[minLength];

        // Index of the next character to be added to password.
        int nextCharIdx;
        
        // Index of the next character group to be processed.
        int nextGroupIdx;

        // Index which will be used to track not processed character groups.
        int nextLeftGroupsOrderIdx;
        
        // Index of the last non-processed character in a group.
        int lastCharIdx;

        // Index of the last non-processed group.
        int lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
        
        // Generate password characters one at a time.
        for (int i=0; i<password.Length; i++)
        {
            // If only one character group remained unprocessed, process it;
            // otherwise, pick a random character group from the unprocessed
            // group list. To allow a special character to appear in the
            // first position, increment the second parameter of the Next
            // function call by one, i.e. lastLeftGroupsOrderIdx + 1.
            if (lastLeftGroupsOrderIdx == 0)
                nextLeftGroupsOrderIdx = 0;
            else
                nextLeftGroupsOrderIdx = random.Next(0, 
                                                     lastLeftGroupsOrderIdx);

            // Get the actual index of the character group, from which we will
            // pick the next character.
            nextGroupIdx = leftGroupsOrder[nextLeftGroupsOrderIdx];

            // Get the index of the last unprocessed characters in this group.
            lastCharIdx = charsLeftInGroup[nextGroupIdx] - 1;
            
            // If only one unprocessed character is left, pick it; otherwise,
            // get a random character from the unused character list.
            if (lastCharIdx == 0)
                nextCharIdx = 0;
            else
                nextCharIdx = random.Next(0, lastCharIdx+1);

            // Add this character to the password.
            password[i] = charGroups[nextGroupIdx][nextCharIdx];
            
            // If we processed the last character in this group, start over.
            if (lastCharIdx == 0)
                charsLeftInGroup[nextGroupIdx] = 
                                          charGroups[nextGroupIdx].Length;
            // There are more unprocessed characters left.
            else
            {
                // Swap processed character with the last unprocessed character
                // so that we don't pick it until we process all characters in
                // this group.
                if (lastCharIdx != nextCharIdx)
                {
                    char temp = charGroups[nextGroupIdx][lastCharIdx];
                    charGroups[nextGroupIdx][lastCharIdx] = 
                                charGroups[nextGroupIdx][nextCharIdx];
                    charGroups[nextGroupIdx][nextCharIdx] = temp;
                }
                // Decrement the number of unprocessed characters in
                // this group.
                charsLeftInGroup[nextGroupIdx]--;
            }

            // If we processed the last group, start all over.
            if (lastLeftGroupsOrderIdx == 0)
                lastLeftGroupsOrderIdx = leftGroupsOrder.Length - 1;
            // There are more unprocessed groups left.
            else
            {
                // Swap processed group with the last unprocessed group
                // so that we don't pick it until we process all groups.
                if (lastLeftGroupsOrderIdx != nextLeftGroupsOrderIdx)
                {
                    int temp = leftGroupsOrder[lastLeftGroupsOrderIdx];
                    leftGroupsOrder[lastLeftGroupsOrderIdx] = 
                                leftGroupsOrder[nextLeftGroupsOrderIdx];
                    leftGroupsOrder[nextLeftGroupsOrderIdx] = temp;
                }
                // Decrement the number of unprocessed groups.
                lastLeftGroupsOrderIdx--;
            }
        }

        // Convert password characters into a string and return the result.
        return new string(password);
     }


    public static string NumberPasswordGenerator(int passwordLength)
    {
        int seed = Random.Next(1, int.MaxValue);
        //const string allowedChars = "ABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
        const string allowedChars = "0123456789";
        
        var chars = new char[passwordLength];
        var rd = new Random(seed);

        for (var i = 0; i < passwordLength; i++)
        {
            chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            
        }

        return new string(chars);


    }

    public static string GetAPPSettingKey(string keyName)
    {
        try
        {
            return System.Configuration.ConfigurationManager.AppSettings[keyName].ParseString();
        }
        catch
        {
            return string.Empty;
        }

    }

    public static IEnumerable<TSource> DistinctBy<TSource, TKey>(
                                this IEnumerable<TSource> source,
                                Func<TSource, TKey> keySelector)
    {
        return source.GroupBy(keySelector).Select(i => i.First());
    }


    #region Image Utilities

    /// <summary>
    /// Loads an image from a URL into a Bitmap object.
    /// Currently as written if there is an error during downloading of the image, no exception is thrown.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static Bitmap LoadPicture(string url)
    {
        System.Net.HttpWebRequest wreq;
        System.Net.HttpWebResponse wresp;
        Stream mystream;
        Bitmap bmp;

        bmp = null;
        mystream = null;
        wresp = null;
        try
        {
            wreq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
            wreq.AllowWriteStreamBuffering = true;

            wresp = (System.Net.HttpWebResponse)wreq.GetResponse();

            if ((mystream = wresp.GetResponseStream()) != null)
                bmp = new Bitmap(mystream);
        }
        catch
        {
            // Do nothing... 
        }
        finally
        {
            if (mystream != null)
                mystream.Close();

            if (wresp != null)
                wresp.Close();
        }

        return (bmp);
    }

    
    #endregion
    

    //private static bool IsInGroup(string ingroup)
    //{
    //    string username = Environment.UserName;

    //    PrincipalContext domainctx = new PrincipalContext(ContextType.Domain,
    //                                                "example",
    //                                                "DC=example,DC=com");

    //    UserPrincipal userPrincipal =
    //                      UserPrincipal.FindByIdentity(domainctx, IdentityType.SamAccountName, username);

    //    bool isMember = userPrincipal.IsMemberOf(domainctx, IdentityType.Name, ingroup);

    //    return isMember;
    //}


    //private static readonly Dictionary<string, Type> Comp = new Dictionary<string, Type>(); 

    //public static void CopyWithDom<T, TU>(T source, TU target)
    //{
    //    var className = GetClassName(typeof (T), typeof (TU));
    //    var flags = BindingFlags.Public | BindingFlags.Static | 
    //                BindingFlags.InvokeMethod;
    //    var args = new object[] {source, target}; 

    //    Comp[className].InvokeMember("CopyProps", flags, null, 
    //                                 null, args);
    //}

    //public static void GenerateCopyClass<T, TU>()
    //{
    //    var sourceType = typeof(T);
    //    var targetType = typeof (TU);
    //    var className = GetClassName(typeof (T), typeof (TU));

    //    if (Comp.ContainsKey(className))
    //        return;

    //    //var builder = new StringBuilder();
    //    //builder.Append("namespace Copy {\r\n");
    //    //builder.Append("    public class ");
    //    //builder.Append(className);
    //    //builder.Append(" {\r\n");
    //    //builder.Append("        public static void CopyProps(");
    //    //builder.Append(sourceType.FullName);
    //    //builder.Append(" source, ");
    //    //builder.Append(targetType.FullName);
    //    //builder.Append(" target) {\r\n");

    //    var map = GetMatchingProperties(sourceType, targetType);
    //    foreach (var item in map)
    //    {
    //        builder.Append("            target.");
    //        builder.Append(item.TargetProperty.Name);
    //        builder.Append(" = ");
    //        builder.Append("source.");
    //        builder.Append(item.SourceProperty.Name);
    //        builder.Append(";\r\n");
    //    }

    //    builder.Append("        }\r\n   }\r\n}");

    //    // Write out method body
    //    Debug.WriteLine(builder.ToString());

    //    var myCodeProvider = new CSharpCodeProvider();
    //    var myCodeCompiler = myCodeProvider.CreateCompiler();
    //    var myCompilerParameters = new CompilerParameters();
    //    myCompilerParameters.ReferencedAssemblies.Add( 
    //        typeof (LinqReflectionPerf).Assembly.Location 
    //    );
    //    myCompilerParameters.GenerateInMemory = true;
    //    var results = myCodeCompiler.CompileAssemblyFromSource 
    //                 (myCompilerParameters, builder.ToString());

    //    // Compiler output
    //    foreach (var line in results.Output)
    //        Debug.WriteLine(line);

    //    var copierType = results.CompiledAssembly.GetType( 
    //                     "Copy." + className);
    //    Comp.Add(className, copierType);
    //}
}

public static class ServiceType
{
    public static string GPCR="gpcrSCAN";
    public static string NHR = "nhrSCAN";
    public static string Epigenetics = "InCell Epigenetics";
    public static string Pathway = "pathSCAN";
    public static string Kinase = "tkSCAN";
    public static string Cytokine = "cytokineSCAN";

}

}