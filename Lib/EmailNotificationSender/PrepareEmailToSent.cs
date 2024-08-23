using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Lib.EmailNotificationSender
{
    public class PrepareEmailToSent
    {
        int _userId;
        string _emailType;
        string _emailDescription;
        int _emailId;
        string _mSubject;
        string _mFrom;
        string _mTo;
        string _mBody;
        string _mCc;
        string _mBcc;
        string _mailServer;
        bool _isBodyHtml;
        int _userIdFrom;
        int _userIdTo;

        public PrepareEmailToSent(int userId, string emailType, string emailDescription, int emailId,
                                  string mSubject, string mFrom, string mTo, string mBody, string mCc, 
                                  string mBcc, string mailServer, bool isBodyHtml, int userIdFrom, int userIdTo)
        {
            _userId = userId;
             _emailType=emailType;
             _emailDescription=emailDescription;
             _emailId=emailId;
             _mSubject=mSubject;
             _mFrom=mFrom;
             _mTo=mTo;
             _mBody=mBody;
             _mCc=mCc;
             _mBcc=mBcc;
             _mailServer=mailServer;
             _isBodyHtml=isBodyHtml;
             _userIdFrom=userIdFrom;
             _userIdTo = userIdTo;
        }

        public void InsertSentEmail(DBSession session)
        {
            ElioSentEmails email = new ElioSentEmails();

            email.UserId = _userId;
            email.Sysdate = DateTime.Now;
            email.LastUpdate = null;
            email.EmailType = _emailType;
            email.EmailDescription = _emailDescription;
            email.EmailId = _emailId;
            email.EmailSubject = _mSubject;
            email.EmailFrom = _mFrom;
            email.EmailTo = _mTo;
            email.EmailBody = _mBody;
            email.EmailCc = _mCc;
            email.EmailBcc = _mBcc;
            email.MailServer = _mailServer;
            email.LastTryDate = null;
            email.RepeatTimes = 0;
            email.Sent = false;
            email.IsBodyHtml = _isBodyHtml;
            email.LastError = null;
            email.UserIdFrom = _userIdFrom;
            email.UserIdTo = _userIdTo;

            DataLoader<ElioSentEmails> loader = new DataLoader<ElioSentEmails>(session);
            loader.Insert(email);
        }
    }
}