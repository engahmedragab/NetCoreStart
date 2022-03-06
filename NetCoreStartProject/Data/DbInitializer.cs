
using NetCoreStartProject.Domain;
using NetCoreStartProject.Extensions;
using System;
using System.Linq;

namespace NetCoreStartProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(DataContext context)
        {
            // Look for any students.
            if (context.Users.Any() || context.Countries.Any())
            {
                return;   // DB has been seeded
            }
            var UserId = Guid.NewGuid();
            var RoleId = Guid.NewGuid();

            var users = new User[]
            {
                new User{
                    Id = UserId,
                    UserName = "SuperAdmin",
                    PasswordHash = "0urBride$05022022$".HashPassword(),
                    Email = "elec.eng.ahmedRagab@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "01110717380",
                    PhoneNumberConfirmed = true
                },
            };

            context.Users.AddRange(users);
            context.SaveChanges();

            var roles = new Role[]
            {
                new Role{
                    Id = RoleId,
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    ConcurrencyStamp =  "SuperAdmin",
                },
            };

            context.Roles.AddRange(roles);
            context.SaveChanges();

            context.UserRoles.Add(new Microsoft.AspNetCore.Identity.IdentityUserRole<Guid>
            {
                RoleId = RoleId,
                UserId = UserId
            });

            context.SaveChanges();
            
            var countries = new Country[]
            {
                new Country {
                    Code = "eg"
                    ,Latitude= 32423432
                    ,Longitude= 234234324
                    ,FlagCode= "1"
                    ,CapitalNames= "Cairo"
                    ,CurrencyCodes= "EGP"
                    ,PhoneCodes= "022"
                    ,alpha2= "EG"
                    ,alpha3= "EGP"
                    ,IsDeleted= false
                    ,CreationDate= new DateTime(2022, 2,2)
                    ,LastModifiedDate = new DateTime(2022, 2,2)
                    ,NameAr = "مصر"
                    ,NameEn= "Egypt"
                    ,DescriptionAr= "مصر"
                    ,DescriptionEn= "Egypt"
                    ,CreatedBy = UserId
                    ,LastModifiedBy = UserId
                },
            };

            context.Countries.AddRange(countries);
            context.SaveChanges();

            //var courses = new Course[]
            //{
            //    new Course{CourseID=1050,Title="Chemistry",Credits=3},
            //    new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            //    new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            //    new Course{CourseID=1045,Title="Calculus",Credits=4},
            //    new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            //    new Course{CourseID=2021,Title="Composition",Credits=3},
            //    new Course{CourseID=2042,Title="Literature",Credits=4}
            //};

            //context.Courses.AddRange(courses);
            //context.SaveChanges();

            //var enrollments = new Enrollment[]
            //{
            //    new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            //    new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            //    new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            //    new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            //    new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            //    new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            //    new Enrollment{StudentID=3,CourseID=1050},
            //    new Enrollment{StudentID=4,CourseID=1050},
            //    new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            //    new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            //    new Enrollment{StudentID=6,CourseID=1045},
            //    new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            //};

            //context.Enrollments.AddRange(enrollments);
            //context.SaveChanges();
        }
    }
}