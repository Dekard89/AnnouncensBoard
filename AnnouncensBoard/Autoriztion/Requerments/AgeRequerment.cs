using Microsoft.AspNetCore.Authorization;

namespace AnnouncensBoard.Autoriztion.Requerments

{
    public class AgeRequerment : IAuthorizationRequirement
    {
        protected internal int Age {  get; set; }

        protected internal bool AdultOnly { get; set; }

        public AgeRequerment(int age)=> Age = age;
        
                
        
    }
}
