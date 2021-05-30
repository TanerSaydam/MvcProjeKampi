using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class WriterValidator: AbstractValidator<Writer>
    {
        public WriterValidator()
        {
            RuleFor(x => x.WriterName).NotEmpty().WithMessage("Yazar adı boş geçilemez");
            RuleFor(x => x.WriterSurName).NotEmpty().WithMessage("Yazar soyadı boş geçilemez");
            RuleFor(x => x.WriterSurName).MinimumLength(2).WithMessage("Yazar soyadı 2 karakterden küçük olamnaz");
            RuleFor(x => x.WriterSurName).MaximumLength(50).WithMessage("Yazar soyadı 2 karakterden küçük olamnaz");
            RuleFor(x => x.WriterAbout).NotEmpty().WithMessage("Hakkında kısmı boş geçilemez");
            RuleFor(x => x.WriterAbout).Must(IsAboutValid).WithMessage("Hakkında kısmında en az bir defa a harfi kullanılmalıdır");
            RuleFor(x => x.WriterMail).NotEmpty().WithMessage("Mail kısmı boş geçilemez");
            RuleFor(x => x.WriterPassword).NotEmpty().WithMessage("Şifre boş geçilemez");
            //RuleFor(x => x.WriterPassword).Must(IsPasswordValid).WithMessage("Parola en az 6 karakter olmalıdır.En az bir harf ve bir sayı içermelidir");
            RuleFor(x => x.WriterTitle).NotEmpty().WithMessage("Ünvan kısmı boş geçilemez");
        }

        private bool IsAboutValid(string arg)
        {
            try
            {
                Regex regex = new Regex(@"^(?=.*[a,A])");
                return regex.IsMatch(arg);
            }
            catch (Exception)
            {

                return false;
            }
        }

        private bool IsPasswordValid(string arg)
        {
            try
            {
                Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$");
                return regex.IsMatch(arg);
            }
            catch
            {
                return false;
            }
        }

        private bool IsContainsValid(string name)
        {
            bool result = name.Contains("a");
            return result;
        }
    }
}
