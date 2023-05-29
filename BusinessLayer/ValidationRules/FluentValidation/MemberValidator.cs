using EntityLayer.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ValidationRules.FluentValidation
{
    public class MemberValidator :AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Üye adı boş bırakılamaz!");
            RuleFor(m => m.UserName).NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz!");
            RuleFor(m => m.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş bırakılamaz!");
            RuleFor(m => m.Surname).NotEmpty().WithMessage("Üye soyadı boş bırakılamaz!");
            RuleFor(m => m.School).NotEmpty().WithMessage("Okul adı boş bırakılamaz");
            //RuleFor()
            RuleFor(m => m.School).MinimumLength(10).WithMessage("Okul adı en az 10 karakter olmalıdır!");
            RuleFor(m => m.UserName).MinimumLength(5).WithMessage("Kullanıcı adı en az 5 karakter olmalıdır!");           
        }
    }
}
