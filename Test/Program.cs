using Spring.Net.Example.EFDao;
using Spring.Net.Example.Model.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HibernatingRhinos.Profiler.Appender.EntityFramework.EntityFrameworkProfiler.Initialize();

            using (var ctx = new EFContext("CodeFirstDb"))
            {

                var o = new FM_USER();
                o.ID = 1;
                o.USER_CODE = "admin";
                o.USER_NAME = "管理员";
                o.USER_PASSWORD = "a4ayc/80/OGda4BO/1o/V0etpOqiLx1JwB5S3beHW0s=";
                o.USER_TYPE = "1";
                o.SORT = 1;
                o.ENABLE_FLAG = "1";
                o.ISSYS_FLAG = "1";
                //o.OrderDate = DateTime.Now;
                //ctx.Users.Add(o);
                ctx.Entry(o);

                ctx.SaveChanges();

                //var query = from order in ctx.Users
                //            select order;
                //foreach (var q in query)
                //{
                //    Console.WriteLine("OrderId:{0},OrderDate", q.ID);
                //}

                Console.Read();
            }
        }
    }
}
