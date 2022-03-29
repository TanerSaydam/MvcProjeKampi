using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Concrete.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfWriterDal : GenericRepository<Writer>, IWriterDal
    {
        public List<WriterDto> GetListDto()
        {
            using (var context = new Context())
            {
                var result = from x in context.Writers
                             select new WriterDto
                             {
                                 WriterName = x.WriterName,
                                 WriteContentCount = context.Contents.Where(c => c.WriterID == x.WriterID).Count()
                             };
                return result.ToList();
            }
        }
    }
}
