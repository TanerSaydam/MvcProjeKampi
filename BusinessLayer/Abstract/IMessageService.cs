using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Message> GetReadList();
        List<Message> GetUnReadList();
        List<Message> GetListInbox();
        List<Message> GetListSendbox();
        List<Message> GetListDraft();
        List<Message> GetListTrash();
        void MessageAdd(Message message);
        Message GetByID(int id);
        void MessageDelete(Message message);
        void MessageUpdate(Message message);
    }
}
