using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ResumeRegister.core.Convertors
{
   public static class TextAndDateConvertor
    {
        public static string ToShamsi(this DateTime valueDateTime)
        {
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(valueDateTime) + "/" + pc.GetMonth(valueDateTime).ToString("00") + "/" + pc.GetDayOfMonth(valueDateTime).ToString("00");
        }
        public static string TrimAndLower(string text)
        {
            return text.Trim().ToLower();
        }
    }
}
