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
    public class EfContentDal : GenericRepository<Content>, IContentDal
    {
        public List<ContentDto> GetAllDto()
        {
            using (var context = new Context())
            {
                var result = from content in context.Contents
                             join writer in context.Writers on content.WriterID equals writer.WriterID
                             join header in context.Headings on content.HeadingID equals header.HeadingID
                             select new ContentDto
                             {
                                 HeadingID = header.HeadingID,
                                 ContentID = content.ContentID,
                                 ContentDate = content.ContentDate,
                                 ContentStatus = content.ContentStatus,
                                 ContentValue = content.ContentValue,
                                 WriterID = content.WriterID,
                                 HeadingName = header.HeadingName,
                                 WriterName = writer.WriterName,
                                 WriterSurName = writer.WriterSurName
                             };
                return result.ToList();
            }
        }        
    }
}
