﻿using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;
        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public Message GetByID(int id)
        {
            return _messageDal.Get(x=> x.MessageID == id);
        }

        public List<Message> GetListInbox(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p);
        }

        public List<Message> GetListSendbox(string p)
        {
            return _messageDal.List(x => x.SenderMail == p);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }

        public List<Message> GetListDraft(string p)
        {
            return _messageDal.List(x=> x.MessageStatus == "Taslak" && x.SenderMail == p);
        }

        public List<Message> GetListTrash(string p)
        {
            return _messageDal.List(x => x.MessageStatus == "Çöp" && x.SenderMail == p);
        }

        public List<Message> GetReadList(string p)
        {
            return _messageDal.List(x => x.MessageRead == true && x.SenderMail == p);
        }

        public List<Message> GetUnReadList(string p)
        {
            return _messageDal.List(x => x.MessageRead == false && x.SenderMail == p);
        }
    }
}
