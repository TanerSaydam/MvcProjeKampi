using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules
{
    public class MessageValidator: AbstractValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.SenderMail).NotEmpty().WithMessage("Bu alanı boş geçemezsiniz");
            RuleFor(x => x.SenderMail).EmailAddress().WithMessage("Geçerli bir mail adresi giriniz");
            RuleFor(x => x.ReceiverMail).NotEmpty().WithMessage("Bu alanı boş geçemezsiniz");
            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Bu alanı boş geçemezsiniz");
            RuleFor(x => x.MessageContent).MinimumLength(5).WithMessage("Bu alan 5 karakterden az olamaz!");
            RuleFor(x => x.Subject).NotEmpty().WithMessage("Bu alanı boş geçemezsiniz");
        }
    }
}
