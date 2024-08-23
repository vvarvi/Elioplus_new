using System;
using System.Linq;
using System.IO;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Lib.Utils
{
    public class GeneratePasswordLib
    {
        public static string CreateRandomStringWithMax11Chars(int randomStringLength)
        {
            //string randomPasswordString = Path.GetRandomFileName();
            //randomPasswordString = randomPasswordString.Replace(".", "").Replace("O", "1").Replace("0", "A");
            //randomPasswordString = randomPasswordString.Substring(0, randomStringLength);
            string randomPasswordString = Guid.NewGuid().ToString().Replace(".", "").Replace("O", "1").Replace("0", "A").Replace("-", "").Replace(" ", "").Substring(0, randomStringLength - 1);

            if (randomPasswordString.Length > randomStringLength)
                throw new Exception("Err310: Random string length too big. Maximum value is 15.");

            //while (Convert.ToInt32(session.GetDataTable("SELECT count(id) AS count FROM Elio_users WHERE password=@password", DatabaseHelper.CreateStringParameter("@password", randomPasswordString)).Rows[0]["count"]) > 0)
            //{
            //    randomPasswordString = Path.GetRandomFileName();
            //    randomPasswordString = randomPasswordString.Replace(".", "").Replace("O", "1").Replace("0", "A");
            //    randomPasswordString = randomPasswordString.Substring(0, randomStringLength);
            //}

            return randomPasswordString;
        }
    }
}