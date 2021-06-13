using BusinessLayer.Abstract;
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

        public List<Message> GetListInbox()
        {
            return _messageDal.List(x => x.ReceiverMail == "admin@gmail.com");
        }

        public List<Message> GetListSendbox()
        {
            return _messageDal.List(x => x.SenderMail == "admin@gmail.com");
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }

        public List<Message> GetListDraft()
        {
            return _messageDal.List(x=> x.MessageStatus == "Taslak");
        }

        public List<Message> GetListTrash()
        {
            return _messageDal.List(x => x.MessageStatus == "Çöp");
        }

        public List<Message> GetReadList()
        {
            return _messageDal.List(x => x.MessageRead == true);
        }

        public List<Message> GetUnReadList()
        {
            return _messageDal.List(x => x.MessageRead == false);
        }
    }
}
